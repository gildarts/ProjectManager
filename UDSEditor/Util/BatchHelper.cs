using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.DSAClient;
using System.Xml;
using System.ComponentModel;
using System.IO;
using ProjectManager.Project;

namespace ProjectManager.Util
{
    class BatchHelper
    {
        public event EventHandler CompleteOne;
        public event EventHandler CompleteAll;
        public event ErrorEventHandler OccuredError;

        public int BatchCount
        {
            get
            {
                if (_recElements.Count % Size > 0)
                    return _recElements.Count / Size + 1;
                else
                    return _recElements.Count / Size;
            }
        }
        public int Size { get; set; }
        public int CompletedCount { get; private set; }
        public XmlElement RequestContentElement { get; set; }

        private List<XmlElement> _recElements;
        private ProjectHandler _project;        
        private string _serviceName;
        private string _batchID;
        
        public BatchHelper(ProjectHandler project, List<XmlElement> recordElement, string serviceName)
        {
            _recElements = recordElement;
            _project = project;            
            _serviceName = serviceName;
            _batchID = Guid.NewGuid().ToString();
            RequestContentElement = XmlHelper.ParseAsDOM("<Request/>");

            this.Size = 100;            
        }

        public void Start()
        {
            int count = _recElements.Count;
           
            for (int batchNumber = 0; batchNumber < this.BatchCount; batchNumber++)
            {
                XmlHelper h = new XmlHelper(RequestContentElement);

                int start = batchNumber * this.Size;
                int end = start + this.Size;
                if (end > count)
                    end = count;

                for (int i = start; i < end; i++)
                {                
                    XmlElement rec = _recElements[i];
                    h.AddElement(".", rec);                    
                }

                Envelope env = new Envelope(h);
                XmlHelper p = new XmlHelper("<Parameter/>");
                p.AddElement(".", "BatchID", _batchID);
                p.AddElement(".", "BatchNumber", batchNumber.ToString());
                env.Headers.Add(p);

                BackgroundWorker w = new BackgroundWorker();
                w.DoWork += new DoWorkEventHandler(w_DoWork);
                w.RunWorkerCompleted += new RunWorkerCompletedEventHandler(w_RunWorkerCompleted);
                w.RunWorkerAsync(env);
            }
        }

        public void Cancel()
        {
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "BatchID", _batchID);
            _project.SendRequest("DS.Base.CancelBatch", new Envelope(h));
        }

        public void Commit()
        {
            XmlHelper h = new XmlHelper();
            h.AddElement(".", "BatchID", _batchID);
            _project.SendRequest("DS.Base.Commit", new Envelope(h));
        }

        void w_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null && OccuredError != null)
            {
                OccuredError.Invoke(this, new ErrorEventArgs(e.Error));
            }
            else
            {
                this.CompletedCount++;

                if (this.CompleteOne != null)
                    CompleteOne.Invoke(this, EventArgs.Empty);

                if(this.CompletedCount == this.BatchCount && this.CompleteAll != null)
                    CompleteAll.Invoke(this, EventArgs.Empty);
            }
        }

        void w_DoWork(object sender, DoWorkEventArgs e)
        {
            Envelope h = e.Argument as Envelope;
            Envelope env = _project.SendRequest(_serviceName, h);
            e.Result = env.Body;
        }
    }
}

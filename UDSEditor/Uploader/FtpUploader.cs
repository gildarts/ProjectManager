using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Windows.Forms;

namespace ProjectManager.Uploader 
{
    public class FtpState
    {
        private ManualResetEvent wait;
        private FtpWebRequest request;
        private string fileName;
        private Exception operationException = null;
        string status;

        public FtpState()
        {
            wait = new ManualResetEvent(false);
        }

        public ManualResetEvent OperationComplete
        {
            get { return wait; }
        }

        public FtpWebRequest Request
        {
            get { return request; }
            set { request = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public Exception OperationException
        {
            get { return operationException; }
            set { operationException = value; }
        }
        public string StatusDescription
        {
            get { return status; }
            set { status = value; }
        }
    }

    public class FtpUploader : IUploader
    {
        public event EventHandler UploadCompleted;
        public event EventHandler<ProgressEventArgs> ProgressChanged;
        public event EventHandler<ThreadExceptionEventArgs> UploadError;

        private string Uri { get; set; }
        private string Filename { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }
        private Control Ctrl { get; set; }

        public FtpUploader()
        {
            Ctrl = new Control();
            IntPtr p = Ctrl.Handle;
        }

        // Command line arguments are two strings:
        // 1. The url that is the name of the file being uploaded to the server.
        // 2. The name of the file on the local machine.
        //
        public void Upload(string ftpUri, string filename, string username, string password)
        {
            Uri = ftpUri;
            Filename = filename;
            UserName = username;
            Password = password;

            // Create a Uri instance with the specified URI string.
            // If the URI is not correctly formed, the Uri constructor
            // will throw an exception.
            ManualResetEvent waitObject;

            Uri target = new Uri(this.Uri);
            string fileName = this.Filename;
            FtpState state = new FtpState();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(target);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.EnableSsl = true;

            // This example uses anonymous logon.
            // The request is anonymous by default; the credential does not have to be specified. 
            // The example specifies the credential only to
            // control how actions are logged on the server.

            request.Credentials = new NetworkCredential(this.UserName, this.Password);

            // Store the request in the object that we pass into the
            // asynchronous operations.
            state.Request = request;
            state.FileName = fileName;

            // Get the event to wait on.
            waitObject = state.OperationComplete;

            // Asynchronously get the stream for the file contents.
            request.BeginGetRequestStream(
                new AsyncCallback(EndGetStreamCallback),
                state
            );
        }
        private void EndGetStreamCallback(IAsyncResult ar)
        {
            FtpState state = (FtpState)ar.AsyncState;

            Stream requestStream = null;
            // End the asynchronous call to get the request stream.
            try
            {
                requestStream = state.Request.EndGetRequestStream(ar);
                // Copy the file contents to the request stream.
                const int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                int count = 0;
                int readBytes = 0;
                FileStream stream = File.OpenRead(state.FileName);
                do
                {
                    readBytes = stream.Read(buffer, 0, bufferLength);
                    requestStream.Write(buffer, 0, readBytes);
                    count += readBytes;

                    Ctrl.Invoke(new Action(delegate()
                    {
                        if (ProgressChanged != null)
                            ProgressChanged.Invoke(this, new ProgressEventArgs(stream.Length, count, readBytes));
                    }));
                }
                while (readBytes != 0);
                stream.Close();

                Console.WriteLine("Writing {0} bytes to the stream.", count);
                // IMPORTANT: Close the request stream before sending the request.
                requestStream.Close();
                // Asynchronously get the response to the upload request.
                state.Request.BeginGetResponse(
                    new AsyncCallback(EndGetResponseCallback),
                    state
                );
            }
            // Return exceptions to the main application thread.
            catch (Exception e)
            {
                if (UploadError != null)
                {
                    ThreadExceptionEventArgs arg = new ThreadExceptionEventArgs(e);
                    UploadError.Invoke(this, arg);
                }
            }
        }

        // The EndGetResponseCallback method  
        // completes a call to BeginGetResponse.
        private void EndGetResponseCallback(IAsyncResult ar)
        {
            FtpState state = (FtpState)ar.AsyncState;
            FtpWebResponse response = null;
            try
            {
                response = (FtpWebResponse)state.Request.EndGetResponse(ar);
                response.Close();
                state.StatusDescription = response.StatusDescription;
                // Signal the main application thread that 
                // the operation is complete.
                state.OperationComplete.Set();
                state.Request.GetResponse();

                Ctrl.Invoke(new Action(delegate
                {
                    if (UploadCompleted != null)
                        UploadCompleted.Invoke(this, EventArgs.Empty);
                }));
            }
            // Return exceptions to the main application thread.
            catch (Exception e)
            {
                if (UploadError != null)
                {
                    ThreadExceptionEventArgs arg = new ThreadExceptionEventArgs(e);
                    UploadError.Invoke(this, arg);
                }
            }
        }
    }
}

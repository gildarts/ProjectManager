using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using ProjectManager.Util.Converter;

namespace ProjectManager.Util.DeployConverter
{
    class DeployConverter
    {
        public const string UDS_PREFIX = "_$_";

        public static XElement ToPhysicalDeployElement(XElement udsCsml)
        {
            XElement x = new XElement("Content");
            XElement app = new XElement("Application", new XAttribute("Name", "shared"));
            x.Add(app);

            foreach (XElement propElement in udsCsml.Elements("Property"))
                app.Add(propElement);

            foreach (XElement udsContract in udsCsml.Elements("Contract"))
            {
                XElement c = ToPhysicalContractElement(udsContract);
                app.Add(c);
            }

            return x;
        }

        public static XElement ToPhysicalContractElement(XElement udsContract)
        {
            string contractName = udsContract.Attribute("Name").Value;
            string enabledString = udsContract.Attribute("Enabled").Value;

            if (contractName.StartsWith(UDS_PREFIX))
                contractName = contractName.Substring(UDS_PREFIX.Length - 1);


            XElement x = new XElement("Contract", new XAttribute("Name", contractName));
            XElement config = ToPhysicalContractConfig(udsContract.Element("Definition"));

            XElement resource = new XElement("Property", new XAttribute("Name", "Resource"), new XElement("Resources"));
            XElement enabled = new XElement("Property", new XAttribute("Name", "Enabled"), enabledString);
            XElement defaultContract = new XElement("Property", new XAttribute("Name", "DefaultContract"), false.ToString());
            XElement package = ToPhysicalPackages(udsContract);
            x.Add(config, resource, enabled, defaultContract, package);
            return x;
        }

        public static XElement ToPhysicalContractConfig(XElement udsContractConfig)
        {
            XElement config = new XElement("Property", new XAttribute("Name", "Config"));

            foreach (XElement e in udsContractConfig.Elements())
                config.Add(e);

            return config;
        }

        public static XElement ToPhysicalPackages(XElement udsContract)
        {
            XElement root = new XElement("Package", new XAttribute("Name", "."));

            foreach (XElement p in udsContract.Elements("Package"))
            {
                string name = p.Attribute("Name").Value;
                if (name == "_")
                {
                    foreach (XElement udsService in p.Elements("Service"))
                    {
                        XElement s = ToPhysicalService(udsService);
                        root.Add(s);
                    }
                }
                else
                {
                    PreparePackages(root, p);
                }
            }
            return root;
        }

        private static void PreparePackages(XElement root, XElement p)
        {
            string packageName = p.Attribute("Name").Value;

            string[] pnames = packageName.Split('.');
            XElement current = root;
            for (int i = 0; i < pnames.Length; i++)
            {
                string name = pnames[i];
                if (i == pnames.Length - 1)
                {
                    p.Attribute("Name").Value = name;
                    XElement pp = ToPhysicalPackage(p);
                    current.Add(pp);
                }
                else
                {
                    XElement e = current.XPathSelectElement("Package[@Name='" + name + "']");
                    if (e == null)
                    {
                        e = new XElement("Package", new XAttribute("Name", name));
                        current.Add(e);
                    }
                    current = e;
                }
            }
        }

        public static XElement ToPhysicalPackage(XElement udsPackage)
        {
            string name = udsPackage.Attribute("Name").Value;
            XElement p = new XElement("Package", new XAttribute("Name", name));

            foreach (XElement s in udsPackage.Elements("Service"))
            {
                XElement ps = ToPhysicalService(s);
                p.Add(ps);
            }
            return p;
        }

        public static XElement ToPhysicalService(XElement udsService)
        {
            IServiceConverter converter = ServiceConverterFactory.CreateToPhysicalConverterInstance(udsService);
            return converter.ToPhysical(udsService);
        }

        public static XElement ToUDSDeployElement(XElement appDeployElement)
        {
            XElement x = new XElement("Content");
            foreach (XElement propElement in appDeployElement.XPathSelectElements("Application/Property"))
            {
                x.Add(propElement);
            }

            foreach (XElement contractElement in appDeployElement.XPathSelectElements("Application/Contract"))
            {
                XElement c = ToUDSContract(contractElement);
                x.Add(c);
            }
            return x;
        }

        public static XElement ToUDSContract(XElement physicalContract)
        {
            string name = UDS_PREFIX + physicalContract.Attribute("Name").Value;

            XElement x = new XElement("Contract", new XAttribute("Name", name), new XAttribute("Enabled", "true"));

            XElement config = physicalContract.XPathSelectElement("Property[@Name='Config']");
            XElement def = ToUDSDefinition(config);
            x.Add(def);

            XElement physicalPackage = physicalContract.XPathSelectElement("Package[@Name='.']");

            XElement dp = new XElement("Package", new XAttribute("Name", "_"));
            foreach (XElement e in physicalPackage.Elements("Service"))
            {
                dp.Add(e);
            }

            if (dp.HasElements)
            {
                ToUDSPackage(x, dp);
            }

            foreach (XElement e in physicalPackage.Elements("Package"))
            {
                ToUDSPackage(x, e);
            }

            return x;
        }

        public static XElement ToUDSDefinition(XElement physicalConfig)
        {
            XElement def = new XElement("Definition");

            XElement acc = physicalConfig.XPathSelectElement("AccessPoint/Authentication");
            def.Add(acc);
            //foreach (XElement e in acc.Elements())
            //    def.Add(e);

            return def;
        }

        private static void ToUDSPackage(XElement top, XElement physicalPackage)
        {
            string name = physicalPackage.Attribute("Name").Value;
            XElement pack = new XElement("Package", new XAttribute("Name", name));
            foreach (XElement service in physicalPackage.Elements("Service"))
            {
                XElement s = ToUDSService(service);
                pack.Add(s);
            }

            foreach (XElement subpack in physicalPackage.Elements("Package"))
            {
                ToUDSPackage(name, top, subpack);
            }

            if (pack.HasElements)
                top.Add(pack);
        }

        private static void ToUDSPackage(string prefixPackageName, XElement top, XElement physicalPackage)
        {
            string name = physicalPackage.Attribute("Name").Value;

            if (!string.IsNullOrWhiteSpace(prefixPackageName))
                name = prefixPackageName + "." + name;

            XElement parent = top.XPathSelectElement("Package[@Name='" + name + "']");
            foreach (XElement service in physicalPackage.Elements("Service"))
            {
                if (parent == null)
                {
                    parent = new XElement("Package", new XAttribute("Name", name));
                    top.Add(parent);
                }

                XElement s = ToUDSService(service);
                parent.Add(s);
            }

            foreach (XElement subpack in physicalPackage.Elements("Package"))
            {
                ToUDSPackage(name, top, subpack);
            }


            //XElement pack = new XElement("Package", new XAttribute("Name", name));

            //foreach (XElement subpack in physicalPackage.Elements("Package"))
            //{
            //    XElement x = ToUDSPackage(name, subpack);
            //    pack.Add(x);
            //}

            //foreach (XElement service in physicalPackage.Elements("Service"))
            //{
            //    XElement s = ToUDSService(service);
            //    pack.Add(s);
            //}
            //return pack;
        }

        public static XElement ToUDSService(XElement physicalService)
        {
            IServiceConverter converter = ServiceConverterFactory.CreateToUDSConverterInstance(physicalService);
            return converter.ToUDS(physicalService);
        }
    }
}

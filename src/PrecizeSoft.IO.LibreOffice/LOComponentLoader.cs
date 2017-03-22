using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using unoidl.com.sun.star.beans;
using unoidl.com.sun.star.frame;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;

namespace PrecizeSoft.IO
{
    public class LOComponentLoader : XComponentLoader
    {
        //Unit test ParallelTest 30x2xFalse failed if the context is not a singleton.
        protected static XComponentLoader serviceManager = null;

        protected static LOComponentLoader instance = new LOComponentLoader();

        public static LOComponentLoader Instance
        {
            get
            {
                return instance;
            }
        }

        private static object initSync = new object();

        protected static void InitServiceManager()
        {
            lock (initSync)
            {
                if (serviceManager == null)
                {
                    Thread thread = new Thread(TryInitServiceManager);
                    thread.Start();
                    if (!thread.Join(30 * 1000))
                    {
                        thread.Abort();

                        IEnumerable<Process> processes = Process.GetProcessesByName("soffice.bin").Union(Process.GetProcessesByName("soffice.exe"));
                        foreach (var p in processes) p.Kill();

                        Thread thread2 = new Thread(TryInitServiceManager);
                        thread2.Start();
                        if (!thread.Join(30 * 1000))
                        {
                            thread2.Abort();
                            throw new System.Exception("LibreOffice init timeout.");
                        }
                    }
                }
            }

            if (serviceManager == null)
                throw new System.Exception("Can't connect to LibreOffice. Probably LibreOffice environment is not configured.");
        }

        protected static void TryInitServiceManager()
        {
            try
            {
                XComponentContext context = uno.util.Bootstrap.bootstrap();
                XMultiServiceFactory manager = (XMultiServiceFactory)context.getServiceManager();
                serviceManager = (XComponentLoader)manager.createInstance("com.sun.star.frame.Desktop");
            }
            catch
            {
            }
        }

        private static object reloadSync = new object();
        

        internal LOComponentLoader()
        {

        }

        public XComponent loadComponentFromURL(string URL, string TargetFrameName, int SearchFlags, PropertyValue[] Arguments)
        {
            if (serviceManager == null)
                InitServiceManager();

            XComponentLoader currentManager = serviceManager;
            try
            {
                return currentManager.loadComponentFromURL(URL, TargetFrameName, SearchFlags, Arguments);
            }
            catch (DisposedException)
            {
                lock (reloadSync)
                {
                    if (currentManager == serviceManager)
                    {
                        serviceManager = null;
                        InitServiceManager();
                    }
                }

                return serviceManager.loadComponentFromURL(URL, TargetFrameName, SearchFlags, Arguments);
            }
        }

        public bool IsAvailable
        {
            get
            {
                if (serviceManager == null)
                    try
                    {
                        InitServiceManager();
                    }
                    catch
                    {

                    }

                return !(serviceManager == null);
            }
        }
    }
}

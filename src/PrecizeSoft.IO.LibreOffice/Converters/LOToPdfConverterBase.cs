using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using unoidl.com.sun.star.frame;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.util;

namespace PrecizeSoft.IO.Converters
{
    public abstract class LOToPdfConverterBase : DiskFileConverter
    {
        protected string libreOfficeFilterName;

        public LOToPdfConverterBase(string libreOfficeFilterName) : base(".pdf")
        {
            this.libreOfficeFilterName = libreOfficeFilterName;
        }

        public TimeSpan SaveTimeout { get; set; } = new TimeSpan(0, 0, 15);

        public TimeSpan CloseTimeout { get; set; } = new TimeSpan(0, 0, 10);

        public override void Convert(string sourceFileName, string destinationFileName)
        {
            Debug.WriteLine($"Start Convert: {Thread.CurrentThread.ManagedThreadId} - {sourceFileName} - {destinationFileName}");

            /*try
            {*/
            if (!File.Exists(sourceFileName))
                throw new FileNotFoundException("File not found", sourceFileName);

            XComponentLoader loader = LOComponentLoader.Instance;

            XComponent component;

            {
                var pv = new unoidl.com.sun.star.beans.PropertyValue[1];
                pv[0] = new unoidl.com.sun.star.beans.PropertyValue()
                {
                    Name = "Hidden",
                    Value = new uno.Any(true)
                };
                component = loader.loadComponentFromURL("file:///" +
                    Path.GetFullPath(sourceFileName).Replace('\\', '/'), "_blank", 0, pv);
            }

            try
            {
                Debug.WriteLine($"File opened: {Thread.CurrentThread.ManagedThreadId} - {sourceFileName} - {destinationFileName}");

                string destinationFullPath = Path.GetFullPath(destinationFileName);
                string destinationDirectory = Path.GetDirectoryName(destinationFullPath);

                if (!Directory.Exists(destinationDirectory))
                    Directory.CreateDirectory(destinationDirectory);

                var storable = (XStorable)component;

                var pv = new unoidl.com.sun.star.beans.PropertyValue[1];
                pv[0] = new unoidl.com.sun.star.beans.PropertyValue()
                {
                    Name = "FilterName",
                    Value = new uno.Any(this.libreOfficeFilterName)
                };
                System.Exception saveException = null;

                Thread saveThread = new Thread(() =>
                {
                    try
                    {
                        storable.storeToURL("file:///" +
                            Path.GetFullPath(destinationFileName).Replace('\\', '/'), pv);
                    }
                    catch (System.Exception e)
                    {
                        saveException = e;
                    }
                });
                saveThread.Start();

                if (!saveThread.Join(this.SaveTimeout))
                {
                    saveThread.Abort();
                    throw new TimeoutException("File save timeout.");
                }
                else if (saveException != null)
                    throw saveException;

                Debug.WriteLine($"File saved: {Thread.CurrentThread.ManagedThreadId} - {sourceFileName} - {destinationFileName}");
            }
            finally
            {
                Thread closeThread = new Thread(() =>
                {
                    try
                    {
                        ((XCloseable)component).close(true);
                    }
                    catch
                    {
                    }
                });
                closeThread.Start();

                if (!closeThread.Join(this.CloseTimeout))
                    closeThread.Abort();

                Debug.WriteLine($"File closed: {Thread.CurrentThread.ManagedThreadId} - {sourceFileName} - {destinationFileName}");
            }
            /*}
            catch (System.Exception e)
            {
                Debug.WriteLine($"File exception: {Thread.CurrentThread.ManagedThreadId} - {sourceFileName} - {destinationFileName}");
                File.WriteAllText(destinationFileName, e.Message + "\n" + e.StackTrace);
            }*/
        }
    }
}

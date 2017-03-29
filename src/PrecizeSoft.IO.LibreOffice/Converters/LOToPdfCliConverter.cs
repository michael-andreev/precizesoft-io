using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrecizeSoft.IO;
using System.Diagnostics;
//using System.Security.Principal;
//using System.Security;

namespace PrecizeSoft.IO.Converters
{
    public class LOToPdfCliConverter : DiskFileConverter
    {
        protected string libreOfficePath = @"c:\Program Files\LibreOffice 5";
        public string LibreOfficePath
        {
            get
            {
                return this.libreOfficePath;
            }
            set
            {
                this.libreOfficePath = value;
            }
        }

        protected string GetLibreOfficeApplicationPath()
        {
            string portableApplicationPath = Path.Combine(this.libreOfficePath, "LibreOfficePortable.exe");

            if (File.Exists(portableApplicationPath))
            {
                return portableApplicationPath;
            }
            else
            {
                return Path.Combine(this.libreOfficePath, @"program\soffice.exe");
            }
        }

        public LOToPdfCliConverter():base(".pdf")
        {

        }

        public LOToPdfCliConverter(string libreOfficePath):base(".pdf")
        {
            this.libreOfficePath = libreOfficePath;
        }

        private IEnumerable<string> supportedFormatCollection = new List<string>()
            { ".123", ".602", ".bmp", ".cdr", ".cgm", ".cmx", ".csv", ".dbf", ".dif", ".doc", ".docm", ".docx", ".dot",
            ".dotm", ".dotx", ".dxf", ".emf", ".eps", ".fodg", ".fodp", ".fods", ".fodt", ".gif", ".htm", ".html", ".hwp",
            ".jpe", ".jpeg", ".jpg", ".lwp", ".met", ".mml", ".odb", ".odf", ".odg", ".odm", ".odp", ".ods", ".odt", ".otg",
            ".oth", ".otp", ".ott", ".oxt", ".pbm", ".pcd", ".pct", ".pcx", ".pgm", ".png", ".pot", ".potm", ".potx", ".ppm",
            ".pps", ".ppsx", ".ppt", ".pptm", ".pptx", ".psd", ".pub", ".ras", ".rtf", ".slk", ".stc", ".std", ".sti",
            ".stw", ".svg", ".sxc", ".sxd", ".sxi", ".sxm", ".sxw", ".tga", ".tif", ".tiff", ".txt", ".uop", ".uos",
            ".uot", ".vdx", ".vsd", ".vsdm", ".vsdx", ".vst", ".wb2", ".wk1", ".wks", ".wmf", ".wpd", ".wpg",
            ".xbm", ".xls", ".xlsb", ".xlsm", ".xslx", ".xlt", ".xltm", ".xltx", ".xlw", ".xml", ".xpm" };
        public override IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                return this.supportedFormatCollection;
            }
        }

        protected override void InternalConvert(string sourceFileName, string destinationFileName)
        {
            string sourceDir = Path.GetDirectoryName(sourceFileName);
            string sourceFileNameWithoutDir = Path.GetFileName(sourceFileName);
            string tempDestinationDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            string tempDestinationFileName = Path.Combine(tempDestinationDir, Path.GetFileNameWithoutExtension(sourceFileNameWithoutDir) + ".pdf");

            var startInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                FileName = this.GetLibreOfficeApplicationPath(),
                Arguments =
                string.Format("--norestore --nofirststartwizard --headless --convert-to pdf --outdir \"{0}\" \"{1}\"",
                tempDestinationDir, sourceFileNameWithoutDir),
                WorkingDirectory = sourceDir, //This is really important
                RedirectStandardError = true
            };
            string errorLog;

            using (var process = Process.Start(startInfo))
            {
                process.WaitForExit();
                errorLog = process.StandardError.ReadToEnd();
            }

            bool hasError = false;

            if (File.Exists(tempDestinationFileName) && errorLog.Length == 0)
            {
                File.Move(tempDestinationFileName, destinationFileName);
            }
            else
            {
                hasError = true;
            }

            Directory.Delete(tempDestinationDir, true);

            if (hasError)
            {
                throw new Exception("Couldn't convert file to PDF.");
            }
        }
    }
}

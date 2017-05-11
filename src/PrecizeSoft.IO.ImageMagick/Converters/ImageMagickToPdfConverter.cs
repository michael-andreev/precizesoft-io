using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace PrecizeSoft.IO.Converters
{
    public class ImageMagickToPdfConverter : IFileConverter
    {
        private IEnumerable<string> supportedFormatCollection = new List<string>()
            { ".aai", ".art", ".arw", ".avi", ".avs", ".bpg", ".bmp", ".bmp2", ".bmp3", ".cals",
            ".cgm", ".cin", ".cmyk", ".cmyka", ".cr2", ".crw", ".cur", ".cut", ".dcm", ".dcr",
            ".dcx", ".dds", ".dib", ".djvu", ".dng",
            //".dot", //Conflict with Word Templates (.dot)
            ".dpx", ".emf", ".epi", ".eps",
            ".eps2", ".eps3", ".epsf", ".epsi", ".ept", ".exr", ".fax", ".fig", ".fits", ".fpx",
            ".gif", ".gplt", ".gray", ".hdr", ".hpgl", ".hrz", ".ico", ".jbig", ".jng", ".jp2",
            ".jpt", ".j2c", ".j2k", ".jpeg", ".jpg", ".jxr", ".man", ".mat", ".miff", ".mono",
            ".mng", ".m2v", ".mpeg", ".mpc", ".mpr", ".mrw", ".msl", ".mtv", ".mvg", ".nef",
            ".orf", ".otb", ".p7", ".palm", ".pam", ".pbm", ".pcd", ".pcds", ".pcl", ".pcx",
            //".pdb", //Old format, but prefer to use LibreOffice
            ".pef", ".pfa", ".pfb", ".pfm", ".pgm", ".picon", ".pict", ".pix", ".png",
            ".png8", ".png00", ".png24", ".png32", ".png48", ".png64", ".pnm", ".ppm", ".ps",
            ".ps2", ".ps3", ".psb", ".psd", ".ptif", ".pwp", ".rad", ".raf", ".rgb", ".rgba",
            ".rfg", ".rla", ".rle", ".sct", ".sfw", ".sgi", ".sid", ".mrsid", ".sun", ".svg",
            ".tga", ".tiff", ".tim", ".ttf", ".uil", ".uyvy", ".vicar", ".viff", ".wbmp", ".wdp",
            ".webp", ".wmf", ".wpg", ".x", ".xbm", ".xcf", ".xpm", ".xwd", ".x3f", ".ycbcr",
            ".ycbcra", ".yuv" };
        public IEnumerable<string> SupportedFormatCollection
        {
            get
            {
                return this.supportedFormatCollection;
            }
        }

        public Stream Convert(Stream sourceStream, string fileExtension)
        {
            MemoryStream result = new MemoryStream();

            // Read image from file
            using (MagickImage image = new MagickImage(sourceStream))
            {
                // Create pdf file with a single page
                image.Write(result, new ImageMagickToPdfConverterWriteDefines());
            }

            result.Position = 0;

            return result;
        }

        public byte[] Convert(byte[] sourceBytes, string fileExtension)
        {
            byte[] result;

            using (MemoryStream resultStream = new MemoryStream())
            {
                // Read image from file
                using (MagickImage image = new MagickImage(sourceBytes))
                {
                    // Create pdf file with a single page
                    image.Write(resultStream, new ImageMagickToPdfConverterWriteDefines());
                }

                result = new byte[resultStream.Length];

                resultStream.Position = 0;
                resultStream.Read(result, 0, result.Length);
            }

            return result;
        }

        public void Convert(string sourceFileName, string destinationFileName)
        {
            // Read image from file
            using (MagickImage image = new MagickImage(sourceFileName))
            {
                // Create pdf file with a single page
                image.Write(destinationFileName, new ImageMagickToPdfConverterWriteDefines());
            }
        }
    }
}

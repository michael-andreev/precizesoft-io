using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrecizeSoft.IO
{
    public static class PathHelper
    {
        public static bool IsValidExtension(string extension)
        {
            bool result = true;

            if (!string.IsNullOrWhiteSpace(extension)
                && extension.Length > 1
                && extension[0] == '.'
                && !extension.Substring(1).Contains('.'))
            {
                char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
                for (int i = 0; i<invalidFileNameChars.Length && result; i++)
                {
                    if (extension.Contains(invalidFileNameChars[i]))
                        result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}

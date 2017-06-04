using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace PrecizeSoft.IO
{
    public static class LOEnvironment
    {
        public static bool ConfigureFromRegistry()
        {
            string path = LOEnvironment.GetLibreOfficeUnoPathFromRegistry();

            if (!string.IsNullOrEmpty(path))
            {
                LOEnvironment.RegisterInEnvironmentVariables(path);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ConfigureByUnoPath(string libreOfficeUnoPath)
        {
            LOEnvironment.RegisterInEnvironmentVariables(libreOfficeUnoPath);
        }

        public static string GetLibreOfficeUnoPathFromRegistry()
        {
            string key = @"SOFTWARE\LibreOffice\UNO\InstallPath";

            //Works only with x86 version of LibreOffice
            if (Environment.Is64BitOperatingSystem)
                key = key.Insert(key.IndexOf('\\'), @"\Wow6432Node");

            // Get the LibreOffice "program" directory
            string libreOfficePath = null;
            using (var reg = Registry.LocalMachine.OpenSubKey(key, false))
            {
                if (reg != null)
                {
                    libreOfficePath = (string)reg.GetValue(null);
                    reg.Close();
                }
            }

            return libreOfficePath;
        }

        public static bool IsRegisteredInEnvironmentVariables(string libreOfficeUnoPath)
        {
            string pathVariable = Environment.GetEnvironmentVariable("PATH");
            string unoPathVariable = Environment.GetEnvironmentVariable("UNO_PATH");

            return
                (pathVariable.ToLower().Contains(libreOfficeUnoPath.ToLower()))
                &&
                !string.IsNullOrEmpty(unoPathVariable)
                &&
                (unoPathVariable.ToLower() == libreOfficeUnoPath.ToLower());
        }

        private static void RegisterInEnvironmentVariables(string libreOfficeUnoPath)
        {
            // If application runs as service, method Path.GetFullPath()
            // and Directory.CurrentDirectory returns C:\Windows\System32,
            //that's why there using AppContext.BaseDirectory
            string unoFullPath =
                Path.IsPathRooted(libreOfficeUnoPath)
                ?
                libreOfficeUnoPath
                :
                Path.Combine(AppContext.BaseDirectory, libreOfficeUnoPath);

            // Set Environment variables
            string pathVariable = Environment.GetEnvironmentVariable("PATH");
            if (!pathVariable.ToLower().Contains(unoFullPath.ToLower()))
            {
                Environment.SetEnvironmentVariable("PATH", $"{pathVariable};{unoFullPath}");
            }

            string unoPathVariable = Environment.GetEnvironmentVariable("UNO_PATH");
            if (string.IsNullOrEmpty(unoPathVariable) || unoPathVariable.ToLower() != unoFullPath.ToLower())
            {
                Environment.SetEnvironmentVariable("UNO_PATH", unoFullPath);
            }
        }
    }
}

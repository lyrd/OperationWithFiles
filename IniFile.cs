﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OperationWithFiles
{
    public class IniFile : IDisposable
    {
        public string path;
        bool disposed;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal,
        int size, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNamesA")]
        static extern int GetPrivateProfileSectionNames(byte[] lpszReturnBuffer, int nSize, string lpFileName);

        public IniFile(string INIPath)
        {
            path = INIPath;
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }


        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
            255, this.path);
            return temp.ToString();
        }

        public byte[] IniSection()
        {
            byte[] buffer = new byte[1024];
            GetPrivateProfileSectionNames(buffer, buffer.Length, this.path);
            return buffer;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    this.path = null;
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~IniFile() { Dispose(false); }
    }
}

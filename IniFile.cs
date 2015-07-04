using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace OperationWithFiles
{
    public class IniFile : Files.IniFilesBaseClass, IDisposable
    {
        private string path;
        private bool disposed;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
        string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
        string key, string def, StringBuilder retVal,
        int size, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNamesA")]
        static extern int GetPrivateProfileSectionNames(byte[] lpszReturnBuffer, int nSize, string lpFileName);

        public IniFile(string pathToFile)
        {
            this.path = pathToFile;
        }

        public override void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public override string Read(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }

        public byte[] GetSection()
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
                    this.path = "";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationWithFiles
{
    public abstract class Files
    {
        public abstract class IniFilesBaseClass
        {
            //public abstract void Create();
            public abstract void Write(string Section, string Key, string Value);
            public abstract string Read(string Section, string Key);
        }

        //TODO: Other classes for files
    }
}

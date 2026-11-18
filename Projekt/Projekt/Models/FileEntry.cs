using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Models
{
    public class FileEntry
    {

        public string Name { get; set; } = "";

        public string FullPath { get; set; } = "";

        public bool IsDirectory { get; set; }

        public bool IsParent { get; set; } 

        public long Size { get; set; }

        public DateTime Modified { get; set; }

    }
}

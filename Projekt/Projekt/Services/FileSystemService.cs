using Projekt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public class FileSystemService
    {

        public List<FileEntry> GetEntries(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            List<FileEntry> result = new List<FileEntry>();

            
            if (directory.Parent != null)
            {
                result.Add(new FileEntry()
                {
                    Name = "..",
                    FullPath = directory.Parent.FullName,
                    IsDirectory = true,
                    IsParent = true,
                    Modified = directory.Parent.LastWriteTime,
                });
            }

            foreach (DirectoryInfo dir in directory.GetDirectories()
                         .OrderBy(d => d.Name, StringComparer.CurrentCultureIgnoreCase))
            {
                result.Add(new FileEntry()
                {
                    Name = dir.Name,
                    FullPath = dir.FullName,
                    IsDirectory = true,
                    Modified = dir.LastWriteTime,
                });
            }

            foreach (FileInfo file in directory.GetFiles()
                         .OrderBy(f => f.Name, StringComparer.CurrentCultureIgnoreCase))
            {
                result.Add(new FileEntry()
                {
                    Name = file.Name,
                    FullPath = file.FullName,
                    Size = file.Length,
                    Modified = file.LastWriteTime,
                });
            }

            return result;

        }
    }
}

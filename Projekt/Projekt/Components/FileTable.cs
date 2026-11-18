using Projekt.Graphics;
using Projekt.Models;
using Projekt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Projekt.Components
{
    public class FileTable : IComponent
    {
        private const int SizeWidth = 7;
        private const int DateWidth = 12;

        private int Inner => this.Width - 2;
        private int NameWidth => this.Inner - SizeWidth - DateWidth - 2;

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public bool IsActive { get; set; }

        public int Selected { get; private set; }

        private int offset = 0;

        private readonly FileSystemService service = new FileSystemService();
        private List<FileEntry> entries = new List<FileEntry>();

        public FileTable(string path)
        {
            this.Open(path);
        }

        private int Visible => this.Height - 4;

        public void Draw()
        {
            int inner = this.Inner;

            Theme.Normal();

            
            Screen.WriteAt(this.X, this.Y,
                Chars.TopLeft + Screen.Repeat(Chars.Horizontal, inner) + Chars.TopRight);

            
            for (int row = 1; row < this.Height - 1; row++)
            {
                Screen.WriteAt(this.X, this.Y + row,
                    Chars.Vertical + this.BuildRow("", "", "") + Chars.Vertical);
            }

            
            Screen.WriteAt(this.X, this.Y + 1,
                Chars.Vertical + this.BuildRow("Name", "Size", "Modify time") + Chars.Vertical);

            
            Screen.WriteAt(this.X, this.Y + 2,
                Chars.TeeRight + Screen.Repeat(Chars.SingleHorizontal, this.NameWidth)
                + Chars.Cross + Screen.Repeat(Chars.SingleHorizontal, SizeWidth)
                + Chars.Cross + Screen.Repeat(Chars.SingleHorizontal, DateWidth)
                + Chars.TeeLeft);

            
            for (int row = 0; row < this.Visible; row++)
            {
                int index = this.offset + row;

                if (index < this.entries.Count)
                {
                    FileEntry entry = this.entries[index];

                    if (this.IsActive && index == this.Selected)
                    {
                        Theme.Highlight();
                    }

                    Screen.WriteAt(this.X + 1, this.Y + 3 + row,
                        this.BuildRow(NameText(entry), SizeText(entry), DateText(entry)));

                    Theme.Normal();
                }
            }

            
            Screen.WriteAt(this.X, this.Y + this.Height - 1,
                Chars.BottomLeft + Screen.Repeat(Chars.Horizontal, inner) + Chars.BottomRight);
        }

        
        private string BuildRow(string name, string size, string date)
        {
            return Screen.Fit(name, this.NameWidth) + Chars.SingleVertical
                 + Screen.FitRight(size, SizeWidth) + Chars.SingleVertical
                 + Screen.Fit(date, DateWidth);
        }

        private static string NameText(FileEntry entry)
        {
            return entry.IsDirectory ? "/" + entry.Name : entry.Name;
        }

        private static string SizeText(FileEntry entry)
        {

            if (entry.IsParent)
            {
                return "UP--DIR";
            }

            if (entry.IsDirectory)
            {
                return "SUB-DIR";
            }

            long mb = 1024 * 1024;     
            long gb = mb * 1024;       

            if (entry.Size < mb)
            {
                return entry.Size.ToString();
            }

            if (entry.Size < gb)
            {
                return (entry.Size / mb) + "M";
            }

            return (entry.Size / gb) + "G";
        }
        

        private static string DateText(FileEntry entry)
        {
            return entry.Modified.ToString("MMM dd HH:mm", CultureInfo.InvariantCulture);
        }

        private void Open(string path)
        {
            try
            {
                this.entries = this.service.GetEntries(path);
                this.Selected = 0;
                this.offset = 0;
            }
            catch (UnauthorizedAccessException)
            {
                
            }
            catch (IOException)
            {
                
            }
        }


        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.UpArrow)
            {
                if (this.Selected > 0)
                {
                    this.Selected--;

                    if (this.Selected < this.offset)
                    {
                        this.offset--;
                    }
                }
            }
            else if (info.Key == ConsoleKey.DownArrow)
            {
                if (this.Selected < this.entries.Count - 1)
                {
                    this.Selected++;

                    if (this.Selected >= this.offset + this.Visible)
                    {
                        this.offset++;
                    }
                }
            
            }

            else if (info.Key == ConsoleKey.Enter)
            {
                FileEntry entry = this.entries[this.Selected];

                if (entry.IsDirectory)
                {
                    this.Open(entry.FullPath);
                }


            }

        }

    }
}

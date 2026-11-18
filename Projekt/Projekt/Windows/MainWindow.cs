using Projekt.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Windows
{
    internal class MainWindow : Window
    {

        private List<FileTable> tables = new List<FileTable>();
        private int active = 0;

        public MainWindow()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            int leftWidth = width / 2;
            int rightWidth = width - leftWidth;

            this.tables.Add(new FileTable(path) { X = 0, Y = 0, Width = leftWidth, Height = height });
            this.tables.Add(new FileTable(path) { X = leftWidth, Y = 0, Width = rightWidth, Height = height });

            this.tables[this.active].IsActive = true;
        }

        public override void Draw()
        {
            foreach (FileTable table in this.tables)
            {
                table.Draw();
            }
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Tab)
            {
                this.tables[this.active].IsActive = false;
                this.active = (this.active + 1) % this.tables.Count;
                this.tables[this.active].IsActive = true;

            }
            else if (info.Key == ConsoleKey.Escape)
            {
                this.Application.Exit();
            }
            


            else
            {
                this.tables[this.active].HandleKey(info);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Projekt.Windows
{
    public abstract class Window
    {

        public Application Application { get; set; } = null!;

        public abstract void HandleKey(ConsoleKeyInfo info);

        public abstract void Draw();

    }
}

using Projekt.Graphics;
using Projekt.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Application
    {

        private Window window = null!;
        private bool running = true;

        public Application(Window startupWindow)
        {
            this.SwitchWindow(startupWindow);
        }

        public void Run()
        {
            while (this.running)
            {
                this.window.Draw();
                this.window.HandleKey(Console.ReadKey(true));
            }

            Theme.Normal();
            Console.Clear();
        }

        public void SwitchWindow(Window window)
        {
            this.window = window;
            this.window.Application = this;

            Theme.Normal();
            Console.Clear();
        }

        public void Exit()
        {
            this.running = false;
        }

    }
}

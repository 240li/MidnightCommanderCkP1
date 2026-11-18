using Projekt.Windows;

namespace Projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            Application app = new Application(new MainWindow());
            app.Run();

        }
    }
}

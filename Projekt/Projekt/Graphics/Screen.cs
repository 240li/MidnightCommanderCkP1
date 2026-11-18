using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Graphics
{
    public class Screen
    {

        public static void WriteAt(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        public static string Repeat(char c, int count)
        {
            return new string(c, Math.Max(count, 0));
        }

        public static string Fit(string text, int width)
        {
            if (width <= 0)
            {
                return "";
            }

            if (text.Length > width)
            {
                return text.Substring(0, width - 1) + "~";
            }

            return text.PadRight(width);
        }

        public static string FitRight(string text, int width)
        {
            if (width <= 0)
            {
                return "";
            }

            if (text.Length > width)
            {
                return text.Substring(0, width - 1) + "~";
            }

            return text.PadLeft(width);
            
        }


    }
}

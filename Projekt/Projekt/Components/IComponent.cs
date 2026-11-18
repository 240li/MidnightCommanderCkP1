using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Components
{
    public interface IComponent
    {
        void HandleKey(ConsoleKeyInfo info);

        void Draw();
    }
}

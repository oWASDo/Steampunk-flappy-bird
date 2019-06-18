using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    class Level
    {
        public static void RunLevel()
        {
            while (Menù.Opened())
            {
                if (Menù.InGame)
                {
                    //GAME
                    Game.Run();
                    Game.Window.Update();
                }
                else
                {
                    //MENU'
                    Menù.Run();
                    Game.Window.Update();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace FlappyBird
{
    public static class Menù
    {
        private static BoundingBox startBox;
        private static BoundingBox escBox;
        private static BoundingBox mouse;
        private static Texture exitButtonTexture;
        private static Texture startButtonTexture;
        private static Sprite startButton;
        private static Sprite exitButton;
        private static float buttonHeight;
        private static float buttonWidth;
        public static bool GameOpened;
        public static bool InGame { private set; get; }
        private static bool isOnStart;
        private static bool isOnExit;

        static Menù()
        {
            buttonWidth = Game.Window.Width / 1.75f;
            GameOpened = true;
            buttonHeight = Game.Window.Height / 10;
            mouse = new BoundingBox((int)Game.Window.mouseX, (int)Game.Window.mouseY, 1, 1);
            startBox = new BoundingBox(Game.Window.Width / 2 - (int)buttonWidth / 2, Game.Window.Height / 4, (int)buttonWidth, (int)buttonHeight);
            escBox = new BoundingBox(Game.Window.Width / 2 - (int)buttonWidth / 2, (int)(Game.Window.Height / 1.5f), (int)buttonWidth, (int)buttonHeight);
            InGame = false;
            isOnStart = false;
            isOnExit = false;
            exitButtonTexture = TextureManager.GetTexture("Exit");
            startButtonTexture = TextureManager.GetTexture("Start");
            startButton = new Sprite(buttonWidth, buttonHeight);
            startButton.position = startBox.position;
            exitButton = new Sprite(buttonWidth, buttonHeight);
            exitButton.position = escBox.position;
        }

        //SET IF IN GAME
        //Set if we are in menù
        public static void SetInGame(bool status)
        {
            InGame = status;
        }

        //MENU' RUN
        //
        public static void Run()
        {
            Draw(Game.SeeBoundindBox);
            Update();
            Input();
            PlayClip();
            
        }

        //UPDATE MENU'
        //
        public static void Update()
        {
            if (Game.Window.opened)
            {
                mouse.position = Game.Window.mousePosition;
            }
        }

        //CHECK THE INPUT
        //Check the input of buttons
        public static void Input()
        {
            if (BoundingBox.IntersectAABBSimple(mouse, startBox))
            {
                #region Start Button
                isOnStart = true;
                if (Game.Window.mouseLeft)
                    InGame = true;
                #endregion
            }
            else
                isOnStart = false;

            if (BoundingBox.IntersectAABBSimple(mouse, escBox))
            {
                #region Exit Button
                isOnExit = true;
                if (Game.Window.mouseLeft)
                    GameOpened = false;
                #endregion
            }
            else
                isOnExit = false;
        }

        //DRAW THE BUTTONS
        //
        public static void Draw(bool seeBoundindBox)
        {
            World.Draw();
            World.DrawTerrain(seeBoundindBox);

            if (isOnStart)
                startButton.DrawTexture(startButtonTexture, (int)startButton.Width, 0, (int)startButton.Width, (int)startButton.Height);
            else
                startButton.DrawTexture(startButtonTexture, 0, 0, (int)startButton.Width, (int)startButton.Height);

            if (isOnExit)
                exitButton.DrawTexture(exitButtonTexture, (int)exitButton.Width, 0, (int)exitButton.Width, (int)exitButton.Height);
            else
                exitButton.DrawTexture(exitButtonTexture, 0, 0, (int)exitButton.Width, (int)exitButton.Height);
        }

        //RETURN IF THE WINDOW IS OPENED
        //
        public static bool Opened()
        {
            return GameOpened;
        }

        //PLAY THE BACKGROUD CLIP 
        //
        public static void PlayClip()
        {
            World.PlayClip();
        }
    }
}

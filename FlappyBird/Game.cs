using Aiv.Fast2D;
using Aiv.Audio;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlappyBird
{
    public static class Game
    {
        public static Window Window { get; private set; }
        private static Counter point;
        private static bool colliding;
        private static int width;
        private static int height;
        public static bool SeeBoundindBox { get; }
        private static AudioSource source;
        private static AudioClip clip;

        static Game()
        {
            //source = new AudioSource();
            //source.Volume = 0.1f;
            clip = Audio.GetAudio("Dead");
            width = 350 ;
            height = 500 ;
            Window = new Window(width, height, "Flappy Bird");
            Window.Update();
            point = new Counter();
            colliding = false;
            SeeBoundindBox = false;
        }

        #region Method
        //GAME RUN
        //
        public static void Run()
        {
            //INPUT
            Game.Input();
            //UPDATE
            Game.Update();
            //AUDIO
            Game.PlayClip();
            //DRAW
            Game.Draw();
        }
        
        //UPDATE THE GAME
        //
        public static void Update()
        {
            if (Player.Start)
            {
                Collision();
                Player.Update();
                ObstacleManager.Update();
            }
            point.Update();
            World.Update();
        }

        //CHECK THE PLAYER INPUT
        //Contain the input method of player class
        public static void Input()
        {
            Player.Input();
        }

        //DRAW THE WORLD OBJECT
        //Contain the draw method of every classes
        public static void Draw()
        {
            World.Draw();
            ObstacleManager.Draw(SeeBoundindBox);
            World.DrawTerrain(SeeBoundindBox);
            point.Draw();
            Player.Draw(SeeBoundindBox);
            
        }

        //RESET ALL CLASSES
        //Contain the reset method of every classes
        public static void Reset()
        {
            PlayDead();
            Menù.SetInGame(false);
            ObstacleManager.Reset();
            Player.Reset();
            point.Reset(0);
        }

        //CHECK COLLISION
        //Check the collision between player and world
        public static void Collision()
        {
            if (BoundingBox.IntersectAABBSimple(Player.Box, World.Limit[1]))
            {
                Reset();
            }
            for (int i = 0; i < World.Limit.Length; i++)
            {
                BoundingBox.IntersectAABBSat(Player.Box, World.Limit[i]);
            }
            for (int i = 0; i < ObstacleManager.numOfPipe; i++)
            {
                bool check1 = BoundingBox.IntersectAABBSimple(Player.Box, ObstacleManager.BottomPipe[i].Box);
                bool check2 = BoundingBox.IntersectAABBSimple(Player.Box, ObstacleManager.TopPipe[i].Box);
                if (check1 || check2)
                {
                    Reset();
                }
                
            }
            for (int i = 0; i < ObstacleManager.numOfPipe; i++)
            {
                if (BoundingBox.IntersectAABBSimple(Player.Box,ObstacleManager.PointBox[i]) && !colliding)
                {
                    point.NumberUp(1);
                    colliding = true;
                }
                if (Player.Box.position.X >=ObstacleManager.PointBox[i].position.X + ObstacleManager.PointBox[i].Width)
                {
                    colliding = false;
                }
            }
        }

        //PLAY AUDIO
        //Play the audio of world
        private static void PlayClip()
        {
            World.PlayClip();
        }

        //PLAY DEAD AUDIO
        //Play audio of player dead
        private static void PlayDead()
        {
            //source.Play(clip);
        }
        #endregion
    }
}

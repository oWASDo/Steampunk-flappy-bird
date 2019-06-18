using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    static class ObstacleManager
    {
        public static Obstacle[] TopPipe;
        public static Obstacle[] BottomPipe;
        public static BoundingBox[] PointBox;
        private static Random random;
        private static Sprite[] topSprite;
        private static Sprite[] bottomSprite;
        private static Texture topTexture;
        private static Texture bottomTexture;
        private static float pipeDistanceX;
        private static float pipeDistanceY;
        public static int numOfPipe;

        static ObstacleManager()
        {
            numOfPipe = 3;
            random = new Random(Guid.NewGuid().GetHashCode());
            TopPipe = new Obstacle[numOfPipe];
            BottomPipe = new Obstacle[numOfPipe];
            PointBox = new BoundingBox[numOfPipe];
            topSprite = new Sprite[numOfPipe];
            bottomSprite = new Sprite[numOfPipe];
            topTexture = TextureManager.GetTexture("Top tube");
            bottomTexture = TextureManager.GetTexture("Bottom tube");
            pipeDistanceX = Game.Window.Width / 1.5f;
            pipeDistanceY = Game.Window.Height / 5f;
            for (int i = 0; i < numOfPipe; i++)
            {
                #region Top Pipe
                TopPipe[i] = new Obstacle(Game.Window.Width + (pipeDistanceX * i), Game.Window.Height / 2);
                TopPipe[i].Box.position.Y -= TopPipe[i].Height;
                #endregion
                #region Point Box
                PointBox[i] = new BoundingBox((int)TopPipe[i].Box.X + (int)TopPipe[i].Box.Width, (int)TopPipe[i].Box.Y + (int)TopPipe[i].Box.Height, 10, (int)pipeDistanceY);
                #endregion
                #region Bottom Pipe
                BottomPipe[i] = new Obstacle(Game.Window.Width + (pipeDistanceX * i), Game.Window.Height / 2);
                BottomPipe[i].Box.position.Y += pipeDistanceY;
                #endregion
                #region Top Sprite
                topSprite[i] = new Sprite(TopPipe[i].Box.Width, TopPipe[i].Box.Height);
                topSprite[i].position = TopPipe[i].Box.position;
                #endregion
                #region Bottom Sprite
                bottomSprite[i] = new Sprite(BottomPipe[i].Box.Width, BottomPipe[i].Box.Height);
                bottomSprite[i].position = BottomPipe[i].Box.position;
                #endregion
            }
        }
        //UPDATE OBSTACLES
        //If "Player.Start" is true update the obstacles
        public static void Update()
        {
            if (Player.Start)
            {
                for (int i = 0; i < numOfPipe; i++)
                {
                    //IF OBSTACLE EXIT
                    //If obstacle exit from left
                    if (TopPipe[i].Box.X + TopPipe[i].Box.Width <= 0)
                    {
                        UpdateInY(i);
                    }
                    UpdateInX(i);
                }
            }
        }

        //UPDATE IN X DIRECTION
        //Update every element in x direction
        private static void UpdateInY(int i)
        {
            TopPipe[i].Box.position.X = Game.Window.Width + pipeDistanceX;
            float value = RandomPosY();
            TopPipe[i].Box.position.Y = value - pipeDistanceY * 4;
            BottomPipe[i].Box.position.Y = TopPipe[i].Box.position.Y + TopPipe[i].Height + pipeDistanceY;
            PointBox[i].BoundingBox_0.position.Y = TopPipe[i].Box.Y + TopPipe[i].Box.Height * 0.5f;
        }

        //UPDATE IN Y DIRECTION
        //Update every element in Y direction
        private static void UpdateInX(int i)
        {
            TopPipe[i].Update();
            BottomPipe[i].Box.position.X = TopPipe[i].Box.X;
            PointBox[i].position.X = TopPipe[i].Box.X + TopPipe[i].Box.Width;
            topSprite[i].position = TopPipe[i].Box.position;
            bottomSprite[i].position = BottomPipe[i].Box.position;
        }

        //RESET ALL ELEMENT
        //
        public static void Reset()
        {
            for (int i = 0; i < numOfPipe; i++)
            {
                TopPipe[i] = new Obstacle(Game.Window.Width + (pipeDistanceX * i), Game.Window.Height / 2 - TopPipe[i].Height);
                //TopPipe[i].Box.position.Y -= TopPipe[i].Height;
                BottomPipe[i] = new Obstacle(Game.Window.Width + (pipeDistanceX * i), Game.Window.Height / 2 + pipeDistanceY);
                //BottomPipe[i].Box.position.Y += pipeDistanceY;
                PointBox[i] = new BoundingBox((int)TopPipe[i].Box.X + (int)TopPipe[i].Box.Width, (int)TopPipe[i].Box.Y + (int)TopPipe[i].Box.Height, 10, (int)pipeDistanceY);
                topSprite[i].position = TopPipe[i].Box.position;
                bottomSprite[i].position = BottomPipe[i].Box.position;
            }
        }

        //DRAW ELEMENT
        //
        public static void Draw(bool seeBoundindBox)
        {
            for (int i = 0; i < numOfPipe; i++)
            {
                if (seeBoundindBox)
                {
                    TopPipe[i].Draw();
                    BottomPipe[i].Draw();
                    PointBox[i].Draw();
                }

                topSprite[i].DrawTexture(topTexture);
                bottomSprite[i].DrawTexture(bottomTexture);
            }
        }

        //RETURN A RANDOM FLOAT
        //
        public static float RandomPosY()
        {
            return random.Next(Game.Window.Height / 8, Game.Window.Height / 3);
        }
    }
}

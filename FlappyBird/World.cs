using Aiv.Fast2D;
using OpenTK;
using Aiv.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    public class World
    {
        public static Vector2 Gravity;
        public static BoundingBox[] Limit;
        private static Texture backgroundTexture;
        private static Texture terrainTexture;
        private static Sprite[] background;
        private static Sprite[] terrain;
        private static float terrainHeight;
        private static AudioSource source;
        private static AudioClip clip;

        static World()
        {
            //source = new AudioSource(3);
            //source.Volume = 1.5f;
            clip = Audio.GetAudio("BackGround");
            terrainHeight = Game.Window.Height / 8;
            Gravity = new Vector2(0, 200);
            backgroundTexture = TextureManager.GetTexture("Background");
            terrainTexture = TextureManager.GetTexture("Terrain");
            Limit = new BoundingBox[2];
            background = new Sprite[2];
            terrain = new Sprite[2];
            for (int i = 0; i < 2; i++)
            {
                Limit[i] = new BoundingBox(0, (i * Game.Window.Height) - (int)terrainHeight, Game.Window.Width, (int)terrainHeight);
            }

            for (int i = 0; i < background.Length; i++)
            {
                background[i] = new Sprite(Game.Window.Width, Game.Window.Height);
                background[i].position = new Vector2(Game.Window.Width * i, 0);
                terrain[i] = new Sprite(Game.Window.Width, Limit[0].Height);
                terrain[i].position = new Vector2(terrain[i].Width * i, (float)Game.Window.Height - terrain[i].Height);
            }

            //backgroundClip = new AudioClip("Assets/Run.wav");
            //backgroundsource = new AudioSource(1);
        }

        //UPDATE WORLD
        //Update the terrain and background sprite
        public static void Update()
        {
            for (int i = 0; i < background.Length; i++)
            {
                if (background[i].position.X + background[i].Width <= 0)
                {
                    if (i == 0)
                    {
                        background[i].position.X = background[1].position.X + background[1].Width;
                    }
                    if (i == 1)
                    {
                        background[i].position.X = background[0].position.X + background[0].Width;
                    }
                }
                if (terrain[i].position.X + terrain[i].Width <=0)
                {
                    if (i == 0)
                    {
                        terrain[i].position.X = terrain[1].position.X + terrain[1].Width;
                    }
                    if (i == 1)
                    {
                        terrain[i].position.X = terrain[0].position.X + terrain[0].Width;
                    }
                }

                background[i].position.X -= ObstacleManager.TopPipe[1].Speed * 0.5f * Game.Window.deltaTime;
                terrain[i].position.X -= ObstacleManager.TopPipe[1].Speed* Game.Window.deltaTime;
            }
        }

        //DRAW WORLD
        //Draw background
        public static void Draw()
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].DrawTexture(backgroundTexture);
            }
            
        }

        //DRAW TERRAIN
        //Draw terrain
        public static void DrawTerrain(bool seeBoundindBox)
        {
            for (int i = 0; i < Limit.Length; i++)
            {
                if (seeBoundindBox)
                    Limit[i].Draw(0, 0, 1, 1);
                terrain[i].DrawTexture(terrainTexture);
            }
        }

        //SET GRAVITY
        //Set the world gravity
        public static void SetGravity(float value)
        {
            Gravity.Y = value;
        }

        //PLAY SOUND
        //Playe background music
        public static void PlayClip()
        {
            //source.Stream(clip, Game.Window.deltaTime, true);
        }
    }
}

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
    public static class Player
    {
        public static BoundingBox Box;
        private static Vector2 StartPosition;
        private static Texture texture;
        private static Sprite sprite;
        public static float X
        {
            get
            {
                return StartPosition.X;
            }
        }
        public static float Y
        {
            get
            {
                return StartPosition.Y;
            }
        }
        private static float width;
        private static float height;
        private static float flySpeed;
        private static float flyLimit;
        private static float time;
        private static float dropTime;
        private static bool isPressed;
        public static bool Start;
        public static AudioSource source;
        public static AudioClip clip;
        //DEBUG
        ///public static Camera cam;

        static Player()
        {
            //source = new AudioSource();
            //source.Volume = 0.1f;
            clip = Audio.GetAudio("Flap");
            StartPosition = new Vector2(Game.Window.Width * 0.2f, Game.Window.Height * 0.5f);
            width = Game.Window.Height / 16;
            height = width;
            flySpeed = 150;
            Box = new BoundingBox((int)X, (int)Y, (int)width, (int)height);
            isPressed = false;
            Start = false;
            flyLimit = 0.3f;
            dropTime = 0;
            texture = TextureManager.GetTexture("Bird");
            sprite = new Sprite(width, height);
            sprite.position.X = Box.position.X + Box.Width / 2;
            sprite.position.Y = Box.position.Y + Box.Height / 2;
            sprite.pivot.X +=  sprite.Width / 2;
            sprite.pivot.Y += sprite.Height / 2;
            
            //DEBUG
            ///cam = new Camera();

        }

        //INPUT UPDATE
        //Player input update
        public static void Input()
        {
            if (Game.Window.GetKey(KeyCode.Esc))
            {
                Menù.SetInGame(false);
                ObstacleManager.Reset();
                Reset();
            }
            if (Game.Window.GetKey(KeyCode.Space))
            {
                if (!isPressed)
                {
                    World.SetGravity(0);
                    isPressed = true;
                    time = 0;
                    Start = true;
                    sprite.EulerRotation = 0;
                    PlayClip();
                }
            }
            //DEBUG
        }

        //PLAYER UPDATE
        //
        public static void Update()
        {
            if (Start)
                Box.position.Y += World.Gravity.Y * Game.Window.deltaTime;
            Fly();
            Rotation();
            SetSpriteToBoundingBox();
            dropTime += Game.Window.deltaTime;
        }

        //SPRITE TO BOUNDINGBPX
        //Set Sprite To Bounding Box
        private static void SetSpriteToBoundingBox()
        {
            sprite.position.X = Box.position.X + Box.Width / 2;
            sprite.position.Y = Box.position.Y + Box.Height / 2;
        }

        //RESET PLAYER
        //Reset Player status
        public static void Reset()
        {
            Box.position = StartPosition;
            Start = false;
            isPressed = false;
            dropTime = 0;
            sprite.EulerRotation = 0;
        }

        //FLY UPDATE
        //Make player fly
        public static void Fly()
        {
            if (World.Gravity.Y == 0f)
            {
                time += Game.Window.deltaTime;
                Box.position.Y -= flySpeed * Game.Window.deltaTime;
                dropTime = 0;

                if (time >= flyLimit)
                {
                    World.SetGravity(200);
                    isPressed = false;
                }
            }
        }

        //ROTATION UPDATE
        //Make player rotation
        public static void Rotation()
        {
            if (World.Gravity.Y == 0 &&sprite.EulerRotation >= -45)
            {
                sprite.EulerRotation -= 4;
            }
            else if(sprite.EulerRotation <= 90)
            {
                sprite.EulerRotation += 4;
            }
        }

        //PLAYER DRAW 
        //
        public static void Draw(bool seeBoundindBox)
        {
            if(seeBoundindBox)
                Box.Draw();
            sprite.DrawTexture(texture);
        }

        //PLAY PLAYER SOUND
        //
        public static void PlayClip()
        {
            //source.Play(clip);
        }
    }
}

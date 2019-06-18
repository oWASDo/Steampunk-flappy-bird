using Aiv.Fast2D;
using System;
using System.IO.MemoryMappedFiles;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace FlappyBird
{
    public class Counter
    {
        private int number;
        private int record;
        private Texture texture;
        private Sprite sprite0;
        private Sprite sprite1;
        private Sprite sprite2;
        private int width;
        private int height;
        private string stringNumber;
        private bool dozen;
        private bool hundreds;

        
        public Counter(int start = 0)
        {
            number = start;
            texture = TextureManager.GetTexture("Number");
            width = 61;
            height = 89;
            sprite0 = new Sprite(width, height);
            sprite1 = new Sprite(width, height);
            sprite2 = new Sprite(width, height);

            sprite0.position.X = Game.Window.Width * 0.5f - sprite0.Width * 0.5f;
            sprite1.position.X = Game.Window.Width * 0.5f;
            sprite2.position.X = Game.Window.Width * 0.5f + sprite2.Width * 0.5f;

            dozen = true;
            hundreds = true;
        }

        //UPDATE THE COUNTER
        //
        public void Update()
        {
            stringNumber = "" + number;
            if (number > record)
            {
                record = number;
            }
        }

        //DRAW COUNTER
        //
        public void Draw()
        {
            int offset0 = int.Parse(stringNumber[0].ToString()) * width;
            sprite0.DrawTexture(texture, offset0, 0, width, height);

            if (number >= 10)
            {
                if (dozen)
                 {
                    sprite0.position.X -= sprite0.Width * 0.5f;
                    dozen = false;
                }
                int offset1 = int.Parse(stringNumber[1].ToString()) * width;
                sprite1.DrawTexture(texture, offset1, 0, width, height);
            }

            if (number >= 100)
            {
                if (hundreds)
                {
                     sprite0.position.X -= sprite0.Width * 0.5f;
                    sprite1.position.X -= sprite1.Width * 0.5f;
                    hundreds = false;
                }
                int offset2 = int.Parse(stringNumber[2].ToString()) * width;
                sprite2.DrawTexture(texture, offset2, 0, width, height);
            }
            
        }

        //SET THE NUMBRE UP
        //Set the number up on a number
        public void NumberUp(int number = 1)
        {
            this.number += number;
        }

        //SET THE NUMBRE DOWN
        //Set the number down on a number
        public void NumberDown(int number = 1)
        {
            this.number += number;
        }

        //RESET THE COUNTER
        //Reset the conter to a number a e the counter sprite
        public void Reset(int number)
        {
            this.number = number;
            sprite0.position.X = Game.Window.Width * 0.5f - sprite0.Width * 0.5f;
            sprite1.position.X = Game.Window.Width * 0.5f;
            sprite2.position.X = Game.Window.Width * 0.5f + sprite2.Width * 0.5f;
            dozen = true;
            hundreds = true;
        }
    }
}

using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    class Obstacle
    {
        public BoundingBox Box;
        private float width;
        public float Height;
        public float Speed { get; }

        public Obstacle(float posX,float posY)
        {
            width = Game.Window.Height / 6;
            Height = Game.Window.Height;
            Box = new BoundingBox((int)posX, (int)posY, (int)width, (int)Height);
            Speed = 150;
        }

        //UPDATE OBSTACLE
        //Move the obstacle on left
        public void Update()
        {
            Box.position.X -= Speed * Game.Window.deltaTime;
        }

        //DRAW OBSTACLE
        //
        public void Draw()
        {
            Box.Draw();
        }
    }
}

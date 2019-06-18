using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using Aiv.Fast2D;

namespace FlappyBird
{
    public class BoundingBox
    {
        public Vector2 position;
        public Vector2 size;
        public Vector2 gravityAccumulator;
        public Vector2 jumpForce;
        private float jumpPower = 400;
        public bool IsGrounded;
        public Mesh BoundingBox_0;
        
        public BoundingBox(int x, int y, int width, int height)
        {
            position = new Vector2(x, y);
            size = new Vector2(width, height);
            gravityAccumulator = new Vector2(0, 0);
            jumpForce = new Vector2(0, 0);
            BoundingBox_0 = new Mesh();
        }
        public Vector2 Center
        {
            get
            {
                return new Vector2(position.X + size.X / 2, position.Y + size.Y / 2);
            }
        }
        public float HalfWidth
        {
            get
            {
                return Width / 2;
            }
        }
        public float HalfHeight
        {
            get
            {
                return Height / 2;
            }
        }
        public float Width
        {
            get
            {
                return size.X;
            }
        }
        public float Height
        {
            get
            {
                return size.Y;
            }
        }
        public float X
        {
            get
            {
                return position.X;
            }
        }
        public float Y
        {
            get
            {
                return position.Y;
            }
        }
        public void Jump(Window window)
        {
            if (!IsGrounded)
                return;

            if (window.GetKey(KeyCode.Space))
            {
                jumpForce = new Vector2(0, -jumpPower);
            }
        }
        public void Draw(float r = 0, float g = 1, float b = 0, float a = 1)
        {
            BoundingBox_0.v = new float[]
            {
                position.X,position.Y,
                position.X + size.X,position.Y,
                position.X + size.X,position.Y + size.Y,

                position.X ,position.Y,
                position.X + size.X,position.Y + size.Y,
                position.X,position.Y + size.Y,
            };
            BoundingBox_0.UpdateVertex();
            BoundingBox_0.DrawWireframe(r,g,b,a, 0.1f);
        }
        // AABB -> Axis Aligned Bounding Box
        public static bool IntersectAABBSimple(BoundingBox boxOne, BoundingBox boxTwo)
        {
            if (boxOne.X + boxOne.Width >= boxTwo.X &&
                boxOne.X <= boxTwo.X + boxTwo.Width &&
                boxOne.Y + boxOne.Height >= boxTwo.Y &&
                boxOne.Y <= boxTwo.Y + boxTwo.Height)
            {
                return true;
            }
            return false;
        }
        // SAT -> Separate Axis Theorem
        public static bool IntersectAABBSat(BoundingBox boxOne, BoundingBox boxTwo)
        {
            // distance between centers
            float distanceX = boxTwo.Center.X - boxOne.Center.X;
            // how much penetration
            float pointX = (boxOne.HalfWidth + boxTwo.HalfWidth) - Math.Abs(distanceX);
            if (pointX <= 0)
                return false;

            // distance between centers
            float distanceY = boxTwo.Center.Y - boxOne.Center.Y;
            // how much penetration
            float pointY = (boxOne.HalfHeight + boxTwo.HalfHeight) - Math.Abs(distanceY);
            if (pointY <= 0)
                return false;

            // repulsion/reposition
            if (pointX < pointY)
            {
                if (distanceX > 0)
                {
                    boxOne.position.X = boxTwo.X - boxOne.Width;
                }
                else
                {
                    boxOne.position.X = boxTwo.X + boxTwo.Width;
                }
                return true;
            }
            else
            {
                if (distanceY > 0)
                {
                    // when touching the ground, cancel gravityAccumulator and jumpForce
                    boxOne.position.Y = boxTwo.Y - boxOne.Height - 1;
                    boxOne.gravityAccumulator = new Vector2(0, 0);
                    boxOne.jumpForce = new Vector2(0, 0);
                    // set as grounded so we can detect another jump
                    boxOne.IsGrounded = true;
                    
                }
                else
                {
                    boxOne.position.Y = boxTwo.Y + boxTwo.Height;
                }
                return true;
            }
        }

    }
}

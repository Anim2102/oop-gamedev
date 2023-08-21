using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity.Behaviour.Attack
{
    public class AttackSquare
{
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }

        public int AttackStartFrame { get; set; }
        public int AttackEndFrame { get; set; }

        public Vector2 FramePosition { get; set; }

        public AttackSquare(int width, int height,int attackStartFrame,int attackEndFrame)
        {
            FrameWidth = width; 
            FrameHeight = height;
            AttackStartFrame = attackStartFrame;
            AttackEndFrame = attackEndFrame;

            FramePosition = Vector2.Zero;
        }

        public Rectangle ReturnFrameWithNewPosition(Vector2 newPosition)
        {
            return new Rectangle((int)newPosition.X, (int)newPosition.Y, FrameWidth, FrameHeight);
        }

        public Rectangle ReturnAttackFrame()
        {
            return new Rectangle((int)FramePosition.X, (int)FramePosition.Y, FrameWidth, FrameHeight);
        }

        public void RemovePosition()
        {
            FramePosition = Vector2.Zero;
        }

        public void ChangeFramePosition(Vector2 position)
        {
            Vector2 newPosition = FramePosition;
            newPosition.X = position.X;
            newPosition.Y = position.Y;
            FramePosition = newPosition;
        }

         
    }
}

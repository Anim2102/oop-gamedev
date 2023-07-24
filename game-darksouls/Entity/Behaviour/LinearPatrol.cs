using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity.Behaviour
{
    public class LinearPatrol : IBehave
    {
        private readonly AnimatedObject animatedObject;

        private Vector2 positionA;
        private Vector2 positionB;
        private Vector2 currentTarget;

        public LinearPatrol(Vector2 positionA, Vector2 positionB, AnimatedObject animatedObject)
        {
            this.positionA = positionA;
            this.positionB = positionB;

            currentTarget = positionA;

            this.animatedObject = animatedObject;
        }

        public void Behave()
        {
            
        }
    }
}

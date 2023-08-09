using game_darksouls.Component;
using game_darksouls.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Utilities
{
    public static class VectorHelpingClass
{
        public static float ReturnDistanceBetweenPlayerLinear(Vector2 currentPosition, Box box)
        {
            Vector2 playerPosition = box.CenterOfBox();

            return CalculateDistanceBetweenTwoVectorsOnX(currentPosition, playerPosition);
        }


        public static float ReturnDistanceBetweenPlayerXandY(Vector2 currentPosition, Box box)
        {
            Vector2 playerPosition = box.CenterOfBox();

            return CalculateDistanceBetweenTwoVectorsOnXandY(currentPosition, playerPosition);
        }


        public static float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }

        public static float CalculateDistanceBetweenTwoVectorsOnXandY(Vector2 a, Vector2 b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}

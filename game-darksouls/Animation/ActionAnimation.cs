using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game_darksouls.Animation
{
    public class ActionAnimation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        private int counter;
        private double secondCounter = 0;
        public int fps = 15;
        public string AnimationName;

        public ActionAnimation(string animationName)
        {
            frames = new List<AnimationFrame>();
            AnimationName = animationName;
        }
        public ActionAnimation()
        {
            frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }
        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }
    }
}

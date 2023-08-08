using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game_darksouls.Animation
{
    public class ActionAnimation
    {
        public AnimationFrame CurrentFrame { get; set; }
        private List<AnimationFrame> frames;
        public string name;
        public int Counter { get; private set; }
        private double secondCounter = 0;
        public int fps = 15;
        public bool IsRunning { get; private set; }
        public bool Loop { get; set; } = true;
        public bool Stopped { get; set; } = false;
        public bool Complete { get; set; } = false;

        public ActionAnimation(string name)
        {
            frames = new List<AnimationFrame>();
            this.name = name;
        }

        public void AddFrame(AnimationFrame frame)
        {
            frames.Add(frame);
            CurrentFrame = frames[0];
        }
        public void Play()
        {
            IsRunning = true;
            Counter = 0;
            secondCounter = 0;
        }
        public void ResetAnimation()
        {
            Complete = false;
            Counter = 0; 
            secondCounter = 0;
            IsRunning = false;
        }
        public void Stop()
        {
            IsRunning = false;
            secondCounter = 0;

            if (!Loop)
            {
                Counter = frames.Count - 1; // Set to the last frame
                Complete = true;
            }
        }
        public void Update(GameTime gameTime)
        {
          
            CurrentFrame = frames[Counter];
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (secondCounter >= 1d / fps)
            {
                Complete = false;
                Counter++;
                secondCounter = 0;
            }

            if (Counter >= frames.Count)
            {
                if (Loop)
                {
                    Counter = 0;
                    Complete = true;
                }
                else
                {
                    Counter = frames.Count - 1;
                    Stop();
                }
                Complete = true;
            }

        }

    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        }

        public void Stop()
        {
            IsRunning = false;
            Counter = 0;
            secondCounter = 0;

            //handy for death animation
            CurrentFrame = frames[0];
        }
        public void Update(GameTime gameTime)
        {
            if (!IsRunning && !Loop)
                return;

            CurrentFrame = frames[Counter];
            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;

            if (secondCounter >= 1d / fps)
            {
                Counter++;
                secondCounter = 0;
            }

            if (Counter >= frames.Count)
            {
                Counter = 0;
                
                if (IsRunning)
                    Stop();
            }

        }

    }
}

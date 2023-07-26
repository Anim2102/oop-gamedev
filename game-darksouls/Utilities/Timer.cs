using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace game_darksouls.Utilities
{
    internal class Timer
    {
        private float seconds;
        private double currentSeconds = 0;

        public bool timeRunning { get; private set; } = false; 

        public Timer(int seconds) { 
            this.seconds = seconds;
        }

        public void Update(GameTime gameTime)
        {
            if (timeRunning)
            {
                if (currentSeconds < seconds)
                {
                    double passedTime = gameTime.ElapsedGameTime.TotalSeconds;
                    currentSeconds += passedTime;
                }

                if (Math.Round(currentSeconds) == seconds)
                {
                    timeRunning = false;
                }
            }
        }

        public void Reset()
        {
            currentSeconds = 0;
        }

        public void Start()
        {
            timeRunning = true;
        }
    }
}

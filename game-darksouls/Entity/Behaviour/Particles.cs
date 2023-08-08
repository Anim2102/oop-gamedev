using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace game_darksouls.Entity.Behaviour
{
    public class Particles
    {
        private Box collisionBox;
        private List<Rectangle> particles;
        private float maxLifeTime = 2f;

        public Particles(Box collisionBox)
        {
            this.collisionBox = collisionBox;
            this.particles = new List<Rectangle>();
        }


        public void SpawnParticles()
        {
            for (int i = 0; i < 10; i++)
            {
                Rectangle newParticle = new Rectangle(collisionBox.Rectangle.X, collisionBox.Rectangle.Y, 10, 10);
                particles.Add(newParticle);
            }
        }
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                Rectangle particle = particles[i];
                particle.X += (int)(0.9f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                particle.Y += (int)(2f * (float)gameTime.ElapsedGameTime.TotalSeconds);
                maxLifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (maxLifeTime <= 0)
            {
                particles.Clear();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var particle in particles)
            {
                spriteBatch.Draw(Game1.redsquareDebug, particle, Color.Red);

            }
        }
    }
}

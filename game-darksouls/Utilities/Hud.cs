using Component.Health;
using game_darksouls.Component.Health;
using game_darksouls.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace game_darksouls.Utilities
{
    public class Hud
    {
        private readonly Texture2D healthTexture;
        private readonly IHealth health;
        private List<Rectangle> amountHealth = new();
        private readonly Viewport viewport;

        private const int SIZE = 50;


        public Hud(IHealth health, ContentManager content, Viewport viewport)
        {
            this.health = health;
            this.healthTexture = content.Load<Texture2D>("hart");
            this.viewport = viewport;   

            
        }

        public void Update()
        {
            UpdateHealth();
        }
        private void UpdateHealth()
        {
            if (amountHealth.Count == health.HealthPoints)
                return;

            amountHealth.Clear();
            int posX = (int)(viewport.Width * 0.05f);
            int posY = (int)(viewport.Height * 0.1f);
            
            for (int i = 0; i < health.HealthPoints; i++)
            {
                Rectangle rec = Rectangle.Empty;
                rec.X = posX;
                rec.Y = posY;
                rec.Width = SIZE; 
                rec.Height = SIZE;

                amountHealth.Add(rec);

                posX += SIZE;
            }
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var hart in amountHealth)
            {
                spriteBatch.Draw(healthTexture, hart, Color.White);
            }
            //spriteBatch.Draw(Game1.redsquareDebug, TestHearth, Color.White);
        }
    }
}

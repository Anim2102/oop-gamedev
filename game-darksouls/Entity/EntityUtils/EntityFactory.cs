using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity.EntityUtils
{
    public static class EntityFactory
{
        public static AnimatedObject EntityCreator(ContentManager content, string textureName,Player player)
        {
            AnimatedObject entity = null;

            if (textureName == "skeleton")
            {
                Texture2D entityTexture = content.Load<Texture2D>("skeleton");
                return new Skeleton(entityTexture, player);
            }

            else if (textureName == "Brain Mole")
            {
                Texture2D entityTexture = content.Load<Texture2D>("Brain Mole");
                return new WingedMob(entityTexture, player);
            }
            else if (textureName == "wizard")
            {
                Texture2D entityTexture = content.Load<Texture2D>("wizard");
                Texture2D fireballTexture = content.Load<Texture2D>("fireball");
                return new Wizard(entityTexture, fireballTexture,player);
            }

            return entity; 
        }
}
}

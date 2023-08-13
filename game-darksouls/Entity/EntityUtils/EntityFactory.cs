using game_darksouls.Component;
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
        public static AnimatedObject EntityCreator(ContentManager content, string textureName,Player player,CollisionManager collision)
        {
            AnimatedObject entity = null;

            if (textureName == "skeleton")
            {
                Texture2D entityTexture = content.Load<Texture2D>("skeleton");
                Skeleton skeleton = new Skeleton(entityTexture, player,collision);
                

                return skeleton;
            }

            else if (textureName == "Brain Mole")
            {
                Texture2D entityTexture = content.Load<Texture2D>("Brain Mole");
                WingedMob winged = new WingedMob(entityTexture, player, collision);

                return winged;
            }
            else if (textureName == "wizard")
            {
                Texture2D entityTexture = content.Load<Texture2D>("wizard");
                Texture2D fireballTexture = content.Load<Texture2D>("fireball");

                Wizard wizard = new Wizard(entityTexture,fireballTexture, player, collision);

                return wizard;
            }

            return entity; 
        }
}
}

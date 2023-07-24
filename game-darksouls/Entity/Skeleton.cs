﻿using game_darksouls.Animation;
using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity
{
    public class Skeleton : AnimatedObject, IEntity
    {
        private NpcMovementManager npcMovementManager;

        public Skeleton(Texture2D texture) {
            this.texture = texture;
            this.animationManager = new(AnimationFactory.LoadSkeletonAnimations());
            this.npcMovementManager = new NpcMovementManager(this, new CollisionManager());

            this.drawingBox.DrawingRectangle = new Rectangle(170, 20, 80, 80);
            
        }
      

        public void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
            npcMovementManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawingBox.DrawingRectangle, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White);
            npcMovementManager.Draw(spriteBatch);
        }
    }
}

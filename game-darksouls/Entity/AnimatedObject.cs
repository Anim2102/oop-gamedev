﻿

using Component.Health;
using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public abstract class AnimatedObject
    {
        public Texture2D Texture { get; set; }
        public AnimationManager AnimationManager { get; set; }
        public Health HealthManager { get; set; }
        public CollisionManager CollisionManager { get; set; }
        public IMovementBehaviour MovementBehaviour { get; set; }
        public Box DrawingBox { get; set; }
        public Box CollisionBox { get; set; }

        public virtual void Update(GameTime gameTime)
        {

            DrawingBox.UpdatePosition(CollisionBox.Position);
        }
    }
}

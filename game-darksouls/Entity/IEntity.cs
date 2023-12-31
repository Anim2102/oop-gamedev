﻿using Component.Health;
using game_darksouls.Component;
using game_darksouls.Component.Health;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public interface IEntity
    {
        IHealth HealthManager { get; }
        Box CollisionBox { get; }
        void StartPosition(Vector2 startPosition);

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}

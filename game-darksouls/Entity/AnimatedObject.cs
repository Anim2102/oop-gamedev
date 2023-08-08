

using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public abstract class AnimatedObject
    {
        private protected AnimationManager animationManager;
        public Health HealthManager { get; set; }
        private protected Texture2D texture;
        internal Box drawingBox = new();
        internal Box collisionBox;


    }
}

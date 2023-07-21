using Microsoft.Xna.Framework;

namespace game_darksouls.Animation
{
    public class AnimationFrame
    {
        public AnimationFrame(Rectangle sourceRectangle)
        {
            SourceRectangle = sourceRectangle;
        }
        public Rectangle SourceRectangle { get; set; }
    }
}

using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game_darksouls.Animation.EntityAnimations
{
    public abstract class AnimationFactory
    {
        public abstract Dictionary<MovementState, ActionAnimation> LoadAnimations();

        public static ActionAnimation LoadAnimations(string name = "none", bool loop = true, int fps = 15, int amountFrames = 1, int yas = 40, int width = 32, int height = 27)
        {
            ActionAnimation animation = new ActionAnimation(name);
            animation.Loop = loop;
            animation.fps = fps;
            int xAs = 0;
            int yAs = yas;


            for (int i = 0; i < amountFrames; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(xAs, yAs, width, height)));
                xAs += width;
            }

            return animation;
        }
    }
}

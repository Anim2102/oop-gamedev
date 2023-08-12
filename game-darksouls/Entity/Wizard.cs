using Component.Health;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public class Wizard : AnimatedObject, IEntity
    {
        private RangeAttack rangeAttack;
        private Texture2D fireball;

        public Wizard(Texture2D texture, Texture2D fireball, Player player)
        {
            this.Texture = texture;
            this.fireball = fireball;

            AnimationManager = new AnimationManager(AnimationFactory.LoadWizardAnimations());
            CollisionManager = new CollisionManager();
            HealthManager = new Health(1, MovementBehaviour, AnimationManager);
            DrawingBox = new Box(250, 20, 64 * 4, 64 * 4, new Vector2(-100, -130));
            CollisionBox = new Box(2575, 720, 64, 100, new Vector2(0, 0));

            MovementBehaviour = new GroundMovement(new CollisionManager(), AnimationManager, CollisionBox);
            HealthManager = new Health(1,MovementBehaviour, AnimationManager);
            rangeAttack = new RangeAttack(player, this, AnimationManager, MovementBehaviour, CollisionManager, fireball);
            rangeAttack.RangeOfAttack = 300;
        }

        public override void Update(GameTime gameTime)
        {

            if (HealthManager.CurrentState != Component.Health.State.DYING &&
                HealthManager.CurrentState != Component.Health.State.DEATH)
            {
                MovementBehaviour.Update(gameTime);
                rangeAttack.Behave(gameTime);

            }
            AnimationManager.Update(gameTime);
            rangeAttack.UpdateSpell(gameTime);
            DrawingBox.UpdatePosition(CollisionBox.Position);
            base.Update(gameTime);
            //Debug.WriteLine(animationManager.currentAnimation.name);
            //Debug.WriteLine(animationManager.currentAnimation.Counter);
            //Debug.WriteLine(animationManager.currentAnimation.Complete);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Green);
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Green);

            rangeAttack.Draw(spriteBatch);
            spriteBatch.Draw(Texture,
                DrawingBox.Rectangle,
                AnimationManager.CurrentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                AnimationManager.SpriteFLip,
                0f);
        }
    }
}

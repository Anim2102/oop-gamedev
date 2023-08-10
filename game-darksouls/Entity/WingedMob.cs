using Component.Health;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public class WingedMob : AnimatedObject, IEntity
    {
        private IMovementBehaviour npcMovementManager;
        private BehaveController entityStateController;

        private Player player;

        private LinearPatrol linearPatrol;
        private Agressive agressive;
        private Attack attackBox;


        public WingedMob(Texture2D texture, Player player)
        {
            this.Texture = texture;
            AnimationManager = new AnimationManager(AnimationFactory.LoadBrainMobAnimations());

            CollisionBox = new Box(2405, 700, 50, 50);
            DrawingBox = new Box(2405, 700, 50, 50);

            npcMovementManager = new FlyMovement(new CollisionManager(), AnimationManager, CollisionBox);
            this.player = player;

            linearPatrol = new(new Vector2(2275, 799), new Vector2(2500, 799), this, npcMovementManager);

            attackBox = new Attack(AnimationManager, CollisionBox, Vector2.Zero);
            attackBox.AttackStartFrame = 2;
            attackBox.AttackEndFrame = 3;
            attackBox.WidthAttackFrame = 90;
            attackBox.HeightAttackFrame = 50;

            agressive = new Agressive(EntityMovementType.FLYING, player, this, npcMovementManager, AnimationManager, attackBox);
            agressive.RangeOfAttack = 50;

            entityStateController = new BehaveController(linearPatrol, agressive, player, this);

            HealthManager = new Health(1, npcMovementManager, AnimationManager);



        }

        public override void Update(GameTime gameTime)
        {
            //Debug.WriteLine(HealthManager.HealthPoints);
            //Debug.WriteLine(animationManager.currentAnimation.name);

            npcMovementManager.Update(gameTime);
            entityStateController.Update(gameTime);
            AnimationManager.Update(gameTime);
            HealthManager.Update(gameTime);
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            /*spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Green);
            spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Green);
            Debug.WriteLine(animationManager.currentAnimation.name);
            */

            entityStateController.Draw(spriteBatch);
            spriteBatch.Draw(Texture,
                DrawingBox.Rectangle,
                AnimationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                AnimationManager.SpriteFLip,
                0f);
        }
    }
}

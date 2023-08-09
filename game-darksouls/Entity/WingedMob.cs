using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Entity
{
    public class WingedMob : AnimatedObject, IEntity
    {
        private IMovementBehaviour npcMovementManager;
        private EntityStateController entityStateController;

        private Player player;

        private LinearPatrol linearPatrol;
        private Agressive agressive;
        private Attack attackBox;

        public WingedMob(Texture2D texture, Player player)
        {
            this.texture = texture;
            animationManager = new AnimationManager(AnimationFactory.LoadBrainMobAnimations());

            collisionBox = new Box(2405, 700, 50, 50);
            drawingBox = new Box(2405, 700, 50, 50);

            npcMovementManager = new FlyMovement(new CollisionManager(), animationManager, collisionBox);
            this.player = player;

            linearPatrol = new(new Vector2(2275, 799), new Vector2(2500, 799), this, npcMovementManager);

            attackBox = new Attack(animationManager, collisionBox, Vector2.Zero);
            attackBox.AttackStartFrame = 2;
            attackBox.AttackEndFrame = 3;
            attackBox.WidthAttackFrame = 90;
            attackBox.HeightAttackFrame = 50;

            agressive = new Agressive(EntityMovementType.FLYING,player, this, npcMovementManager, animationManager,attackBox);
            agressive.RangeOfAttack = 50;

            entityStateController = new EntityStateController(linearPatrol, agressive, player, this);
           
        }

        public void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
            npcMovementManager.Update(gameTime);
            drawingBox.UpdatePosition(collisionBox.Position);
            entityStateController.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            /*spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Green);
            spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Green);
            Debug.WriteLine(animationManager.currentAnimation.name);
            */
            entityStateController.Draw(spriteBatch);
            spriteBatch.Draw(texture,
                drawingBox.Rectangle,
                animationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                animationManager.SpriteFLip,
                0f);
        }
    }
}

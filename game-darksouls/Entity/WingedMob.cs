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

        public WingedMob(Texture2D texture, Player player)
        {
            this.texture = texture;
            this.animationManager = new AnimationManager(AnimationFactory.LoadBrainMobAnimations());
            this.collisionBox = new Box(50, 50, 50, 50);
            this.drawingBox = new Box(50, 50, 50, 50);

            this.npcMovementManager = new FlyMovement(new CollisionManager(), animationManager, collisionBox);
            this.player = player;
            this.linearPatrol = new(new Vector2(170, 0), new Vector2(450, 0), this, npcMovementManager);
            this.agressive = new Agressive(player, this, npcMovementManager, animationManager);

            entityStateController = new EntityStateController(linearPatrol, agressive, player, this);
           
        }

        public void Update(GameTime gameTime)
        {
            this.npcMovementManager.Update(gameTime);
            this.animationManager.Update(gameTime);
            this.entityStateController.Update(gameTime);

            drawingBox.UpdatePosition(collisionBox.Position);
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            /*spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Green);
            spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Green);
            Debug.WriteLine(animationManager.currentAnimation.name);
            */
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

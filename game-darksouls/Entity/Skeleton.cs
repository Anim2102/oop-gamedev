using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Entity
{
    public class Skeleton : AnimatedObject, IEntity
    {
        private IMovementBehaviour npcMovementManager;

        private readonly Player player;
        private EntityStateController entityStateController;

        //temp switch to manager
        private LinearPatrol linearPatrol;
        private Agressive agressive;


        public Skeleton(Texture2D texture, Player player)
        {
            collisionBox = new Box();
            collisionBox.Rectangle = new Rectangle(200, 909, 60, 50);
            this.drawingBox = new Box(170, 20, 64 * 2, 64 * 2 , new Vector2(-35, -50));

            this.texture = texture;
            this.animationManager = new(AnimationFactory.LoadSkeletonAnimations());
            this.npcMovementManager = new GroundMovement(new CollisionManager(), animationManager,collisionBox);

            this.linearPatrol = new(new Vector2(170, 909), new Vector2(450, 909), this, npcMovementManager);
            this.agressive = new Agressive(player, this, npcMovementManager, animationManager);

            entityStateController = new EntityStateController(linearPatrol, agressive, player, this);
            
        }


        public void Update(GameTime gameTime)
        {
            //Debug.WriteLine(collisionBox.Position);

            animationManager.Update(gameTime);
            npcMovementManager.Update(gameTime);
            drawingBox.UpdatePosition(collisionBox.Position);
            entityStateController.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, drawingBox.DrawingRectangle, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White);

            /*Rectangle rec = this.drawingBox.DrawingRectangle;
            rec.Width = rec.Width * 2;
            rec.Height = rec.Height * 2;
            */
            
            //spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Green);
            

            spriteBatch.Draw(texture, 
                drawingBox.Rectangle,
                animationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f, 
                Vector2.Zero,
                animationManager.SpriteFLip,
                0f);
            Debug.WriteLine(this.collisionBox.CenterOfBox());
            linearPatrol.Draw(spriteBatch);
            //npcMovementManager.Draw(spriteBatch);
        }
    }
}

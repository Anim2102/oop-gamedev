using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.Behaviour;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public class Skeleton : AnimatedObject, IEntity
    {
        private NpcMovementManager npcMovementManager;

        private protected Player player;
        private EntityStateController entityStateController;

        //temp switch to manager
        private LinearPatrol linearPatrol;
        private Agressive agressive;

        public Skeleton(Texture2D texture, Player player)
        {
            this.texture = texture;
            this.animationManager = new(AnimationFactory.LoadPlayerAnimations());
            this.npcMovementManager = new NpcMovementManager(this, new CollisionManager(), animationManager);

            this.drawingBox.DrawingRectangle = new Rectangle(170, 20, 60, 50);


            this.linearPatrol = new(new Vector2(170, 0), new Vector2(450, 0), this, npcMovementManager);
            this.agressive = new Agressive(player, this, npcMovementManager, animationManager);

            entityStateController = new EntityStateController(linearPatrol, agressive, player, this);
        }


        public void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
            npcMovementManager.Update(gameTime);
            entityStateController.Update(gameTime);
            //agressive.Behave(gameTime);
            //linearPatrol.Behave(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, drawingBox.DrawingRectangle, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White);
            
            
            spriteBatch.Draw(texture, 
                drawingBox.DrawingRectangle,
                animationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f, 
                Vector2.Zero,
                animationManager.SpriteFLip,
                0f);

            //linearPatrol.Draw(spriteBatch);
            npcMovementManager.Draw(spriteBatch);
        }
    }
}

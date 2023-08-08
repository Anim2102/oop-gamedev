using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Entity
{
    public class Player : AnimatedObject, IEntity
    {
        private PlayerMovement playerMovement;
        private Attack attack;
        public Particles particles;

        public Player(Texture2D texturePlayer)
        {
            texture = texturePlayer;

            animationManager = new AnimationManager(AnimationFactory.LoadPlayerAnimations());
            playerMovement = new(this,new CollisionManager(),animationManager,new());
            //attack = new Attack(new Rectangle(),1,1,animationManager,this,npcMovementManager,this)
            collisionBox = new Box(350, 600, 30, 40);
            drawingBox.Rectangle = new Rectangle(0, 0, 50, 50);
            drawingBox.Offset = new Vector2(-10, -10);

            particles = new Particles(collisionBox);
            
        }
        public void Update(GameTime gameTime)
        {
            Debug.WriteLine(collisionBox.Position);
            drawingBox.UpdatePosition(collisionBox.Position);
            playerMovement.Update(gameTime);
            animationManager.Update(gameTime);
            particles.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Red);
            //spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Red);
            //playerMovement.Draw(spriteBatch);
            spriteBatch.Draw(texture, 
                drawingBox.Rectangle,
                animationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White, 
                0f,
                Vector2.Zero,
                animationManager.SpriteFLip,
                0f );

            particles.Draw(spriteBatch);
        }
    }
}

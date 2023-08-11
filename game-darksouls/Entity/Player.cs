using Component.Health;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Entity
{
    public class Player : AnimatedObject, IEntity
    {
        private IMovementBehaviour playerMovement;
        private Attack attack;
        private PlayerAbilities playerAbilities;

        public bool IsPlayerAttack => playerAbilities.Attacking;

        public Player(Texture2D texturePlayer)
        {
            Texture = texturePlayer;

           
            CollisionBox = new Box(500, 700, 30, 40);
            DrawingBox = new Box(0, 0, 50, 50);
            DrawingBox.Offset = new Vector2(-10, -10);

            AnimationManager = new AnimationManager(AnimationFactory.LoadPlayerAnimations());
            playerMovement = new PlayerMovement(new CollisionManager(),CollisionBox,AnimationManager,new(), this);

            attack = new Attack(AnimationManager,CollisionBox,Vector2.Zero);
            attack.WidthAttackFrame = 40;
            attack.HeightAttackFrame = 32;
            attack.AttackStartFrame = 2;
            attack.AttackEndFrame = 4;

            HealthManager = new Health(5, playerMovement,AnimationManager);
            playerAbilities = new PlayerAbilities(playerMovement, attack, new(),AnimationManager);
            
        }
        public override void Update(GameTime gameTime)
        {
            //Debug.WriteLine(collisionBox.Position);
            playerMovement.Update(gameTime);
            AnimationManager.Update(gameTime);
            HealthManager.Update(gameTime);
            playerAbilities.Update(gameTime);
            HealthManager.Update(gameTime);
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Red);
            //spriteBatch.Draw(Game1.redsquareDebug, attack, Color.Red);
            //playerMovement.Draw(spriteBatch);
            //playerAbilities.Draw(spriteBatch);
            
            attack.Draw(spriteBatch);
            spriteBatch.Draw(Texture, 
                DrawingBox.Rectangle,
                AnimationManager.CurrentAnimation.CurrentFrame.SourceRectangle,
                HealthManager.CurrentColor, 
                0f,
                Vector2.Zero,
                AnimationManager.SpriteFLip,
                0f );
        }
    }
}

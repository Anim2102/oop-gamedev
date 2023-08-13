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

        private int collisionBoxWidth = 30;
        private int collisionBoxHeight = 40;
        private int drawingBoxWidth = 50;
        private int drawingBoxHeight = 50;
        private float drawingBoxOffsetY = -10;
        private float drawingBoxOffsetX = -10;

        

        private int HEALTH = 5;
        public bool IsPlayerAttack => playerAbilities.Attacking;

        public Player(Texture2D texturePlayer, CollisionManager collisionManager) : base(collisionManager)
        {
            Texture = texturePlayer;
           
            CollisionBox = new Box(0, 0, collisionBoxWidth, collisionBoxHeight);
            DrawingBox = new Box(0, 0, drawingBoxWidth, drawingBoxHeight);
            DrawingBox.Offset = new Vector2(drawingBoxOffsetX, drawingBoxOffsetY);

            AnimationManager = new AnimationManager(AnimationFactory.LoadPlayerAnimations());
            playerMovement = new PlayerMovement(CollisionManager,CollisionBox,AnimationManager,new(), this);

            attack = new Attack(this,AnimationManager,CollisionBox,Vector2.Zero,collisionManager);
            attack.WidthAttackFrame = 40;
            attack.HeightAttackFrame = 32;
            attack.AttackStartFrame = 2;
            attack.AttackEndFrame = 4;

            HealthManager = new Health(HEALTH, playerMovement,AnimationManager);
            playerAbilities = new PlayerAbilities(playerMovement, attack, new(),AnimationManager);
            
        }
        public override void Update(GameTime gameTime)
        {
            
            playerMovement.Update(gameTime);
            AnimationManager.Update(gameTime);
            playerAbilities.Update(gameTime);
            HealthManager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}

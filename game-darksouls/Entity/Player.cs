using Component.Health;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Component.Health;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Enum;
using game_darksouls.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Entity
{
    public class Player : AnimatedObject, IEntity
    {
        private IMovementBehaviour movementBehaviour;
        private CollisionManager collisionManager;
        private CloseAttack attack;
        private PlayerAbilities playerAbilities;
        private Health healthManager;

        private ISoundManager soundManager;

        private const int COLLISIONBOXWIDTH = 30;
        private const int COLLISIONBOXHEIGHT = 40;
        private const int DRAWINGBOXWIDTH = 50;
        private const int DRAWINGBOXHEIGHT = 50;
        private const float DRAWINGBOXOFFSETY = -10;
        private const float DRAWINGBOXOFFSETX = -10;

        private const int HEALTH = 5;
        public bool IsPlayerAttack => playerAbilities.IsAttacking;

        public IHealth HealthManager
        {
            get
            {
                return healthManager;
            }
        }

        public Box CollisionBox
        {
            get
            {
                return collisionBox;
            }
        }

        public Player(Texture2D texturePlayer, ContentManager content, CollisionManager collisionManager) : base(texturePlayer)
        {
            soundManager = new SoundManager();
            soundManager.AddSoundEffect("swing", content.Load<SoundEffect>("sounds/sword swing air"));
            soundManager.AddSoundEffect("hit swing", content.Load<SoundEffect>("sounds/sword swing hit"));


            this.collisionManager = collisionManager;

            collisionBox = new Box(0, 0, COLLISIONBOXWIDTH, COLLISIONBOXHEIGHT);
            drawingBox = new Box(0, 0, DRAWINGBOXWIDTH, DRAWINGBOXHEIGHT);
            drawingBox.Offset = new Vector2(DRAWINGBOXOFFSETX, DRAWINGBOXOFFSETY);

            animationManager = new AnimationManager(AnimationFactory.LoadPlayerAnimations());

            attack = new CloseAttack(this,animationManager,collisionBox,collisionManager);
            attack.WidthAttackFrame = 40;
            attack.HeightAttackFrame = 32;
            attack.AttackStartFrame = 2;
            attack.AttackEndFrame = 4;

            playerAbilities = new PlayerAbilities(attack, new(),soundManager);

            movementBehaviour = new PlayerMovement(collisionManager, collisionBox, animationManager, new(),playerAbilities);
            healthManager = new Health(HEALTH, movementBehaviour, (IDeathAnimation)animationManager);

        }
        public override void Update(GameTime gameTime)
        {
            //Debug.WriteLine(collisionBox.Position);
            movementBehaviour.Update(gameTime);
            animationManager.Update(gameTime);
            playerAbilities.Update(gameTime);
            HealthManager.Update(gameTime);
            collisionManager.CheckIfCollectible(this);
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                drawingBox.Rectangle,
                animationManager.CurrentAnimation.CurrentFrame.SourceRectangle,
                healthManager.CurrentColor,
                0f,
                Vector2.Zero,
                animationManager.SpriteFlip,
                0f);
        }
    }
}

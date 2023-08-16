using Component.Health;
using game_darksouls.Animation;
using game_darksouls.Component;
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
        private IMovementBehaviour playerMovement;
        private Attack attack;
        private PlayerAbilities playerAbilities;

        private SoundManager soundManager;

        private const int COLLISIONBOXWIDTH = 30;
        private const int COLLISIONBOXHEIGHT = 40;
        private const int DRAWINGBOXWIDTH = 50;
        private const int DRAWINGBOXHEIGHT = 50;
        private const float DRAWINGBOXOFFSETY = -10;
        private const float DRAWINGBOXOFFSETX = -10;

        private const int HEALTH = 5;
        public bool IsPlayerAttack => playerAbilities.Attacking;

        public Player(Texture2D texturePlayer, CollisionManager collisionManager, ContentManager content) : base(collisionManager)
        {
            Texture = texturePlayer;
            soundManager = new SoundManager();
            soundManager.AddSoundEffect("swing", content.Load<SoundEffect>("sounds/sword swing air"));

            CollisionBox = new Box(0, 0, COLLISIONBOXWIDTH, COLLISIONBOXHEIGHT);
            DrawingBox = new Box(0, 0, DRAWINGBOXWIDTH, DRAWINGBOXHEIGHT);
            DrawingBox.Offset = new Vector2(DRAWINGBOXOFFSETX, DRAWINGBOXOFFSETY);

            AnimationManager = new AnimationManager(AnimationFactory.LoadPlayerAnimations());
            playerMovement = new PlayerMovement(CollisionManager,CollisionBox,AnimationManager,new(), this);

            attack = new Attack(this,AnimationManager,CollisionBox,Vector2.Zero,collisionManager);
            attack.WidthAttackFrame = 40;
            attack.HeightAttackFrame = 32;
            attack.AttackStartFrame = 2;
            attack.AttackEndFrame = 4;

            HealthManager = new Health(HEALTH, playerMovement,AnimationManager);
            playerAbilities = new PlayerAbilities(playerMovement, attack, new(),soundManager);

            
            
        }
        public override void Update(GameTime gameTime)
        {
            
            playerMovement.Update(gameTime);
            AnimationManager.Update(gameTime);
            playerAbilities.Update(gameTime);
            HealthManager.Update(gameTime);
            CollisionManager.CheckIfCollectible(this);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, CollisionBox.Rectangle,Color.Red);
            base.Draw(spriteBatch);
        }
    }
}

﻿using game_darksouls.Animation;
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
            texture = texturePlayer;
            
            collisionBox = new Box(2405, 700, 30, 40);
            drawingBox.Rectangle = new Rectangle(0, 0, 50, 50);
            drawingBox.Offset = new Vector2(-10, -10);

            animationManager = new AnimationManager(AnimationFactory.LoadPlayerAnimations());
            playerMovement = new PlayerMovement(new CollisionManager(),collisionBox,animationManager,new(), this);

            attack = new Attack(animationManager,collisionBox,Vector2.Zero);
            attack.WidthAttackFrame = 40;
            attack.HeightAttackFrame = 32;
            attack.AttackStartFrame = 2;
            attack.AttackEndFrame = 4;

            HealthManager = new Health(5, playerMovement);
            playerAbilities = new PlayerAbilities(playerMovement, attack, new(),animationManager);
            
        }
        public void Update(GameTime gameTime)
        {
            Debug.WriteLine(collisionBox.Position);
            drawingBox.UpdatePosition(collisionBox.Position);
            playerMovement.Update(gameTime);
            animationManager.Update(gameTime);
            HealthManager.Update(gameTime);
            playerAbilities.Update(gameTime);
            HealthManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Red);
            //spriteBatch.Draw(Game1.redsquareDebug, attack, Color.Red);
            //playerMovement.Draw(spriteBatch);
            //playerAbilities.Draw(spriteBatch);
            
            attack.Draw(spriteBatch);
            spriteBatch.Draw(texture, 
                drawingBox.Rectangle,
                animationManager.currentAnimation.CurrentFrame.SourceRectangle,
                HealthManager.CurrentColor, 
                0f,
                Vector2.Zero,
                animationManager.SpriteFLip,
                0f );
        }
    }
}

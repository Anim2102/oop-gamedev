using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.Behaviour
{
    internal class Agressive : IBehave
    {
        private EntityMovementType entityMovement;
        private readonly Player player;
        private readonly AnimatedObject animatedObject;
        private readonly AnimationManager animationManager;
        private readonly IMovementBehaviour npcMovementManager;

        //private Timer waitTimerBeforeAttack;

        private bool attacking = false;
        private bool attackPossible = false;

        private Attack attackBox;

        private Vector2 currentPosition => animatedObject.CollisionBox.Position;
        private Vector2 playerPosition => player.CollisionBox.CenterOfBox();

        public float RangeOfAttack { get; set; } = 40f;

        public Agressive(EntityMovementType movementType,Player player, AnimatedObject animatedObject, IMovementBehaviour npcMovementManager, AnimationManager animationManager, Attack attackBox)
        {
            this.player = player;
            this.animatedObject = animatedObject;
            this.npcMovementManager = npcMovementManager;
            this.animationManager = animationManager;

            //waitTimerBeforeAttack = new Timer(3);
            this.attackBox = attackBox;

        }

        public void Behave(GameTime gameTime)
        {
            float distanceBetweenPlayer = 0f;
            if (entityMovement == EntityMovementType.FLYING)
            {
                 distanceBetweenPlayer = VectorHelpingClass.ReturnDistanceBetweenPlayerXandY(currentPosition, player.CollisionBox);
            }

            if (entityMovement == EntityMovementType.GROUND)
            {
                distanceBetweenPlayer = VectorHelpingClass.ReturnDistanceBetweenPlayerLinear(currentPosition, player.CollisionBox);

            }
            //Debug.WriteLine("currentposition" + currentPosition);
            //Debug.WriteLine("player position" + playerPosition);
            //Debug.WriteLine("attacking: " + attacking + " aaval mogelijk: " + attackPossible);
            //Debug.WriteLine(distanceBetweenPlayer);
            if (!attacking && distanceBetweenPlayer < RangeOfAttack)
            {
                attackPossible = true;
            }
        
            if (attackPossible && distanceBetweenPlayer < RangeOfAttack)
            {
                attacking = true;
                npcMovementManager.ResetDirection();
                attackBox.AttackWithFrame();

                if (attackBox.AttackFinished)
                {
                    attacking = false;
                    attackPossible = false;
                }
            }

            if (attacking && distanceBetweenPlayer > RangeOfAttack)
            {
                attacking = false;
                attackPossible = false;
            }
            

            if (!attacking && currentPosition != playerPosition)
            {
                attackBox.RemoveAttackFrame();
                Vector2 normalized = Vector2.Normalize(playerPosition - currentPosition);
                //Debug.WriteLine("onderweg:" + normalized);
                npcMovementManager.Push(normalized);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.redsquareDebug, new Rectangle((int)currentPosition.X, (int)currentPosition.Y, 2, 2), Color.Red);
            spriteBatch.Draw(Game1.redsquareDebug, new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 2, 2), Color.Red);

            spriteBatch.Draw(Game1.redsquareDebug,attackBox.attackFrame,Color.Red);
        }
        
        
    }
}

using game_darksouls.Animation;
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
        private Box collisionBox;
        private readonly IAnimationManager animationManager;
        private readonly IMovementBehaviour npcMovementManager;

        //private Timer waitTimerBeforeAttack;

        private bool attacking = false;
        private bool attackPossible = false;

        private MeleeAttack attackBox;

        private Vector2 currentPosition => collisionBox.Position;
        private Vector2 playerPosition => collisionBox.CenterOfBox();

        public float RangeOfAttack { get; set; } = 40f;

        public Agressive(EntityMovementType movementType,Player player,Box collisionbox, IMovementBehaviour npcMovementManager, IAnimationManager animationManager, MeleeAttack attackBox)
        {
            this.player = player;
            this.npcMovementManager = npcMovementManager;
            this.animationManager = animationManager;

            //waitTimerBeforeAttack = new Timer(3);
            this.attackBox = attackBox;
            this.collisionBox = collisionbox;

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

using Entity.Behaviour.Attack;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.Behaviour.Attack;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.Behaviour
{
    internal class BehaveController : IComponent
    {
        

        public Vector2 PatrolPointA { get; set; }
        public Vector2 PatrolPointB { get; set; }
        public EntityMovementType MovementType { get; set; }
        public IAttack Attack { get; set; }
        public float RangeOfAttack { get; set; }


        private IBehave currentBehaviour;

        public IBehave PatrolState { get; private set; }
        public IBehave AgressiveState { get; private set; }

        public BehaveController(Player player,EntityMovementType movementType,
            Vector2 patrolPointA,Vector2 patrolPointB, IAttack attack, IMovementBehaviour movementBehaviour, Box collissionBox)
        {
                        
            PatrolPointA = patrolPointA;
            PatrolPointB = patrolPointB;

            Attack = attack;
            MovementType = movementType;

            PatrolState = new LinearPatrol(player,patrolPointA, patrolPointB,collissionBox,movementBehaviour,this);
            AgressiveState = new Agressive(movementType, player, collissionBox, movementBehaviour, Attack,this);


            SetBehaveState(PatrolState);
        }   

        public void Update(GameTime gameTime)
        {
            currentBehaviour.Behave(gameTime);
        }

        public void SetBehaveState(IBehave behave)
        {
            currentBehaviour = behave;
        }
    }
}

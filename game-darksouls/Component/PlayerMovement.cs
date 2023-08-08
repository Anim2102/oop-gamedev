﻿using game_darksouls.Entity;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Enum;
using game_darksouls.Input;
using Microsoft.Xna.Framework;
using System;

namespace game_darksouls.Component
{
    internal class PlayerMovement : IComponent, IMovementBehaviour
    {
        public CollisionManager CollisionManager { get; set; }
        public AnimationManager AnimationManager { get; set; }
        public Box CollisionBox { get; set; }
        public MovementState CurrentMovementState { get; set; }

        public Player Player { get; set; }
        private InputManager inputManager;

        private Vector2 direction;
        private Vector2 speed;
        private Vector2 velocity;

        private bool jumping;
        private bool onFloor;
        private const float MAXJUMP = 200f;
        private const float JUMPFORCE = 15f;
        private float currentJumpTime;



        public PlayerMovement(CollisionManager collisionManager, Box CollisionBox, AnimationManager playerAnimation, InputManager inputManager, Player player)
        {
            this.CollisionBox = CollisionBox;
            this.CollisionManager = collisionManager;
            this.AnimationManager = playerAnimation;
            this.inputManager = inputManager;
            this.Player = player;

            this.direction = Vector2.Zero;
            this.onFloor = false;

            speed = new Vector2(0.3f, 0.4f);
            velocity = new Vector2(1, 1);
            jumping = false;
        }

        public void Update(GameTime gameTime)
        {
            CheckFloor();
            JumpPlayer(gameTime);
            ApplyGravity(gameTime);
            Move(gameTime);
            ChangeMovingState(direction);
            ChangeFlipOnDirection(direction);
        }

        private void Move(GameTime gameTime)
        {
            direction = inputManager.GetInput();
            Vector2 currentPosition = CollisionBox.Position;
            Vector2 futurePosition = currentPosition;

            if (!direction.Equals(Vector2.Zero))
            {
                futurePosition = currentPosition + direction * velocity * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            MoveWithCollision(futurePosition);

        }

        private void MoveWithCollision(Vector2 futurePosition)
        {
            Rectangle newPosition = new Rectangle((int)futurePosition.X, (int)futurePosition.Y, CollisionBox.Rectangle.Width,
                CollisionBox.Rectangle.Height);

            if (!CollisionManager.CheckForCollision(newPosition))
                CollisionBox.UpdatePosition(futurePosition);
        }

        private void ApplyGravity(GameTime gameTime)
        {
            if (!onFloor)
            {
                Vector2 currentPosition = CollisionBox.Position;
                Vector2 futurePosition = currentPosition + new Vector2(0, velocity.Y * speed.Y * (float)gameTime.ElapsedGameTime.TotalMilliseconds);

                MoveWithCollision(futurePosition);
            }

        }

        private void JumpPlayer(GameTime gameTime)
        {
            //Debug.WriteLine(jumping);
            if (!jumping && inputManager.IsJumpButtonPress() && onFloor)
            {
                jumping = true;
                currentJumpTime = 0;
            }
            if (jumping)
            {
                currentJumpTime += (float)gameTime.ElapsedGameTime.Milliseconds;
                if (currentJumpTime < MAXJUMP)
                {
                    Vector2 futurePosition = CollisionBox.Position;
                    futurePosition.Y -= JUMPFORCE;
                    MoveWithCollision(futurePosition);
                }
                else
                {
                    jumping = false;
                }

            }
        }

        private void CheckFloor()
        {
            Rectangle feet = new Rectangle(CollisionBox.Rectangle.X,
                CollisionBox.Rectangle.Y + CollisionBox.Rectangle.Height,
                CollisionBox.Rectangle.Width,
                8);

            onFloor = CollisionManager.CheckForCollision(feet);
        }

        private void ChangeFlipOnDirection(Vector2 direction)
        {
            if (direction.X > 0)
            {
                AnimationManager.FacingLeft = false;
            }
            else if (direction.X < 0)
            {
                AnimationManager.FacingLeft = true;
            }
        }

        private void ChangeMovingState(Vector2 direction)
        {
            if (Player.IsPlayerAttack)
                return;

            if (!onFloor || jumping)
            {
                CurrentMovementState = MovementState.FALLING;
            }
            if (direction.X != 0 && !jumping && onFloor)
            {
                CurrentMovementState = MovementState.MOVING;
            }
            else if (direction.X == 0 && onFloor)
            {
                CurrentMovementState = MovementState.IDLE;
            }

            AnimationManager.UpdateAnimationOnState(CurrentMovementState);
        }

        public void Push(Vector2 direction)
        {
            this.direction = direction;
        }

        public void ResetDirection()
        {
            this.direction.X = 0;
        }
    }
}

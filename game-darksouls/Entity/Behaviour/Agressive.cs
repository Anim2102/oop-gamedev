using game_darksouls.Component;
using Microsoft.Xna.Framework;

namespace game_darksouls.Entity.Behaviour
{
    internal class Agressive : IBehave
    {
        private readonly Player player;
        private readonly AnimatedObject animatedObject;
        private readonly NpcMovementManager npcMovementManager;

        private Vector2 currentPosition;

        public Agressive(Player player, AnimatedObject animatedObject, NpcMovementManager npcMovementManager)
        {
            this.player = player;
            this.animatedObject = animatedObject;
            this.npcMovementManager = npcMovementManager;
        }

        public void Behave(GameTime gameTime)
        {
            Vector2 currentPosition = new Vector2(animatedObject.drawingBox.DrawingRectangle.X,
                animatedObject.drawingBox.DrawingRectangle.Y);

            Vector2 playerPosition = new Vector2(player.drawingBox.DrawingRectangle.X,
                player.drawingBox.DrawingRectangle.Y);

            if (currentPosition != playerPosition)
            {
                Vector2 normalized = Vector2.Normalize(playerPosition - currentPosition);
                npcMovementManager.MoveNpc(normalized);
            }

        }
    }
}

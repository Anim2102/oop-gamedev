using Microsoft.Xna.Framework;

namespace game_darksouls.Level
{
    public class Tile
    {
        public Rectangle TileBox { get; set; }
        public Rectangle SourceRectangle { get; set; }
        
        public Tile(int x,int y, int width, int height)
        {
            TileBox = new Rectangle(x,y,width,height);
        }
    }
}

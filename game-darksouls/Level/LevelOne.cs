using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace game_darksouls.Level
{
    public class LevelOne
    {
        private static TempLevel instance;

        private readonly Texture2D tilesetTexture;
        private const int TILESIZE = 50;
        private int tileSetWidth;
        private int tileSetHeight;

        private int columns;
        private int rows;

        private int[,] tileArray;

        public List<Tile> Tiles { get; private set; } = new();
        public LevelOne(Texture2D tileset)
        {
            this.tilesetTexture = tileset;
            this.tileSetWidth = tilesetTexture.Width;
            this.tileSetHeight = tilesetTexture.Height;

            this.columns = tileSetWidth / TILESIZE;
            this.rows = tileSetHeight / TILESIZE;

            tileArray = new int[columns, rows];
            tileArray = ReadCsv();

            CreateTileSet();
        }
        public static TempLevel GetInstance()
        {
            if (instance == null)
            {
                instance = new TempLevel();
            }

            return instance;
        }

        private void CreateTileSet()
        {
            for (int i = 0; i < tileArray.Length; i++)
            {
                for (int j = 0; j < tileArray.GetLength(1); j++)
                {
                    Tile newTile = new Tile(i * TILESIZE, j * TILESIZE, TILESIZE, TILESIZE);
                    Tiles.Add(newTile);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in Tiles)
            {
                spriteBatch.Draw(Game1.redsquareDebug, tile.TileBox, Color.Red);

            }
        }
        private int[,] ReadCsv()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Tim\\source\\repos\\game-darksouls\\game-darksouls\\Level\\csv levels\\test.csv");
            int rows = lines.Length;
            //Debug.Write(lines[0]);
            int columns = lines[0].Split(',').Length;

            int[,] array = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    string[] line = lines[i].Split(',');
                    array[i, j] = Convert.ToInt32(line[j]);
                }
            }

            return array;
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace game_darksouls.Level
{
    public class LevelOne
    {
        private static LevelOne instance;

        private readonly Texture2D tilesetTexture;
        private const int TILESIZE = 50;
        private int tileSetWidth;
        private int tileSetHeight;

        private int columns;
        private int rows;

        private int[,] tileArray;

        public List<Tile> Tiles { get; private set; } = new();

        public LevelOne()
        {
            this.tilesetTexture = Game1.dungeonTexture;
            this.tileSetWidth = tilesetTexture.Width;
            this.tileSetHeight = tilesetTexture.Height;

            this.columns = tileSetWidth / TILESIZE;
            this.rows = tileSetHeight / TILESIZE;

            tileArray = new int[columns, rows];
            tileArray = ReadCsv();

            CreateTileSet();
        }

        public static LevelOne GetInstance()
        {
            if (instance == null)
            {
                instance = new LevelOne();
            }

            return instance;
        }

        private void CreateTileSet()
        {
            for (int i = 0; i < tileArray.GetLength(0); i++)
            {
                for (int j = 0; j < tileArray.GetLength(1) -1; j++)
                {
                    int tileId = tileArray[i, j];
                    if (tileId != 0)
                    {
                        Tile newTile = new Tile();
                        newTile.TileBox = new Rectangle(j * TILESIZE,i * TILESIZE, TILESIZE,TILESIZE);
                        Tiles.Add(newTile);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in Tiles)
            {
                spriteBatch.Draw(Game1.redsquareDebug, tile.TileBox, Color.Red);
                //spriteBatch.Draw(Game1.dungeonTexture, tile.TileBox,tile.SourceRectangle,Color.White);
            }
        }
        private int[,] ReadCsv()
        {
            string[] lines = File.ReadAllLines("C:\\Users\\Tim\\source\\repos\\game-darksouls\\game-darksouls\\Level\\csv levels\\test2.csv");
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

            Debug.Write(array);
            return array;
        }

    }
}

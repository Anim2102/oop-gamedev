using game_darksouls.Collectible;
using game_darksouls.Entity;
using game_darksouls.Levels;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace game_darksouls.Level
{
    public abstract class LevelSetup
    {
        public List<Tile> Tiles { get; protected set; } = new();
        public List<IEntity> entitys { get; protected set; } = new();
        

        public int[,] TileArray { get; protected set; }
        
        private const int TILESIZE = 50;
        private int tileSetWidth;
        private int tileSetHeight;

        private int columns;
        private int rows;


        protected string pathToCsv;
        private Texture2D tileSetTexture;

        public int MapWidth
        {
            get
            {
                return TileArray.GetLength(1) * TILESIZE;
            }
        }

        public int MapHeight
        {
            get
            {
                return TileArray.GetLength(0) * TILESIZE;
            }
        }

        public Rectangle GetMapSize
        {
            get
            {
                return new Rectangle(0,0,MapWidth,MapHeight);
            }
        }

        public LevelSetup(Texture2D tileSetTexture,string filepath)
        {
            this.tileSetTexture = tileSetTexture;
            this.tileSetWidth = tileSetTexture.Width;
            this.tileSetHeight = tileSetTexture.Height;

            this.columns = tileSetWidth / TILESIZE;
            this.rows = tileSetHeight / TILESIZE;
            this.pathToCsv = filepath;

            TileArray = new int[columns, rows];
            TileArray = ReadCsv();

            CreateTileSet();
        }


        public virtual void Update(GameTime gameTime)
        {
            foreach (var entity in entitys)
            {
                entity.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entitys)
            {
                entity.Draw(spriteBatch);
            }
            foreach (var tile in Tiles)
            {
                spriteBatch.Draw(tileSetTexture, tile.TileBox, tile.SourceRectangle, Color.White);
            }
        }

        private void CreateTileSet()
        {

            for (int i = 0; i < TileArray.GetLength(0); i++)
            {
                for (int j = 0; j < TileArray.GetLength(1); j++)
                {
                    int tileId = TileArray[i, j];
                    if (tileId != 0)
                    {
                        Tile newTile = new Tile();
                        newTile.TileBox = new Rectangle(j * TILESIZE, i * TILESIZE, TILESIZE, TILESIZE);

                        //bron voor de 'formule': https://stackoverflow.com/questions/41094534/tile-id-on-grid-from-x-y-position?fbclid=IwAR3K2L55mB_xApg2Ty_N8JSUtGvsDMro2F1AAEXtNd2vMWB5WJkYwuXpJG0
                        // de id's verwijzen naar het correcte vakje op de texture.
                        int sourceX = tileId % columns;
                        int sourceY = tileId / columns;

                        newTile.SourceRectangle = new Rectangle(sourceX * TILESIZE, sourceY * TILESIZE, TILESIZE, TILESIZE);
                        Tiles.Add(newTile);
                    }
                }
            }
        }
        private int[,] ReadCsv()
        {
            string[] lines = File.ReadAllLines(pathToCsv);
            int rows = lines.Length;
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

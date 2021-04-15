using System;
using System.Collections.Generic;
using System.Text;
using MonogameELP.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonogameELP.Gameobjects
{
    class Level1Tilemap : TileMap
    {
        public override int TILEMAP_WIDTH { get => base.TILEMAP_WIDTH; set => base.TILEMAP_WIDTH = value; }
        public override int TILEMAP_HEIGHT => base.TILEMAP_HEIGHT;

        public override Tile.TileColliderType GetTileColliderType(int[] tileNum)
        {
            return Tile.TileColliderType.Null;
        }

        public override Dictionary<String, Tile> GetTile()
        {
            Dictionary<String, Tile> tiles = new Dictionary<string, Tile>();
            tiles.Add("Ground", new Tile(this, new int[] { 2, 0 }));
            tiles.Add("Dirt", new Tile(this, new int[] { 2, 1 }));

            tiles.Add("Left Slight Triangle 1", new Tile(this, new int[] { 3, 0 }));
            tiles.Add("Left Slight Triangle 2", new Tile(this, new int[] { 4, 0 }));
            tiles.Add("Right Slight Triangle 1", new Tile(this, new int[] { 7, 0 }));
            tiles.Add("Right Slight Triangle 2", new Tile(this, new int[] { 8, 0 }));

            tiles.Add("Left Steep Triangle", new Tile(this, new int[] { 1, 0 }));
            tiles.Add("Right Steep Triangle", new Tile(this, new int[] { 10, 0 }));

            return tiles;
        }

        public override float[] TileToWorldPos(int[] tilePos)
        {
            return null;
        }

        public override List<Tile> LevelTiles()
        {
            List < Tile > levelTiles = new List<Tile>();

            Tile tile0 = GetTile()["Left Steep Triangle"];
            tile0.SetTilePosition(new Vector2(1, 3));
            levelTiles.Add(tile0);

            Tile tile1 = GetTile()["Ground"];
            tile1.SetTilePosition(new Vector2(2, 3));
            levelTiles.Add(tile1);

            Tile tile2 = GetTile()["Ground"];
            tile2.SetTilePosition(new Vector2(3, 3));
            levelTiles.Add(tile2);

            Tile tile3 = GetTile()["Ground"];
            tile3.SetTilePosition(new Vector2(4, 3));
            levelTiles.Add(tile3);

            Tile tile4 = GetTile()["Right Steep Triangle"];
            tile4.SetTilePosition(new Vector2(5, 3));
            levelTiles.Add(tile4);

            return levelTiles;
        }

        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            TILEMAP_WIDTH = 12;
            TILEMAP_HEIGHT = 7;
        }

        public override void Update()
        {

        }

        public override void DrawTiles(Texture2D texture, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            LevelTiles().ForEach(delegate (Tile tile)
            {
                spriteBatch.Draw(texture: texture,
                position: tile.transform.Position,
                sourceRectangle: new Rectangle(x: tile.getTileNumber()[0] * tile.TILE_SIZE, y: tile.getTileNumber()[1] * tile.TILE_SIZE, width: 16, height: 16),
                color: Color.White,
                rotation: tile.transform.Rotation,
                origin: new Vector2(0, 0),
                scale: tile.transform.Scale,
                effects: SpriteEffects.None,
                layerDepth: 0.5f);
            });
        }


        /*public override void DrawTiles(Texture2D texture, GraphicsDevice graphics, SpriteBatch spriteBatch, Tile tile)
        {
            spriteBatch.Draw(texture: texture,
                            position: tile.transform.Position,
                            sourceRectangle: new Rectangle(x: tile.getTileNumber()[0]*tile.TILE_SIZE, y: tile.getTileNumber()[1]* tile.TILE_SIZE, width: 16, height: 16),
                            color: Color.White,
                            rotation: tile.transform.Rotation,
                            origin: new Vector2(0, 0),
                            scale: tile.transform.Scale,
                            effects: SpriteEffects.None,
                            layerDepth: 0.5f);
        }*/
    }
}

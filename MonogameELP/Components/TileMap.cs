using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameELP.Components
{
    // WIP
    abstract class TileMap
    {
        public virtual int TILEMAP_WIDTH { get; set; }
        public virtual int TILEMAP_HEIGHT { get; set; }

        public abstract Tile.TileColliderType GetTileColliderType(int[] tileNum);

        public abstract Dictionary<String, Tile> GetTile();

        public abstract float[] TileToWorldPos(int[] tilePos);

        public abstract List<Tile> LevelTiles();

        public abstract void Initialize();

        public abstract void LoadContent();

        public abstract void Update();

        public abstract void DrawTiles(Texture2D texture, GraphicsDevice graphics, SpriteBatch spriteBatch);
    }
}

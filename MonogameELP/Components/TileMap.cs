using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameELP.Components
{
    // WIP
    class TileMap
    {
        public Tile.TileColliderType GetTileColliderType(int[] tileNum)
        {
            return Tile.TileColliderType.Null;
        }

        public Texture2D GetTileTexture()
        {
            return null;
        }

        public Tile MatchTileWithPosition(int[] tileNum)
        {
            return null;
        }

        public void CreateTiles()
        {

        }

        public void DrawTiles()
        {

        }
    }
}

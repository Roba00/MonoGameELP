using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonogameELP.Components
{
    //WIP
    class Tile
    {
        readonly int TILE_SIZE = 32;

        TileMap tileMap; // Tile map that the tile is from.
        Vector2 worldPos; // Tile position in world cooridnates
        Vector2 tilePos;  // Tile position in tile cooridnates
        int[] tileNumber;  // Tile number in the tilemap. Mapped in [x,y].
        public enum TileColliderType {Block, SteepTriangle, SlightTriangle, Null}; // Defines the collider types.
        public TileColliderType tileCollider; // What collider type the tile has.

        public Tile(TileMap tileMap, Vector2 tilePos, int[] tileNumber)
        {
            this.tileMap = tileMap;
            this.worldPos = worldPos;
            this.tilePos = worldPos / TILE_SIZE;
            this.tileNumber = tileNumber;
            tileCollider = tileMap.GetTileColliderType(tileNumber);
        }
    }
}

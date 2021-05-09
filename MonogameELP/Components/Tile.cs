using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonogameELP.Components
{
    //WIP
    //TODO: Work on collision detection
    class Tile
    {
        public readonly int TILE_SIZE = 16;

        TileMap tileMap; // Tile map that the tile is from.
        public Transform transform { get; private set; }
        public BoxCollider collider;

        Vector2 tilePos;  // Tile position in tile cooridnates
        Vector2 worldPos; // Tile position in world cooridnates
        int[] tileNumber;  // Tile number in the tilemap. Mapped in [x,y].

        public enum TileColliderType {Block, SteepTriangle, SlightTriangle, Null}; // Defines the collider types.
        public TileColliderType tileCollider; // What collider type the tile has.

        public Tile(TileMap tileMap, int[] tileNumber, Vector2 tilePos)
        {

            this.tileMap = tileMap;
            this.tileNumber = tileNumber;
            this.tilePos = tilePos;
            worldPos = tilePos * TILE_SIZE;
            tileCollider = tileMap.GetTileColliderType(tileNumber);

            transform = new Transform();
            transform.Rotation = 0f;
            transform.Scale = new Vector2(8f, 8f);
            transform.Position = worldPos * transform.Scale.X;
        }

        public Tile(TileMap tileMap, int[] tileNumber)
        {
            this.tileMap = tileMap;
            this.tileNumber = tileNumber;
            tileCollider = tileMap.GetTileColliderType(tileNumber);

            transform = new Transform();
            transform.Rotation = 0f;
            transform.Scale = new Vector2(8f, 8f);
            transform.Position = Vector2.Zero;
        }

        public int[] getTileNumber()
        {
            return tileNumber;
        }

        public void SetTilePosition(Vector2 tilePos)
        {
            this.tilePos = tilePos;
            worldPos = tilePos * TILE_SIZE;
            transform.Position = worldPos*transform.Scale.X;
        }

        public void CreateCollider()
        {
            System.Diagnostics.Debug.WriteLine("COLLIDER 1");
            Transform tf = transform;
            tf.Scale = transform.Scale * TILE_SIZE;
            tf.Scale = new Vector2(tf.Scale.X, tf.Scale.Y); // Removed the +10f to tf.Scale.X
            collider = new BoxCollider(tf, false, false, "Ground");
        }

        public void UpdateCollider()
        {
            collider.Update();
        }

        public BoxCollider GetBoxCollider()
        {
            return collider;
        }
    }
}

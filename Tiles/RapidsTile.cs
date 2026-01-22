using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.Tilemaps;
using System;

namespace PlaguePilgrims.Tiles
{
    /// <summary>
    /// A tile that moves objects in a certain direction
    /// </summary>
    public class ConveyorTile : AnimatedTile
    {
        // TODO : Work out how to handle cardinal directions
        // public int Direction { get; set; }

        public float Speed { get; set; }

        public ConveyorTile() : base() 
        {


        }


        public ConveyorTile(Tileset tileset, TimeSpan frameDuration, params int[] tileIndexes)
            : base(tileset, frameDuration, tileIndexes)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}

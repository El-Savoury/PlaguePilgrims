using Microsoft.Xna.Framework;
using MonogameLibrary.Tilemaps;
using System.Collections.Generic;

namespace PlaguePilgrims.Tiles
{
    public abstract class WeightedTile : Tile
    {
        // Default neighbour tile weights
        public Dictionary<TileType, int> NeighbourWeights = new Dictionary<TileType, int>
        {
            { TileType.Water, 100},
            { TileType.Rock, 3 },
            { TileType.Weeds, 10},
        };


        public WeightedTile() : base()
        {
        }


        public override void Update(GameTime gameTime)
        {

        }
    }
}

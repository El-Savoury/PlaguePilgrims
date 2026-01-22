using Microsoft.Xna.Framework;
using MonogameLibrary.Tilemaps;
namespace PlaguePilgrims.Tiles
{
    /// <summary>
    /// A solid tile 
    /// </summary>
    public class RockTile : WeightedTile
    {
        public RockTile()
        {
            TilesetIndex = 2;

            NeighbourWeights[TileType.Weeds] = 200;
            NeighbourWeights[TileType.Rock] = 50;
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}

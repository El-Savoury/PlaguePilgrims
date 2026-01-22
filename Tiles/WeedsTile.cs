using Microsoft.Xna.Framework;
using MonogameLibrary.Tilemaps;

namespace PlaguePilgrims.Tiles
{
    /// <summary>
    /// A tile that slows the movement of objects 
    /// </summary>
    public class WeedsTile : WeightedTile
    {
        public float Friction { get; set; } 

        public WeedsTile(float frictionAmount) : base()
        {
            TilesetIndex = 3;
            Friction = frictionAmount;
;
            NeighbourWeights[TileType.Rock] = 10;
            NeighbourWeights[TileType.Weeds] = 100;
        }

        public override void Update(GameTime gameTime)
        {
           
        }
    }
}

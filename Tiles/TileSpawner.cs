using MonogameLibrary.Utilities;
using MonogameLibrary.Tilemaps;
using System;
using System.Collections.Generic;

namespace PlaguePilgrims.Tiles
{
    public enum TileType
    {
        Water,
        Rock,
        Rapid,
        Weeds,
        Grass
    }


    public class TileSpawner
    {
        const int MAX_NEIGHBOURS = 5;
        const float DECAY = 0.75f;


        // Default tile weights
        const int WATER_WEIGHT = 80;
        const int ROCK_WEIGHT = 5;
        const int WEEDS_WEIGHT = 10;



        private WeightedPicker<TileType> _weightedPicker = new WeightedPicker<TileType>();


        public TileSpawner()
        {
            _weightedPicker.Add(TileType.Water, WATER_WEIGHT);
            _weightedPicker.Add(TileType.Rock, ROCK_WEIGHT);
            _weightedPicker.Add(TileType.Weeds, WEEDS_WEIGHT);
        }


        public void SetWeights(Dictionary<TileType, int> tileWeights)
        {
            _weightedPicker.Clear();
            foreach (KeyValuePair<TileType, int> weights in tileWeights)
            {
                _weightedPicker.Add(weights.Key, weights.Value);
            }
        }


        // TODO:
        // Check the type of previous tiles. Dynamically weight tile selection based on which tiles 
        // we want to appear more frequently next to the previous tile type.
        // E.g. if previous tile is a conveyor tile, give a higher chance to place another conveyor tile.

        // TODO: 
        // Set a maximum number of tiles and drastically reduce the chance of the same tile
        // type spawning again after this limit is reached.

        // TODO:
        // Apply a contextual modifier value e.g. to change tile type based on distance to the end of the level.

        public Tile SpawnTile(Random random)
        {
            TileType type = _weightedPicker.Pick(random);

            switch (type)
            {
                case TileType.Water:
                    return new WaterTile();

                case TileType.Rock:
                    return new RockTile();

                case TileType.Weeds:
                    return new WeedsTile(3);

                case TileType.Rapid:
                    return new ConveyorTile();

                default:
                    return null;
            }
        }
    }
}

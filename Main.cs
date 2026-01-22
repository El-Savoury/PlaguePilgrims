using MonogameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonogameLibrary.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using MonogameLibrary.Input;
using MonogameLibrary.Maths;
using MonogameLibrary.Utilities;
using System.Net;
using System.Collections.Generic;
using MonogameLibrary.Entities;
using MonogameLibrary.Tilemaps;
using PlaguePilgrims.Tiles;

namespace PlaguePilgrims
{
    public enum TileLayer
    {
        solidLayer,
        baseLayer,
    }



    public class Main : Core
    {
        private const string TITLE = "Plague Pilgrims";
        private const int SCREEN_WIDTH = 640;
        private const int SCREEN_HEIGHT = 480;
        private const bool FULLSCREEN = false;

        Player player;
        Tilemap tilemap;

        public Main() : base(TITLE, SCREEN_WIDTH, SCREEN_HEIGHT, FULLSCREEN)
        {
            // 60 FPS
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0d / 60d);

            // Vsync
            Graphics.SynchronizeWithVerticalRetrace = true;
            Graphics.ApplyChanges();
        }


        protected override void Initialize()
        {
            base.Initialize();

            player = new Player(new Vector2(10, 10), Vector2.Zero);
        }


        protected override void LoadContent()
        {
            base.LoadContent();

            FontManager.I.AddFont("monogram", "Fonts/monogram");

            TextureAtlas atlas = new TextureAtlas(Content.Load<Texture2D>("Textures/tileset"));
            atlas.AddRegion("tiles", 0, 0, atlas.Texture.Width, atlas.Texture.Height);

            int tileWidth = 32;
            int tileHeight = 32;
            int columns = 20;
            int rows = 20;

            Tileset tileset = new Tileset(atlas.GetRegion("tiles"), tileWidth, tileHeight);
            tilemap = new Tilemap(tileset, Vector2.Zero, tileWidth, tileHeight, columns, rows);

            tilemap.AddLayer(TileLayer.baseLayer);
            tilemap.AddLayer(TileLayer.solidLayer);
        }

        private void SpawnTilemap()
        {
            TileSpawner spawner = new TileSpawner();
            Random random = new Random();

            // Always start with a row of water tiles
            //for (int x = 0; x < tilemap.Columns; x++)
            //{
            //    tilemap.SetTile(TileLayer.baseLayer, new WeightedTile(), x, 0);
            //}

            for (int y = 1; y < tilemap.Rows; y++)
            {
                // TODO: If first tile of row, check vertical neighbour tile to determine what this tile should be
                tilemap.SetTile(TileLayer.baseLayer, spawner.SpawnTile(random), 0, y);

                for (int x = 1; x < tilemap.Columns; x++)
                {
                    // Get tile in preceding index to where we are about to place new tile
                    WeightedTile prevTileX = tilemap.GetTile<WeightedTile>(TileLayer.baseLayer.ToString(), x - 1, y);
                    int sameTileCount = 0;

                    // Loop back over previously placed tiles to check if same tile type
                    for (int i = x - 1; i > 0; i--)
                    {
                        // If same tile type keep count of how many of this tile there are
                        if (tilemap.GetTile("baseLayer", i, y) == prevTileX)
                        {
                            sameTileCount++;
                        }
                    }

                    // Otherwise calculate weights for new tile spawner
                    spawner.SetWeights(prevTileX.NeighbourWeights);
                    Tile tile = spawner.SpawnTile(random);
                    tilemap.GetLayer(TileLayer.baseLayer).SetTile(x, y, tile);
                }
            }
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Reset player pos
            if (InputManager.I.KeyboardInput.IsKeyDown(Keys.R))
            {
                player.Position = Vector2.Zero;
            }

            if (InputManager.I.KeyboardInput.IsKeyPressed(Keys.Space))
            {
                SpawnTilemap();
            }

            player.Update(gameTime);
            tilemap.Update(gameTime);


            for (int x = 0; x < tilemap.Columns; x++)
            {
                for (int y = 0; y < tilemap.Rows; y++)
                {
                    float tileX = tilemap.Position.X + x * 32;
                    float tileY = tilemap.Position.Y + y * 32;
                    RectF tileBounds = new RectF(tileX, tileY, 32, 32);


                    if (tilemap.GetLayer(TileLayer.baseLayer).GetTile(x, y) is RockTile && Collision.I.RectVsRect(player.Bounds, tileBounds))
                    {
                        player.OnCollideSolid(tileBounds);
                    }

                }
            }


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            tilemap.Draw(SpriteBatch);
            player.Draw(SpriteBatch);

            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

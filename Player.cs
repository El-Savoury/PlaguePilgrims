using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameLibrary.Entities;
using MonogameLibrary.Input;
using MonogameLibrary.Utilities;

namespace PlaguePilgrims
{
    public class Player : DynamicEntity
    {
        public Player(Vector2 pos, Vector2 vel) : base(pos, vel)
        {
            Width = 32;
            Height = 32;
        }


        public override void LoadContent() { }



        public override void Update(GameTime gameTime)
        {
            float speed = 180f;
            Vector2 moveDir = Vector2.Zero;

            if (InputManager.I.KeyboardInput.IsKeyDown(Keys.A)) { moveDir.X = -1; }
            if (InputManager.I.KeyboardInput.IsKeyDown(Keys.D)) { moveDir.X = 1; }
            if (InputManager.I.KeyboardInput.IsKeyDown(Keys.W)) { moveDir.Y = -1; }
            if (InputManager.I.KeyboardInput.IsKeyDown(Keys.S)) { moveDir.Y = 1; }

            if (moveDir != Vector2.Zero) { moveDir.Normalize(); }

            Velocity = moveDir * speed;
            Position += Velocity * Utility.I.DeltaTime(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            Draw2D.I.DrawRect(spriteBatch, Bounds, Color.Red * 0.6f);
        }       
    }
}

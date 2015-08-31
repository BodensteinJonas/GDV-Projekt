using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace test
{


    public class Game1 : Microsoft.Xna.Framework.Game
    {

        public static Game1 instance;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        Matrix projection;
        MouseState cur_xnaMouse, old_xnaMouse;
        KeyboardState lastKb = Keyboard.GetState();
        public Matrix viewMatrix;
        Vector3 camPos;
        int centerX, centerY;
        Player p;
        public Vector3 moveNearFar;
        public Vector3 moveLeftRight;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            p = new Player(Vector3.Zero);
            instance = this;
            
        }


        protected override void Initialize()
        {
            centerX = GraphicsDevice.Viewport.Width / 2;
            centerY = GraphicsDevice.Viewport.Height / 2;
            
            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            p.loadContent(Content);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), graphics.GraphicsDevice.Viewport.AspectRatio, .1f, 10000f);
            




        }


        protected override void UnloadContent()
        {
            Content.Unload();
            
        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            p.update();
            UpdateControls();

            base.Update(gameTime);
        }

        public void UpdateControls()
        {
            Mouse.SetPosition(centerX, centerY);
            // Get mouse and keyboard
            KeyboardState keyboard = Keyboard.GetState();

            cur_xnaMouse = Mouse.GetState();

            // Var for cam angle
            Vector2 angle = Vector2.Zero;

            // Turnspeed for mouse
            float turnSpeed = 0.4f;

            // Keyboard speed
            float speed = 2.0f;

            
            // Mouse pitch (neigen)
            angle.X += MathHelper.ToRadians((cur_xnaMouse.Y - centerY) * turnSpeed);

            // Mouse yaw (gieren)
            angle.Y += MathHelper.ToRadians((cur_xnaMouse.X - centerX) * turnSpeed);


            //Console.WriteLine("Deg X: " + (cur_xnaMouse.Y - centerY) * turnSpeed + " | Deg Y: " + (cur_xnaMouse.X - centerX) * turnSpeed);
            //Console.WriteLine("Rad X: " + angle.X + " | Rad Y: " + angle.Y);

            // Move to direction we are looking at
            moveNearFar = Vector3.Normalize(new Vector3((float)Math.Sin(-angle.Y) * (float)Math.Cos(angle.X), (float)Math.Sin(angle.X), (float)Math.Cos(-angle.Y) * (float)Math.Cos(angle.X)));
            //Vector3.Normalize(new Vector3((float)Math.Sin(-angle.Y), (float)Math.Sin(angle.X), (float)Math.Cos(-angle.Y)));
            moveLeftRight = Vector3.Normalize(new Vector3((float)Math.Cos(angle.Y), 0f, (float)Math.Sin(angle.Y)));

            if (keyboard.IsKeyDown(Keys.W))
                camPos -= moveNearFar * speed;

            if (keyboard.IsKeyDown(Keys.S))
                camPos += moveNearFar * speed;

            if (keyboard.IsKeyDown(Keys.D))
                camPos += moveLeftRight * speed;

            if (keyboard.IsKeyDown(Keys.A))
                camPos -= moveLeftRight * speed;

            if (keyboard.IsKeyDown(Keys.E))
                camPos += Vector3.Up * speed;

            if (keyboard.IsKeyDown(Keys.Q))
                camPos += Vector3.Down * speed;

            // Set rotation matrix
            Matrix rotMatrix = Matrix.CreateRotationY(angle.Y) * Matrix.CreateRotationX(angle.X); //Matrix.CreateRotationZ(angle.Z)

            // Set view matrix
            viewMatrix = Matrix.Identity * Matrix.CreateTranslation(-camPos) * rotMatrix;
            
            

            lastKb = Keyboard.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            p.draw(projection);

            base.Draw(gameTime);
        }
    }
}

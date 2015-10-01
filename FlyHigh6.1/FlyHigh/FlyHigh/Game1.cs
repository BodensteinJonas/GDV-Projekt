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

namespace FlyHigh
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public int Highscore;

        public static Game1 instance;
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public GameTimer timer;
        public SpriteFont font;

        // Controls
        public MouseState lastMouseState, mouse;
        public KeyboardState lastKb;

        // Screen center
        int centerX, centerY;

        // Player object
        public Flugzeug player;

        // Raum Objekte
        public Raum room;

        // Camera ------------------------
        public Vector3 camPos;
        // Var for cam angle by mouse input
        public Vector2 angle = Vector2.Zero;
        public Vector3 moveNearFar;
        public Vector3 moveLeftRight;
        public Matrix viewMatrix, projectionMatrix, cameraRotationMatrix;

        public Quaternion qCamRotation = Quaternion.Identity;
        public Vector3 rollLeftRight;
        public Vector3 CamPosition;


        /// <summary>
        /// Define active camera (view matrix).
        /// - FPV: First person view
        /// - TPV: Third person view
        /// - SV : Static view 
        /// Note: Chance style with F1-Key
        /// </summary>
        public enum CameraStyle { FPV, TPV, SV };
        public CameraStyle cameraStyle = CameraStyle.TPV;

        public enum GameState { startMenue, ingame, pause, gameSettings, gameover, win };
        public GameState gameState = GameState.startMenue;

        public Sounds sound;

        public SchussManager schussManager;
        public ScheibenManager scheibenManager;
        public IntersectionManager intersectionManager;

        //Modelauswahl
        public int model;

        // Menues
        Menue startMenue;
        Settings settingMenue;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;

            // Global game instance
            instance = this;

            // Game settings
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            // Control settings
            lastMouseState = Mouse.GetState();
            Mouse.SetPosition(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        }

        protected override void Initialize()
        {
            // Screen center
            centerX = GraphicsDevice.Viewport.Width / 2;
            centerY = GraphicsDevice.Viewport.Height / 2;
            Mouse.SetPosition(centerX, centerY);

            startMenue = new Menue();
            settingMenue = new Settings();
            sound = new Sounds();

        //    room = new Raum(this);
            player = new Flugzeug(this);
            Console.WriteLine(player.playerPosition);

            // Init projection 
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45f), graphics.GraphicsDevice.Viewport.AspectRatio, .1f, 10000f);

            camPos = Vector3.Zero;

            Console.WriteLine("Menü Time: " + settingMenue.time);;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            //timer.loadContent(Content);
            player.loadContent(Content);
        }


        protected override void UnloadContent()
        {
            Content.Unload();
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // Switch gamestates
            switch (gameState)
            {
                case GameState.startMenue:
                    // Update Menue
                    sound.playStartmenueTrack();

                    startMenue.updateStartMenue(gameTime);
                    IsMouseVisible = true;
                    break;

<<<<<<< HEAD
                case GameState.ingame:
                    if (model == 1)
                        sound.playInGameTrackFlieger();
                    else
                        sound.playInGameTrackSpace();
=======
                case GameState.ingame:                

>>>>>>> Final
                    player.update();
                    IsMouseVisible = false;

                    if (Keyboard.GetState().IsKeyDown(Keys.F1) && lastKb.IsKeyUp(Keys.F1) && cameraStyle == CameraStyle.TPV)
                        cameraStyle = CameraStyle.FPV;
                    else if (Keyboard.GetState().IsKeyDown(Keys.F1) && lastKb.IsKeyUp(Keys.F1) && cameraStyle == CameraStyle.FPV)
                        cameraStyle = CameraStyle.SV;
                    else if (Keyboard.GetState().IsKeyDown(Keys.F1) && lastKb.IsKeyUp(Keys.F1) && cameraStyle == CameraStyle.SV)
                        cameraStyle = CameraStyle.TPV;

                    if (cameraStyle == CameraStyle.TPV)
                        UpdateCameraThirdPerson();
                    if (cameraStyle == CameraStyle.FPV)
                        UpdateCameraFirstPerson();

                    schussManager.update();
                    scheibenManager.update(gameTime);
                    intersectionManager.update();

                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                         Game1.instance.gameState = Game1.GameState.pause;
                    }

                    // Win Bedingung

                    if (scheibenManager.scheibenListe.Count == 0)
                    {
                        Game1.instance.gameState = Game1.GameState.win;
                    }

                    timer.Update(gameTime);
                   
                    break;

                case GameState.pause:
                    // pausemenu updaten
                    startMenue.updatePauseMenue();
                    IsMouseVisible = true;

                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        Game1.instance.gameState = Game1.GameState.ingame;
                    }
                    break;
                case GameState.gameover:
                   
                    Game1.instance.sound.playGameover();
                    startMenue.updateGameover();
                    IsMouseVisible = true;
                    break;

                case GameState.win:
                    startMenue.updateGameover();
                    break;
            }

            lastKb = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PowderBlue);

            switch (gameState)
            {
                case GameState.startMenue:
                    // Draw Menue
                    startMenue.drawStartMenue(spriteBatch);
                    break;

                case GameState.gameSettings:
                    sound.playStartmenueTrack();
                    settingMenue.update();
                    settingMenue.draw(spriteBatch);
                    break;

                case GameState.ingame:
                    // Draw ingame
                    room.Draw(gameTime);
                    schussManager.draw();
                    scheibenManager.draw(gameTime);
                    player.draw();
                    timer.Started = true;
                    timer.Draw(spriteBatch);
                    break;

                case GameState.pause:
                    // pausemenu draw
                    startMenue.drawPauseMenue(spriteBatch);
                    timer.Draw(spriteBatch);
                    break;

                case GameState.gameover:
                    startMenue.drawGameover(spriteBatch);
                    break;

                case GameState.win:
                    startMenue.drawGameover(spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }

        #region Controls
        private void UpdateCameraThirdPerson()
        {
            // Set camera offset and transform it with player rotation
            CamPosition = new Vector3(0, 0.2f, -1);
            CamPosition = Vector3.Transform(CamPosition, Matrix.CreateFromQuaternion(player.qPlayerRotation));

            // Add player position 
            CamPosition += player.playerPosition;

            // Look at player position
            Vector3 lookAt = player.playerPosition;

            // Define up vector and transform it with player rotation
            Vector3 up = new Vector3(0, 1, 0);
            up = Vector3.Transform(up, Matrix.CreateFromQuaternion(player.qPlayerRotation));

            // Define oculus rotation matrix
            //Matrix oculusRot = Matrix.CreateFromQuaternion(OculusRift.Oculus.OculusClient.GetPredictedOrientation());

            // Set look at
            viewMatrix = Matrix.CreateLookAt(CamPosition, lookAt, up); // * oculusRot;

        }

        private void UpdateCameraFirstPerson()
        {
            CamPosition = new Vector3(0, 0, .15f);
            CamPosition = Vector3.Transform(CamPosition, Matrix.CreateFromQuaternion(player.qPlayerRotation));
            CamPosition += player.playerPosition;
            Vector3 lookAtOffset = new Vector3(0, 0, 1);
            lookAtOffset = Vector3.Transform(lookAtOffset, Matrix.CreateFromQuaternion(player.qPlayerRotation));
            Vector3 lookAt = player.playerPosition + lookAtOffset;
            Vector3 up = new Vector3(0, 1, 0);
            up = Vector3.Transform(up, Matrix.CreateFromQuaternion(player.qPlayerRotation));
            viewMatrix = Matrix.CreateLookAt(CamPosition, lookAt, up) * Matrix.CreateTranslation(new Vector3(0, 0, 0));
        }

        public void UpdateControls()
        {
            // Turnspeed for mouse
            float turnSpeed = 0.2f;

            // Keyboard speed
            float speed = 0.1f;

            // Get mouse and keyboard
            KeyboardState keyboard = Keyboard.GetState();

            // Diese oder vorherige Mauspos holen
            mouse = Mouse.GetState();

            // Vorherige Mausposition speichern
            // Vector2 oldMousePos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

            // maus auf bildschirmmitte zentrieren
            Mouse.SetPosition(centerX, centerY);

            // Mouse pitch (neigen)
            angle.X += MathHelper.ToRadians((mouse.Y - centerY) * turnSpeed);

            // Mouse yaw (gieren)
            angle.Y += MathHelper.ToRadians((mouse.X - centerX) * turnSpeed);

            //Console.WriteLine("Deg X: " + (oldMousePos.Y - centerY) * turnSpeed + " | Deg Y: " + (oldMousePos.X - centerX) * turnSpeed);
            //Console.WriteLine("Rad X: " + angle.X + " | Rad Y: " + angle.Y);

            // Move to direction we are looking at
            moveNearFar = Vector3.Normalize(new Vector3((float)Math.Sin(-angle.Y) * (float)Math.Cos(angle.X), (float)Math.Sin(angle.X), (float)Math.Cos(-angle.Y) * (float)Math.Cos(angle.X)));
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

 

            // Keyevent wird nur einmalig ausgeführt
            if (keyboard.IsKeyDown(Keys.F1) && lastKb.IsKeyUp(Keys.F1) && cameraStyle == CameraStyle.TPV)
                cameraStyle = CameraStyle.FPV;
            else if (keyboard.IsKeyDown(Keys.F1) && lastKb.IsKeyUp(Keys.F1) && cameraStyle == CameraStyle.FPV)
                cameraStyle = CameraStyle.SV;
            else if (keyboard.IsKeyDown(Keys.F1) && lastKb.IsKeyUp(Keys.F1) && cameraStyle == CameraStyle.SV)
                cameraStyle = CameraStyle.TPV;

            // Set view matrix third person view
            if (cameraStyle == CameraStyle.TPV)
            {
                // Set rotation and view matrix
                cameraRotationMatrix = Matrix.Identity * Matrix.CreateRotationY(angle.Y);
                viewMatrix = Matrix.Identity * Matrix.CreateTranslation(-camPos) * cameraRotationMatrix; // * Matrix.Invert(Matrix.CreateTranslation(Vector3.Transform(offset, cameraRotationMatrix)));
                IsMouseVisible = true;
            }

            // Set view matrix for debug first person view
            if (cameraStyle == CameraStyle.FPV)
            {
                // Set rotation and view matrix
                cameraRotationMatrix = Matrix.Identity * Matrix.CreateRotationY(angle.Y) * Matrix.CreateRotationX(angle.X);
                viewMatrix = Matrix.Identity * Matrix.CreateTranslation(-camPos) * cameraRotationMatrix;
                IsMouseVisible = true;
            }

            // Set view matrix for static view 
            if (cameraStyle == CameraStyle.SV)
            {
                Vector3 staticViewPosition = new Vector3(0.0f, 0.0f, -250.0f);
                cameraRotationMatrix = Matrix.Identity * Matrix.CreateRotationY(angle.Y);
                viewMatrix = Matrix.CreateLookAt(staticViewPosition, camPos, Vector3.Up);
                IsMouseVisible = false;
            }

            // if (mouse.LeftButton == ButtonState.Pressed)
            if (keyboard.IsKeyDown(Keys.P))
            //gameState = GameState.ingame;


            // Alter keyboardstate muss aktualisiert werden -> für einmaliges Keyevent 
            lastKb = Keyboard.GetState();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace FlyHigh
{
    public class GameTimer : GameComponent
    {
        private SpriteFont font;
        private String text;
        public float time;

        private Vector2 position;
        private bool started;
        private bool paused;
        private bool finished;

        public GameTimer(Game game, float startTime)
            : base(game)
        {
            time = startTime * 60;
            started = false;
            paused = false;
            finished = false;
            Text = "";
        }

        public void loadContent(ContentManager c)
        {
            font = c.Load<SpriteFont>("font");
        }

        #region Properties

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        public bool Started
        {
            get { return started; }
            set { started = value; }

        }

        public bool Paused
        {
            get { return paused; }
            set { paused = value; }
        }

        public bool Finished
        {
            get { return finished; }
            set { finished = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion

        public override void Update(GameTime gameTime)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (started)
            {
                if (!paused)
                {
                    if (time > 0)
                        time -= deltaTime;
                    else
                        //   finished = true;
                        Game1.instance.gameState = Game1.GameState.startMenue;  // wechsel in win screen

                }

            }

            Text = time.ToString("0");

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, position, Color.Red);
            spriteBatch.End();
        }

        public void updateTime(float t)
        {
            time = t * 60;
        }
    }
}

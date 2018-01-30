using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ALIQAssignment4
{
    /// <summary>
    /// The bat class
    /// </summary>
    public class Bat : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        public Texture2D tex;
        public Vector2 position;
        private Vector2 speed;
        private Vector2 stage;
        public Input input;

        /// <summary>
        /// The constructor for the bat class
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="input"></param>
        /// <param name="speed"></param>
        /// <param name="stage"></param>
        public Bat(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Input input,Vector2 speed, Vector2 stage) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            this.input = input;
        }
        /// <summary>
        /// Updates the bats' positions
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();


            if (ks.IsKeyDown(input.up))
            {
                position -= speed;
            }

            if (ks.IsKeyDown(input.down))
            {
                position += speed;
            }

           
            //checks for the lower boundry and makes sure that bat should not cross that
            if (position.Y + tex.Height > stage.Y)
            {
                position.Y = stage.Y - tex.Height;
            }

            //checks that bat should not cross the upper boundry
            if (position.Y < 0)
            {
                position.Y = 0;
            }

            base.Update(gameTime);
        }
        /// <summary>
        /// Drawing the bats
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex,position,Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// To get bound of the bat to indicate the collision with the ball.
        /// </summary>
        /// <returns>The rectangle coordination of the bats</returns>
        public Rectangle getBound()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}

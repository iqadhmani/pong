using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace ALIQAssignment4
{
    /// <summary>
    /// The pong ball Class
    /// </summary>
    public class Ball : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D tex;
        public Vector2 position;
        public Vector2 speed;
        private Vector2 stage;
        private SoundEffect hitSound;
        private SoundEffect dingSound;

        public int playerOneScore;
        public int playerTwoScore;

        public bool scored = true;



        /// <summary>
        /// The Ball Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="stage"></param>
        /// <param name="hitSound"></param>
        /// <param name="dingSound"></param>
        /// <param name="playerOneScore"></param>
        /// <param name="playerTwoScore"></param>
        public Ball(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed, Vector2 stage, SoundEffect hitSound, SoundEffect dingSound, int playerOneScore, int playerTwoScore) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            this.hitSound = hitSound;
            this.dingSound = dingSound;
            this.playerOneScore = playerOneScore;
            this.playerTwoScore = playerTwoScore;
        }

        /// <summary>
        /// Updates for the Ball class
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position += speed;

            //top wall
            if (position.Y <= 0)
            {
                speed.Y = Math.Abs(speed.Y);
                hitSound.Play();
            }

            //right wall
            if (position.X >= stage.X)
            {
              
                dingSound.Play();
                this.Enabled = false;
                scored = true;
                ReturnScore(playerOneScore++);
            }

            //left wall
            if (position.X + tex.Width <= 0)
            {
                
                dingSound.Play();
                this.Enabled = false;
                scored = true;
                ReturnScore(playerTwoScore++);
            }

            //bottom wall

            if (position.Y + tex.Height >= stage.Y)
            {
                speed.Y = -Math.Abs(speed.Y);
                hitSound.Play();
            }
           
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the ball
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Get bound of the ball to indicate if it collided with the bat
        /// </summary>
        /// <returns>The rectangle coordination for the ball</returns>
        public Rectangle getBound()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }

        /// <summary>
        /// A public method to pass the score to another class
        /// </summary>
        /// <param name="score">Player1 or Player2 score</param>
        /// <returns>The score that was passed</returns>
        public int ReturnScore(int score)
        {
            return score;
        }
    }
}

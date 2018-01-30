using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ALIQAssignment4
{
    /// <summary>
    /// The Score class
    /// </summary>
    public class Score : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        private SpriteFont font;

        private string player;
        
        private Vector2 position;
        private Color color;
        Ball ball;
        SoundEffect missSound;

        int playerScore;
        public bool gameOver;

        const int WINNING_SCORE = 2;


        /// <summary>
        /// The Score Constructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="font"></param>
        /// <param name="player"></param>
        /// <param name="playerScore"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="ball"></param>
        /// <param name="missSound"></param>
        public Score(Game game, SpriteBatch spriteBatch, SpriteFont font, string player, int playerScore, Vector2 position, Color color, Ball ball, SoundEffect missSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.player = player;
         
            this.position = position;
            this.color = color;
            this.font = font;
            this.ball = ball;
            this.playerScore = playerScore;
            this.missSound = missSound;
        }



        /// <summary>
        /// Updates the scores for each player
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (player.Contains("Archana"))
            {
                playerScore = ball.ReturnScore(ball.playerOneScore);
            }
            if (player.Contains("Ibrahim"))
            {
                playerScore = ball.ReturnScore(ball.playerTwoScore);
            }
            if (EndGame(playerScore))
            {
                gameOver = true;
            }
            else
            {
                gameOver = false;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Drawing the scores on screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, player + playerScore, position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// A boolean function to check if end-game occured or not.
        /// </summary>
        /// <param name="score">The score that was passed to compare it with the winning score</param>
        /// <returns>The boolean value of endgame</returns>
        public bool EndGame(int score)
        {
            if (score == WINNING_SCORE)
            {
                if (score == WINNING_SCORE && ball.Visible)
                {
                    missSound.Play();
                }
                ball.Enabled = false;
                ball.Visible = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

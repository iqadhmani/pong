/*  Programmers: Archana Lohani - Ibrahim Qadhmani
 *  Program Name: ALIQAssignment4 (Pong Monogame Game)
 *  Revision History:
 *      Created By:         Archana Lohani - December 8, 2017
 *      Classes:            Archana Lohani - December 10, 2017
 *      Random Speed:       Ibrahim Qadhmani - December 10, 2017
 *      Debugging:          Archana Lohani - December 16, 2017
 *      Score:              Ibrahim Qadhmani - December 24, 2017
 *      Final Debugging:    Ibrahim Qadhmani - December 25, 2017
 *      Project Done:       Archana Lohani - December 26, 2017
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALIQAssignment4
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       
        Bat bat1;
        Bat bat2;
        Vector2 batPos1;
        Vector2 batPos2;
        Texture2D batTex1;
        Texture2D batTex2;

        Texture2D scoreBarTex;
        Vector2 scoreBarPos;

        Score scoreBar1;
        Score scoreBar2;

        int playerOneScore;
        int playerTwoScore;

        Ball ball;
        Vector2 window;
        Vector2 stage;
        Vector2 ballPos;
        Vector2 ballSpeed;
        Texture2D ballTex;
        SpriteFont font;



        /// <summary>
        /// The constructor of the Game1 class
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            window = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            scoreBarTex = this.Content.Load<Texture2D>("Images/Scorebar");
            scoreBarPos = new Vector2(0, window.Y - scoreBarTex.Height);

            stage = new Vector2(graphics.PreferredBackBufferWidth, scoreBarPos.Y);

            font = this.Content.Load<SpriteFont>("Fonts/SpriteFont1");

            

            SoundEffect hitSound = this.Content.Load<SoundEffect>("Music/click");
            SoundEffect missSound = this.Content.Load<SoundEffect>("Music/applause1");
            SoundEffect dingSound = this.Content.Load<SoundEffect>("Music/ding");


            //ball
            ballTex = this.Content.Load<Texture2D>("Images/Ball");
            ballPos = new Vector2(stage.X / 2 - ballTex.Width / 2 , stage.Y / 2 - ballTex.Height / 2);
            ballSpeed = GetSpeed();
            ball = new Ball(this, spriteBatch, ballTex, ballPos, ballSpeed, stage, hitSound, dingSound, playerOneScore, playerTwoScore);
            ball.Enabled = false;
            this.Components.Add(ball);

            scoreBar1 = new Score(this, spriteBatch, font, "Archana Lohani: ", playerOneScore, new Vector2(0, window.Y - (scoreBarTex.Height / 2) - (font.MeasureString($"Archana Lohani: {playerOneScore}").Y / 2)), Color.White, ball, missSound);
            scoreBar2 = new Score(this, spriteBatch, font, "Ibrahim Qadamani: ", playerTwoScore, new Vector2(window.X - font.MeasureString($"Ibrahim Qadhmani: {playerTwoScore}").X, window.Y - (scoreBarTex.Height / 2) - (font.MeasureString($"Ibrahim Qadhmani: {playerTwoScore}").Y / 2)), Color.White, ball, missSound);
            this.Components.Add(scoreBar1);
            this.Components.Add(scoreBar2);


            Vector2 batSpeed = new Vector2(0,4);

            //bat1
            batTex1 = this.Content.Load<Texture2D>("Images/BatLeft");
            batPos1 = new Vector2(10, stage.Y / 2 - batTex1.Height / 2 );
            Input inputBat1 = new Input()
            {              
                up = Keys.A,
                down = Keys.Z,
            };
            
            bat1 = new Bat(this, spriteBatch, batTex1, batPos1, inputBat1, batSpeed, stage);
            this.Components.Add(bat1);

           
            //bat2
            batTex2 = this.Content.Load<Texture2D>("Images/BatRight");                   
            batPos2 = new Vector2(stage.X-10-batTex2.Width,stage.Y/2-batTex2.Height/2);
            Input inputBat2 = new Input()
            {
                up = Keys.Up,
                down = Keys.Down,
            };

            bat2 = new Bat(this, spriteBatch, batTex2, batPos2, inputBat2, batSpeed, stage);         
            this.Components.Add(bat2);

            

            CollisionDetection cd1 = new CollisionDetection(this, bat1, ball,hitSound);
            this.Components.Add(cd1);

            CollisionDetection cd2 = new CollisionDetection(this, bat2, ball, hitSound);
            this.Components.Add(cd2);

        

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
           

            
            batPos2 = new Vector2(stage.X - 10 - batTex2.Width, stage.Y / 2 - batTex2.Height / 2);
            batPos1 = new Vector2(10, stage.Y / 2 - batTex1.Height / 2);

            if (ball.Enabled == false)
            {
                ball.Enabled = true;
                ballPos = new Vector2(stage.X / 2 - ballTex.Width / 2, stage.Y / 2 - ballTex.Height / 2);
                ball.position = ballPos;
                ball.speed = Vector2.Zero;                
            }
            if (ball.scored && Keyboard.GetState().IsKeyDown(Keys.Enter) && scoreBar1.gameOver == false && scoreBar2.gameOver == false)
            {
                ball.scored = false;
                ballSpeed = GetSpeed();
                ball.speed = ballSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && (scoreBar1.gameOver || scoreBar2.gameOver))
            {

                ball.Enabled = false;
                ball.Visible = true;

                ballPos = new Vector2(stage.X / 2 - ballTex.Width / 2, stage.Y / 2 - ballTex.Height / 2);
                ballSpeed = new Vector2(5, -5);

                ball.position = ballPos;
                ball.speed = ballSpeed;
                bat1.position = batPos1;
                bat2.position = batPos2;
                ball.playerOneScore = 0;
                ball.playerTwoScore = 0;

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(scoreBarTex, scoreBarPos, Color.White);
            if (scoreBar1.gameOver || scoreBar2.gameOver)
            {
                if (scoreBar1.gameOver)
                {
                    string gameOverMessage = "Game Over\n"+"Winner is Archana Lohani\n"+"Press Space to Restart!";
                    spriteBatch.DrawString(font, gameOverMessage, new Vector2(stage.X / 2 - font.MeasureString(gameOverMessage).X / 2, stage.Y / 2 - font.MeasureString(gameOverMessage).Y / 2), Color.Red);
                }
                else
                {

                    string gameOverMessage = "Game Over\n" + "Winner is Ibrahim Qadhmani\n" + "Press Space to Restart!";
                    spriteBatch.DrawString(font, gameOverMessage, new Vector2(stage.X / 2 - font.MeasureString(gameOverMessage).X / 2, stage.Y / 2 - font.MeasureString(gameOverMessage).Y / 2), Color.Red);
                }
               // string gameOverMessage = "Game Over. Press Space to Restart!";
               // spriteBatch.DrawString(font, gameOverMessage, new Vector2(stage.X / 2 - font.MeasureString(gameOverMessage).X / 2, stage.Y /2 - font.MeasureString(gameOverMessage).Y / 2), Color.Red);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// This function generate a random speed for the ball.
        /// </summary>
        /// <returns>The random generated speed.</returns>
        public Vector2 GetSpeed()
        {
            Random random = new Random();
            int magnitudeX = random.Next(3, 9);
            int magnitudeY = random.Next(3, 9);
            int signX = random.Next(0, 2);
            int signY = random.Next(0, 2);

            if (signX == 1)
            {
                magnitudeX = -magnitudeX;
            }

            if (signY == 1)
            {
                magnitudeY = -magnitudeY;
            }

            return new Vector2(magnitudeX, magnitudeY);
        }

    }
}

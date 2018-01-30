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
    /// The CollisionDetection class inheriting from GameComponent
    /// </summary>
    public class CollisionDetection : GameComponent
    {
        private Ball ball;
        private Bat bat;
        private SoundEffect hitSound;

        /// <summary>
        /// The constructor of the CollisionDetection class
        /// </summary>
        /// <param name="game"></param>
        /// <param name="bat"></param>
        /// <param name="ball"></param>
        /// <param name="hitSound"></param>
        public CollisionDetection(Game game, Bat bat, Ball ball, SoundEffect hitSound) : base(game)
        {
            this.bat = bat;
            this.ball = ball;
            this.hitSound = hitSound;
        }

        /// <summary>
        /// Updates to check the collision
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            if (ball.getBound().Intersects(bat.getBound()))
            {

                if (ball.speed.X > 0)
                {
                    ball.speed.X = -Math.Abs(ball.speed.X);
                }
                else if (ball.speed.X < 0)
                {
                    ball.speed.X = Math.Abs(ball.speed.X);
                }
                hitSound.Play();
            }
            base.Update(gameTime);
        }       


    }
}

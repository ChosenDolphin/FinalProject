using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D circleSprite;
        Texture2D groundSprite;
        Texture2D trailSprite;

        SpriteFont text;

        Rectangle spriteRect;
        Vector2 spriteOrg;

        private double timer;

        private double x, y;
        private double imgAngle, moveAngle;
        private double speed;
        private double dx, dy;
        private double friction = .75;
        const int width = 1200;
        const int height = 1000;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            circleSprite = Content.Load<Texture2D>("CircleGuy");
            groundSprite = Content.Load<Texture2D>("FlagsMid");
            trailSprite = Content.Load<Texture2D>("Trail");
            text = Content.Load<SpriteFont>("timer");

            spriteOrg = new Vector2(circleSprite.Width / 2, circleSprite.Height / 2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);

            //Rectangle spriteRect = new Rectangle((int)x, (int)y, circleSprite.Width, circleSprite.Height );
            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            checkKeys();
            move();
            calcSpeedAngle();
            applyFriction();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //_spriteBatch.Draw(circleSprite, new Vector2((float)x, (float)y), Color.White);
            _spriteBatch.Draw(circleSprite, new Vector2((float)x, (float)y), null, Color.White, (float)moveAngle, spriteOrg, 1f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(text, speed.ToString(), new Vector2(0, 0), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void checkKeys()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if(Keyboard.GetState().IsKeyDown(Keys.LeftShift) && timer > 750)
                {
                    dx += 60;
                    timer = 0;
                }
                else
                {
                    dx += 5;
                }
                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && timer > 750)
                {
                    dx -= 60;
                    timer = 0;
                }
                else
                {
                    dx -= 5;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && timer > 750)
                {
                    dy -= 60;
                    timer = 0;
                }
                else
                {
                    dy -= 5;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && timer > 750)
                {
                    dy += 60;
                    timer = 0;
                }
                else
                {
                    dy += 5;
                }
            }
        }

        public void move()
        {
            x += dx;
            if (x > width)
            {
                x = 0;
            }
            if (x < 0)
            {
                x = width;
            }

            y += dy;
            if (y > height)
            {
                y = 0;
            }
            if (y < 0)
            {
                y = height;
            }

        }

        public void applyFriction()
        {
            /*
            if (dx > 0 && dx > .5)
            {
                dx -= friction;
            }
            else if (dx < 0 && dx < .5)
            {
                dx += friction;
            }
            else
            {
                dx = 0;
            }

            if (dy > 0 && dy > .5)
            {
                dy -= friction;
            }
            else if (dy < 0 && dy < .5)
            {
                dy += friction;
            }
            else
            {
                dy = 0;
            }
            */
            speed = speed * friction;
            calcVector();
        }

        public void setX(double newX)
        { x = newX; }
        public void setY(double newY)
        { y = newY; }
        //something
        public void setDX(double newDX)
        { dx = newDX; }
        public void setDY(double newDY)
        { dy = newDY; }
        public void changeX(double tempDX)
        { x += tempDX; }
        public void changeY(double tempDY)
        { y += tempDY; }
        //hide and show for future
        public void calcVector()
        {
            dx = speed * Math.Cos(moveAngle);
            dy = speed * Math.Sin(moveAngle);
        }

        public void calcSpeedAngle()
        {
            speed = Math.Sqrt((dx * dx) + (dy * dy));
            moveAngle = Math.Atan2(dy, dx);
            imgAngle = Math.Atan2(dy, dx);
        }

        /*
        public void setSpeed(double speed)
        {
            this.speed = speed;
            calcVector();
        }

        public double getSpeed()
        {
            speed = Math.Sqrt((dx * dx) + (dy * dy));
            return speed;
        }

        public void changeSpeed(double amt)
        {
            speed += amt;
            calcVector();
        }
        public double getImgAngle()
        {
            return (imgAngle * 180.0 / Math.PI) + 90;
        }

        public void setImgAngle(double degrees)
        {
            degrees = degrees - 90;
            imgAngle = degrees * Math.PI / 180;
        }

        public void changeImgAngle(double degrees)
        {
            double radians = degrees * Math.PI / 180;
            imgAngle += radians;
        }

        public void setMoveAngle(double degrees)
        {
            degrees = degrees - 90;
            moveAngle = degrees * Math.PI / 180;
            //calcVector();
        }

        public void changeMoveAngle(double degrees)
        {
            double radian = degrees * Math.PI / 180;
            moveAngle += radian;
            calcVector();
        }
        public void setAngle(double degrees)
        {
            setImgAngle(degrees);
            setMoveAngle(degrees);
        }
        public void changeAngle(double degrees)
        {
            changeImgAngle(degrees);
            changeMoveAngle(degrees);
        }
        */
    }
}

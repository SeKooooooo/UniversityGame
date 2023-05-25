using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.StateGame;
using System.Collections;

namespace Project1
{
    enum Stat{
        SplashScreen,
        Game,
        Defeat,
        Win,
        Tutorial,
        Mode,
    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Stat state=Stat.SplashScreen;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen=true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SplashScreen.Backgroung = Content.Load<Texture2D>("Background");
            SplashScreen.Play =Content.Load<Texture2D>("but-play");
            SplashScreen.Tutorial = Content.Load<Texture2D>("but-tutorial");
            SplashScreen.Exit = Content.Load<Texture2D>("but-exit");
            Tutorial.Backgroung = Content.Load<Texture2D>("tutorial");
            Tutorial.Exit = Content.Load<Texture2D>("but-exit3");
            Mode.Backgroung = Content.Load<Texture2D>("Background");
            Mode.Ordinary = Content.Load<Texture2D>("but-ordinary");
            Mode.Infinity = Content.Load<Texture2D>("but-infinity");
            Defeat.Backgroung = Content.Load<Texture2D>("defeat");
            Defeat.Restart = Content.Load<Texture2D>("but-restart");
            Defeat.Exit = Content.Load<Texture2D>("but-exit2");
            Win.Backgroung = Content.Load<Texture2D>("win");
            Win.Restart = Content.Load<Texture2D>("but-restart2");
            Win.Exit = Content.Load<Texture2D>("but-exit4");
            Nest.Texture2D = Content.Load<Texture2D>("nest");            
            Wave.Texture2D = Content.Load<Texture2D>("Wave");
            Duck.DuckDefault = Content.Load<Texture2D>("duck");
            Duck.Duck1 = Content.Load<Texture2D>("duck_1");
            Duck.Duck2 = Content.Load<Texture2D>("duck_2");
            Duck.DuckDive = Content.Load<Texture2D>("dive");
            Shore.Texture2D = Content.Load<Texture2D>("shores");
            Log.Texture2D=Content.Load<Texture2D>("log");
            Island.Texture2D = Content.Load<Texture2D>("island");        
            Stone.Texture2D = Content.Load<Texture2D>("stone");
            Worm.Texture2D = Content.Load<Texture2D>("worm");
            StateGame.Game.Font = Content.Load<SpriteFont>("GameFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
             switch (state)
            {
                case Stat.SplashScreen:
                    SplashScreen.Update();
                    if (SplashScreen.ButtonPlay.Pointed(Mouse.GetState().X, Mouse.GetState().Y) &&Mouse.GetState().LeftButton==ButtonState.Pressed)  state = Stat.Mode;                  
                    if (SplashScreen.ButtonExit.Pointed(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed) Exit();
                    if (SplashScreen.ButtonTutorial.Pointed(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed) state = Stat.Tutorial;
                    break;                
                case Stat.Game:
                    StateGame.Game.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.W)) StateGame.Game.Duck.Up();
                    if (Keyboard.GetState().IsKeyDown(Keys.S)) StateGame.Game.Duck.Down();
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !Duck.PushSpace) {Duck.Dive=true; Duck.PushSpace = true;}
                    if (Keyboard.GetState().IsKeyUp(Keys.Space)) Duck.PushSpace = false;
                    if (StateGame.Game.FlagDefeat) state = Stat.Defeat;
                    if (StateGame.Game.FlagWin) state = Stat.Win;
                    break;
                case Stat.Defeat:
                    Defeat.Update();
                    if (Defeat.ButtonRestart.Pointed(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed){ StateGame.Game.Init(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight); state = Stat.Game; }
                    if (Defeat.ButtonExit.Pointed(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed) state = Stat.SplashScreen;
                    break;
                case Stat.Win:
                    Win.Update();
                    if (Win.ButtonRestart.Pointed(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed) { StateGame.Game.Init(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight); state = Stat.Game; }
                    if (Win.ButtonExit.Pointed(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed) state = Stat.SplashScreen;
                    break;
                case Stat.Mode:
                    Mode.Update();
                    if (Mode.ButtonOrdinary.Pointed(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed) 
                    {   
                        state = Stat.Game;
                        StateGame.Game.Init(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                        StateGame.Game.FlagInfinity = false; 
                    }
                    if (Mode.ButtonInfinity.Pointed(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        state = Stat.Game;
                        StateGame.Game.Init(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                        StateGame.Game.FlagInfinity = true;
                    }                     
                    break;
                case Stat.Tutorial:
                    Tutorial.Update();
                    if (Tutorial.ButtonExit.Pointed(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed) state= Stat.SplashScreen;
                    break;
            }            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);
            spriteBatch.Begin();
            switch (state)
            {
                case Stat.SplashScreen :
                    SplashScreen.Draw(spriteBatch);
                    break;
                case Stat.Game:
                    StateGame.Game.Draw();
                    break;
                case Stat.Defeat:
                    Defeat.Draw(spriteBatch);
                    break;
                case Stat.Tutorial:
                    Tutorial.Draw(spriteBatch);
                    break;
                case Stat.Mode:
                    Mode.Draw(spriteBatch);
                    break;
                case Stat.Win:
                    Win.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
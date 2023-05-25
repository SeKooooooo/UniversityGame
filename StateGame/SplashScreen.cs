using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Win32;

namespace Project1.StateGame
{
    class SplashScreen
    {
        public static Texture2D Backgroung;
        public static Texture2D Play;
        public static Texture2D Tutorial;
        public static Texture2D Exit;
        public static Button ButtonPlay { get; private set; } = new Button(new Vector2(140, 230));
        public static Button ButtonTutorial { get; private set; } = new Button(new Vector2(140, 460));
        public static Button ButtonExit { get; private set; } = new Button(new Vector2(140, 690));
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Backgroung, Vector2.Zero, Color.White);
            spriteBatch.Draw(Play, ButtonPlay.Pos, Color.White);
            spriteBatch.Draw(Tutorial, ButtonTutorial.Pos, Color.White);
            spriteBatch.Draw(Exit, ButtonExit.Pos, Color.White);
        }
        public static void Update(){}     
    }
}

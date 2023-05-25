using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Win32;

namespace Project1.StateGame
{
    class Win
    {
        public static Texture2D Backgroung;
        public static Texture2D Restart;
        public static Texture2D Exit;
        public static Button ButtonRestart { get; private set; } = new Button(new Vector2(710, 520));
        public static Button ButtonExit { get; private set; } = new Button(new Vector2(710, 770));
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Backgroung, Vector2.Zero, Color.White);
            spriteBatch.Draw(Restart, ButtonRestart.Pos, Color.White);
            spriteBatch.Draw(Exit, ButtonExit.Pos, Color.White);
        }
        public static void Update()
        {

        }
    }
}

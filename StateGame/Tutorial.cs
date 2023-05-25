using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Win32;

namespace Project1
{
    class Tutorial
    {
        public static Texture2D Backgroung;
        public static Texture2D Exit;
        public static Button ButtonExit { get; private set; } = new Button(new Vector2(710, 870));
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Backgroung, Vector2.Zero, Color.White);
            spriteBatch.Draw(Exit, ButtonExit.Pos, Color.White);
        }
        public static void Update() { }
    }
}

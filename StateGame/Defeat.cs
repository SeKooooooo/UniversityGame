using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1.StateGame
{
    static class Defeat
    {
        public static Texture2D Backgroung;
        public static Texture2D Restart;
        public static Texture2D Exit;
        static int CountWorms=0;
        public static Button ButtonRestart { get;private set; } = new Button(new Vector2(710, 520));
        public static Button ButtonExit { get; private set; } = new Button(new Vector2(710, 770));

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Backgroung, Vector2.Zero, Color.White);
            spriteBatch.Draw(Restart, ButtonRestart.Pos, Color.White);
            spriteBatch.Draw(Exit, ButtonExit.Pos, Color.White);
            if (Game.FlagInfinity)
                spriteBatch.DrawString(Game.Font, "Record " + CountWorms.ToString(), new Vector2(1500, 965), Color.Red);
        }

        public static void Update()
        {
            Game.FlagDefeat = false;
            if (Game.CountWorms > CountWorms)
                CountWorms = Game.CountWorms;
        }
    }
}
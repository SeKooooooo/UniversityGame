using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.StateGame;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Project1
{
    class Wave
    {
        public Vector2 Pos;
        Vector2 Dir;
        private int Line;
        public Vector2 Size { get;private set; } = new Vector2(30, 180);
        public static Texture2D Texture2D;

        public Wave(Vector2 pos, Vector2 dir, int line)
        {
            Pos = pos;
            Dir = dir;
            Line = line;
        }

        public Wave(Vector2 dir)
        {
            Dir = dir;
        }

        public void Update()
        {
            Dir = StateGame.Game.Speed;
            Pos += Dir;
            if (Pos.X < -Texture2D.Width && !StateGame.Game.FlagWin)
            {
                Set();
            }
        }

        private void Set()
        {
            Pos = new Vector2(StateGame.Game.Width, StateGame.Game.Height * (float)(0.225 + 0.2 * Line));
        }

        public void Draw()
        {
            StateGame.Game.SpriteBatch.Draw(Texture2D, Pos, Color.White);
        }
    }
}
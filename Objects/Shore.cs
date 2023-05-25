using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.StateGame;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Project1
{
    class Shore
    {
        public Vector2 Pos;
        Vector2 Dir;
        public static Texture2D Texture2D;

        public Shore(Vector2 pos, Vector2 dir)
        {
            Pos = pos;
            Dir = dir;
        }

        public void Update()
        {
            if (Pos.X <= -Texture2D.Width && !StateGame.Game.FlagWin)
            {
                Set();
            }
            Dir = StateGame.Game.Speed;
            Pos += Dir;
            
        }

        private void Set()
        {
            var shoreOne = StateGame.Game.Shores[0].Pos.X;
            var shoreTwo = StateGame.Game.Shores[1].Pos.X;
            var shoreThree = StateGame.Game.Shores[2].Pos.X;
            if (shoreOne<shoreTwo && shoreTwo<shoreThree)
                Pos = new Vector2(StateGame.Game.Shores[2].Pos.X + Texture2D.Width-1, 0);
            else if (shoreTwo<shoreThree && shoreThree<shoreOne)
                Pos = new Vector2(StateGame.Game.Shores[0].Pos.X + Texture2D.Width-1, 0);
            else if (shoreThree<shoreOne && shoreOne<shoreTwo)
                Pos = new Vector2(StateGame.Game.Shores[1].Pos.X + Texture2D.Width - 1, 0);
        }

        public void Draw()
        {
            StateGame.Game.SpriteBatch.Draw(Texture2D, Pos, Color.White);
        }
    }
}
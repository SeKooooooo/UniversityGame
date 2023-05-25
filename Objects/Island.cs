using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.StateGame;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
namespace Project1
{
    class Island
    {
        public Vector2 Pos;
        Vector2 Dir;
        public static Texture2D Texture2D;
        public Vector2 Size { get;private set; } = new Vector2(200, 200);

        public Island(Vector2 pos, Vector2 dir)
        {
            Pos = pos;
            Dir = dir;
        }

        public void Update()
        {
            Dir = StateGame.Game.Speed;
            Pos += Dir;
            if (Pos.X < -Texture2D.Width && !StateGame.Game.FlagCreateNest)
            {
                Set();
            }
        }

        private void Set()
        {
            var newPos = StateGame.Game.GeneratePos(200);
            while (StateGame.Game.Collision(newPos, Size))
            {
                newPos = StateGame.Game.GeneratePos(200);
            }
            Pos = newPos;
        }

        public void Draw()
        {
            StateGame.Game.SpriteBatch.Draw(Texture2D, Pos, Color.White);
        }

        public bool Kill(Duck duck)
        {
            return duck.IsIntersect(new Rectangle((int)Pos.X + 25, (int)Pos.Y+10, Texture2D.Width-50, Texture2D.Height-15));
        }
    }
}
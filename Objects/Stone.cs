using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.StateGame;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Project1
{
    class Stone 
    {
        public Vector2 Pos;
        Vector2 Dir;
        public Vector2 Size { get;private set; } = new Vector2(292,164);
        public static Texture2D Texture2D;

        public Stone(Vector2 pos, Vector2 dir)
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
            var newPos = StateGame.Game.GeneratePos(164);
            while (StateGame.Game.Collision(newPos, Size))
            {
                newPos = StateGame.Game.GeneratePos(164);
            }          
            Pos=newPos;
        }

        public void Draw()
        {
            StateGame.Game.SpriteBatch.Draw(Texture2D, Pos, Color.White);
        }

        public bool Kill(Duck duck)
        {
            return duck.IsIntersect(new Rectangle((int)Pos.X + 40, (int)Pos.Y + 15, Texture2D.Width - 50, Texture2D.Height - 15));
        }
    }
}
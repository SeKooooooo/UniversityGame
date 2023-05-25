using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.StateGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Nest
    {
        public Vector2 Pos { get; set; }
        Vector2 Dir;
        public static Texture2D Texture2D;

        public Nest(Vector2 pos, Vector2 dir)
        {
            Pos = pos;
            Dir = dir;
        }

        public void Update()
        {
            Dir = StateGame.Game.Speed;
            if (!StateGame.Game.FlagCreateNest)
            {
                CreateNest();
                StateGame.Game.FlagCreateNest = true;
            }
            Pos += Dir;                     
        }

        public void Draw()
        {
            StateGame.Game.SpriteBatch.Draw(Texture2D, Pos, Color.White);
        }

        public bool Win(Duck duck)
        {
            return duck.IsIntersect(new Rectangle((int)Pos.X - 20, (int)Pos.Y, Texture2D.Width, Texture2D.Height));
        }

        private void CreateNest()
        {
            if (StateGame.Game.Shores[1].Pos.X > 0)
                Pos = new Vector2(StateGame.Game.Shores[1].Pos.X + 1920, 0);
            if (StateGame.Game.Shores[0].Pos.X > 0)
                Pos = new Vector2(StateGame.Game.Shores[0].Pos.X + 1920, 0);
        }
    }
}

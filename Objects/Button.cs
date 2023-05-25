using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.StateGame;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Project1
{
    class Button
    {
        public Vector2 Pos { get; private set; }
        readonly Vector2 Size;

        public Button(Vector2 pos)
        {
            Pos = pos;
            Size = new Vector2(540 + Pos.X, 170 + Pos.Y);
        }

        public bool Pointed(float x, float y)
        {
            return x > this.Pos.X && x < this.Size.X && y >=this.Pos.Y && y < this.Size.Y;
        }
    }
}

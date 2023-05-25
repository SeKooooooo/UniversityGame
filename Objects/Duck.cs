using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.StateGame;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Project1
{
    class Duck
    {
        Vector2 Pos;
        public int Speed = 4;
        public static bool Dive=false;
        public static Texture2D DuckDefault;
        public static Texture2D Duck1;
        public static Texture2D Duck2;
        public static Texture2D DuckDive;
        public static Texture2D Texture2D =DuckDefault;
        public static int Length;
        public static bool PushSpace=false;

        public Duck(Vector2 pos, int speed)
        {
            Pos = pos;
            Speed = speed;
        }

        public void Down()
        {
            if (Pos.Y < 735) Pos.Y += Speed;
        } 

        public void Up()
        {         
            if (Pos.Y > 210) Pos.Y -= Speed;
        }

        public void Update()
        {
            if (Dive  )
            {
                Length -= 5;               
                if (Length <=0 )
                {
                    Dive = false;
                    Length = 340;
                }
            }
        }

        public void Draw()
        {
            if (Length==340 || Length==0)
                StateGame.Game.SpriteBatch.Draw(DuckDefault, Pos, Color.White);
            else if (Length >= 330 || Length<=10)
                StateGame.Game.SpriteBatch.Draw(Duck1, Pos, Color.White);
            else if (Length >= 320 || Length<=20)
                StateGame.Game.SpriteBatch.Draw(Duck2, Pos, Color.White);
            else if (Length < 320 && Length > 20)
                StateGame.Game.SpriteBatch.Draw(DuckDive, Pos, Color.White);       
            
        }

        public bool IsIntersect(Rectangle rectangle)
        {
            return rectangle.Intersects(new Rectangle((int)Pos.X, (int)Pos.Y + 80, DuckDefault.Width - 30, DuckDefault.Height - 80)) ||
                rectangle.Intersects(new Rectangle((int)Pos.X, (int)Pos.Y + 80, Duck1.Width, Duck1.Height - 80)) ||
                rectangle.Intersects(new Rectangle((int)Pos.X, (int)Pos.Y + 80, Duck2.Width, Duck2.Height - 80));
        }
    }
}

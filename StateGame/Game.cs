using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Project1.StateGame
{
    class Game
    {
        public static int Width { get;private set; } 
        public static int Height { get;private set; }
        public static SpriteBatch SpriteBatch { get;private set; }
        public static Random Random = new Random();
        public static SpriteFont Font;
        public static Duck Duck { get;private set; }
        static List<Wave> Waves;
        static List<Worm> Worms;
        public static List<Shore> Shores { get; private set; }
        static List<Log> Logs;
        static List<Island> Islands;
        static List<Stone> Stones;
        static Nest Nest;
        public static int CountWorms { get; private set; }
        static int LastCountWorms;
        static int SpeedDuck;
        static Vector2 LastSpeed;
        public static Vector2 Speed { get; private set; }
        public static bool FlagDefeat;
        public static bool FlagWin;
        public static bool FlagInfinity;
        public static bool FlagMaxWorms;
        public static bool FlagCreateNest;
        static Vector2 Confines;
        static int LeftBorderGeneration=5000;


        public static void Init(SpriteBatch SpriteBatch, int Width, int Height)
        {
            Game.Width = Width;
            Game.Height = Height;
            Game.SpriteBatch = SpriteBatch;
            Speed = new Vector2(-5 ,0);
            if (!FlagInfinity) Nest = new Nest(new Vector2(2000, 0), Speed);
            DoWaves();
            SpeedDuck = 4;
            LastSpeed = Speed;
            Duck = new Duck(new Vector2(200, Game.Height / 2),SpeedDuck);
            Duck.Dive = false;
            DoShores();
            DoLogs();
            DoIslands();
            DoStones();
            DoWorms();
            Confines = new Vector2(150, 100);
            CountWorms = 0;
            LastCountWorms = 0;
            FlagWin=false;
            FlagDefeat =false;
            FlagWin = false;
            FlagMaxWorms=false;
            FlagCreateNest = false;
            LeftBorderGeneration = 5000;
            Duck.Length = 340;
        }
        static void DoWorms()
        {
            Worms = new List<Worm>();
            var size = new Vector2(16,68);
            for (var i = 0; i < 10; i++)
            {
                var newPos = GeneratePos(68);
                while (Collision(newPos, size))
                {
                    newPos = GeneratePos(68);
                }
                Worms.Add(new Worm(newPos, Speed));
            }
        }

        static void DoWaves()
        {
            Waves = new List<Wave>();
            for (int i = 0; i < 15; i++)
            {
                var line = i % 3;
                var position = i / 3;
                Waves.Add(new Wave(new Vector2(Width + position * 400, Height * (float)(0.225 + line * 0.2)), Speed, line));
            }

        }

        static void DoLogs()
        {
            Logs = new List<Log> { new Log(new Vector2(2120,210), Speed),
                new Log(new Vector2(2600,410), Speed),
                new Log(new Vector2(3500,220), Speed)};           
        }

        static void DoIslands()
        {
            Islands = new List<Island> { new Island(new Vector2(2800,200), Speed),
                 new Island(new Vector2(3200,400), Speed),
                 new Island(new Vector2(3900,600), Speed)};
        }

        static void DoStones()
        {
            Stones=new List<Stone> { new Stone(new Vector2(2900,700), Speed),
                new Stone(new Vector2(3800,300), Speed),
                new Stone(new Vector2(4200,450), Speed),
            };
        }

        static void DoShores()
        {
            Shores = new List<Shore>();
            Shores.Add(new Shore(new Vector2(0, 0), Speed));
            Shores.Add(new Shore(new Vector2(1920 , 0), Speed));
            Shores.Add( new Shore(new Vector2(3840 , 0), Speed));

        }

        public static void Draw()
        {           
            foreach (Wave wave in Waves)
                wave.Draw();           
            foreach (Island island in Islands)
                island.Draw();
            foreach (Stone stone in Stones)
                stone.Draw();
            foreach (Shore shore in Shores)
                shore.Draw();
            foreach(Worm worm in Worms)
                worm.Draw();           
            if (!Duck.Dive)
            {
                foreach (Log log in Logs)
                    log.Draw();
                Duck.Draw();
            }
            else
            {
                Duck.Draw();
                foreach (Log log in Logs)
                    log.Draw();
            }
            if (!FlagInfinity) Nest.Draw();
            SpriteBatch.DrawString(Font, "Worms " + CountWorms.ToString(), new Vector2(1553, 965),Color.Red);
        }

        public static void Update()
        {
            FlagWin = Nest.Win(Duck);
            if (!FlagInfinity)
            {                   
                if (CountWorms >= 100)
                {
                    Nest.Update();
                    UpdateFinal();            
                }                    
                if (FlagWin)
                {
                    Speed = new Vector2(0, 0);
                }                   
            }
            ChangeGameValues();
            Duck.Update();
            foreach (Wave wave in Waves)
                wave.Update();
            foreach (Shore shore in Shores)
                shore.Update();
            foreach(Worm worm in Worms)
            {
                worm.Update();
                if (worm.Eat(Duck))
                {
                    CountWorms += 1;
                    var newPos = GeneratePos(68);
                    while (Collision(newPos, new Vector2(16,68)))
                    { 
                        newPos = GeneratePos(68);
                    }
                    worm.Pos = newPos;
                }
            }
            foreach(Log log in Logs)
            {
                log.Update();
                if(log.Kill(Duck))
                    FlagDefeat=true;
            }
            foreach (Island island in Islands)
            {
                island.Update();
                if (island.Kill(Duck))
                    FlagDefeat = true;
            }
            foreach (Stone stone in Stones)
            {
                stone.Update();
                if (stone.Kill(Duck))
                    FlagDefeat = true;
            }           
        }

        static void UpdateFinal()
        {
            var nestX = Nest.Pos.X - 200;
            var newPos = new Vector2(-400, 0);
            foreach (Worm worm in Worms)
            {
                if (worm.Pos.X > nestX)
                    worm.Pos = newPos;
            }
            foreach (Log log in Logs)
            {
                if (log.Pos.X > nestX)
                    log.Pos = newPos;
            }
            foreach (Island island in Islands)
            {
                if (island.Pos.X > nestX)
                    island.Pos = newPos;
            }
            foreach(Stone stone in Stones)
            {
                if (stone.Pos.X > nestX)
                    stone.Pos = newPos;
            }
        }
        static void ChangeGameValues()
        {
            if (!FlagInfinity)
            {
                if (CountWorms - LastCountWorms >= 10)
                {
                    var lastSpeed = Speed.X;
                    Speed = new Vector2(lastSpeed - 0.3f, 0);
                    LastCountWorms = CountWorms;
                }
            }
            else
            {
                if (CountWorms - LastCountWorms >= 10)
                {
                    var lastSpeed = Speed.X;
                    Speed = new Vector2(lastSpeed - 0.25f, 0);
                    LeftBorderGeneration += 50;
                    Confines.X += 5;
                    LastCountWorms = CountWorms;
                }
                if ((Speed-LastSpeed).X>2.5)
                {
                    SpeedDuck += 1;
                    LastSpeed = Speed;
                }
            }
        }

        public static bool Collision(Vector2 newPos,Vector2 size)
        {
            var width = (int)size.X;
            var height= (int)size.Y;
            var dX = Confines.X;
            var dY= Confines.Y;
            var newObj = new Rectangle((int)(newPos.X-dX),(int)(newPos.Y-dY), (int)(width +2*dX), (int)(height+2*dY));
            foreach (var obj in Stones)
            {
                if (!new Rectangle(obj.Pos.ToPoint(),obj.Size.ToPoint()).Intersects(newObj))
                    continue;
                return true;
            }
            foreach(var obj in Islands)
            {
                if (!new Rectangle(obj.Pos.ToPoint(), obj.Size.ToPoint()).Intersects(newObj))
                    continue;
                return true;
            }
            foreach (var obj in Logs)
            {
                if (!new Rectangle(obj.Pos.ToPoint(), obj.Size.ToPoint()).Intersects(newObj))
                    continue;
                return true;
            }
            foreach (var obj in Worms)
            {
                if (!new Rectangle(obj.Pos.ToPoint(), obj.Size.ToPoint()).Intersects(new Rectangle((int)newPos.X-50,(int)newPos.Y - 50,width+100, height+100)))
                    continue;
                return true;
            }
            return false; 
        }

        public static Vector2 GeneratePos(float height)
        {
            return new Vector2(Random.Next(1920, LeftBorderGeneration), Random.Next(220, 859 - (int)height));
        }
    }
}
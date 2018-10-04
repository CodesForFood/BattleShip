using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace BattleShip.GameFolder
{
    public class Board
    {
    
        const int WIDTH = 20;
        const int HEIGHT = 10;

        public static Tile[,] PlayerSide;
        public static Tile[,] EnemySide;

        public static List<Tile> AllTiles;
        public static List<Ship> AllShips;

        public static List<Tile> PSideList;
        public static List<Tile> ESideList;

        public static Rectangle FullPlayerSide; //Both 640,320 in size 
        public Rectangle FullEnemySide; //both start at 0,0 in array



        public Board()
        {
            PlayerSide = new Tile[WIDTH, HEIGHT];
            EnemySide = new Tile[WIDTH, HEIGHT];

            AllTiles = new List<Tile>();
            AllShips = new List<Ship>();

            PSideList = new List<Tile>();
            ESideList = new List<Tile>();

            SetUpPlayerSide();
            SetUpEnemySide();
            AddShips();

            FullPlayerSide = new Rectangle(PlayerSide[0, 0].Border.Location, new Point(640, 320));
            FullEnemySide = new Rectangle(EnemySide[0, 0].Border.Location, new Point(640, 320));

        }


        void SetUpPlayerSide()
        {
            for(int y = 0; y < PlayerSide.GetLength(1);y++)
                for(int x = 0; x < PlayerSide.GetLength(0);x++)
                {
                    PlayerSide[x, y] = new Tile()
                    {
                        GridLoc = new Vector2(x, y),
                        TTeam = Team.Player,
                        TColor = Color.Blue,                  
                    };

                    PlayerSide[x, y].SetLocation(300 + (x * Tile.SIZE), 420 + (y * Tile.SIZE));
                    AllTiles.Add(PlayerSide[x, y]);
                    PSideList.Add(PlayerSide[x, y]);

                }
        }

        void SetUpEnemySide()
        {
            for (int y = 0; y < EnemySide.GetLength(1); y++)
                for (int x = 0; x < EnemySide.GetLength(0); x++)
                {
                    EnemySide[x, y] = new Tile()
                    {
                        GridLoc = new Vector2(x, y),
                        TTeam = Team.Enemy,
                        TColor = Color.Blue,
                    };

                    EnemySide[x, y].SetLocation(300 + (x * Tile.SIZE), 80 + (y * Tile.SIZE));
                    AllTiles.Add(EnemySide[x, y]);
                    ESideList.Add(EnemySide[x, y]);
                }
        }

        void AddShips()
        {
            AllShips.Add(new Ship("Battleship", Image.battleShipPic,Team.Player));
            AllShips.Add(new Ship("Carrier", Image.carrierPic, Team.Player));
            AllShips.Add(new Ship("Cruiser", Image.cruiserPic, Team.Player));
           
        }

        public static Tile GetPTile(Vector2 gridLoc) { return PlayerSide[(int)gridLoc.X, (int)gridLoc.Y]; }
        public static Tile GetETile(Vector2 gridLoc) { return EnemySide[(int)gridLoc.X, (int)gridLoc.Y]; }


        public static bool OnPlayerBoard(Point loc)
        {
            if (FullPlayerSide.Contains(loc))
                return true;
            else { return false; }
        }

        public static bool CheckAllDeployed()
        {
            foreach(var ship in AllShips)
            {
                if (!ship.IsDeployed) { return false; }
            }
            return true;
        }

     


        public static void SetShip(Ship ship)
        {
            ship.Loc = Controller.HoverTile.Border.Center.ToVector2();
            ship.Border.Location = ship.OriginWrap; //Controller.HoverTile.Border.Location;
            ship.GridLoc = Controller.HoverTile.GridLoc;
            ship.ClaimTiles();
        }

        

    }
}

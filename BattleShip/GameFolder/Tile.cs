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
    public class Tile
    {
        public const int SIZE = 32;

        public Vector2 GridLoc { get; set; }
        public Team TTeam { get; set; }
        public Color TColor { get; set; }
        public bool HasShip { get; set; } = false;


        public Rectangle Border;

        public Tile()
        {
            Border = new Rectangle()
            {
                Size = new Point(SIZE)
            };          

        }


        

        public void SetLocation(int x, int y) { Border.Location = new Point(x, y); }


    }



}

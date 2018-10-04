using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.GameFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleShip.GameFolder
{
    public class Ship : Tile
    {
        public string Name { get; private set; }
        public Texture2D Picture { get; private set; }
        public float Angle { get; set; } = 0f;   
        public float Scale { get; set; }
        public bool IsSunk { get; set; } = false;
        public bool IsDeployed { get; set; } = false;
        public int Health { get; set; }
       
        public bool IsSideways { get { return Angle == MathHelper.ToRadians(270) || Angle == MathHelper.ToRadians(90); } }

        public Vector2 Loc { get; set; }
        public List<Tile> Positions;
       
        public Point OriginWrap { get { return new Point((int)Loc.X - 16, (int)Loc.Y - 16); } }

        public Vector2 Origin { get { return new Vector2(Picture.Width/ 2, Picture.Height / 2); } }
       


        public Ship(string name, Texture2D pic, Team team)
        {
            Name = name;
            Picture = pic;
            TTeam = team;
            Loc = GetDeployInfo();
            Positions = new List<Tile>();
            Border = new Rectangle(OriginWrap,new Point(32));      
            
          
        }

        ///Put health here, wont duplicate if statements and gets called in constructor
        ///Shouldnt be called after Deployment, as it would reset health.
        public Vector2 GetDeployInfo()
        {
            if (Name == "Battleship")
            {
                Health = 3;
                Scale = .8f;
                return new Vector2(20, 300);               
            }
            else if( Name == "Carrier")
            {
                Health = 3;
                Scale = .9f;
                return new Vector2(60, 300);
            }
            else if(Name == "Cruiser")
            {
                Health = 2;
                Scale = .8f;
                return new Vector2(20, 400);
            }
            else { return Vector2.Zero; }
        }

        Point GetShipSize()
        {
            if(Name == "Battleship") { return new Point(32, 96); }
            else if(Name == "Carrier") { return new Point(32, 96); }
            else { return new Point(32, 96); }
        }      

        public void ClaimTiles()
        {
           
            try
            {
               if(Health == 3) { ClaimTwo(); }
               else if(Health == 2) { ClaimOne(); }

                foreach(var tile in Positions) { tile.HasShip = true; }
                IsDeployed = true;
            }
            catch(Exception ex)           
            {
                Loc = GetDeployInfo();
                Border.Location = OriginWrap;
                Angle = 0f;
                IsDeployed = false;
                Screen.Message = ex.Message;
            }
        }

        /// <summary>
        /// Claims the top and bottom tile or side tiles for ships with 3 health.      
        /// </summary>
        private void ClaimTwo()
        {
            Tile sideBL;
            Tile sideTR;

            if (IsSideways)
            {
                sideBL = Board.PlayerSide[(int)GridLoc.X - 1, (int)GridLoc.Y];
                sideTR = Board.PlayerSide[(int)GridLoc.X + 1, (int)GridLoc.Y];

                Positions.Add(Board.GetPTile(GridLoc));
                Positions.Add(sideBL);
                Positions.Add(sideTR);

                CheckCollision();

                var sides = Rectangle.Union(sideBL.Border, sideTR.Border);
                Border = Rectangle.Union(Border, sides);
            }
            else
            {               
                sideBL = Board.PlayerSide[(int)GridLoc.X, (int)GridLoc.Y - 1];
                sideTR = Board.PlayerSide[(int)GridLoc.X, (int)GridLoc.Y + 1];

                Positions.Add(Board.GetPTile(GridLoc));
                Positions.Add(sideBL);
                Positions.Add(sideTR);

                CheckCollision();

                var sides = Rectangle.Union(sideBL.Border, sideTR.Border);
                Border = Rectangle.Union(Border, sides);
            }             
        }      


        private void ClaimOne()
        {
            Tile extra;

            if (IsSideways)
            {
                if (Angle == MathHelper.ToRadians(90))
                    extra = Board.PlayerSide[(int)GridLoc.X - 1, (int)GridLoc.Y];
                else
                    extra = Board.PlayerSide[(int)GridLoc.X + 1, (int)GridLoc.Y];

                Positions.Add(Board.GetPTile(GridLoc));
                Positions.Add(extra);

                CheckCollision();
                Border = Rectangle.Union(Border, extra.Border);
            }
            else
            {
                if (Angle == MathHelper.ToRadians(180))
                    extra = Board.PlayerSide[(int)GridLoc.X, (int)GridLoc.Y - 1];
                else
                    extra = Board.PlayerSide[(int)GridLoc.X, (int)GridLoc.Y + 1];

                Positions.Add(Board.GetPTile(GridLoc));
                Positions.Add(extra);

                CheckCollision();
                Border = Rectangle.Union(Border, extra.Border);
            }
        }

        /// <summary>
        /// This to Check its collision with other ships while being deployed.
        /// </summary>
        private void CheckCollision()
        {
            foreach (var tile in Positions)
            {
                if (tile.HasShip)
                {
                    Positions.Clear();
                    throw new InvalidOperationException("Collision Detected");
                }
            }
        }

        public void ResetShip()
        {
            Border.Size = new Point(32);
            if(Positions.Count > 0)
            {
                RemoveClaims();
                Positions.Clear();
            }           
        }

        private void RemoveClaims()
        {
            foreach (var tile in Positions) { tile.HasShip = false; }
        }
       
    } 
}

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
    public class Phase
    {
        public Phases phases;
        bool IsHeld { get; set; } = false;

        public Ship HeldShip { get; private set; }

        public static Random rand = new Random();

        public Phase()
        {
            phases = Phases.Deploy;
        }

      
        public void Deploy(Controller c)
        {        
            if(!IsHeld)
            {
                foreach(var ship in Board.AllShips)
                {
                    if (ship.Border.Contains(Controller.mousePos) && c.OnceLeftM())
                    {
                        //Here is when I pick up the ship
                        ship.ResetShip();
                        HeldShip = ship;
                        IsHeld = true;
                    }
                }
            }
            else if(IsHeld && HeldShip != null)
            {
                HeldShip.Loc = Controller.mousePos;             
                c.AllowRotate(HeldShip);            

                if (c.OnceLeftM() && Board.OnPlayerBoard(HeldShip.Loc.ToPoint()))
                {
                    Board.SetShip(HeldShip);
                    IsHeld = false;
                }
                else if(c.OnceLeftM() && !Board.OnPlayerBoard(HeldShip.Loc.ToPoint()))
                {
                    HeldShip.Loc = HeldShip.GetDeployInfo();
                    HeldShip.Border.Location = HeldShip.OriginWrap;
                    HeldShip.Angle = 0f;
                    HeldShip.GridLoc = Vector2.Zero;
                    IsHeld = false;
                }
                       
            }
        }

        public void EndDeploy()
        {
            //check which player tiles have ships,
            //set enemy ship in their place
            //change the phases

            foreach(var tile in Board.PSideList)
            {
                if(tile.HasShip) { tile.TColor = Color.Green; }
                else { tile.TColor = Color.Blue; }
            }

            //here trying to put the ships that collide back, must reset the borders
            if (Board.CheckAllDeployed()) 
            {
                var num = rand.Next(Board.EnemySide.Length);
                Board.ESideList[num].HasShip = true;
                phases = Phases.PlayerTurn;
            }
                   
        }


        public void PlayerTurn(Controller c)
        {
            foreach(var tile in Board.EnemySide)
            {
                if(tile.Border.Contains(Controller.mousePos) && c.OnceLeftM())
                {
                    if(tile.HasShip)
                    {
                        tile.TColor = Color.Red;
                        phases = Phases.EnemyTurn;
                    }
                    else
                    {
                        tile.TColor = Color.White;
                        phases = Phases.EnemyTurn;
                    }
                }
            }
        }

        public void EnemyTurn()
        {
            int num = rand.Next(Board.PSideList.Count);

            if (Board.PSideList[num].TColor == Color.Blue)
            {
                if (Board.PSideList[num].HasShip)
                {
                    Board.PSideList[num].TColor = Color.Red;
                    phases = Phases.PlayerTurn;
                }
                else
                {
                    Board.PSideList[num].TColor = Color.White;
                    phases = Phases.PlayerTurn;
                }
            }
            else { EnemyTurn(); }

        }

        public override string ToString() { return phases.ToString(); }
    }
}

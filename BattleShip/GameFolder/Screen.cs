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
    class Screen
    {
        public static string Message = "Test";
        public static Vector2 ScreenDim = new Vector2(1200,800);

        public static Board mainBoard;
        SpriteBatch batch;    

        public Screen() { }

        public void GetBatch(SpriteBatch b) { batch = b; }
        public void CreateBoard() { mainBoard = new Board(); }

      

        public void ShowDebug(Phase p)
        {
            batch.DrawString(Image.font, Message, new Vector2(0, Screen.ScreenDim.Y - 25), Color.Black);
            batch.DrawString(Image.font, Controller.mouseString, new Vector2(0, ScreenDim.Y - 50), Color.Black);

            if(Controller.HoverTile != null)
            {
                batch.DrawString(Image.font, "Grid Loc: " + Controller.HoverTile.GridLoc.ToString(), new Vector2(0, 0), Color.Black);
                batch.DrawString(Image.font, "Team: " + Controller.HoverTile.TTeam, new Vector2(0, 21), Color.Black);
                batch.DrawString(Image.font, "Has Ship: " + Controller.HoverTile.HasShip, new Vector2(0, 42), Color.Black);
                batch.DrawString(Image.font, "Phase: " + p.phases, new Vector2(300, 42), Color.Black);
                if(p.HeldShip != null)
                {
                    batch.DrawString(Image.font, "Box: " + p.HeldShip.Border, new Vector2(0, 63), Color.Black);
                    batch.DrawString(Image.font, "Angle: " + p.HeldShip.Angle, new Vector2(0, 84), Color.Black);
                    batch.DrawString(Image.font, "IsSideWays: " + p.HeldShip.IsSideways, new Vector2(300, 0), Color.Black);
                    batch.DrawString(Image.font, "Ship Grid: " + p.HeldShip.GridLoc, new Vector2(300, 21), Color.Black);
                }
               
            }               
          
           
        }

        public void ShowBoard()
        {
            foreach(var tile in Board.AllTiles)
            {
                batch.Draw(Image.blankBox, tile.Border, tile.TColor);
            }

        }

        public void ShowShips()
        {
            foreach(var ship in Board.AllShips)
            {
                //try to set sprite by vector2 position then draw seperate rect for collision that follows sprite through rotations
               
                batch.Draw(ship.Picture, ship.Loc, null,
                    Color.White, ship.Angle, ship.Origin, ship.Scale, SpriteEffects.None, 0);
            }
            
        }

        public void ShowButtons()
        {
            foreach(var btn in GUI.Button.BtnList)
            {
                batch.Draw(Image.blankBox, btn.Border, Color.Red);
                batch.DrawString(Image.font, btn.Text, btn.TextLoc, Color.Black);
            }

        }
    }
}

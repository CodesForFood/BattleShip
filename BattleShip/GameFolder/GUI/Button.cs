using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BattleShip.GameFolder.GUI
{
    public class Button
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public Rectangle Border { get; }                  
        public Vector2 TextLoc;

        public static List<Button> BtnList = new List<Button>();

        public Button(int id, Point loc, string text)
        {
            Id = id;
            Border = new Rectangle(loc, new Point(150, 40));
            Text = text;
            TextLoc = new Vector2(Border.X + 5, Border.Y + 5);

            BtnList.Add(this);
        }
            
        public void Clicked(Phase p)
        {
            if (Id == (int)Phases.Deploy) { p.EndDeploy(); }
            else if (Id == 10) { Screen.mainBoard = new Board(); p.phases = Phases.Deploy; }

        }


    }
}

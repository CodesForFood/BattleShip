using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BattleShip.GameFolder.GUI;

namespace BattleShip.GameFolder
{
    public class Controller
    {
        KeyboardState kState, oldKState;
        MouseState mState, oldMState;

        Game game;

        static public Vector2 mousePos;
        static public string mouseString;

        public static Tile HoverTile { get; set; }
        public Phase oPhase;

        Button PhaseBtn;
        Button resetBtn;

        public Controller(Game g)
        {
            game = g;
            UpdateState();
            UpdateOldState();
            oPhase = new Phase();
            PhaseBtn = new Button((int)oPhase.phases, new Point(1000, 200), oPhase.ToString());
            resetBtn = new Button(10, new Point(1000, 250), "Reset");
        }

        bool OnceK(Keys k) { return kState.IsKeyDown(k) && !oldKState.IsKeyDown(k); }//Press button one at time
        bool HeldK(Keys k) { return kState.IsKeyDown(k); }//Hold button for slide

        public bool OnceLeftM() { return mState.LeftButton == ButtonState.Pressed && oldMState.LeftButton != ButtonState.Pressed; }
        bool OnceRightM() { return mState.RightButton == ButtonState.Pressed && oldMState.RightButton != ButtonState.Pressed; }

        bool HeldLeftM() { return mState.LeftButton == ButtonState.Pressed; }
        bool HeldRightM() { return mState.RightButton == ButtonState.Pressed; }

        public static bool PressM(ButtonState bs) { return bs == ButtonState.Pressed; }


        public void UpdateState()
        {
            kState = Keyboard.GetState();
            mState = Mouse.GetState();

            mousePos.X = mState.X;
            mousePos.Y = mState.Y;
            mouseString = mousePos.ToString();
        }

        public void UpdateOldState()
        {
            oldKState = Keyboard.GetState();
            oldMState = Mouse.GetState();
        }

        public void CheckButtonClicks()
        {       
            foreach(var btn in Button.BtnList)
            {
                if (btn.Border.Contains(mousePos) && OnceLeftM())
                {
                    btn.Clicked(oPhase);
                }
            }
                  
        }

        public void HoveredTile()
        {
            foreach (var tile in Board.AllTiles)
            {
                if (tile.Border.Contains(mousePos)) { HoverTile = tile; }
            }
        }


        public void RunPhase()
        {
            if(oPhase.phases == Phases.Deploy)
            {
                oPhase.Deploy(this);
            }
            else if(oPhase.phases == Phases.PlayerTurn)
            {
                oPhase.PlayerTurn(this);
            }
            else if(oPhase.phases == Phases.EnemyTurn)
            {
                oPhase.EnemyTurn();
            }
        }

        /// <summary>
        /// Adds 90 degrees to the held ship every press of 'R'
        /// </summary>
        /// <param name="s">The ship thats held</param>
        public void AllowRotate(Ship s)
        {
            if (OnceK(Keys.R)) { s.Angle += (float)Math.PI / 2.0f; }
            if(s.Angle == ((float)Math.PI * 2.0f)) { s.Angle = 0f; }                       
        }

    }
}

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
    static class Image
    {

        public static SpriteFont font;
        public static Texture2D blankBox;
        public static Texture2D shipPic;
        public static Texture2D battleShipPic;
        public static Texture2D carrierPic;
        public static Texture2D cruiserPic;
        public static Texture2D destroyerPic;
        public static Texture2D rescuePic;

        public static void LoadTextures(Game g)
        {
            font = g.Content.Load<SpriteFont>("Font");
            blankBox = g.Content.Load<Texture2D>("BlankBox");
            shipPic = g.Content.Load<Texture2D>("ShipSprites");

            battleShipPic = g.Content.Load<Texture2D>("Ships/ShipBattleshipHull");
            carrierPic = g.Content.Load<Texture2D>("Ships/ShipCarrierHull");
            cruiserPic = g.Content.Load<Texture2D>("Ships/ShipCruiserHull");
            destroyerPic = g.Content.Load<Texture2D>("Ships/ShipDestroyerHull");
            rescuePic = g.Content.Load<Texture2D>("Ships/ShipRescue");

        }
    }
}

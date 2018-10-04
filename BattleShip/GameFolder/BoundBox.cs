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
    public class BoundBox
    {
        public Vector4 TopBounds { get; set; }
        public Vector4 BottomBounds { get; set; }

        public Vector2 Location { get; set; }

        public Rectangle Bounds;

        Vector2 TopL { get; set; }
        Vector2 TopR { get; set; }
        Vector2 BottomL { get; set; }
        Vector2 BottomR { get; set; }


      


        // public BoundBox() { }

        public BoundBox(Rectangle rect)
        {
            TopL = new Vector2(rect.Left,rect.Top);
            TopR = new Vector2(rect.Right, rect.Top);
            BottomL = new Vector2(rect.Left, rect.Bottom);
            BottomR = new Vector2(rect.Right, rect.Bottom);

            TopBounds = new Vector4(rect.Left, rect.Top, rect.Right, rect.Top);
            BottomBounds = new Vector4(rect.Left, rect.Bottom, rect.Right, rect.Bottom);
            Bounds = rect;
        }

        public BoundBox(Vector2 tl,Vector2 tr,Vector2 bl, Vector2 br)
        {
            TopBounds = new Vector4(tl, tr.X, tr.Y);
            BottomBounds = new Vector4(bl, br.X, br.Y);
        }

        public void SetTopBounds(Vector2 tl, Vector2 tr) { TopBounds = new Vector4(tl, tr.X, tr.Y); }
        public void SetTopBounds(int tlX,int tlY,int trX, int trY) { TopBounds = new Vector4(tlX, tlY, trX, trY); }

        public void SetBottomBounds(Vector2 bl, Vector2 br) { BottomBounds = new Vector4(bl, br.X, br.Y); }
        public void SetBottomBounds(int blX, int blY, int brX, int brY) { TopBounds = new Vector4(blX, blY, brX, brY); }

        public void SetBounds()
        {
            Bounds = new Rectangle()
            {
                Location = new Point((int)TopBounds.X, (int)TopBounds.Y),
                Size = new Point((int)Math.Abs(TopBounds.X - TopBounds.Z), (int)Math.Abs(TopBounds.Y - BottomBounds.Y)),
            };
        }

        public Rectangle GetBounds() { return Bounds; }

        public void RotateBounds(double deg)
        {
            Matrix rotMatrix = new Matrix()
            {
                M11 = (float)Math.Cos(deg),
                M12 = (float)-(Math.Sin(deg)),
                M21 = (float)Math.Sin(deg),
                M22 = (float)Math.Cos(deg)
            };

            TopR = Vector2.Transform(new Vector2(TopBounds.X,TopBounds.Y), rotMatrix);//was top L
            BottomR = Vector2.Transform(new Vector2(TopBounds.Z, TopBounds.W), rotMatrix);//was top R
            TopL = Vector2.Transform(new Vector2(BottomBounds.X, BottomBounds.Y), rotMatrix);//was bottom L
            BottomL = Vector2.Transform(new Vector2(BottomBounds.Z, BottomBounds.W), rotMatrix);// was bottom R

            SetTopBounds(TopL, TopR);
            SetBottomBounds(BottomL, BottomR);
            SetBounds();           
        }

        public void TranslateBounds(Vector2 amount)
        {
            Matrix TranMatrix = new Matrix()
            {
                M11 = 1, M12 = 0, M13 = amount.X,
                M21 = 0, M22 = 1, M23 = amount.Y,
                M31 = 0, M32 = 0, M33 = 1
            };

            var nTL = Vector3.Transform(new Vector3(TopBounds.X, TopBounds.Y, 1), TranMatrix);
            var nTR = Vector2.Transform(new Vector2(TopBounds.Z, TopBounds.W), TranMatrix);
            var nBL = Vector2.Transform(new Vector2(BottomBounds.X, BottomBounds.Y), TranMatrix);
            var nBR = Vector2.Transform(new Vector2(BottomBounds.Z, BottomBounds.W), TranMatrix);

            SetTopBounds(new Vector2(nTL.X,nTL.Y), nTR);
            SetBottomBounds(nBL, nBR);
            SetBounds();
        }

        public Rectangle CompOrigin()
        {
            Bounds.Location = new Point(Bounds.X + (Bounds.Width / 2), Bounds.Y + (Bounds.Y + (Bounds.Height / 2)));
            return Bounds;
        }
    }
}

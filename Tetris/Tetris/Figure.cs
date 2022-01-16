using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    abstract class Figure
    {
        const int LENGHT = 4;
        protected Point[] points = new Point[LENGHT];

        public void Draw()
        {
            foreach (Point p in points)
            {
                p.Draw();
            }
        }

        //public void Move(Direction dir)
        //{
        //    Hide();
        //    foreach(Point p in points)
        //    {                
        //        p.Move(dir);
        //    }
        //    Draw();
        //}


        internal void TryMove(Direction dir)
        {
            Hide();
            var clone = Clone();
            Move(clone, dir);
            if (VerifyPosition(clone))
                points = clone;
            Draw();
        }

        private bool VerifyPosition(Point[] pList)
        {
            foreach(var p in pList)
            {
                if (p.X < 0 || p.Y < 0 || p.X >= Field.Width || p.Y >= Field.Height)
                    return false;                
            }

            return true;
        }

        public void Move(Point[] pList, Direction dir)
        {
            foreach(var p in pList)
            {
                p.Move(dir);
            }
        }

        internal void TryRotate()
        {
            Hide();
            var clone = Clone();
            Rotate(clone);

            if (VerifyPosition(clone))
                points = clone;
            Draw();
        }

        private Point[] Clone()
        {
            var newPoints = new Point[LENGHT];
            for (int i = 0; i < LENGHT; i++)
            {
                newPoints[i] = new Point(points[i]);
            }
            return newPoints;
        }


        public abstract void Rotate(Point[] pList);

        internal void Hide()
        {
            foreach(Point p in points)
            {
                p.Hide();
            }
        }

        
    }
}

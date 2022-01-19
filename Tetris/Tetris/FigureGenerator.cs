using System;

namespace Tetris
{
    internal class FigureGenerator
    {
        private int _x;
        private int _y;
        

        private Random _rand = new Random();

        public FigureGenerator(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Figure GetNewFigure()
        {
            if (_rand.Next(0, 2) == 0)
                return new Square(_x, _y);
            else
                return new Stick(_x, _y);            
        }
    }
}
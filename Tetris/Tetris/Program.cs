using System;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 30);
            Console.SetBufferSize(40, 30);

            Square s = new Square(2, 5, '*');
            s.Draw();

            Stick st = new Stick(5, 8, '*');
            st.Draw();




            Console.ReadLine();
        }

    }
}

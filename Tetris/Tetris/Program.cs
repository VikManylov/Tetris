using System;
using System.Threading;
using System.Timers;

namespace Tetris
{
    class Program
    {
        const int TIMER_INTERVAL = 500;
        static System.Timers.Timer timer;
        static private Object _lockObject = new object();

        static Figure currentFigure;
        static FigureGenerator generator;
        static void Main(string[] args)
        {
            Console.SetWindowSize(Field.Width, Field.Height);
            Console.SetBufferSize(Field.Width, Field.Height);
            
            generator = new FigureGenerator(Field.Width / 2, 0, Drawer.DEFAULT_SYMBOL);
            currentFigure = generator.GetNewFigure();
            SetTimer();

            while(true)
            {
                if(Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    Monitor.Enter(_lockObject);
                    var result = HandleKey(currentFigure, key.Key);
                    ProcessResult(result, ref currentFigure);
                    Monitor.Exit(_lockObject);
                }
            }
        }

        private static bool ProcessResult(Result result, ref Figure currentFigure)
        {
            if (result == Result.HEAP_STRIKE || result == Result.DOWN_BORDER_STRIKE)
            {
                Field.AddFigure(currentFigure);
                Field.TryDeleteLines();
                currentFigure = generator.GetNewFigure();
                return true;
            }
            else
                return false;
        }

        private static Result HandleKey(Figure currentFigure, ConsoleKey key)
        {
            switch(key)
            {
                case ConsoleKey.LeftArrow:
                    return currentFigure.TryMove(Direction.LEFT);
                case ConsoleKey.RightArrow:
                    return currentFigure.TryMove(Direction.RIGHT);
                case ConsoleKey.DownArrow:
                    return currentFigure .TryMove(Direction.DOWN);
                case ConsoleKey.Spacebar:
                    return currentFigure.TryRotate(); 
            }
            return Result.SUCCESS;
        }

        private static void SetTimer()
        {
            //Create a timer with a two second interval.
            timer = new System.Timers.Timer(TIMER_INTERVAL);
            //Hook up the Elapsed event for the timer.
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Monitor.Enter(_lockObject);
            var result = currentFigure.TryMove(Direction.DOWN);
            ProcessResult(result, ref currentFigure);
            Monitor.Exit(_lockObject);
        }
    }
}

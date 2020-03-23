using MatrixSolver.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var toSolve = GeneratedArray(200, 200);
            var algo = new Algorithim(toSolve);
            algo.Solve();

            for(var i = 0; i < 100; i++) {
                toSolve = GeneratedArray(random.Next(50, 300), random.Next(50, 300));
                algo = new Algorithim(toSolve);
                algo.Solve();
            }

            

            toSolve = GeneratedArray(1920, 1080);
            algo = new Algorithim(toSolve);
            algo.Solve();
            toSolve = GeneratedArray(10000, 10000);
            algo = new Algorithim(toSolve);
            algo.Solve();
        }

        private static int[,] GeneratedArray(int argRowsToSolve, int argColsToSolve)
        {
            int[,] ret = new int[argRowsToSolve, argColsToSolve];
            Random rand = new Random();
            for (int i = 0; i < argRowsToSolve; i++)
            {
                for (int j = 0; j < argColsToSolve; j++)
                {
                    ret[i, j] = rand.Next(0, 2);
                }
            }

            return ret;
        }

    }
}

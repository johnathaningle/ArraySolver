using MatrixSolver.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixSolver
{
    public class Algorithim
    {
        public Algorithim(int[,] array)
        {
            this.SolverId = Guid.NewGuid();
            this.LargestSquare = new LargestSquare();
            this.ArrayToSolve = array;
        }
        public Guid SolverId { get; set; }
        public int LargestPossibleIndex { get; set; }
        public long RunTime { get; set; }
        public LargestSquare LargestSquare { get; set; }
        public int[,] ArrayToSolve {get; set;}

        public void Solve()
        {
            var length = this.ArrayToSolve.GetLength(0);
            var width = this.ArrayToSolve.GetLength(1);
            if (length > width)
            {
                this.LargestPossibleIndex = width - 1;
            }
            else
            {
                this.LargestPossibleIndex = length - 1;
            }

            Console.WriteLine("Array to solve");
            Console.WriteLine($"Size: x = {this.ArrayToSolve.GetLength(1)}, y = {this.ArrayToSolve.GetLength(0)}");
            
            var watch = System.Diagnostics.Stopwatch.StartNew();
            FindLargestSquare();
            watch.Stop();

            this.RunTime = watch.ElapsedMilliseconds;

            Console.WriteLine($"Largest Square (x, y): ({LargestSquare.TopLeftCorner.Item1},{LargestSquare.TopLeftCorner.Item2})");

            Console.WriteLine("Largest Square Size: " + LargestSquare.Size);
            Console.WriteLine("Took: " + this.RunTime + "ms");
            Console.WriteLine("Total Elements array: " + this.ArrayToSolve.Length);
            WriteImage();
        }

        



        public bool FindLargestSquare()
        {
            //loop through each item in array
            for (int i = 0; i < this.ArrayToSolve.GetLength(0); i++)
            {
                for (int j = 0; j < this.ArrayToSolve.GetLength(1); j++)
                {
                    var max = this.LargestPossibleIndex;
                    if(i > j) {
                        max = max - i;
                    } else {
                        max = max - j;
                    }
                     
                    if(max > this.LargestSquare.Size) {
                        for(var s = max; s > 0; s--) {
                            var corners = new {
                                TopLeft = (i, j), 
                                BottomLeft = (i, j+s), 
                                TopRight = (i+s, j), 
                                BottomRight = (i+s, j+s)
                            };
                            try {
                                var values = new {
                                    TopLeft = ArrayToSolve[i, j], 
                                    BottomLeft = ArrayToSolve[i, j+s], 
                                    TopRight = ArrayToSolve[i+s, j], 
                                    BottomRight = ArrayToSolve[i+s, j+s] 
                                };
                                if(values.TopLeft == values.TopRight && 
                                    values.BottomRight == values.BottomLeft &&
                                    values.TopLeft == values.BottomLeft) {
                                    if(s + 1 > this.LargestSquare.Size) {
                                        this.LargestSquare.TopLeftCorner = corners.TopLeft;
                                        this.LargestSquare.Size = s + 1;
                                    }
                                    if(!CanASquareBeLarger(i, j)) {
                                        return true;
                                    }
                                }
                            } catch(Exception ex) {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                        
                    }
                }
            }

            return true;
        }

        private bool CanASquareBeLarger(int x, int y)
        {
            if(x + this.LargestSquare.Size - 1 > this.LargestPossibleIndex) {
                return false;
            }
            if(y + this.LargestSquare.Size - 1 > this.LargestPossibleIndex) {
                return false;
            }
            return true;
        }

        public void WriteImage()
        {
            using (var image = new Image<Rgba32>(ArrayToSolve.GetLength(0), ArrayToSolve.GetLength(1)))
            {
                for (int i = 0; i < ArrayToSolve.GetLength(0); i++)
                {
                    for (int j = 0; j < ArrayToSolve.GetLength(1); j++)
                    {
                        if (ArrayToSolve[i, j] == 0)
                        {
                            image[i, j] = new Rgba32(0, 0, 0);
                        }
                        else
                        {
                            image[i, j] = new Rgba32(255, 255, 255);
                        }
                    }
                }
                image.Save($"{this.SolverId.ToString()}.png"); //disposes the image
            }
            using (var image = new Image<Rgba32>(ArrayToSolve.GetLength(0), ArrayToSolve.GetLength(1)))
            {
                for (int i = 0; i < ArrayToSolve.GetLength(0); i++)
                {
                    for (int j = 0; j < ArrayToSolve.GetLength(1); j++)
                    {
                        if (ArrayToSolve[i, j] == 0)
                        {
                            image[i, j] = new Rgba32(0, 0, 0);
                        }
                        else
                        {
                            image[i, j] = new Rgba32(255, 255, 255);
                        }
                    }
                }
                //write red lines where the largest square is
                var startX = this.LargestSquare.TopLeftCorner.Item1;
                var endX =  startX  + this.LargestSquare.Size;
                for(var x = startX; x < endX; x++) {
                    image[x, this.LargestSquare.TopLeftCorner.Item2] = new Rgba32(255, 0, 0);
                    image[x, this.LargestSquare.TopLeftCorner.Item2 + this.LargestSquare.Size - 1] = new Rgba32(255, 0, 0);
                }
                var startY = this.LargestSquare.TopLeftCorner.Item2;
                var endY = startY + this.LargestSquare.Size;
                for(var y = startY; y < endY; y++) {
                    image[this.LargestSquare.TopLeftCorner.Item1, y] = new Rgba32(255, 0, 0);
                    image[this.LargestSquare.TopLeftCorner.Item1 + this.LargestSquare.Size - 1, y] = new Rgba32(255, 0, 0);
                }      
                image.Save($"{this.SolverId.ToString()} - highlighted.png");
            }

        }
    }
}

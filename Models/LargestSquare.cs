namespace MatrixSolver.Models
{
    public class LargestSquare
    {
        public LargestSquare()
        {
            this.TopLeftCorner = (0, 0);
            this.Size = 1;
        }
        public int Size { get; set; }
        public (int, int) TopLeftCorner { get; set; }
    }
}
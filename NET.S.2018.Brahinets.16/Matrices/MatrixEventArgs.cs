namespace Matrices
{
    public class MatrixEventArgs
    {
        public int i { get; }
        public int j { get; }

        public MatrixEventArgs(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
    }
}
namespace Matrices
{
    public class MatrixEventArgs
    {
        public int i { get; }
        public int j { get; }
        
        /// <summary>
        /// Initialize an event arguments for the valuechanged event of the matrix class.
        /// </summary>
        /// <param name="i">A Row where a change was made.</param>
        /// <param name="j">A Collumn where a change was made.</param>
        public MatrixEventArgs(int i, int j)
        {
            this.i = i;
            this.j = j;
        }
    }
}
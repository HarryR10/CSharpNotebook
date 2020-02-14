namespace Scene2d
{
    public class LineCounter
    {
        private int _counter;

        public override string ToString()
        {
            return "Error in line " + _counter + ": ";
        }

        public LineCounter() => _counter = 0;

        public void Next() => _counter++;

        public void Reset() => _counter = 0;
    }
}

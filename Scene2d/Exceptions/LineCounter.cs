namespace Scene2d
{
    public class LineCounter
    {
        public int Counter { get; set; }

        public LineCounter()
        {
            Counter = 0;
        }

        public void Next() => Counter++;

        public void Reset() => Counter = 0;
    }
}

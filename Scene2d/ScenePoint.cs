namespace Scene2d
{
    public struct ScenePoint
    {
        public ScenePoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public override string ToString()
        {
            return "(" + X + ", " + Y +")";
        }
    }
}

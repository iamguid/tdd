namespace TagsCloudVisualization
{
    public class Size
    {
        public int Width { get; }
        public int Height { get; }
        
        public Size(int w, int h)
        {
            Width = w;
            Height = h;
        }
        

        public override string ToString()
        {
            return $"Width: {Width}, Height: {Height}";
        }
    }
}
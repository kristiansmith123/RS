namespace RS.Bots.Domain.Entities
{
    public class VisionResult
    {
        public string Type { get; set; }
        public double Confidence { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        public double PositionMidX => PositionX + (Width / 2);
        public double PositionMidY => PositionY + (Height / 2);
    }
}

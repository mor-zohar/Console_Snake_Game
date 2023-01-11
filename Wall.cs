namespace Snake_Game
{
    public class Wall : Shape
    {
        readonly bool isHorizontal;
        readonly bool atStart;

        public Wall(bool isHorizontal, char filler, bool atStart)
            : base(filler)
        {
            this.isHorizontal = isHorizontal;
            this.atStart = atStart;
        }

        public void Draw(int height, int width)
        {

            if (isHorizontal && atStart)
            {
                for (int i = 0; i <= width; i++)
                {
                    points.Add((i, 0));
                }
            }
            else if (isHorizontal)
            {
                for (int i = 0; i <= width; i++)
                {
                    points.Add((i, height));
                }
            }
            else if (!isHorizontal && atStart)
            {
                for (int i = 0; i <= height; i++)
                {
                    points.Add((0, i));
                }
            }
            else
            {
                for (int i = 0; i <= height; i++)
                {
                    points.Add((width, i));
                }
            }
            Draw();
        }
    }
}

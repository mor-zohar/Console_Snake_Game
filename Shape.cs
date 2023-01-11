namespace Snake_Game
{
    public class Shape
    {
        protected readonly char filler;
        protected List<(int x, int y)> points = new();

        public Shape(char filler, IEnumerable<(int x, int y)>? points = default)
        {
            this.filler = filler;

            if (points is not null)
            {
                foreach (var point in points)
                {
                    this.points.Add(point);
                }
            }


        }

        public static Shape Create(int boardHeight, int boardWidth)
        {
            Random rnd = new Random();
            List<(int x, int y)> points = new();
            char filler = ' ';

            while (true)
            {
                var top = rnd.Next(2, boardHeight - 1);
                var left = rnd.Next(2, boardWidth - 1);

                switch (rnd.Next(0, 3))
                {
                    case 0: // Line

                        filler = '=';

                        var line = rnd.Next(2, 11);

                        if (line + left > boardWidth) { continue; }

                        for (int i = 0; i < line; i++)
                        {
                            points.Add((i + left, top));
                        }
                        break;

                    case 1: // Triangle

                        filler = '#';

                        var triangle = rnd.Next(2, 10);

                        if (triangle + top > boardHeight || triangle + left > boardWidth) { continue; }

                        for (int y = 0; y < triangle; y++)
                        {
                            for (int x = 0; x <= y; x++)
                            {
                                points.Add((x + left, y + top));
                            }
                        }
                        break;

                    case 2: //Square

                        filler = '+';
                        var square = rnd.Next(3, 11);
                        if (square + top > boardHeight || square + left > boardWidth)
                        {
                            continue;
                        }

                        for (int i = top; i < square + top; i++)
                        {
                            for (int j = left; j <= square + left; j++)
                            {
                                points.Add((j, i));
                            }
                        }
                        break;
                }
                break;
            }
            return new Shape(filler, points);
        }

        public bool HasPoint((int x, int y)? point)
        {
            if (point is null) { return false; }

            return points.Contains(point.Value);
        }

        public void Draw()
        {
            foreach (var (x, y) in points)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(filler);

            }
        }

        public static bool HasCollision(Shape shape1, Shape shape2)
        {
            foreach (var (x1, y1) in shape1.points)
            {
                foreach (var (x2, y2) in shape2.points)
                {
                    if (x1 == x2 && y1 == y2) { return true; };
                }
            }
            return false;
        }
    }
}

namespace Snake_Game
{
    public class Snake : Shape
    {
        Board board;
        private static Random rnd = new();
        public (int x, int y) Head => points[^1];
        public Directions? LastDirection { get; private set; } = null;


        public Snake(char filler, int boardHeight, int boardWidth)
            : base(filler)
        {
            points.Add((
                rnd.Next(1, boardWidth),
                rnd.Next(1, boardHeight)
                ));
        }

        public void DrawHead()
        {
            Console.SetCursorPosition(Head.x, Head.y);
            Console.Write(filler);
        }

        private (int x, int y) getNewHead(Directions direction)
        {

            (int x, int y) newHead = Head;

            switch (direction)
            {
                case Directions.Left: newHead.x -= 1; break;
                case Directions.Right: newHead.x += 1; break;
                case Directions.Up: newHead.y -= 1; break;
                case Directions.Down: newHead.y += 1; break;

            }

            return newHead;
        }

        public (int x, int y)? TryGetNewHead(Directions direction)
        {
            if (
                LastDirection == Directions.Up && direction == Directions.Down ||
                LastDirection == Directions.Down && direction == Directions.Up ||
                LastDirection == Directions.Right && direction == Directions.Left ||
                LastDirection == Directions.Left && direction == Directions.Right
                )
            {
                return null;
            }

            return getNewHead(direction);

        }

        public void Move(Directions direction)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            DrawHead();
            var newHead = getNewHead(direction);
            points.Add(newHead);
            Console.ResetColor();
            DrawHead();
            LastDirection = direction;

        }
    }
}

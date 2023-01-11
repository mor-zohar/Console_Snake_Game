namespace Snake_Game
{
    public class Board
    {
        public int Height { get; }
        public int Width { get; }
        public int CurrentLevel { get; private set; } = 0;
        Snake snake;
        List<Shape> shapes = new();
        public int Score { get; set; } = 0;

        public int DisqualifiedCount { get; set; } = 0;

        public Wall[] Walls { get; }

        public Board(int height, int width)
        {
            Height = height;
            Width = width;

            Walls = new Wall[]{

                new (false,'|',true),
                new (false,'|',false),
                new (true,'-',true),
                new (true,'-',false)
            };

        }

        public void StartGame()
        {
            Logo();
            Instructions();
            Console.ResetColor();

            StartLevel();

            while (true)
            {
                Console.SetCursorPosition(1, 26);
                Console.CursorVisible = false;

                Console.Write($"Your Score: {Score}");

                Directions direction;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        direction = Directions.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        direction = Directions.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = Directions.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = Directions.Right;
                        break;
                    default:
                        continue;
                }

                bool newLevel = false;
                var newHead = snake.TryGetNewHead(direction);
                if (newHead is null) { newLevel = true; }

                if (snake.HasPoint(newHead))
                {
                    newLevel = true;
                }

                else
                {
                    bool continueOuter = false;

                    foreach (var wall in Walls)
                    {
                        if (wall.HasPoint(newHead))
                        {
                            continueOuter = true;
                            break;
                        }
                    }
                    if (continueOuter)
                    {
                        continue;
                    }
                }
                foreach (var shape in shapes)
                {
                    if (Shape.HasCollision(snake, shape))
                    {
                        newLevel = true;
                    }
                }

                if (newLevel)
                {
                    Disqualified();
                    continue;
                }
                else
                {
                    snake.Move(direction);
                    Score++;
                }
            }
        }

        private void StartLevel()
        {
            CurrentLevel += 1;
            snake = new('*', Height, Width);

            Console.Clear();
            foreach (var wall in Walls)
            {
                wall.Draw(Height, Width);
            }

            var shapeCount =
                shapes.Count == 0 ?
                    new Random().Next(3, 7) :
                    shapes.Count + 1;

            shapes.Clear();

            bool hasCollision;
            for (int i = 0; i < shapeCount; i++)
            {
                Shape newShape;

                do
                {
                    hasCollision = false;
                    newShape = Shape.Create(Height, Width);
                    foreach (var shape in shapes)
                    {
                        if (Shape.HasCollision(shape, newShape))
                        {
                            hasCollision = true;
                            break;
                        }
                        if (Shape.HasCollision(snake, newShape))
                        {

                            hasCollision = true;
                            break;
                        }
                    }
                } while (hasCollision);

                if (shapeCount == 15)
                {
                    Console.Clear();
                    Console.SetCursorPosition(35, 5);
                    Console.WriteLine("Game Over!!");
                    Console.SetCursorPosition(30, 8);
                    Console.WriteLine("Press any Key To Exit");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else { shapes.Add(newShape); }
            }

            foreach (var shape in shapes)
            {
                shape.Draw();
            }
            Console.SetCursorPosition(60, 26);
            Console.Write($"NUMBER OF SHAPES: {shapeCount}");

            snake.DrawHead();
        }

        private void Disqualified()
        {
            DisqualifiedCount++;
            Console.Clear();
            Console.WriteLine("\n\n\n\n                              YOU GOT DISQUALIFIED!!\n\n");
            Console.WriteLine($"                           NUMBER OF DISQUALIFICATIONS: {DisqualifiedCount}");
            Console.WriteLine($"                                   YOUR SCORE: {Score}");
            Console.WriteLine("\n\n                     PRESS ANY KEY TO PROCEED TO THE NEXT LEVEL");
            Console.ReadKey();
            StartLevel();
        }

        private static void Logo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("               ___        ___   __       ____        _    __   ______ ");
            Console.WriteLine("              /   \\      /   | / /      / __ \\      | |  / /  |  ____|");
            Console.WriteLine("              | |_|     / /| |/ /      / /__\\ \\     | | / /   | |____ ");
            Console.WriteLine("             _ \\ \\     / / | | /      / ______ \\    | |/ /    |  ____|");
            Console.WriteLine("            | \\/ /    / /  |  /      / /      \\ \\   | |\\ \\    | |____ ");
            Console.WriteLine("             \\__/    /_/   |_/      /_/        \\_\\  |_| \\_\\   |______|");
            Console.Write("\n");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                             PRESS ANY KEY TO START");
            Console.Write("\n");
            Console.ReadKey();
            Console.Clear();
        }

        private static void Instructions()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            Console.WriteLine("                                HELLO PLAYER");
            Console.WriteLine("                         WELCOME TO THE SNAKE GAME");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" -) In The Next Game Your Mission Is To Fill The Board With The Snake");
            Console.WriteLine(" -) The Snake Head Is White");
            Console.WriteLine(" -) The Snake Body Is Blue \n\n");
            Console.WriteLine(" -) There is Obstacles In The Game");
            Console.WriteLine(" -) If You Hit These Obstacles Or If You Hit The Snake Body");
            Console.WriteLine(" -) You Will Be Disqualified And Move To The Next Level\n");
            Console.WriteLine(" -) Each Time You Get Disqualified");
            Console.WriteLine(" -) Another Obstacle Will Be Added To The Next Level");
            Console.WriteLine("    For Example: ");
            Console.WriteLine("    If 3 obstacles appeared on the screen and you are disqualified");
            Console.WriteLine("    In The Next Level 4 obstacles will appear and so on\n\n");
            Console.WriteLine(" -) The Game Is Over When You Reach 15 Obstacles On The Board");
            Console.WriteLine();
            Console.WriteLine("\n                           PRESS ANY KEY TO START");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

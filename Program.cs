using Snake_Game;

public class Program
{
    private static void Main(string[] args)
    {
        Console.SetWindowSize(82, 28);

        Console.CursorVisible = false;

        var board = new Board(25, 80);
        board.StartGame();
    }
}
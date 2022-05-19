using System;
namespace Lesson_3
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Random random = new Random();

            using (var game = new Game1())
            game.Run();
            
        }
    }
}

using System;

namespace secret_santa
{
    public class Welcome
    {
        public void PrintWelcomeMessage()
        {
            Console.Write('\n');
            PrintColorfulHoHoHo();
            Console.WriteLine(", Merry Christmas!");
            Console.WriteLine("This will help you pair people up with someone who isn't their spouse for Secret Santa");
            Console.WriteLine("Be sure to format names as \n participant/spouse \n");
        }

        private void PrintColorfulHoHoHo()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (i == 0)
                {
                    Console.Write("H");
                }
                else
                {
                    Console.Write("h");
                }

                Console.ForegroundColor = ConsoleColor.Red;
                if (i == 2)
                {
                    Console.Write("o");
                }
                else
                {
                    Console.Write("o ");
                }
            }

            Console.ResetColor();
        }
    }
}
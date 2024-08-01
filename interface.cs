using namespaceTexts;

namespace namespaceGUI
{

    public static class GUI
    {

        public static void iniConsole()
        {
            Console.Clear();
            Console.Title = ("TL1 - " + TXT.title);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void horLines()
        {
            Console.WriteLine("_______________________________________________________________________________________________");
        }

        public static void tutorial()
        {
            horLines();
            Console.WriteLine("\n ----- " + TXT.tutorial + " -----");
            horLines();
        }

        public static void mainMenu()
        {
            Console.WriteLine("\n " + TXT.title);
            Console.WriteLine(" " + TXT.welcome + "\n");
            tutorial();
            Console.WriteLine("\n 1: " + TXT.play);
            Console.WriteLine(" 2: " + TXT.characters);
            Console.WriteLine(" 3: " + TXT.options);
            Console.WriteLine(" 4: " + TXT.exit);
            horLines();
            Console.WriteLine("\n");
        }

    }

}
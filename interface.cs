using namespaceGlobal;
using namespaceTexts;
using namespaceSoundManager;
using System.Text;

namespace namespaceGUI
{

    public static class GUI
    {

        public static void iniConsole()
        {
            Console.Clear();
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = ("TL1 - " + TXT.title);
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void horLines()
        {
            Console.WriteLine("______________________________________________________________________________________________________");
        }

        public static void tutorial()
        {
            horLines();
            Console.WriteLine("\n ----- " + TXT.tutorial + " -----");
            horLines();
        }

        public static void refresh()
        {
            int steps = 5;
            int framerate = (int)float.Round(GLOBAL.consoleRefreshSleep/steps);
            string loading = "...............";
            Console.Clear();

            for (int i = 0; i < steps; i++)
            {
                Console.WriteLine(loading);
                loading += "...............";
                Thread.Sleep(framerate);
                Console.Clear();
            }
        }

        public static void mainMenu()
        {
            refresh();
            Console.WriteLine("\n " + TXT.title);
            Console.WriteLine(" " + TXT.welcome + "\n");
            tutorial();
            Console.WriteLine("\n 1: " + TXT.play);
            Console.WriteLine(" 2: " + TXT.characters);
            Console.WriteLine(" 3: " + TXT.options);
            Console.WriteLine(" 4: " + TXT.exit);
            horLines();
        }

        public static void makeInput()
        {
            Console.Write(TXT.makeinput);
        }

        public static void NaNinput()
        {
            horLines();
            Console.WriteLine(TXT.naninput);
        }

        public static void outOfRangeInput()
        {
            horLines();
            Console.WriteLine(TXT.outofrange);
        }

        public static void playMenu()
        {
            refresh();
            Console.WriteLine("\n " + TXT.play);
            tutorial();
            Console.WriteLine("\n 1: " + TXT.tournament);
            Console.WriteLine(" 2: " + TXT.oneVone);
            Console.WriteLine(" 3: " + TXT.freeforall);
            Console.WriteLine(" 4: " + TXT.goback);
            horLines();
        }

        public static void charMenu()
        {
            refresh();
            Console.WriteLine("\n " + TXT.characters);
            tutorial();
            Console.WriteLine("\n 1: " + TXT.customchar);
            Console.WriteLine(" 2: " + TXT.randomchar);
            Console.WriteLine(" 3: " + TXT.browsechars);
            Console.WriteLine(" 4: " + TXT.goback);
            horLines();
        }

        public static void optionsMenu()
        {
            refresh();
            Console.WriteLine("\n " + TXT.options);
            tutorial();
            Console.WriteLine("\n 1: " + TXT.changelan);
            Console.WriteLine(" 2: " + TXT.changedmgmod);
            Console.WriteLine(" 3: " + TXT.changeattperlvl);
            Console.WriteLine(" 4: " + TXT.changetextcolor);
            Console.WriteLine(" 5: " + TXT.goback);
            horLines();
        }

    }

}
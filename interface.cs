using namespaceGlobal;
using namespaceTexts;
using namespaceCharacter;
using System.Text;

namespace namespaceGUI
{

    public static class GUI
    {

        #region Misc

        public static void iniConsole()
        {
            Console.Clear();
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = ("TL1 - " + TXT.title);
            Console.CursorVisible = false;
            Console.ForegroundColor = GLOBAL.textColor;
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

        public static void horLines()
        {
            string lineStr = "";
            int nLines = 120;

            for (int i = 0; i < nLines; i++)
            {
                lineStr += "_";
            }

            Console.WriteLine(lineStr);
        }

        public static void makeInput()
        {
            Console.Write(" " + TXT.makeinput);
        }

        public static void inputNumber()
        {
            Console.Write(" " + TXT.inputnumber);
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

        public static void tutorial()
        {
            horLines();
            Console.WriteLine("\n ----- " + TXT.tutorial + " -----");
            horLines();
        }

        #endregion

        #region Menus

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

        #region Play

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

        #endregion

        #region Characters

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

        public static void charRmdMenu()
        {
            refresh();
            Console.WriteLine("\n " + TXT.characters);
            Console.WriteLine(" " + TXT.randomchar);
            horLines();
            Console.WriteLine(" " + TXT.explainrandomchar);
            Console.WriteLine(" " + TXT.validrandomchar);
            horLines();
        }

        public static void charSuccesRdm()
        {
            horLines();
            Console.WriteLine("\n " + TXT.succesrandomchar);
            horLines();
        }

        public static void charBrowseMenu()
        {
            refresh();
            Console.WriteLine("\n " + TXT.characters);
            Console.WriteLine(" " + TXT.explainbrwose + "\n");
        }

        public static void charListItem(int item,playerCharacter character)
        {
            string characterString = (" " + (item + 1).ToString() + "| ");
            characterString += (TXT.charname + ": " + character.cName);
            while (characterString.Length < 40) {characterString += " ";}
            characterString += ("| " + TXT.charvictories + ": " + character.VICTORIES);
            while (characterString.Length < 60) {characterString += " ";}
            characterString += ("| " + TXT.charclass + ": " + classToText(character.CLASS));
            while (characterString.Length < 85) {characterString += " ";}
            characterString += ("| " + TXT.charlevel + ": " + character.LEVEL);

            horLines();
            Console.WriteLine(characterString);
        }

        private static string classToText(string _class)
        {
            string text = "";

            switch(_class)
            {
                case "Scout":
                {
                    text = TXT.charscout;
                    break;
                }
                case "Rogue":
                {
                    text = TXT.charrogue;
                    break;
                }
                case "Knight":
                {
                    text = TXT.charknight;
                    break;
                }
                case "Barbarian":
                {
                    text = TXT.charbarbarian;
                    break;
                }
            }

            return text;
        }

        public static void charEndBrowse(int item)
        {
            horLines();
            horLines();
            Console.WriteLine(" " + item.ToString() + ": " + TXT.goback + "\n");
        }

        #endregion

        #region Options

        public static void optionsMenu()
        {
            refresh();
            Console.WriteLine("\n " + TXT.options);
            tutorial();
            Console.WriteLine("\n 1: " + TXT.changelan);
            Console.WriteLine(" 2: " + TXT.changedmgadj);
            Console.WriteLine(" 3: " + TXT.changeattperlvl);
            Console.WriteLine(" 4: " + TXT.changetextcolor);
            Console.WriteLine(" 5: " + TXT.goback);
            horLines();
        }

        public static void changeDmgAdj()
        {
            refresh();
            Console.WriteLine("\n " + TXT.options);
            Console.WriteLine(" " + TXT.changedmgadj);
            horLines();
            Console.WriteLine("\n " + TXT.explaindmgadj);
            Console.WriteLine(" " + TXT.validdmgadj);
            Console.WriteLine(" " + TXT.currentvalue + GLOBAL.dmgAdjust.ToString("N2"));
            horLines();
        }

        public static void changeAttPLvl()
        {
            refresh();
            Console.WriteLine("\n " + TXT.options);
            Console.WriteLine(" " + TXT.changeattperlvl);
            horLines();
            Console.WriteLine("\n " + TXT.explainattperlvl);
            Console.WriteLine(" " + TXT.validattperlvl);
            Console.WriteLine(" " + TXT.currentvalue + GLOBAL.LVLUP.attributesPerLevel.ToString());
            horLines();
        }

        public static void changeColor()
        {
            refresh();
            Console.WriteLine("\n " + TXT.options);
            Console.WriteLine(" " + TXT.changetextcolor);
            tutorial();
            Console.WriteLine("\n 01: " + TXT.color_white);
            Console.WriteLine(" 02: " + TXT.color_green);
            Console.WriteLine(" 03: " + TXT.color_red);
            Console.WriteLine(" 04: " + TXT.color_blue);
            Console.WriteLine(" 05: " + TXT.color_yellow);
            Console.WriteLine(" 06: " + TXT.color_cyan);
            Console.WriteLine(" 07: " + TXT.color_magenta);
            Console.WriteLine(" 08: " + TXT.color_dkgreen);
            Console.WriteLine(" 09: " + TXT.color_dkred);
            Console.WriteLine(" 10: " + TXT.color_dkblue);
            Console.WriteLine(" 11: " + TXT.color_dkyellow);
            Console.WriteLine(" 12: " + TXT.color_dkcyan);
            Console.WriteLine(" 13: " + TXT.goback);
            horLines();
        }

        #endregion

        #endregion

    }

}
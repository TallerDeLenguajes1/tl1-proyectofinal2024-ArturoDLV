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
            Console.Write(" " + TXT.makeinput + ": ");
        }

        public static void inputNumber()
        {
            Console.Write(" " + TXT.inputnumber + ": ");
        }

        public static void NaNinput()
        {
            horLines();
            Console.WriteLine(" " + TXT.naninput);
        }

        public static void outOfRangeInput()
        {
            horLines();
            Console.WriteLine(" " + TXT.outofrange);
        }

        public static void inputString()
        {
            Console.Write(" " + TXT.inputstring + ": ");
        }

        public static void invalidString()
        {
            horLines();
            Console.WriteLine(" " + TXT.invalidstring);
        }

        public static void invalidName()
        {
            horLines();
            Console.WriteLine(" " + TXT.invaliname);
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
            Console.WriteLine(" 4: " + TXT.resetcharacters);
            Console.WriteLine(" 5: " + TXT.goback);
            horLines();
        }

        public static void charCustomMenu(string name, string nick, int age, string _class, float hp, int spd, int str, int dex, int armor)
        {
            refresh();
            Console.WriteLine("\n " + TXT.characters);
            Console.WriteLine(" " + TXT.customchar);
            horLines();
            
            string auxString;
            int itemSep = 34; 
            Console.WriteLine(" " + TXT.charpersonalinfo + ": \n");
            auxString = (" | " + TXT.charname + ": " + name);
            while (auxString.Length < itemSep) {auxString += " ";}
            auxString += ("| " + TXT.charnick + ": " + nick);
            Console.WriteLine(auxString);
            auxString = ("\n | " + TXT.charage + ": " + age.ToString());
            while (auxString.Length < itemSep) {auxString += " ";}
            auxString += ("| " + TXT.charclass + ": " + classToText(_class));
            Console.WriteLine(auxString);
            auxString = "";
            horLines();

            Console.WriteLine(" " + TXT.charstatistics + ": \n");
            Console.WriteLine(" | " + TXT.charhp + ": " + hp.ToString("N0") + "\n");
            auxString += (" | " + TXT.charspd + ": " + spd.ToString("N0"));
            while (auxString.Length < itemSep) {auxString += " ";}
            auxString += (" | " + TXT.chardex + ": " + dex.ToString("N0"));
            Console.WriteLine(auxString);
            auxString = "";
            auxString += (" | " + TXT.charstr + ": " + str.ToString("N0"));
            while (auxString.Length < itemSep) {auxString += " ";}
            auxString += (" | " + TXT.chararmor + ": " + armor.ToString("N0"));
            Console.WriteLine(auxString);

            tutorial();
            Console.WriteLine("\n 1: " + TXT.charpersonalinfo);
            Console.WriteLine(" 2: " + TXT.charstatistics);
            Console.WriteLine(" 3: " + TXT.finishchar);
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
                default:
                {
                    text = "";
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

        public static void charShowInfo(int item, playerCharacter character)
        {
            string auxString = "";
            int itemSep = 34;

            refresh();
            charListItem(item,character);
            Console.WriteLine("\n " + TXT.charid + ": " + character.id.ToString());
            horLines();

            Console.WriteLine(" " + TXT.charpersonalinfo + ": \n");
            auxString += (" | " + TXT.charnick + ": " + character.cNickname);
            while (auxString.Length < itemSep) {auxString += " ";}
            auxString += ("| " + TXT.charage + ": " + character.AGE.ToString());
            Console.WriteLine(auxString);
            auxString = "";
            horLines();

            Console.WriteLine(" " + TXT.charstatistics + ": \n");
            Console.WriteLine(" | " + TXT.charxp + ":  " + character.XP.ToString());
            Console.WriteLine(" | " + TXT.charhp + ": " + character.CURRENT_HP.ToString("N0") + "/" + character.MAX_HP.ToString("N0") + "\n");
            auxString += (" | " + TXT.charspd + ": " + character.SPEED.ToString("N0"));
            while (auxString.Length < itemSep) {auxString += " ";}
            auxString += (" | " + TXT.chardex + ": " + character.DEXTERITY.ToString("N0"));
            Console.WriteLine(auxString);
            auxString = "";
            auxString += (" | " + TXT.charstr + ": " + character.STRENGH.ToString("N0"));
            while (auxString.Length < itemSep) {auxString += " ";}
            auxString += (" | " + TXT.chararmor + ": " + character.ARMOR.ToString("N0"));
            Console.WriteLine(auxString);
        }

        public static void charEditMenu()
        {
            tutorial();
            Console.WriteLine("\n 1: " + TXT.charchangename);
            Console.WriteLine(" 2: " + TXT.charchangenick);
            Console.WriteLine(" 3: " + TXT.chardelete);
            Console.WriteLine(" 4: " + TXT.goback);
            horLines();
        }

        public static void charChangeName(bool flag)
        {
            string auxString = "";
            if (flag == true) {auxString = TXT.charvalidname;} else {auxString = TXT.charvalidnick;}

            refresh();
            Console.WriteLine("\n " + TXT.charchangename);
            Console.WriteLine("\n " + auxString);
            horLines();
        }

        public static void charDeleteConfirm(int item, playerCharacter character)
        {
            refresh();
            charListItem(item,character);
            Console.WriteLine(" " + TXT.chardelete);
            horLines();
            Console.WriteLine(" " + TXT.chardeletesure);
            Console.WriteLine(" " + TXT.chardeleteexplain);
            horLines();
            Console.WriteLine("\n 1: " + TXT.chardeleteconfirm);
            Console.WriteLine(" 2: " + TXT.goback);
            horLines();
        }

        public static void charResetConfirm()
        {
            refresh();
            Console.WriteLine("\n " + TXT.characters);
            Console.WriteLine(" " + TXT.resetcharacters);
            horLines();
            Console.WriteLine(" " + TXT.surereset);
            Console.WriteLine(" " + TXT.explainreset);
            horLines();
            Console.WriteLine("\n 1: " + TXT.chardeleteconfirm);
            Console.WriteLine(" 2: " + TXT.goback);
            horLines();
        }

        public static void charNoCharacters()
        {
            refresh();
            Console.WriteLine("\n " + TXT.characters + "\n");
            horLines();
            Console.WriteLine(" " + TXT.nocharacters);
            horLines();
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
            Console.WriteLine(" " + TXT.currentvalue + ": " + GLOBAL.dmgAdjust.ToString("N2"));
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
            Console.WriteLine(" " + TXT.currentvalue + ": " + GLOBAL.LVLUP.attributesPerLevel.ToString());
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
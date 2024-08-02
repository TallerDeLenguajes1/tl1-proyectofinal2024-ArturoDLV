using namespaceGlobal;
using IniParser;
using IniParser.Model;

namespace namespaceConfig
{

    public static class configIni
    {

        private static FileIniDataParser parser = new FileIniDataParser();

        public static void createDefault()
        {
            IniData data = new IniData();

            data["Options"]["locCurrentLanguage"] = GLOBAL.locSp;
            data["Options"]["dmgAdjust"] = "500";
            data["Options"]["attributesPerLevel"] = "2";
            data["Options"]["textColor"] = "dkgreen";

            parser.WriteFile(GLOBAL.configIni,data);
        }

        public static void loadData()
        {
            IniData data = parser.ReadFile(GLOBAL.configIni);

            GLOBAL.locCurrentLanguage = data["Options"]["locCurrentLanguage"];
            float.TryParse(data["Options"]["dmgAdjust"], out GLOBAL.dmgAdjust);
            int.TryParse(data["Options"]["attributesPerLevel"], out GLOBAL.LVLUP.attributesPerLevel);
            GLOBAL.textColor = convertToConsoleColor(data["Options"]["textColor"]);
        }

        public static void saveData(string option, string value)
        {
            IniData data = parser.ReadFile(GLOBAL.configIni);

            switch(option)
            {
                case "locCurrentLanguage":
                {
                    GLOBAL.locCurrentLanguage = value;
                    data["Options"]["locCurrentLanguage"] = value;
                    break;
                }
                case "dmgAdjust":
                {
                    float.TryParse(value, out GLOBAL.dmgAdjust);
                    data["Options"]["dmgAdjust"] = value;
                    break;
                }
                case "attributesPerLevel":
                {
                    int.TryParse(value, out GLOBAL.LVLUP.attributesPerLevel);
                    data["Options"]["attributesPerLevel"] = value;
                    break;
                }
                case "textColor":
                {
                    if (validColor(value) == true)
                    {
                        GLOBAL.textColor = convertToConsoleColor(value);
                        data["Options"]["textColor"] = value;
                    }
                    break;
                }
            }

            parser.WriteFile(GLOBAL.configIni,data);
        }

        private static ConsoleColor convertToConsoleColor(string colorStr)
        {
            ConsoleColor colorCC = ConsoleColor.Green;

            switch(colorStr)
            {
                case "white":
                {
                    colorCC = ConsoleColor.White;
                    break;
                }
                case "red":
                {
                    colorCC = ConsoleColor.Red;
                    break;
                }
                case "blue":
                {
                    colorCC = ConsoleColor.Blue;
                    break;
                }
                case "yellow":
                {
                    colorCC = ConsoleColor.Yellow;
                    break;
                }
                case "cyan":
                {
                    colorCC = ConsoleColor.Cyan;
                    break;
                }
                case "magenta":
                {
                    colorCC = ConsoleColor.Magenta;
                    break;
                }
                case "dkgreen":
                {
                    colorCC = ConsoleColor.DarkGreen;
                    break;
                }
                case "dkred":
                {
                    colorCC = ConsoleColor.DarkRed;
                    break;
                }
                case "dkblue":
                {
                    colorCC = ConsoleColor.DarkBlue;
                    break;
                }
                case "dkyellow":
                {
                    colorCC = ConsoleColor.DarkYellow;
                    break;
                }
                case "dkcyan":
                {
                    colorCC = ConsoleColor.DarkCyan;
                    break;
                }
            }

            return colorCC;
        }

        private static bool validColor(string color)
        {
            if ((color == "white") || (color == "green") || (color == "red") || (color == "blue") || (color == "yellow") || (color == "cyan") || (color == "magenta") || (color == "dkgreen") || (color == "dkred") || (color == "dkblue") || (color == "dkyellow") || (color == "dkcyan"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
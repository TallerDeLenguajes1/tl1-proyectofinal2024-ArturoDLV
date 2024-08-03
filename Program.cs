#region Libraries and Namespaces

using System.Text.Json;
using System.Reflection;

using namespaceGlobal;
using namespaceConfig;
using namespaceTexts;
using namespaceGUI;
using namespaceSoundManager;
using namespaceCharacter;

#endregion

#region Main

checkFilesIntegrity();
configIni.loadData();
locLoad(GLOBAL.locCurrentLanguage);
checkInternetConnection();
GUI.iniConsole();
sndManager.iniMusic();
sndManager.iniFx();
characterManager.iniCharcters();
mainMenu();

#endregion

#region Functions

#region Files and Errors

static void showError(bool close, string errorText)
{
    Console.Clear();
    Console.WriteLine("\nERROR: " + errorText);

    if (close == true)
    {
        Console.WriteLine("Closing program... \n");
        Thread.Sleep(2000);
        Environment.Exit(1);
    }
}

static bool locFilesAvailable()
{
    bool check = false;

    if (Directory.Exists(GLOBAL.locFolder) == true)
    {
        if ((File.Exists(GLOBAL.locEn) == true) && (File.Exists(GLOBAL.locSp) == true))
        {
            check = true;
        }
    }

    return check;
}

static bool musAvailable()
{
    bool check = false;

    if (Directory.Exists(GLOBAL.sndFolder) == true)
    {
        if (Directory.Exists(GLOBAL.musFolder) == true)
        {
            if ((File.Exists(GLOBAL.musMegalovania) == true) && (File.Exists(GLOBAL.musMenu) == true))
            {
                check = true;
            }
        }
    }

    return check;
}

static bool fxAvailable()
{
    bool check = false;

    if (Directory.Exists(GLOBAL.sndFolder) == true)
    {
        if (Directory.Exists(GLOBAL.fxFolder) == true)
        {
            if ((File.Exists(GLOBAL.fxDeath)) && (File.Exists(GLOBAL.fxError)) && (File.Exists(GLOBAL.fxHitBarbarian)) && (File.Exists(GLOBAL.fxHitKnight)) && (File.Exists(GLOBAL.fxHitRogue)) && (File.Exists(GLOBAL.fxHitScout)) && (File.Exists(GLOBAL.fxOff)) && (File.Exists(GLOBAL.fxSelect)) && (File.Exists(GLOBAL.fxVictory)))
            {
                check = true;
            }
        }
    }

    return check;
}

static void charCheck()
{

    if (Directory.Exists(GLOBAL.charFolder) == false)
    {
        showError(true,"Characters folder does not exist or cannot be acces");
    }

    if (File.Exists(GLOBAL.charIndex) == false)
    {
        showError(true,"Characters index does not exist or cannot be acces");
    }
    else
    {
        string[] index = File.ReadAllLines(GLOBAL.charIndex);
        foreach (string item in index)
        {
            if (String.IsNullOrEmpty(item) == false)
            {
                Guid id;
                if (Guid.TryParse(item, out id) == true)
                {
                    if(File.Exists(GLOBAL.charFolder + "/Char_" + item + ".json") == false)
                    {
                        showError(true,"A character exists in the character index, but does not have its corresponding .JSON file");
                    }
                }
                else
                {
                    showError(true,"The character index is corrupted");
                }
            }
        }
    }

}

static void checkFilesIntegrity()
{

    if (locFilesAvailable() == true)
    {
        if (musAvailable() == true)
        {
            if (fxAvailable() == false)
            {
                showError(true,"Cannot acces sound effects");
            }
        }
        else
        {
            showError(true,"Cannot acces music");
        }
    }
    else
    {
        showError(true,"Language Files are missing, inaccesible or otherwise corrupted");
    }

    if (File.Exists(GLOBAL.configIni) == false)
    {
        configIni.createDefault();
    }

    charCheck();

}

#endregion

#region Language

static DynamicTXT locGetString(string file)
{
    try
    {
        string jsonString = File.ReadAllText(file);
        DynamicTXT tempObject = JsonSerializer.Deserialize<DynamicTXT>(jsonString);
        return tempObject;
    }
    catch (Exception ex)
    {
        showError(true,"Problem detected reading or deserializing JSON file: " + ex);
        return null;
    }
}

static void locLoad(string file)
{
    DynamicTXT tempObject = locGetString(file);
    if (tempObject != null)
    {
        Type staticClassType = typeof(TXT);
        Type tempObjectType = tempObject.GetType();

        foreach (System.Reflection.PropertyInfo property in tempObjectType.GetProperties())
        {
            System.Reflection.PropertyInfo staticProperty = staticClassType.GetProperty(property.Name, BindingFlags.Public | BindingFlags.Static);
            if ((staticProperty != null) && (staticProperty.CanWrite == true))
            {
                staticProperty.SetValue(null, property.GetValue(tempObject));
            }
        }
    }
    else 
    {
        showError(true,"Null JSON Object.");
    }

}

#endregion

#region Menus

static int inputOption(int optionsCount)
{
    bool check = false;
    int option = 0;

    while (check == false)
    {
        GUI.makeInput();
        bool parse = int.TryParse(Console.ReadLine(), out option);

        if (parse == true)
        {
            if ((option >= 1) && (option <= optionsCount))
            {
                check = true;
                sndManager.fxPlay(1);
            }
            else
            {
                GUI.outOfRangeInput();
                sndManager.fxPlay(0);
            }
        }
        else
        {
            GUI.NaNinput();
            sndManager.fxPlay(0);
        }
    }

    return option;
}

static void mainMenu()
{

    GUI.mainMenu();
    int option = inputOption(4);
    switch (option)
    {
        case 1:
        {
            playMenu();
            break;
        }
        case 2:
        {
            charMenu();
            break;
        }
        case 3:
        {
            optionsMenu();
            break;
        }
        case 4:
        {
            sndManager.fxPlay(2);
            GUI.refresh();
            Thread.Sleep(1000);
            sndManager.disposeMusic();
            sndManager.disposeFx();
            Environment.Exit(0);
            break;
        }
    }

}

#region Play

static void playMenu()
{

    GUI.playMenu();
    int option = inputOption(4);
    switch (option)
    {
        case 1:
        {
            playTournamentMenu();
            break;
        }
        case 2:
        {
            playOVOMenu();
            break;
        }
        case 3:
        {
            playFFAMenu();
            break;
        }
        case 4:
        {
            mainMenu();
            break;
        }
    }

}

static void playTournamentMenu()
{

}

static void playOVOMenu()
{
    
}

static void playFFAMenu()
{
    
}

#endregion

#region Characters

static void charMenu()
{

    GUI.charMenu();
    int option = inputOption(4);
    switch (option)
    {
        case 1:
        {
            charCustomMenu();
            break;
        }
        case 2:
        {
            charRandomMenu();
            break;
        }
        case 3:
        {
            charBrowseMenu();
            break;
        }
        case 4:
        {
            mainMenu();
            break;
        }
    }

}

static void charCustomMenu()
{

}

static void charRandomMenu()
{
    GUI.charRmdMenu();
    int count = inputOption(5000);
    characterManager.makeRmdChar(count);
    GUI.charSuccesRdm();
    Console.ReadLine();
    charMenu();
}

static void charBrowseMenu()
{

    GUI.charBrowseMenu();
    characterManager.sortListbyVictories();
    int charCount = characterManager.charactersList.Count;
    for (int i = 0; i < charCount; i++)
    {
        GUI.charListItem(i,characterManager.charactersList[i]);
    }
    GUI.charEndBrowse(charCount + 1);

    int option = inputOption(charCount + 1);
    if (option == (charCount + 1))
    {
        charMenu();
    }
    else
    {
        charEditMenu(characterManager.charactersList[option]);
    }

}

static void charEditMenu(playerCharacter character)
{

}

#endregion

#region Options

static void optionsMenu()
{

    GUI.optionsMenu();
    int option = inputOption(5);
    switch (option)
    {
        case 1:
        {
            optionsLanguageChange();
            break;
        }
        case 2:
        {
            optionsDmgAdjMenu();
            break;
        }
        case 3:
        {
            optionsAttPLvlMenu();
            break;
        }
        case 4:
        {
            optionsTxtColorMenu();
            break;
        }
        case 5:
        {
            mainMenu();
            break;
        }
    }

}

static void optionsLanguageChange()
{
    if (GLOBAL.locCurrentLanguage == GLOBAL.locSp)
    {
        GLOBAL.locCurrentLanguage = GLOBAL.locEn;
    }
    else
    {
        GLOBAL.locCurrentLanguage = GLOBAL.locSp;
    }

    configIni.saveData("locCurrentLanguage",GLOBAL.locCurrentLanguage);
    locLoad(GLOBAL.locCurrentLanguage);
    optionsMenu();
}

static void optionsDmgAdjMenu()
{
    GUI.changeDmgAdj();
    GLOBAL.dmgAdjust = getValidDmgAdj();
    configIni.saveData("dmgAdjust",GLOBAL.dmgAdjust.ToString());
    optionsMenu();
}

static float getValidDmgAdj()
{
    float newValue = 500;
    bool check = false;

    while (check == false)
    {
        GUI.inputNumber();
        bool parse = float.TryParse(Console.ReadLine(), out newValue);

        if (parse == true)
        {
            if ((newValue > 0) && (newValue <= 1000))
            {
                check = true;
                sndManager.fxPlay(1);
            }
            else
            {
                GUI.outOfRangeInput();
                sndManager.fxPlay(0);
            }
        }
        else
        {
            GUI.NaNinput();
            sndManager.fxPlay(0);
        }
    }

    return newValue;
}

static void optionsAttPLvlMenu()
{
    GUI.changeAttPLvl();
    GLOBAL.LVLUP.attributesPerLevel = inputOption(10);
    configIni.saveData("attributesPerLevel",GLOBAL.LVLUP.attributesPerLevel.ToString());
    optionsMenu();
}

static void optionsTxtColorMenu()
{
    GUI.changeColor();
    int option = inputOption(13);
    switch (option)
    {
        case 1:
        {
            Console.ForegroundColor = ConsoleColor.White;
            configIni.saveData("textColor","white");
            optionsTxtColorMenu();
            break;
        }
        case 2:
        {
            Console.ForegroundColor = ConsoleColor.Green;
            configIni.saveData("textColor","green");
            optionsTxtColorMenu();
            break;
        }
        case 3:
        {
            Console.ForegroundColor = ConsoleColor.Red;
            configIni.saveData("textColor","red");
            optionsTxtColorMenu();
            break;
        }
        case 4:
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            configIni.saveData("textColor","blue");
            optionsTxtColorMenu();
            break;
        }
        case 5:
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            configIni.saveData("textColor","yellow");
            optionsTxtColorMenu();
            break;
        }
        case 6:
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            configIni.saveData("textColor","cyan");
            optionsTxtColorMenu();
            break;
        }
        case 7:
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            configIni.saveData("textColor","magenta");
            optionsTxtColorMenu();
            break;
        }
        case 8:
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            configIni.saveData("textColor","dkgreen");
            optionsTxtColorMenu();
            break;
        }
        case 9:
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            configIni.saveData("textColor","dkred");
            optionsTxtColorMenu();
            break;
        }
        case 10:
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            configIni.saveData("textColor","dkblue");
            optionsTxtColorMenu();
            break;
        }
        case 11:
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            configIni.saveData("textColor","dkyellow");
            optionsTxtColorMenu();
            break;
        }
        case 12:
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            configIni.saveData("textColor","dkcyan");
            optionsTxtColorMenu();
            break;
        }
        case 13:
        {
            optionsMenu();
            break;
        }
    }
}

#endregion

#endregion

#region Internet

static async void checkInternetConnection()
{
    //If could not reach Google assume no stable internet connection
    HttpClient client = new HttpClient();
    try
    {
        using (client)
        {
            HttpResponseMessage response = await client.GetAsync("https://www.google.com");
            if (response.IsSuccessStatusCode == false)
            {
                GLOBAL.internetConnection = false;
            }
            else
            {
                GLOBAL.internetConnection = true;
            }
        }
    }
    catch
    {
        GLOBAL.internetConnection = false;
    }
}

#endregion

#endregion
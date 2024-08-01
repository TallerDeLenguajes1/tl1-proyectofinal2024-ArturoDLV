#region Libraries and Namespaces

using System.Text.Json;
using System.Reflection;

using namespaceGlobal;
using namespaceCharacter;
using namespaceTexts;
using namespaceGUI;

#endregion

#region Main

checkFilesIntegrity();
locLoad(GLOBAL.locCurrentLanguage);
GUI.iniConsole();
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

static void checkFilesIntegrity()
{

    if (locFilesAvailable() == true)
    {

    }
    else
    {
        showError(true,"Language Files are missing, inaccesible or otherwise corrupted");
    }

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
            }
            else
            {
                GUI.outOfRangeInput();
            }
        }
        else
        {
            GUI.NaNinput();
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
            GUI.refresh();
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
    
}

static void charBrowseMenu()
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
            optionsLanguageMenu();
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

static void optionsLanguageMenu()
{

}

static void optionsDmgAdjMenu()
{
    
}

static void optionsAttPLvlMenu()
{
    
}

static void optionsTxtColorMenu()
{
    
}

#endregion

#endregion

#endregion
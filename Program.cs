#region Libraries and Namespaces

using System.Text.Json;
using System.Reflection;

using namespaceGlobal;
using namespaceCharacter;
using namespaceTexts;
using namespaceGUI;

#endregion

#region Main

locLoad(GLOBAL.locCurrentLanguage);
GUI.iniConsole();
GUI.mainMenu();
Console.ReadLine();

#endregion

#region Functions

#region Language

static bool locFileAvailable(string file)
{
    bool check = false;

    if (Directory.Exists(GLOBAL.locFolder) == true)
    {
        if (File.Exists(file) == true)
        {
            check = true;
        }
    }

    return check;
}

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
        Console.Clear();
        Console.WriteLine("ERROR: Problem detected reading or deserializing JSON file: " + ex);
        Console.WriteLine("Closing program... \n");
        Thread.Sleep(2000);
        Environment.Exit(1);
        return null;
    }
}

static void locLoad(string file)
{
    if (locFileAvailable(file) == true)
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
            Console.Clear();
            Console.WriteLine("\nERROR: Null JSON Object.");
            Console.WriteLine("Closing program... \n");
            Thread.Sleep(2000);
            Environment.Exit(1);
        }
    }
    else
    {
        Console.Clear();
        Console.WriteLine("\nERROR: Unable to open localization file.");
        Console.WriteLine("Closing program... \n");
        Thread.Sleep(2000);
        Environment.Exit(1);
    }
}

#endregion

#endregion
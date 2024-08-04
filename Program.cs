#region Libraries and Namespaces

using System.Text.Json;
using System.Reflection;
using System.Text.RegularExpressions;

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
menuHandler();

#endregion

#region Functions

#region Checks and Files

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

static bool locCheckValues(object value)
{
    bool check = false;

    if (value != null)
    {
        if (value.GetType() == typeof(string))
        {
            if (String.IsNullOrEmpty((string)value) == false)
            {
                check = true;
            }
            else
            {
                showError(true,"Language file has at least one empty textline");
            }
        }
        else
        {
            showError(true,"Internal temporal language object not set properly");
        }
    }
    else
    {
        showError(true,"Language file lacks at least one textline");
    }

    return check;
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
                object tempValue = property.GetValue(tempObject);
                if (locCheckValues(tempValue) == true)
                {
                    staticProperty.SetValue(null,tempValue);
                }
                else
                {
                    showError(true,"Unexpected language related error");
                }
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

#region Menu Handling

static int inputOption(int optionsCount)
{
    bool check = false;
    string consoleRead = "";
    int option = 0;

    while (check == false)
    {
        GUI.makeInput();
        consoleRead = Console.ReadLine();
        consoleRead = Regex.Replace(consoleRead, @"\D", "");
        consoleRead.Trim();
        bool parse = int.TryParse(consoleRead, out option);

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

static string getString()
{
    bool check = false;
    string pattern = @"^[\p{L}\d\s.!¡?¿'-_()]+$";
    string str = "";

    while (check == false)
    {
        GUI.inputString();
        str = Console.ReadLine();
        str = str.Trim();
        if (Regex.IsMatch(str,pattern) == false)
        {
            GUI.invalidString();
            sndManager.fxPlay(0);
        }
        else
        {
            check = true;
            sndManager.fxPlay(1);
        }
    }

    return str;
}

static void menuHandler()
{
    while (true)
    {
        switch(GLOBAL.menuState)
        {
            case "mainMenu":
            {
                mainMenu();
                break;
            }

            #region Play

            case "playMenu":
            {
                int[] playerListIndex = playMenu();
                if ((playerListIndex[0] != -1) && (playerListIndex[1] != -1))
                {
                    playCombat(playerListIndex);
                }
                break;
            }

            #endregion

            #region Characters

            case "charMenu":
            {
                charMenu();
                break;
            }
            case "charCustomMenu":
            {
                createCustomChar();
                GLOBAL.menuState = "charMenu";
                break;
            }
            case "charRandomMenu":
            {
                charRandomMenu();
                break;
            }
            case "charBrowseMenu":
            {
                int listIndex = charBrowseMenu();
                if (listIndex != -1)
                {
                    do
                    {
                        listIndex = charEditMenu(listIndex,characterManager.charactersList[listIndex]);
                    } while (listIndex != -1);
                }
                break;
            }
            case "resetCharactersMenu":
            {
                resetCharactersMenu();
                break;
            }

            #endregion

            #region Options

            case "optionsMenu":
            {
                optionsMenu();
                break;
            }
            case "optionsLanguageChange":
            {
                optionsLanguageChange();
                break;
            }
            case "optionsDmgAdjMenu":
            {
                optionsDmgAdjMenu();
                break;
            }
            case "optionsAttPLvlMenu":
            {
                optionsAttPLvlMenu();
                break;
            }
            case "optionsTxtColorMenu":
            {
                optionsTxtColorMenu();
                break;
            }

            #endregion

            default:
            {
                mainMenu();
                break;
            }
        }
    }
}

static void mainMenu()
{
   
    GUI.mainMenu();
    int option = inputOption(4);
    switch (option)
    {
        case 1:
        {
            GLOBAL.menuState = "playMenu";
            break;
        }
        case 2:
        {
            GLOBAL.menuState = "charMenu";
            break;
        }
        case 3:
        {
            GLOBAL.menuState = "optionsMenu";
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

#endregion

#region Play

static int[] playMenu()
{
    int charCount = characterManager.charactersList.Count;
    int[] playerListIndex = [-1,-1];

    if (charCount >= 2)
    {

        GUI.playMenu(true);
        listCharacters(charCount,-1);

        int option = inputOption(charCount + 1);
        if (option == (charCount + 1))
        {
            GLOBAL.menuState = "mainMenu";
            return playerListIndex;
        }
        else
        {
            playerListIndex[0] = (option - 1);
            GUI.playMenu(false);
            listCharacters(charCount,playerListIndex[0]);

            option = inputOption(charCount + 1);
            while((option - 1) == playerListIndex[0])
            {
                GUI.playAlreadySelected();
                sndManager.fxPlay(0);
                option = inputOption(charCount + 1);
            }

            if (option == (charCount + 1))
            {
                GLOBAL.menuState = "mainMenu";
                return playerListIndex;
            }
            else
            {
                playerListIndex[1] = (option - 1);
                return playerListIndex;
            }
        }

    }
    else
    {
        noCharacters();
        return playerListIndex;
    }
}

#region Combat

static void playCombat(int[] playerIndex)
{
    playerCharacter player1 = characterManager.charactersList[playerIndex[0]];
    playerCharacter player2 = characterManager.charactersList[playerIndex[1]];
    GUI.playStartCombat(player1,player2);
    int option = inputOption(2);
    if (option == 2)
    {
        GLOBAL.menuState = "playMenu";
        return;
    }

    int first = 0;
    float initiative1 = player1.turnInitiative();
    float initiative2 = player2.turnInitiative();
    if (initiative1 > initiative2) {first = 1;} else {first = 2;}
    int winner = combatLoop(first,player1,player2);

    GUI.playWinner(winner,player1.cName,player2.cName);
    option = inputOption(2);
    if (winner == 1)
    {
        player1.takeVictory();
        player2.takeLoss();
        if (option == 1)
        {
            killPlayer(player2);
        }
    }
    if (winner == 2)
    {
        player2.takeVictory();
        player1.takeLoss();
        if (option == 1)
        {
            killPlayer(player1);
        }
    }

    sndManager.switchMusic();
    GLOBAL.menuState = "mainMenu";
}

static int combatLoop(int turnOrder, playerCharacter player1, playerCharacter player2)
{
    int winner = 0;
    
    GUI.refresh();
    sndManager.switchMusic();
    while((player1.cCurrenthp > 0) && (player2.cCurrenthp > 0))
    {
        if (turnOrder == 1)
        {
            float dmg = player1.dealDamage();
            float rmd = (float)GLOBAL.randomGen.Next(1,101);
            dmg *= rmd;
            player2.takeDamage(dmg);
            sndManager.fxPlay(player1.attackFx());
            winner = 1;
            turnOrder = 2;
        }
        else
        {
            float dmg = player2.dealDamage();
            float rmd = (float)GLOBAL.randomGen.Next(1,101);
            dmg *= rmd;
            player1.takeDamage(dmg);
            sndManager.fxPlay(player2.attackFx());
            winner = 2;
            turnOrder = 1;
        }

        GUI.combatTurn(player2,player1,turnOrder);
        Console.ReadLine();
    }

    player1.cCurrenthp = player1.MAX_HP;
    player2.cCurrenthp = player2.MAX_HP;
    return winner;
}

static void killPlayer(playerCharacter player)
{
    player.deleteChar();
    player = null;
    GC.Collect();
    GC.WaitForPendingFinalizers();
    sndManager.fxPlay(7);
    GUI.killed();
    Console.ReadLine();
}

#endregion

#endregion

#region Characters

static void charMenu()
{

    GUI.charMenu();
    int option = inputOption(5);
    switch (option)
    {
        case 1:
        {
            GLOBAL.menuState = "charCustomMenu";
            return;
        }
        case 2:
        {
            GLOBAL.menuState = "charRandomMenu";
            return;
        }
        case 3:
        {
            GLOBAL.menuState = "charBrowseMenu";
            return;
        }
        case 4:
        {
            GLOBAL.menuState = "resetCharactersMenu";
            return;
        }
        case 5:
        {
            GLOBAL.menuState = "mainMenu";
            return;
        }
    }

}

static unsafe void createCustomChar()
{
    characterManager.dummyChar character = new characterManager.dummyChar();
    character.iniValues();
    characterManager.dummyChar *pointer = &character;

    int option;
    do
    {
        option = charCustomMenu(pointer);
        switch(option)
        {
            case 1:
            {
                bool done = false;
                do
                {
                    done = charCustomInfo(pointer);
                } while (done == false);
                break;
            }
            case 2:
            {
                GUI.refresh();
                bool done = false;
                do
                {
                    done = charCustomStatistics(pointer);
                } while (done == false);
                break;
            }
            case 3:
            {
                if (charCustomValidate(pointer) == true)
                {
                    playerCharacter customCharacter = new playerCharacter((*pointer).cName,(*pointer).cNickname,(*pointer).AGE,(*pointer).CLASS,(*pointer).MAX_HP,(*pointer).SPEED,(*pointer).STRENGH,(*pointer).DEXTERITY,(*pointer).ARMOR,Guid.NewGuid(),0,1,0);
                    customCharacter.saveChar(true);
                    characterManager.charactersList.Add(customCharacter);
                    sndManager.fxPlay(8);
                    option = 4;
                }
                else
                {
                    GUI.charInvalidChar();
                    Console.ReadLine();
                }
                break;
            }
        }
    } while (option != 4);

    pointer = null;
    character = null;
    GC.Collect();
    GC.WaitForPendingFinalizers();
}

static unsafe int charCustomMenu(characterManager.dummyChar *character)
{
    GUI.charCustomMenu((*character).cName,(*character).cNickname,(*character).AGE,(*character).CLASS,(*character).MAX_HP,(*character).SPEED,(*character).STRENGH,(*character).DEXTERITY,(*character).ARMOR);
    int option = inputOption(4);
    return option;
}

static unsafe bool charCustomInfo(characterManager.dummyChar *character)
{
    GUI.charCustomInfo((*character).cName,(*character).cNickname,(*character).AGE,(*character).CLASS);
    int option = inputOption(5);
    switch(option)
    {
        case 1:
        {
            (*character).cName = getName();
            return false;
        }
        case 2:
        {
            (*character).cNickname = getName();
            return false;
        }
        case 3:
        {
            (*character).AGE = inputOption(300);
            return false;
        }
        case 4:
        {
            switch((*character).CLASS)
            {
                case "":
                {
                    (*character).CLASS = "Scout";
                    break;
                }
                case "Scout":
                {
                    (*character).CLASS = "Rogue";
                    break;
                }
                case "Rogue":
                {
                    (*character).CLASS = "Knight";
                    break;
                }
                case "Knight":
                {
                    (*character).CLASS = "Barbarian";
                    break;
                }
                case "Barbarian":
                {
                    (*character).CLASS = "";
                    break;
                }
            }
            return false;
        }
        case 5:
        {
            return true;
        }
    }
    return true;
}

static unsafe bool charCustomStatistics(characterManager.dummyChar *character)
{
    GUI.charCustomStatistics((*character).MAX_HP,(*character).SPEED,(*character).STRENGH,(*character).DEXTERITY,(*character).ARMOR,(*character).availablePoints);
    int option = inputOption(11);
    switch(option)
    {
        case 1:
        {
            if ((-(GLOBAL.charCreation.hpCost * 10) + (*character).availablePoints) >= 0)
            {
                (*character).MAX_HP += 10;
                (*character).availablePoints -= (GLOBAL.charCreation.hpCost * 10);
            }
            return false;
        }
        case 2:
        {
            if (((*character).MAX_HP - 1) > 100)
            {
                (*character).MAX_HP -= 10;
                (*character).availablePoints += (GLOBAL.charCreation.hpCost * 10);
            }
            return false;
        }
        case 3:
        {
            if ((-GLOBAL.charCreation.spdCost + (*character).availablePoints) >= 0)
            {
                (*character).SPEED ++;
                (*character).availablePoints -= GLOBAL.charCreation.spdCost;
            }
            return false;
        }
        case 4:
        {
            if (((*character).SPEED - 1) > 1)
            {
                (*character).SPEED --;
                (*character).availablePoints += GLOBAL.charCreation.spdCost;
            }
            return false;
        }
        case 5:
        {
            if ((-GLOBAL.charCreation.dexCost + (*character).availablePoints) >= 0)
            {
                (*character).DEXTERITY ++;
                (*character).availablePoints -= GLOBAL.charCreation.dexCost;
            }
            return false;
        }
        case 6:
        {
            if (((*character).DEXTERITY - 1) > 1)
            {
                (*character).DEXTERITY --;
                (*character).availablePoints += GLOBAL.charCreation.dexCost;
            }
            return false;
        }
        case 7:
        {
            if ((-GLOBAL.charCreation.strCost + (*character).availablePoints) >= 0)
            {
                (*character).STRENGH ++;
                (*character).availablePoints -= GLOBAL.charCreation.strCost;
            }
            return false;
        }
        case 8:
        {
            if (((*character).STRENGH - 1) > 1)
            {
                (*character).STRENGH --;
                (*character).availablePoints += GLOBAL.charCreation.strCost;
            }
            return false;
        }
        case 9:
        {
            if ((-GLOBAL.charCreation.armorCost + (*character).availablePoints) >= 0)
            {
                (*character).ARMOR ++;
                (*character).availablePoints -= GLOBAL.charCreation.armorCost;
            }
            return false;
        }
        case 10:
        {
            if (((*character).ARMOR - 1) > 1)
            {
                (*character).ARMOR --;
                (*character).availablePoints += GLOBAL.charCreation.armorCost;
            }
            return false;
        }
        case 11:
        {
            return true;
        }
    }
    return true;
}

static unsafe bool charCustomValidate(characterManager.dummyChar *character)
{
    if (String.IsNullOrEmpty((*character).cName) == false)
    {
        if (String.IsNullOrEmpty((*character).cNickname) == false)
        {
            if ((*character).AGE > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }
}

static void charRandomMenu()
{
    GUI.charRmdMenu();
    int count = inputOption(1000);
    characterManager.makeRmdChar(count);
    GUI.charSuccesRdm();
    Console.ReadLine();
    GLOBAL.menuState = "charMenu";
}

static int charBrowseMenu()
{
    int charCount = characterManager.charactersList.Count;
    if (charCount >= 1)
    {

        GUI.charBrowseMenu();
        listCharacters(charCount,-1);

        int option = inputOption(charCount + 1);
        if (option == (charCount + 1))
        {
            GLOBAL.menuState = "charMenu";
            return -1;
        }
        else
        {
            return (option - 1);
        }

    }
    else
    {
        noCharacters();
        return -1;
    }
}

static void listCharacters(int charCount, int mark)
{
    characterManager.sortListbyVictories();
    for (int i = 0; i < charCount; i++)
    {
        if (i == mark)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            GUI.charListItem(i,characterManager.charactersList[i]);
            Console.ForegroundColor = GLOBAL.textColor;
        }
        else
        {
            GUI.charListItem(i,characterManager.charactersList[i]);
        }
    }
    GUI.charEndBrowse(charCount + 1);
}

static int charEditMenu(int item, playerCharacter character)
{
    GUI.charShowInfo(item,character);
    GUI.charEditMenu();
    int option = inputOption(4);

    switch(option)
    {
        case 1:
        {
            GUI.charChangeName(true);
            string name = getName();
            character.cName = name;
            character.saveChar(false);
            GLOBAL.menuState = "charEditMenu";
            return item;
        }
        case 2:
        {
            GUI.charChangeName(false);
            string nick = getName();
            character.cNickname = nick;
            character.saveChar(false);
            GLOBAL.menuState = "charEditMenu";
            return item;
        }
        case 3:
        {
            GUI.charDeleteConfirm(item,character);
            if (inputOption(2) == 1)
            {
                character.deleteChar();
                character = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GLOBAL.menuState = "charBrowseMenu";
                return -1;
            }
            else
            {
                GLOBAL.menuState = "charEditMenu";
                return item;
            }
        }
        case 4:
        {
            GLOBAL.menuState = "charBrowseMenu";
            return -1;
        }
    }
    return -1;
}

static string getName()
{
    bool check = false;
    string name = "";

    while (check == false)
    {
        name = getString();
        if (characterManager.isValidName(name) == false)
        {
            GUI.invalidString();
        }
        else
        {
            check = true;
        }
    }

    return name;
}

static void resetCharactersMenu()
{
    if (characterManager.charactersList.Count >= 1)
    {
        GUI.charResetConfirm();
        if (inputOption(2) == 1)
        {
            characterManager.resetCharacters();
        }
        GLOBAL.menuState = "charMenu";
        return;
    }
    else
    {
        noCharacters();
        return;
    }
}

static void noCharacters()
{
    sndManager.fxPlay(0);
    GUI.charNoCharacters();
    Console.ReadLine();
    GLOBAL.menuState = "charMenu";
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
            GLOBAL.menuState = "optionsLanguageChange";
            return;
        }
        case 2:
        {
            GLOBAL.menuState = "optionsDmgAdjMenu";
            return;
        }
        case 3:
        {
            GLOBAL.menuState = "optionsAttPLvlMenu";
            return;
        }
        case 4:
        {
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 5:
        {
            GLOBAL.menuState = "mainMenu";
            return;
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
    GLOBAL.menuState = "optionsMenu";
}

static void optionsDmgAdjMenu()
{
    GUI.changeDmgAdj();
    GLOBAL.dmgAdjust = getValidDmgAdj();
    configIni.saveData("dmgAdjust",GLOBAL.dmgAdjust.ToString());
    GLOBAL.menuState = "optionsMenu";
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
    GLOBAL.menuState = "optionsMenu";
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
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 2:
        {
            Console.ForegroundColor = ConsoleColor.Green;
            configIni.saveData("textColor","green");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 3:
        {
            Console.ForegroundColor = ConsoleColor.Red;
            configIni.saveData("textColor","red");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 4:
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            configIni.saveData("textColor","blue");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 5:
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            configIni.saveData("textColor","yellow");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 6:
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            configIni.saveData("textColor","cyan");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 7:
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            configIni.saveData("textColor","magenta");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 8:
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            configIni.saveData("textColor","dkgreen");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 9:
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            configIni.saveData("textColor","dkred");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 10:
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            configIni.saveData("textColor","dkblue");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 11:
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            configIni.saveData("textColor","dkyellow");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 12:
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            configIni.saveData("textColor","dkcyan");
            GLOBAL.menuState = "optionsTxtColorMenu";
            return;
        }
        case 13:
        {
            GLOBAL.menuState = "optionsMenu";
            return;
        }
    }
}

#endregion

#endregion


#endregion
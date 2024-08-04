using System.Text.Json;
using System.Text.Json.Serialization;
using namespaceGlobal;

namespace namespaceCharacter
{

    public class playerCharacter
    {

        #region Character Data

        private Guid ID;
        public Guid id {get => ID;}
        private int cVictories;
        public int VICTORIES {get => cVictories;}

        #region Personal information

        public string cName {get; set;}
        private string cClass;
        public string cNickname {get; set;}
        private DateTime cBirth;
        private int cAge;

        #region Getter & Setter

        public string CLASS {get => cClass;}
        public DateTime BIRTH {get => cBirth;}
        public int AGE {get => cAge;}

        #endregion

        #endregion

        #region Physical Attributes

        private int cLevel;
        private float cXp;
        private float cMaxhp;
        public float cCurrenthp;
        private int cSpeed;
        private int cStrengh;
        private int cDexterity;
        private int cArmor;

        #region Getter & Setter

        public int LEVEL {get => cLevel;}
        public float XP {get => cXp;}
        public float MAX_HP {get => cMaxhp;}
        public float CURRENT_HP {get => cCurrenthp; set => cCurrenthp = value;}
        public int SPEED {get => cSpeed;}
        public int STRENGH {get => cStrengh;}
        public int DEXTERITY {get => cDexterity;}
        public int ARMOR {get => cArmor;}
        
        #endregion

        #endregion

        #endregion

        #region Character Methods

        public float turnInitiative()
        {
            float iniatiative;

            int rdmModifier = GLOBAL.randomGen.Next(1, 10);
            iniatiative = ((cSpeed * cSpeed * cDexterity * rdmModifier)/100);

            return iniatiative;
        }

        public float dealDamage()
        {
            float dmg;
            dmg = (cDexterity * cStrengh * cLevel);
            return dmg;
        }

        public void takeDamage(float dmg)
        {
            int defence = (cArmor * cSpeed);
            dmg = Math.Max((dmg - defence),100);
            cCurrenthp -= (dmg/GLOBAL.dmgAdjust);
        }

        public int attackFx()
        {
            switch(cClass)
            {
                case "Rogue":
                {
                    return 4;
                }
                case "Knight":
                {
                    return 5;
                }
                case "Barbarian":
                {
                    return 6;
                }
                default:
                {
                    return 3;
                }
            }
        }

        public void takeVictory()
        {
            cVictories ++;
            cXp += GLOBAL.LVLUP.xpPerVic;
            if (xpThreshold(cXp,cLevel) == true)
            {
                levelUp();
                cXp = 0;
            }
        }

        public void takeLoss()
        {
            if ((cXp - GLOBAL.LVLUP.xpPerLos) > 0) {cXp -= GLOBAL.LVLUP.xpPerLos;}
            cVictories--;
            saveChar(false);
        }

        public void levelUp()
        {
            cLevel ++;
            for (int i = 0; i < GLOBAL.LVLUP.attributesPerLevel; i++)
            {
                int attributeChance = GLOBAL.randomGen.Next(0, 101);
                int chancePointer = 0;

                switch (cClass)
                {

                    case "Scout":
                    {
                        cMaxhp += GLOBAL.LVLUP.SCOUT.hpIncrease;
                        cCurrenthp = cMaxhp;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.SCOUT.speedChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.SCOUT.speedIncrease;
                            cSpeed += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.SCOUT.speedChance;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.SCOUT.dexChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.SCOUT.dexIncrease;
                            cDexterity += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.SCOUT.dexChance;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.SCOUT.strChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.SCOUT.strIncrease;
                            cStrengh += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.SCOUT.strChance;

                        if ((attributeChance >= chancePointer) && (attributeChance <= (chancePointer + GLOBAL.LVLUP.SCOUT.armorChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.SCOUT.armorIncrease;
                            cArmor += attributeIncrement;
                        }

                        break;
                    }

                    case "Rogue":
                    {
                        cMaxhp += GLOBAL.LVLUP.ROGUE.hpIncrease;
                        cCurrenthp = cMaxhp;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.ROGUE.speedChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.ROGUE.speedIncrease;
                            cSpeed += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.ROGUE.speedChance;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.ROGUE.dexChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.ROGUE.dexIncrease;
                            cDexterity += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.ROGUE.dexChance;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.ROGUE.strChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.ROGUE.strIncrease;
                            cStrengh += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.ROGUE.strChance;

                        if ((attributeChance >= chancePointer) && (attributeChance <= (chancePointer + GLOBAL.LVLUP.ROGUE.armorChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.ROGUE.armorIncrease;
                            cArmor += attributeIncrement;
                        }

                        break;
                    }

                    case "Knight":
                    {
                        cMaxhp += GLOBAL.LVLUP.KNIGHT.hpIncrease;
                        cCurrenthp = cMaxhp;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.KNIGHT.speedChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.KNIGHT.speedIncrease;
                            cSpeed += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.KNIGHT.speedChance;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.KNIGHT.dexChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.KNIGHT.dexIncrease;
                            cDexterity += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.KNIGHT.dexChance;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.KNIGHT.strChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.KNIGHT.strIncrease;
                            cStrengh += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.KNIGHT.strChance;

                        if ((attributeChance >= chancePointer) && (attributeChance <= (chancePointer + GLOBAL.LVLUP.KNIGHT.armorChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.KNIGHT.armorIncrease;
                            cArmor += attributeIncrement;
                        }

                        break;
                    }

                    case "Barbarian":
                    {
                        cMaxhp += GLOBAL.LVLUP.BARBARIAN.hpIncrease;
                        cCurrenthp = cMaxhp;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.BARBARIAN.speedChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.BARBARIAN.speedIncrease;
                            cSpeed += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.BARBARIAN.speedChance;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.BARBARIAN.dexChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.BARBARIAN.dexIncrease;
                            cDexterity += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.BARBARIAN.dexChance;

                        if ((attributeChance >= chancePointer) && (attributeChance < (chancePointer + GLOBAL.LVLUP.BARBARIAN.strChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.BARBARIAN.strIncrease;
                            cStrengh += attributeIncrement;
                        }
                        chancePointer += GLOBAL.LVLUP.BARBARIAN.strChance;

                        if ((attributeChance >= chancePointer) && (attributeChance <= (chancePointer + GLOBAL.LVLUP.BARBARIAN.armorChance)))
                        {
                            int attributeIncrement = GLOBAL.LVLUP.BARBARIAN.armorIncrease;
                            cArmor += attributeIncrement;
                        }

                        break;
                    }

                }
            }

            saveChar(false);
        }

        public void saveChar(bool addToIndex)
        {
            string fileName = (GLOBAL.charFolder + "/Char_" + ID.ToString() + ".json");
            string jsonString = JsonSerializer.Serialize(this,new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName,jsonString);

            if (addToIndex == true)
            {
                StreamWriter index = new StreamWriter(GLOBAL.charIndex, append: true);
                using (index)
                {
                    index.WriteLine(ID.ToString());
                }
            }
        }

        public void deleteChar()
        {
            File.Delete(GLOBAL.charFolder + "/Char_" + ID.ToString() + ".json");
            string[] updatedIndex = File.ReadAllLines(GLOBAL.charIndex);
            updatedIndex = updatedIndex.Where(line => !line.Contains(ID.ToString())).ToArray();
            File.WriteAllLines(GLOBAL.charIndex,updatedIndex);
            characterManager.charactersList.Remove(this);
        }

        private bool xpThreshold(float xp, int level)
        {
            bool levelup = false;
            double threshold = Math.Round(Math.Pow((double)(level),1.5));
            if (xp >= threshold)
            {
                levelup = true;
            }

            return levelup;
        }

        #endregion

        #region Constructor
        public playerCharacter(string name, string nick, int age, string _class, float hp, int speed, int str, int dex, int armor, Guid id, int victories, int level, float xp)
        {

            ID = id;
            cVictories = victories;
            cName = name;
            cNickname = nick;
            cAge = age;
            cLevel = level;
            cXp = xp;
            cMaxhp = hp;
            cCurrenthp = cMaxhp;
            cSpeed = speed;
            cStrengh = str;
            cDexterity = dex;
            cArmor = armor;
            cBirth = DateTime.Now.AddYears(-(cAge));

            #region Class

            if (String.IsNullOrEmpty(_class) == true)
            {
                int bigger = int.Max(cSpeed,cDexterity);
                bigger = int.Max(bigger,cStrengh);
                bigger = int.Max(bigger,cArmor);

                if (bigger == cSpeed) {cClass = "Scout";}
                if (bigger == cDexterity) {cClass = "Rogue";}
                if (bigger == cStrengh) {cClass = "Knight";}
                if (bigger == cArmor) {cClass = "Barbarian";}
            }
            else
            {
                cClass = _class;
            }

            #endregion

        }
        #endregion

    }

    public static class characterManager
    {

        public static List<playerCharacter> charactersList = new List<playerCharacter>();

        public static void sortListbyVictories()
        {
            charactersList.Sort((character1,character2) => character2.VICTORIES.CompareTo(character1.VICTORIES));
        }
        
        public static void resetCharacters()
        {
            for (int i = (charactersList.Count - 1); i >= 0; i--)
            {
                playerCharacter character = charactersList[i];
                character.deleteChar();
                charactersList.Remove(character);
                character = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static bool isValidName(string name)
        {
            bool check = false;

            if (String.IsNullOrEmpty(name) == false)
            {
                if (name.Length < 30)
                {
                    check = true;
                }
            }

            return check;
        }

        #region Loading Characters

        public class dummyChar
        {

            public int VICTORIES {get; set;}

            #region Personal information

            public string cName {get; set;}
            public string CLASS {get; set;}
            public string cNickname {get; set;}
            public DateTime BIRTH {get; set;}
            public int AGE {get; set;}

            #endregion

            #region Physical Attributes

            public int LEVEL {get; set;}
            public int XP {get; set;}
            public float MAX_HP {get; set;}
            public float CURRENT_HP {get; set;}
            public int SPEED {get; set;}
            public int STRENGH {get; set;}
            public int DEXTERITY {get; set;}
            public int ARMOR {get; set;}
            
            #endregion

            public double availablePoints;

            public void iniValues()
            {
                availablePoints = GLOBAL.charCreation.pointsGiven;
                cName = "";
                cNickname = "";
                CLASS = "";
                AGE = 0;
                MAX_HP = 100;
                SPEED = 1;
                STRENGH = 1;
                DEXTERITY = 1;
                ARMOR = 1;
            }

        }

        private static void loadChar(string charID)
        {

            playerCharacter character;
            dummyChar dummy;
            string jsonString;
            string file = (GLOBAL.charFolder + "/Char_" + charID + ".json");
            Guid ID;

            jsonString = File.ReadAllText(file);
            dummy = JsonSerializer.Deserialize<dummyChar>(jsonString);
            Guid.TryParse(charID, out ID);
            character = new playerCharacter(dummy.cName,dummy.cNickname,dummy.AGE,dummy.CLASS,dummy.MAX_HP,dummy.SPEED,dummy.STRENGH,dummy.DEXTERITY,dummy.ARMOR,ID,dummy.VICTORIES,dummy.LEVEL,dummy.XP);
            charactersList.Add(character);

        }

        public static void iniCharcters()
        {
            string[] index = File.ReadAllLines(GLOBAL.charIndex);
            foreach (string item in index)
            {
                if (String.IsNullOrEmpty(item) == false)
                {
                    loadChar(item);
                }
            }
        }

        #endregion

        #region Random Characters Creation
        public static void makeRmdChar(int count)
        {
            personalInfo data = generatePersonalInfo(count);

            for (int i = 0; i < count; i++)
            {
                int age = GLOBAL.randomGen.Next(18,300);
                int[] stats = assignPoints();
                string name = (data.info[i].name.title + " " + data.info[i].name.first + " " + data.info[i].name.last);

                playerCharacter character = new playerCharacter(name,data.info[i].nick.nick,age,"",stats[0],stats[1],stats[2],stats[3],stats[4],Guid.NewGuid(),0,1,0);
                character.saveChar(true);
                charactersList.Add(character);
            }
        }

        private static int[] assignPoints()
        {
            int[] stats = [100,1,1,1,1];
            double points = GLOBAL.charCreation.pointsGiven;

            while (points > 0)
            {
                int rdm = GLOBAL.randomGen.Next(1,6);
                switch(rdm)
                {
                    case 1:
                    {
                        stats[0] += 10;
                        points -= (GLOBAL.charCreation.hpCost * 10);
                        break;
                    }
                    case 2:
                    {
                        stats[1]++;
                        points -= GLOBAL.charCreation.spdCost;
                        break;
                    }
                    case 3:
                    {
                        stats[2]++;
                        points -= GLOBAL.charCreation.strCost;
                        break;
                    }
                    case 4:
                    {
                        stats[3]++;
                        points -= GLOBAL.charCreation.dexCost;
                        break;
                    }
                    case 5:
                    {
                        stats[4]++;
                        points -= GLOBAL.charCreation.armorCost;
                        break;
                    }
                }
            }

            return stats;
        }

        private static personalInfo generatePersonalInfo(int count)
        {
            personalInfo personalInformation;

            if (GLOBAL.internetConnection == true)
            {
                HttpClient client = new HttpClient();
                try
                {
                    using (client)
                    {
                        string data = client.GetStringAsync("https://randomuser.me/api/?inc=name,login&nat=mx,us,br&noinfo&results=" + count.ToString()).Result;
                        personalInformation = JsonSerializer.Deserialize<personalInfo>(data);
                        return personalInformation;
                    }
                }
                catch
                {
                    return noInternetInfo(count);
                }
            }
            else
            {
                return noInternetInfo(count);
            }
        }

        private static personalInfo noInternetInfo(int count)
        {
            personalInfo personalInformation = new personalInfo();
            personalInformation.info = new List<Result>();
            for (int i = 0; i < count; i++)
            {
                personalInformation.info.Add(new Result());
            }

            foreach (Result item in personalInformation.info)
            {
                item.name = new Name();
                item.nick = new Login();
                item.name.title = "Mr.";
                item.name.first = "Juan";
                item.name.last = "Comun";
                item.nick.nick = "J.J.";
            }

            return personalInformation;
        }

        #region Personal Information API Classes

        public class Login
        {
            [JsonPropertyName("username")]
            public string? nick {get; set;}
        }

        public class Name
        {
            public string? title {get; set;}
            public string? first {get; set;}
            public string? last {get; set;}
        }

        public class Result
        {
            [JsonPropertyName("name")]
            public Name? name {get; set;}
            [JsonPropertyName("login")]
            public Login? nick {get; set;}
        }

        private class personalInfo
        {
            [JsonPropertyName("results")]
            public List<Result>? info {get; set;}
        }

        #endregion

        #endregion

    }

}
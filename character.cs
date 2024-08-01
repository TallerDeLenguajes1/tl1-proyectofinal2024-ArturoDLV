using namespaceGlobal;

namespace namespaceCharacter
{

    public class playerCharacter
    {

        #region Character Data

        #region Personal information

        private string cName;
        private string cClass;
        private string cNickname;
        private DateTime cBirth;
        private int cAge;

        #region Getter & Setter
        public string CName {get => cName; set => cName = value;}
        public string CClass {get => cClass;}
        public string CNickname {get => cNickname; set => cNickname = value;}
        public DateTime CBirth {get => cBirth;}
        public int CAge {get => cAge;}
        #endregion

        #endregion

        #region Physical Attributes

        private int cLevel;
        private int cXp = 0;
        private float cMaxhp;
        private float cCurrenthp;
        private int cSpeed;
        private int cStrengh;
        private int cDexterity;
        private int cArmor;

        #region Getter & Setter
        public int CLevel {get => cLevel;}
        public int CXp {get => cXp; set => cXp = value;}
        public float CMaxhp {get => cMaxhp;}
        public float CCurrenthp {get => cCurrenthp;}
        public int CSpeed {get => cSpeed;}
        public int CStrengh {get => cStrengh;}
        public int CDexterity {get => cDexterity;}
        public int CArmor {get => cArmor;}
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
            cCurrenthp -= ((dmg - defence)/GLOBAL.dmgAdjust);
        }

        public void levelUp()
        {
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
        }

        #endregion

        #region Constructor
        public playerCharacter(string name, string nick, int age, string _class, float hp, int speed, int str, int dex, int armor)
        {

            cName = name;
            cNickname = nick;
            cAge = age;
            cLevel = 1;
            cMaxhp = hp;
            cCurrenthp = CMaxhp;
            cSpeed = speed;
            cStrengh = str;
            cDexterity = dex;
            cArmor = armor;
            cBirth = DateTime.Now.AddYears(-(CAge));

            #region Class
            //Assign a descriptive class if none is provided

            if (_class == "")
            {

                int biggerN = 0;
                cClass = "";
                
                if (CSpeed >= biggerN)
                {
                    biggerN = CSpeed;
                    cClass = "Scout";
                }
                if ((CDexterity * 2) >= biggerN)
                {
                    biggerN = (CDexterity * 2);
                    cClass = "Rogue";
                }
                if (CArmor >= biggerN)
                {
                    biggerN = CArmor;
                    cClass = "Knight";
                }
                if (CStrengh >= biggerN)
                {
                    biggerN = CStrengh;
                    cClass = "Barbarian";
                }

            }
            else
            {
                cClass = _class;
            }

            #endregion

        }
        #endregion

    }

}
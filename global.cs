namespace namespaceGlobal
{

    public static class GLOBAL
    {

        //Instances
        public static Random randomGen = new Random();

        //Constants
        public const float dmgAdjust = 500;

        //Level UP mechanic
        public static class LVLUP
        {

            public const int attributesPerLevel = 2;    //How many attributes must be increased per level up

            #region Character's class values
            public static class SCOUT
            {
                #region Chances
                //Chances for each attribute, from 0 to 100, must add up to 100
                public const int speedChance = 50;
                public const int dexChance = 25;
                public const int strChance = 15;
                public const int armorChance = 10;
                #endregion

                #region Ammounts
                //By how much can each attribute increase
                public const float hpIncrease = 5;
                public const int speedIncrease = 5;
                public const int dexIncrease = 5;
                public const int strIncrease = 1;
                public const int armorIncrease = 1;
                #endregion
            }

            public static class ROGUE
            {
                #region Chances
                //Chances for each attribute, from 0 to 100, must add up to 100
                public const int speedChance = 25;
                public const int dexChance = 25;
                public const int strChance = 25;
                public const int armorChance = 25;
                #endregion

                #region Ammounts
                //By how much can each attribute increase
                public const float hpIncrease = 10;
                public const int speedIncrease = 2;
                public const int dexIncrease = 2;
                public const int strIncrease = 2;
                public const int armorIncrease = 2;
                #endregion
            }

            public static class KNIGHT
            {
                #region Chances
                //Chances for each attribute, from 0 to 100, must add up to 100
                public const int speedChance = 5;
                public const int dexChance = 10;
                public const int strChance = 35;
                public const int armorChance = 50;
                #endregion

                #region Ammounts
                //By how much can each attribute increase
                public const float hpIncrease = 30;
                public const int speedIncrease = 1;
                public const int dexIncrease = 1;
                public const int strIncrease = 2;
                public const int armorIncrease = 5;
                #endregion
            }

            public static class BARBARIAN
            {
                #region Chances
                //Chances for each attribute, from 0 to 100, must add up to 100
                public const int speedChance = 3;
                public const int dexChance = 20;
                public const int strChance = 75;
                public const int armorChance = 2;
                #endregion

                #region Ammounts
                //By how much can each attribute increase
                public const float hpIncrease = 15;
                public const int speedIncrease = 1;
                public const int dexIncrease = 4;
                public const int strIncrease = 4;
                public const int armorIncrease = 1;
                #endregion
            }

            #endregion

        }

    }

}
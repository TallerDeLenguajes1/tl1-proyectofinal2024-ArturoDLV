namespace namespaceTexts
{
    
    public static class TXT
    {

        #region Text for the menus

        public static string? title {get; set;}
        public static string? welcome {get; set;}
        public static string? tutorial {get; set;}
        public static string? play {get; set;}
        public static string? characters {get; set;}
        public static string? options {get; set;}
        public static string? exit {get; set;}

        #endregion

    }

    #region Non-Static Duplicate
    public class DynamicTXT
    {
        public string? title {get; set;}
        public string? welcome {get; set;}
        public string? tutorial {get; set;}
        public string? play {get; set;}
        public string? characters {get; set;}
        public static string? options {get; set;}
        public string? exit {get; set;}
    }
    #endregion

}
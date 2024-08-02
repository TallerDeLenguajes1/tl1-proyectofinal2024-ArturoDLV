using namespaceGlobal;
using NAudio.Wave;

namespace namespaceSoundManager
{
    
    public static class sndManager
    {

        #region Music

        private static WaveOutEvent? outputDevice;
        private static AudioFileReader? menu;
        private static AudioFileReader? megalovania;
        private static bool isPlayingMenu;
        private static Thread? musicThread;
        private static bool threadLoop = true;

        public static void iniMusic()
        {
            menu = new AudioFileReader(GLOBAL.musMenu);
            megalovania = new AudioFileReader(GLOBAL.musMegalovania);
            outputDevice = new WaveOutEvent();

            isPlayingMenu = true;
            musicThread = new Thread(playMusicLoop);
            musicThread.IsBackground = true;
            musicThread.Start();
        }

        private static void playMusicLoop()
        {
            while(threadLoop == true)
            {
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }

                outputDevice.Stop();

                if (isPlayingMenu == true)
                {
                    outputDevice.Init(menu);
                    outputDevice.PlaybackStopped += (s, e) =>
                    {
                        menu.Position = 0;
                    };
                }
                else
                {
                    outputDevice.Init(megalovania);
                    outputDevice.PlaybackStopped += (s, e) =>
                    {
                        megalovania.Position = 0;
                    };
                }

                outputDevice.Play();

            }
        }

        public static void switchMusic()
        {
            outputDevice.Stop();

            if (isPlayingMenu)
            {
                megalovania.Position = 0;
            }
            else
            {
                menu.Position = 0;
            }

            isPlayingMenu = !isPlayingMenu;
        }

        public static void disposeMusic()
        {
            outputDevice.Stop();
            outputDevice.Dispose();
            menu.Dispose();
            megalovania.Dispose();
            threadLoop = false;
        }

        #endregion

    }

}
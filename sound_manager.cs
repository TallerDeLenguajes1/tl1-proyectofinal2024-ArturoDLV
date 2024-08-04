using namespaceGlobal;
using NAudio.Wave;

namespace namespaceSoundManager
{
    
    public static class sndManager
    {

        #region Sound Effects

        private static WaveOutEvent[] fxOutput = new WaveOutEvent[9];
        private static AudioFileReader[] fxFiles = new AudioFileReader[9];


        public static void iniFx()
        {

            for (int i = 0; i < 9; i++)
            {
                fxOutput[i] = new WaveOutEvent();
            }

            fxFiles[0] = new AudioFileReader(GLOBAL.fxError);
            fxFiles[1] = new AudioFileReader(GLOBAL.fxSelect);
            fxFiles[2] = new AudioFileReader(GLOBAL.fxOff);
            fxFiles[3] = new AudioFileReader(GLOBAL.fxHitScout);
            fxFiles[4] = new AudioFileReader(GLOBAL.fxHitRogue);
            fxFiles[5] = new AudioFileReader(GLOBAL.fxHitKnight);
            fxFiles[6] = new AudioFileReader(GLOBAL.fxHitBarbarian);
            fxFiles[7] = new AudioFileReader(GLOBAL.fxDeath);
            fxFiles[8] = new AudioFileReader(GLOBAL.fxVictory);

        }

        public static void fxPlay(int n)
        {
            fxOutput[n].Init(fxFiles[n]);
            fxFiles[n].Position = 0;
            fxOutput[n].Play();
        }

        public static void disposeFx()
        {

            foreach (WaveOutEvent item in fxOutput)
            {
                if (item != null)
                {
                    item.Stop();
                    item.Dispose();
                }
            }

            foreach (AudioFileReader item in fxFiles)
            {
                if (item != null)
                {
                    item.Dispose();
                }
            }

        }

        #endregion

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

                if ((menu != null) && (megalovania != null))
                {
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
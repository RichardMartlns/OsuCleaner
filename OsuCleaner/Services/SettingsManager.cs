



namespace OsuCleaner.Services
{
    public class SettingsManager
    {
        private static readonly Properties.Settings settings = Properties.Settings.Default;

        public string GetSavePath()
        {
            return settings.SongsFolderPath;
        }

        public void SavePath(string path)
        {
            settings.SongsFolderPath = path;
            settings.Save();
        }
    }
}

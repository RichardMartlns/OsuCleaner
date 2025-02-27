using System.IO;


namespace OsuCleaner.Services
{
    public class FileScanner
    {
        private string _rootDirectoryPath; // caminho da pasta raiz onde estao os maps
        private Dictionary<int, List<string>> _mapsByMode; // lista de arquivos .osu encontrados

        public FileScanner(string rootDirectoryPath)
        {
            _rootDirectoryPath = rootDirectoryPath;
            _mapsByMode = new Dictionary<int, List<string>>();
        }

        public void SearchMaps()
        {

            _mapsByMode.Clear(); // garante que a lista começa vazia


            if (!Directory.Exists(_rootDirectoryPath))
                throw new DirectoryNotFoundException("The specified root folder does not exist.");


            string[] folderMaps = Directory.GetDirectories(_rootDirectoryPath, "*", SearchOption.AllDirectories);


            foreach (string folder in folderMaps)
            {
                string[] fileOsu = Directory.GetFiles(folder, "*.osu");


                foreach (string file in fileOsu)
                {
                    try

                    { 
                        int? mode = OsuFileReader.GetModeFromOsuFile(file);


                        if (mode.HasValue)
                        {

                            if (!_mapsByMode.TryGetValue(mode.Value, out List<string>? value))
                            {
                                value = new List<string>();
                                _mapsByMode[mode.Value] = value;
                            }

                            value.Add(file);
                        }
                        else
                        {
                            Console.WriteLine($"Skipping file {file} due to invalid mode.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error when reading {file}: {ex.Message}");
                    }
    
                 }
            }
        }

        public Dictionary<int, List<string>> GetMapsByMode()
        {
            return new Dictionary<int, List<string>>(_mapsByMode);
        }

        public Dictionary<int, int> CountMapsByMode()
        {
            return _mapsByMode.ToDictionary(entry => entry.Key, entry => entry.Value.Count);
        }
    }
}

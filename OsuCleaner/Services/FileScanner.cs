using System.IO;
using OsuCleaner.Beatmap;

namespace OsuCleaner.Services
{
    
    public class FileScanner
    {

        private string _rootDirectoryPath;
        private Dictionary<int, List<BeatmapInfo>> _mapsByMode;

        
        public FileScanner(string rootDirectoryPath)
        {

            _rootDirectoryPath = rootDirectoryPath;
            _mapsByMode = new Dictionary<int, List<BeatmapInfo>>();

        }


        public void SearchMaps()
        {


            _mapsByMode.Clear();
            if (!Directory.Exists(_rootDirectoryPath)) return;


            foreach (string folder in Directory.GetDirectories(_rootDirectoryPath, "*", SearchOption.AllDirectories))
            {

                foreach (string file in Directory.GetFiles(folder, ".osu"))
                {

                    var beatmap = OsuFileReader.ReadOsuFile(file);
                    if (beatmap == null) continue;

                    if (!_mapsByMode.ContainsKey(int.Parse(beatmap.Mode)))
                        _mapsByMode[int.Parse(beatmap.Mode)] = new List<BeatmapInfo>();


                    _mapsByMode[int.Parse(beatmap.Mode)].Add(beatmap);
                }
            }
        }

        public Dictionary<int, List<BeatmapInfo>> GetMapsByMode() => new Dictionary<int, List<BeatmapInfo>>(_mapsByMode);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuCleaner.Beatmap
{
    public class BeatmapInfo
    {
        public string Title { get; set; } = "Unknown";
        public string Artist { get; set; } = "Unknown";
        public string Difficulty { get; set; } = "Unknown";


        public BeatmapInfo() { }
        public BeatmapInfo(string mode) => Mode = mode;

        public string Mode { get; set; } = "Unknown";
        public string FilePath { get; set; } = "Unknown";


        public override string ToString() => $"{Artist} - {Title} [{Difficulty}] (mode: {Mode})";

    }
}




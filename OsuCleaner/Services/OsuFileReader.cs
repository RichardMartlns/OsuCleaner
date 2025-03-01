using System.IO;
using OsuCleaner.Beatmap;


namespace OsuCleaner.Services
{

    public class OsuFileReader
    {

        public static BeatmapInfo? ReadOsuFile(string filePath)
        {

            if (!File.Exists(filePath)) return null;

            BeatmapInfo beatmap = new BeatmapInfo() { FilePath = filePath }; 


            try
            {

                using (var reader = new StreamReader(filePath))
                {

                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {

                        if (line.StartsWith("Title:", StringComparison.OrdinalIgnoreCase))
                            beatmap.Title = line.Split(':')[1].Trim();


                        else if (line.StartsWith("Artist:", StringComparison.OrdinalIgnoreCase))
                            beatmap.Artist = line.Split(':')[1].Trim();


                        else if (line.StartsWith("Version:", StringComparison.OrdinalIgnoreCase))
                            beatmap.Difficulty = line.Split(':')[1].Trim();


                        else if (line.StartsWith("Mode:", StringComparison.OrdinalIgnoreCase) &&
                                 int.TryParse(line.Split(':')[1].Trim(), out int mode))
                            beatmap.Mode = mode.ToString();


                        string mode1 = beatmap.Mode;
                        if (beatmap.Title == "Unknown" || beatmap.Artist == "Unknow" || beatmap.Difficulty == "Unknown" || beatmap.Mode == "Unknown")
                            continue;
                        break;
                    }
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
                return null;
            }

            return beatmap;
        }
    }
}
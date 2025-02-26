using System.IO;



namespace OsuCleaner.Services
{
    public class OsuFileReader
    {
        public static int GetModeFromOsuFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("file .osu not found.", filePath);


            foreach (string line in File.ReadLines(filePath))
            {
                if (line.StartsWith("Mode:"))
                {
                    if (int.TryParse(line.Split(':')[1].Trim(), out int mode))
                    {
                        return mode;
                    }
                }
            }


            return -1;
        }
    }
}

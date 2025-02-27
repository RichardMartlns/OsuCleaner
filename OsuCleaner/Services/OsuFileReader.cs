using System.IO;



namespace OsuCleaner.Services
{
    public class OsuFileReader
    {
        public static int? GetModeFromOsuFile(string filePath)
        {

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"The .osu file '{filePath}' cannot be read.");
                return null;
            }

            try
            {

                foreach (string line in File.ReadLines(filePath))
                {
                    if (line.StartsWith("Mode:", StringComparison.OrdinalIgnoreCase))
                    {
                        string[] parts = line.Split(':');

                        if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int mode))
                        {

                            if (mode >= 0 && mode <= 5)
                            {
                                return mode;
                            }
                            else
                            {
                                Console.WriteLine($"Error: Invalid mode ({mode}) found in file {filePath}.");
                                return null;
                            }
                        }
                    }
                }


                Console.WriteLine($"Warning: no mode information found in file {filePath}");
                return null;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"error reading file {filePath}: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"unexpected error while processing the file {filePath}: {ex.Message}");
                return null;
            }
        }
    }
}

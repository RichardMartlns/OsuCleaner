using Vanara.PInvoke;
using static Vanara.PInvoke.Shell32;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Drawing.Text;
using System.IO;

namespace OsuCleaner.Services
{
    public class TrashManager
    {
        public void MoveToTrash(string path)
        {

            if (Directory.Exists(path))
            {

                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    MoveFileToTrash(file);
                }


                Directory.Delete(path, true);

            }
            else if (File.Exists(path))
            {

                MoveFileToTrash(path);

            }
            else
            {
                throw new FileNotFoundException("The specified file or directory was not found.", path);
            }

        }
        
       private void MoveFileToTrash(string filePath)
        {


            // Convert file path to int(pointer)
            nint pFrom = Marshal.StringToHGlobalAuto(filePath);

            // Move file to Trash using Shell32 API
            var file = new SHFILEOPSTRUCT
            {
                wFunc = ShellFileOperation.FO_DELETE,
                pFrom = pFrom, // The file path with the null terminator
                fFlags = FILEOP_FLAGS.FOF_NOCONFIRMATION | FILEOP_FLAGS.FOF_SILENT | FILEOP_FLAGS.FOF_ALLOWUNDO
            };

            var result = SHFileOperation(ref file);

            if (result != 0)
            {
                Console.WriteLine($"Failed to move the file to the trash. Error code: {result}");
            }
            else
            {
                Console.WriteLine($"Moved {filePath} moved to trash.");
            }
        }
    }
}

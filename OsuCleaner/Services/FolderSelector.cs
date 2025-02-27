using System.IO;
using System.Windows.Forms;


namespace OsuCleaner.Services
{
    public class FolderSelector
    {

        public string? SelectFolderDialog(string description = "Select a Folder")
        {
            using (var dialog = new FolderBrowserDialog())
            {

                dialog.Description = description;
                dialog.ShowNewFolderButton = false;

              
                if (dialog.ShowDialog() == DialogResult.OK &&
                    !string.IsNullOrEmpty(dialog.SelectedPath) &&
                    Directory.Exists(dialog.SelectedPath))
                {
                    return dialog.SelectedPath;
                }
                else
                {
                    return null; 
                }
            }
        }
    }
}


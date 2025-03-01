using OsuCleaner.Services;
using System.Diagnostics;
using System.IO;
using System.Windows;



namespace OsuCleaner
{

    public partial class MainWindow : Window
    {
        private FolderSelector folderSelector;
        private SettingsManager settingsManager;
        private Dictionary<int, int> mapCounts = new();

        public MainWindow()
        {
            InitializeComponent();
            folderSelector = new FolderSelector();
            settingsManager = new SettingsManager();


            string savedPath = settingsManager.GetSavePath();
            if (!string.IsNullOrEmpty(savedPath) && Directory.Exists(savedPath))
            {
                TxtSelectedPath.Text = savedPath;
            }
        }


        private void BtnSelectFolder_click(object sender, RoutedEventArgs e)
        {

            string? selectedPath = folderSelector.SelectFolderDialog("Select the songs folder from Osu!");

            if (!string.IsNullOrEmpty(selectedPath))
            {
                TxtSelectedPath.Text = selectedPath;
            }
            else
            {
                TxtSelectedPath.Text = "No folder selected";
            }
        }


        private void BtnSearchMaps_Click(object sender, RoutedEventArgs e)
        {

            string osuPath = TxtSelectedPath.Text;


            if (!string.IsNullOrEmpty(osuPath) && Directory.Exists(osuPath))
            {

                FileScanner scanner = new FileScanner(osuPath);
                scanner.SearchMaps();
               
                mapCounts.Clear();

                var mapsByMode = scanner.GetMapsByMode();


                foreach (var mode in mapsByMode)
                {
                    mapCounts[mode.Key] = mode.Value.Count;
                }

                UpdateList();

            }
            else
            {
                MessageBox.Show("The specified folder does not exist. check the path!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        private void UpdateList()
        {

            MapListView.Items.Clear();

            foreach (var item in mapCounts)
            {
                MapListView.Items.Add(new { Mode = $"Modo {item.Key}", Count = $"{item.Value} maps" });
            }
        }


        private void ModeFilter_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (mapCounts.Count == 0) return;


            var selectedItem = ((System.Windows.Controls.ComboBoxItem)ModeFilter.SelectedItem).Content.ToString();
            if (string.IsNullOrEmpty(selectedItem)) return;


            string selectedMode = selectedItem;


            MapListView.Items.Clear();


            if (selectedMode == "All Mods")
            {
                UpdateList();
            }
            else
            {
                if (int.TryParse(selectedMode.Split(' ')[1], out int modeNumber))
                {
                    if (mapCounts.ContainsKey(modeNumber))
                    {
                        MapListView.Items.Add(new { Mode = $"Modo {modeNumber}", Count = $"{mapCounts[modeNumber]} maps" });
                    }
                }

            }
        }

        private void BtnDeleteMaps_click(object sender, RoutedEventArgs e)
        {

            if (mapCounts.Count == 0)
            {

                MessageBox.Show("No maps found to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to move the selected maps to the trash?",
                                                    "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                TrashManager trashManager = new TrashManager();

                foreach (var mode in mapCounts)
                {

                    string mapPath = Path.Combine(TxtSelectedPath.Text, mode.Key.ToString());

                    if (Directory.Exists(mapPath))
                    {
                        trashManager.MoveToTrash(mapPath);
                    }
                }

                MessageBox.Show("Maps moved to trash successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);       
            }

        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}

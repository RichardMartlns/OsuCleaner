using OsuCleaner.Services;
using System.IO;
using System.Windows;


namespace OsuCleaner
{
    public partial class MainWindow : Window
    {
        private FolderSelector folderSelector;
        private Dictionary<int, int> mapCounts = new();

        public MainWindow()
        {
            InitializeComponent();
            folderSelector = new FolderSelector();
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


            if (!Directory.Exists(osuPath))
            {
                MessageBox.Show("The specified folder does not exist. check the path!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            FileScanner scanner = new FileScanner(osuPath);
            scanner.SearchMaps();


            mapCounts = scanner.CountMapsByMode();

            UpdateList();

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
            MessageBox.Show("nao implementado");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using OsuCleaner.Services; // Ajuste o namespace correto

namespace OsuCleaner
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // ⚠ Aqui precisa estar dentro do construtor!
        }

        private void BtnSearchMaps_Click(object sender, RoutedEventArgs e)
        {
            string osuPath = @"C:\Osu\Songs"; // Ajuste para seu caminho correto
            FileScanner scanner = new FileScanner(osuPath);
            scanner.SearchMaps();

            Dictionary<int, List<string>> mapsByMode = scanner.GetMapsByMode();
            string resultado = "";

            foreach (var mode in mapsByMode)
            {
                resultado += $"Modo {mode.Key}: {mode.Value.Count} mapas encontrados\n";
            }

            MessageBox.Show(resultado, "Resultado da Busca");
        }


        
    }
}

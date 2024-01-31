using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddGameApp.DeveloperPage
{
    /// <summary>
    /// Логика взаимодействия для AddGame.xaml
    /// </summary>
    public partial class AddGame : Page
    {
        AddGameBDEntities contextBD = new AddGameBDEntities();
        Games game = new Games();

        public AddGame()
        {
            InitializeComponent();
            DataContext = game;
            CmbSelectOc.ItemsSource = contextBD.OC.ToList();
            CmbSelectCPU.ItemsSource = contextBD.CPU.ToList();
            CmbSelectGPU.ItemsSource = contextBD.GPU.ToList();
            CmbSelectMode.ItemsSource = contextBD.GameMode.ToList();
            CmbSelectGenre.ItemsSource = contextBD.GameGenre.ToList();
        }

        private void BtnSaveChange_Click(object sender, RoutedEventArgs e)
        {
            game.idDeveloper = MenuDeveloper.idDeveloper;
            game.pathImage = TxtPathImage.Text;
            if(game.id == 0)
                contextBD.Games.Add(game);
            
           
            try
            {
                contextBD.SaveChanges();
                MessageBox.Show("Игра добавлена!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new MenuDeveloper());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSelectPathImage_Click(object sender, RoutedEventArgs e)
        {
            string copy = Directory.GetCurrentDirectory();
            copy = copy.Substring(0, copy.Length - 9) + @"Documets\";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:";
            ofd.Filter = "jpeg files (*.jpg)|*.jpg|All fiels (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == true)
            {
                var str = ofd.FileName.Split(new[] { '\\' }).Last();
                File.Copy(ofd.FileName, System.IO.Path.Combine(copy, str), true);
                string name = ofd.SafeFileName;

                TxtPathImage.Text = copy + name;
            }
        }

        private void TxtreleaseDate_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}

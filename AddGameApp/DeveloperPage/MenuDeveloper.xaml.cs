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
    /// Логика взаимодействия для MenuDeveloper.xaml
    /// </summary>
    public partial class MenuDeveloper : Page
    {
        AddGameBDEntities contextBd = new AddGameBDEntities();
        Games games = new Games();
        Games gameCheckChange = new Games();
        bool controlSwitch = true;
        bool controlSelectGame;
        bool controlDeleteGame = true;

        public static int idDeveloper;

        public MenuDeveloper()
        {
            InitializeComponent();
            controlSelectGame = false;
            ListSelectGame.ItemsSource = contextBd.Games.Where(x => x.idDeveloper == idDeveloper).ToList();
            var checkGame = contextBd.Games.FirstOrDefault(x => x.idDeveloper == idDeveloper);
            if (checkGame == null)
            {
                ListSelectGame.Visibility = Visibility.Hidden;
                GridGameFields.Visibility = Visibility.Hidden;
                TxtIfNull.Visibility = Visibility.Visible;
            }
            CmbSelectOc.ItemsSource = contextBd.OC.ToList();
            CmbSelectCPU.ItemsSource = contextBd.CPU.ToList();
            CmbSelectGPU.ItemsSource = contextBd.GPU.ToList();
            CmbSelectMode.ItemsSource = contextBd.GameMode.ToList();
            CmbSelectGenre.ItemsSource = contextBd.GameGenre.ToList();
        }

        private void BtnSelectedGame_Click(object sender, RoutedEventArgs e)
        {
            if (controlDeleteGame)
            {
                GridGameFields.Visibility = Visibility.Visible;

                if (gameCheckChange.id != 0)
                {
                    if (CheckChange(games) == false)
                        GetContextBD();
                }
                if (controlSwitch == false)
                {
                    controlSwitch = true;
                    return;
                }

                games = ((sender as Button).DataContext as Games);
                ListSelectGame.ItemsSource = contextBd.Games.Where(x => x.idDeveloper == idDeveloper).ToList();
                if (games.id != 0)
                    SetContextBD(games);
                gameCheckChange = games;

                controlSelectGame = true;
            }
            controlDeleteGame = true;
        }


        bool CheckChange(Games game)
        {
            if (game.name == TxtName.Text && game.idOC == Convert.ToInt32(CmbSelectOc.SelectedValue) && game.idCPU == Convert.ToInt32(CmbSelectCPU.SelectedValue)
                && game.idGPU == Convert.ToInt32(CmbSelectGPU.SelectedValue) && game.RAM == Convert.ToInt32(TxtRAM.Text) && game.diskSpace == Convert.ToInt32(TxtdiskSpace.Text)
                    && game.idGameMode == Convert.ToInt32(CmbSelectMode.SelectedValue) && game.idGameGenre == Convert.ToInt32(CmbSelectGenre.SelectedValue)
                    && game.price == Convert.ToDecimal(TxtPrice.Text) && game.discount == Convert.ToDouble(TxtDiscount.Text) && game.description == TxtDescription.Text
                    && game.pathImage == TxtPathImage.Text)
            {
                return true;
            }
            else
                return false;

        }

        void SetContextBD(Games game)
        {
            CmbSelectOc.IsSynchronizedWithCurrentItem = true;
            CmbSelectOc.SelectedItem = game.OC;
            CmbSelectCPU.SelectedItem = game.CPU;
            CmbSelectGPU.SelectedItem = game.GPU;
            CmbSelectMode.SelectedItem = game.GameMode;
            CmbSelectGenre.SelectedItem = game.GameGenre;

            TxtName.Text = game.name;
            TxtRAM.Text = Convert.ToString(game.RAM);
            TxtdiskSpace.Text = Convert.ToString(game.diskSpace);
            DateTime dt = Convert.ToDateTime(game.releaseDate);
            TxtreleaseDate.Text = dt.ToString("dd.MM.yyyy");
            TxtPrice.Text = game.price.ToString("0.00");
            if (game.discount != null)
                TxtDiscount.Text = Convert.ToString(game.discount);
            else
                TxtDiscount.Text = "";
            if (game.description != null)
                TxtDescription.Text = game.description;
            else
                TxtDescription.Text = "";
            TxtPathImage.Text = game.pathImage;

        }

        void GetContextBD()
        {
            if (controlSelectGame)
            {

                var resault = MessageBox.Show("Хотите сохранить изменения?", "Сохранение",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Information);


                if (resault == MessageBoxResult.Yes)
                {

                    contextBd.Games.FirstOrDefault(x => x.id == games.id).name = TxtName.Text;
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).idOC = Convert.ToInt32(CmbSelectOc.SelectedValue);
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).idCPU = Convert.ToInt32(CmbSelectCPU.SelectedValue);
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).idGPU = Convert.ToInt32(CmbSelectGPU.SelectedValue);
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).RAM = Convert.ToInt32(TxtRAM.Text);
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).diskSpace = Convert.ToInt32(TxtdiskSpace.Text);
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).idGameMode = Convert.ToInt32(CmbSelectMode.SelectedValue);
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).idGameGenre = Convert.ToInt32(CmbSelectGenre.SelectedValue);
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).releaseDate = Convert.ToDateTime(TxtreleaseDate.Text);
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).price = Convert.ToDecimal(TxtPrice.Text);
                    if (TxtDiscount.Text != "")
                        contextBd.Games.FirstOrDefault(x => x.id == games.id).discount = Convert.ToDouble(TxtDiscount.Text);
                    else
                        contextBd.Games.FirstOrDefault(x => x.id == games.id).discount = null;
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).description = TxtDescription.Text;
                    contextBd.Games.FirstOrDefault(x => x.id == games.id).pathImage = TxtPathImage.Text;


                    try
                    {
                        contextBd.SaveChanges();
                        controlSwitch = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (resault == MessageBoxResult.Cancel)
                {
                    controlSwitch = false;
                }
            }

        }

       
        
        private void BtnSaveChange_Click(object sender, RoutedEventArgs e)
        {            
            GetContextBD();
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

        private void BtnDeleteGame_Click(object sender, RoutedEventArgs e)
        {
            controlDeleteGame = false;
            var GameDelete = (sender as Button).DataContext as Games;

            contextBd.Games.Remove(GameDelete);
            try
            {
                contextBd.SaveChanges();
                ListSelectGame.ItemsSource = contextBd.Games.Where(x => x.idDeveloper == idDeveloper).ToList();
               
                MessageBox.Show("Игра была удалена из магазина", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
    }
}

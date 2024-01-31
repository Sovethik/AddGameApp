using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace AddGameApp.UserPage
{
    /// <summary>
    /// Логика взаимодействия для GameForm.xaml
    /// </summary>
    public partial class GameForm : Page
    {
        AddGameBDEntities contextBD = new AddGameBDEntities();
        
        List<Games> gameToCart = new List<Games>();
        Games gamesIsTrueBD = new Games();
        Favorites favorites = new Favorites();
        public GameForm(Games games)
        {
            InitializeComponent();
            gameToCart.Add(games);
            gamesIsTrueBD = games;
            DataContext = games;

            //FillingForm(games);

            var InCart = CartUser.contextBD.OrderList.Local.FirstOrDefault(s => s.idGame == gamesIsTrueBD.id);
            var InOrdersList = contextBD.OrderList.FirstOrDefault(x => x.idGame == gamesIsTrueBD.id && x.Orders.idUsers == MainWindow.IdUser);
            var InFavorites = contextBD.Favorites.FirstOrDefault(x => x.idUser == MainWindow.IdUser && x.idGame == games.id);

            if(InOrdersList != null)
            {
                BorderBuy.Visibility = Visibility.Hidden;
                BorderDontBuy.Visibility = Visibility.Visible;
                TxtDontBuy.Text = "Товар есть в библиотеке";
            }
            if ( InCart != null )
            {
                BorderBuy.Visibility = Visibility.Hidden;
                BorderDontBuy.Visibility = Visibility.Visible;
            }

            if(InFavorites != null)
            {
                BtnDeleteFavorite.Visibility = Visibility.Visible;
                BtnFavorits.Visibility = Visibility.Hidden;
            }
        }

       

        private void BtnBAck_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuProduct());
        }

        private void BtnAddCart_Click(object sender, RoutedEventArgs e)
        {
            var InCart = CartUser.contextBD.OrderList.Local.FirstOrDefault(s => s.idGame == gamesIsTrueBD.id);
            var InOrdersList = contextBD.OrderList.FirstOrDefault(x => x.idGame == gamesIsTrueBD.id && x.Orders.idUsers == MainWindow.IdUser);
            if (InCart == null && InOrdersList == null)
            {
                NavigationService.Navigate(new CartUser(gameToCart));
            }


        }

        private void BtnFavorites_Click(object sender, RoutedEventArgs e)
        {
            
            favorites.idGame = gamesIsTrueBD.id;
            favorites.idUser = MainWindow.IdUser;

            if(favorites.id == 0)
                contextBD.Favorites.Add(favorites);

            
            try
            {
                contextBD.SaveChanges();
                BtnFavorits.Visibility = Visibility.Hidden;
                BtnDeleteFavorite.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message.ToString(),"", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeleteFavorite_Click(object sender, RoutedEventArgs e)
        {
            var DeleteFavoriteGame = contextBD.Favorites.FirstOrDefault(x => x.idGame == gamesIsTrueBD.id && x.idUser == MainWindow.IdUser);
            contextBD.Favorites.Remove(DeleteFavoriteGame);

            try
            {
                contextBD.SaveChanges();
                BtnFavorits.Visibility = Visibility.Visible;
                BtnDeleteFavorite.Visibility = Visibility.Hidden;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}

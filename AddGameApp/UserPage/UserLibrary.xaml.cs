using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UserLibrary.xaml
    /// </summary>
    public partial class UserLibrary : Page
    {
        AddGameBDEntities contextBD = new AddGameBDEntities();
        Orders orders = new Orders();
        static int IdlastSelectGame = 0;

        public UserLibrary()
        {
            InitializeComponent();
            DataContext = orders;
            listGame.DataContext = contextBD.Games.ToList();
            listGame.ItemsSource = contextBD.OrderList.Where(x => x.Orders.idUsers == MainWindow.IdUser).ToList();

           
           
            var dataGame = contextBD.OrderList.AsNoTracking().FirstOrDefault(x => x.Orders.idUsers == MainWindow.IdUser);
            if (IdlastSelectGame != 0)
                dataGame = contextBD.OrderList.AsNoTracking().FirstOrDefault(x => x.Orders.idUsers == MainWindow.IdUser && x.idGame == IdlastSelectGame);

            if (dataGame != null)
            {
                TxtTextKey.Visibility = Visibility.Visible;
                ImageGame.Source = new BitmapImage(new Uri(dataGame.Games.pathImage));
                TxtNameGame.Text = dataGame.Games.name;
                TxtKeyGame.Text = dataGame.keyGame;
            }
            else
                BorderImage.Visibility = Visibility.Hidden;
        }

        private void BtnDataGame_Click(object sender, RoutedEventArgs e)
        {
            var dataGame = ((sender as Button).DataContext as OrderList);
            if (dataGame != null)
            {
                TxtTextKey.Visibility = Visibility.Visible;
                IdlastSelectGame = dataGame.idGame;

                ImageGame.Source = new BitmapImage(new Uri(dataGame.Games.pathImage));
                TxtNameGame.Text = dataGame.Games.name;
                TxtKeyGame.Text = dataGame.keyGame;
            }

        }
    }
}

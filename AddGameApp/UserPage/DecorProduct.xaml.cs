using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для DecorProduct.xaml
    /// </summary>
    public partial class DecorProduct : Page
    {
        AddGameBDEntities contextBD = new AddGameBDEntities();

        public DecorProduct()
        {
            InitializeComponent();
            CmbPay.ItemsSource = contextBD.PaymentMethod.ToList();

        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (CmbPay.SelectedIndex != -1)
            {

                CartUser.orders.idPaymentMethod = Convert.ToInt32(CmbPay.SelectedValue);

                if (CartUser.orders.id == 0)
                {
                    CartUser.contextBD.Orders.Add(CartUser.orders);
                }
                try
                {
                    CartUser.contextBD.SaveChanges();


                    Orders ordersTime = new Orders();
                    AddGameBDEntities contextBDTime = new AddGameBDEntities();
                    CartUser.contextBD = contextBDTime;
                    CartUser.orders = ordersTime;

                    //listGame.DataContext = contextBD.Games.ToList();
                    //DataContext = orders;
                    //listGame.ItemsSource = contextBD.OrderList.Local.ToBindingList();
                    //TxtPriceAll.Text = Convert.ToString(Math.Round(orders.priceAll, 2));
                    CartUser.countGame = 0;
                    MainWindow.windowUser.BtnCart.Content = $"Корзина";
                    MessageBox.Show("Оплата прошла успешно", "Поздравляем!", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new UserLibrary());

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Выберите способ оплаты", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartUser(null));
        }
    }
}

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
    /// Логика взаимодействия для CartUser.xaml
    /// </summary>
    public partial class CartUser : Page
    {

        public static Orders orders = new Orders();
        public static AddGameBDEntities contextBD = new AddGameBDEntities();
        public static int countGame = 0;

        public CartUser(List<Games> game)
        {
            InitializeComponent();

            orders.idUsers = MainWindow.IdUser;


            listGame.DataContext = contextBD.Games.ToList();
            DataContext = orders;
            listGame.ItemsSource = contextBD.OrderList.Local.ToBindingList();

            if(game != null)
            {
                foreach(Games item in game)
                {
                    try
                    {
                        OrderList orderList = new OrderList();
                        orderList.idOrder = orders.id;
                        orderList.idGame = item.id;
                        orderList.discount = item.discount;
                        orderList.price =item.price;

                        string key = CreateKey();
                        while (contextBD.OrderList.FirstOrDefault(x => x.keyGame == key) != null)
                        {
                            key = CreateKey();
                        }
                        orderList.keyGame = key;

                        if (item.discount == null)
                        {
                            orders.priceAll += item.price;
                        }

                        else
                        {
                            orders.priceAll += Convert.ToDecimal(item.DoubleDiscount);
                        }
                        

                        orders.OrderList.Add(orderList);
                        contextBD.OrderList.Add(orderList);
                        contextBD.Entry(orderList).State = EntityState.Added;
                        

                        countGame++;
                        MainWindow.windowUser.BtnCart.Content = $"Корзина({countGame})";

                        

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            TxtPriceAll.Text = Convert.ToString(Math.Round(orders.priceAll,2));

        }

        private void BtnGoShop_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuProduct());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            OrderList item = (OrderList)btn.DataContext as OrderList;
           
            try
            {
                if (item.Games.discount == null)
                {
                    orders.priceAll -= item.Games.price;
                }
                else
                {
                    orders.priceAll -= Convert.ToDecimal(item.Games.DoubleDiscount);
                }
                TxtPriceAll.Text = Convert.ToString(Math.Round(orders.priceAll, 2));


                countGame--;
                MainWindow.windowUser.BtnCart.Content = $"Корзина({countGame})";
                if (countGame == 0)
                    MainWindow.windowUser.BtnCart.Content = "Корзина";
                

                contextBD.OrderList.Local.Remove(item);
                orders.OrderList.Remove(item);

                
                listGame.ItemsSource = contextBD.OrderList.Local;
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDesign_Click(object sender, RoutedEventArgs e)
        {
            if (contextBD.OrderList.Local.Count < 1)
                MessageBox.Show("Корзина пуста", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                NavigationService.Navigate(new DecorProduct());
            }
            
        }

        string CreateKey()
        {
            string Simbol;
            Simbol = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            Simbol += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            Simbol += "1,2,3,4,5,6,7,8,9,0";

            string[] arraySimbol = Simbol.Split(',');
            string resault = "";
            string temp= "";
            Random rnd = new Random();

            for(int  i = 0; i < 9; i++)
            {
                temp = arraySimbol[rnd.Next(0, arraySimbol.Length)];
                resault += temp;
            }

            return resault;
        }
    }
}

using AddGameApp.DeveloperPage;
using AddGameApp.UserPage;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace AddGameApp
{
    /// <summary>
    /// Логика взаимодействия для WindowUser.xaml
    /// </summary>
    public partial class WindowUser : Window
    {
        static public SolidColorBrush FGBtnShop = new SolidColorBrush();
        AddGameBDEntities contextBD;
        
        
        bool controlBtnLogin;
        bool controlBtnLogin_2;

        public WindowUser()
        {
            InitializeComponent();
            FGBtnShop.Color = Color.FromRgb(245, 222, 179);
            frameUser.NavigationService.Navigate(new UserPage.MenuProduct());
            BtnShop.Foreground = FGBtnShop;
            controlBtnLogin = true;
            controlBtnLogin_2 = false;
            contextBD = new AddGameBDEntities();

            string path = contextBD.Usesrs.FirstOrDefault(x => x.id == MainWindow.IdUser).pathAvatar;
            if (contextBD.Usesrs.FirstOrDefault(x => x.id == MainWindow.IdUser).pathAvatar != null && path != "")
            {
                ImageAvatarUser.Source = new BitmapImage(new Uri(contextBD.Usesrs.FirstOrDefault(x => x.id == MainWindow.IdUser).pathAvatar));
            }
            else
                ImageAvatarUser.Source = new BitmapImage(new Uri("Image/IconUserDefault.png", UriKind.Relative));
            TxtLogin.Text = contextBD.Usesrs.FirstOrDefault(x => x.id == MainWindow.IdUser).login;
        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {
            frameUser.NavigationService.Navigate(new UserPage.CartUser(null));
        }

        private void BtnLibrary_Click(object sender, RoutedEventArgs e)
        {
            frameUser.NavigationService.Navigate(new UserPage.UserLibrary());
        }

        private void BtnShop_Click(object sender, RoutedEventArgs e)
        {
            frameUser.NavigationService.Navigate(new UserPage.MenuProduct());
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            

            if (controlBtnLogin == true)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = 0;
                doubleAnimation.To = 85;
                doubleAnimation.Duration = TimeSpan.FromMilliseconds(80);
                GridMenuLogin.Visibility = Visibility.Visible;
                GridMenuLogin.BeginAnimation(Grid.HeightProperty, doubleAnimation);

                controlBtnLogin = false;
            }
            else
            {
                controlBtnLogin = true;
                GridMenuLogin.Visibility = Visibility.Hidden;
            }
        }

        private void BtnLoginASDeveloper_Click(object sender, RoutedEventArgs e)
        {
            if (contextBD.Developers.AsNoTracking().FirstOrDefault(x => x.idUser == MainWindow.IdUser) == null)
            {
                GridMenuLogin.Visibility = Visibility.Hidden;
                GridMenuUser.Visibility = Visibility.Hidden;
                frameUser.NavigationService.Navigate(new DeveloperPage.EnterData());
            }
            else
            {
                MenuDeveloper.idDeveloper = contextBD.Developers.FirstOrDefault(x => x.idUser == MainWindow.IdUser).id;
                GridMenuLogin.Visibility = Visibility.Hidden;
                GridMenuUser.Visibility = Visibility.Hidden;
                frameUser.NavigationService.Navigate(new DeveloperPage.MenuDeveloper());
                GridMenuDeveloper.Visibility = Visibility.Visible;
            }
            controlBtnLogin = true;
        }

        private void BtnChangeAvatar_Click(object sender, RoutedEventArgs e)
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

                contextBD.Usesrs.FirstOrDefault(x => x.id == MainWindow.IdUser).pathAvatar = copy + name;

                try
                {
                    contextBD.SaveChanges();
                    ImageAvatarUser.Source = new BitmapImage(new Uri(contextBD.Usesrs.FirstOrDefault(x => x.id == MainWindow.IdUser).pathAvatar));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }


            }
           


        }

        private void BtnMyGame_Click(object sender, RoutedEventArgs e)
        {
            frameUser.NavigationService.Navigate(new DeveloperPage.MenuDeveloper());
        }

        private void BtnAddGame_Click(object sender, RoutedEventArgs e)
        {
            frameUser.NavigationService.Navigate(new DeveloperPage.AddGame());
        }

        private void BtnExitMenuDeveloper_Click(object sender, RoutedEventArgs e)
        {
            GridMenuDeveloper.Visibility = Visibility.Hidden;
            GridMenuUser.Visibility = Visibility.Visible;
            frameUser.NavigationService.Navigate(new UserPage.MenuProduct());
        }

        private void BtnLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            
            if (controlBtnLogin == false || controlBtnLogin_2 == true)
            {
                GridMenuLogin.Visibility = Visibility.Visible;
                controlBtnLogin_2 = true;
            }
           
            
        }

        private void BtnLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            GridMenuLogin.Visibility=Visibility.Hidden;
            controlBtnLogin = true;
        }

        private void GridMenuLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            GridMenuLogin.Visibility = Visibility.Visible;
        }

        private void GridMenuLogin_MouseLeave(object sender, MouseEventArgs e)
        {
            GridMenuLogin.Visibility = Visibility.Hidden;
            controlBtnLogin = true;
        }
    }
}

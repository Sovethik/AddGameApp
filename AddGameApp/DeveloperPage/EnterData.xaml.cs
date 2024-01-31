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

namespace AddGameApp.DeveloperPage
{
    /// <summary>
    /// Логика взаимодействия для EnterData.xaml
    /// </summary>
    public partial class EnterData : Page
    {
        AddGameBDEntities contextBD = new AddGameBDEntities();
        Developers newDeveloper = new Developers();


        public EnterData()
        {
            InitializeComponent();
            DataContext = newDeveloper;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            MainWindow.windowUser.GridMenuUser.Visibility = Visibility.Visible;
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (TxtCountry.Text == "" || TxtCity.Text == "" || TxtStreet.Text == "" || TxtHome.Text == "" || TxtNameBrand.Text == "")
            {
                MessageBox.Show("Заполните все поля", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TxtNameBrand.Text.Length < 2)
            {
                MessageBox.Show("Наименование компании должно содержать не менее 2 сомволов", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var nameBrand = contextBD.Developers.AsNoTracking().FirstOrDefault(x => x.name == TxtNameBrand.Text);
            if(nameBrand == null)
            {
                newDeveloper.address = TxtCountry.Text + ", " + TxtCity.Text + ", " + TxtStreet.Text + ", " + TxtHome.Text;
                newDeveloper.name = TxtNameBrand.Text;
                newDeveloper.dateCreate = DateTime.Now;
                newDeveloper.idUser = MainWindow.IdUser;

                if(newDeveloper.id == 0)
                    contextBD.Developers.Add(newDeveloper);

                try
                {
                    contextBD.SaveChanges();
                    MenuDeveloper.idDeveloper = contextBD.Developers.FirstOrDefault(x => x.idUser == MainWindow.IdUser).id;
                    MessageBox.Show("Регистрация прошла успешно!", "Информирование", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new MenuDeveloper());
                    MainWindow.windowUser.GridMenuDeveloper.Visibility = Visibility.Visible;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Такое наименование компании уже существует", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

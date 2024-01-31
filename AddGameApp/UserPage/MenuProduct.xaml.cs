using AddGameApp.NeuronNetworkPage;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Runtime.InteropServices;
using AddGameApp.Classes;
using System.ComponentModel.Design;

namespace AddGameApp.UserPage
{
    /// <summary>
    /// Логика взаимодействия для MenuProduct.xaml
    /// </summary>
    public partial class MenuProduct : Page
    {
        AddGameBDEntities contextBD = new AddGameBDEntities();
        static AddGameBDEntities contextBDForBanner = new AddGameBDEntities();// для gameArray
        Games games = new Games();
        bool controlDelete = true;


        NeuralNetwork neuralNetwork;
        Timer timer;
        static int countNeedGames = 3; //Количество выводимых игр в баннер
        static int[] indexGame_1 = new int[countNeedGames]; // Индекс выводимых игр в баннер
        int countUpdateBanner = 0; //Количество загрузок картинок в баннер
        Games[] gameArray = contextBDForBanner.Games.ToArray();// Для вывода определенных игр в баннер
        SaveInJson saveInJson;// Для сохранения обученной нейросети
        private readonly string PATH = $"{Environment.CurrentDirectory}\\neuralNetwork.json";

        public MenuProduct()
        {
            InitializeComponent();
            timer = new Timer(3000);
            DataContext = games;
            listGame.ItemsSource = contextBD.Games.ToList();
            WindowUser.FGBtnShop.Color = Color.FromRgb(245, 222, 179);

            CmBGenre.ItemsSource = contextBD.GameGenre.ToList();
            CmBMode.ItemsSource = contextBD.GameMode.ToList();

            

            ControlNeuralNetwork();

        }


        public void ControlNeuralNetwork()
        {

            
            saveInJson = new SaveInJson(PATH);

            try
            {
                neuralNetwork = saveInJson.LoadDataNeuralNetwork(inputSize: 4, hiddenSize: 10, hiddenSise_2: 10, outputSize: 1, learningRate: 0.1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


            if (neuralNetwork.IsLearn == false)
            {
                LearningNeuralNetwork();
                neuralNetwork.IsLearn = true;

                try
                {
                    saveInJson.SaveDataNeuralNetwork(neuralNetwork);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }


            

            double[] resultsAnalysis = new double[gameArray.Length];
            for (int i = 0; i < gameArray.Length; i++)
            {
                resultsAnalysis[i] = StartNeuralNetWork(gameArray[i]);
            }

            

            

            double[] valueGames_1 = new double[countNeedGames];

           
            valueGames_1[0] = resultsAnalysis[0];
            
            for(int i = 1; i < valueGames_1.Length; i++)
            {
                valueGames_1[i] = 0;
            }
           

            for(int i = 1; i < resultsAnalysis.Length; i++) //Нахождение индекса игр, анализируемых нейросетью , которые с наибольшей вероятностью купят
            {
                if (resultsAnalysis[i] > valueGames_1[0]) 
                {
                    valueGames_1[0] = resultsAnalysis[i];
                    indexGame_1[0] = i;

                    
                }

                for (int j = 1; j < valueGames_1.Length; j++) 
                {
                   
                    for (int k = 0; k <= i; k++)
                    {
                        if (valueGames_1[j] < resultsAnalysis[k] && valueGames_1[j] < valueGames_1[j - 1] && resultsAnalysis[k] < valueGames_1[j - 1])
                        {
                            valueGames_1[j] = resultsAnalysis[k];
                            indexGame_1[j] = k;
                        }
                    }
                }
            }

            timer.Elapsed += UpdateBanner;
            timer.AutoReset = true;
            timer.Enabled = true;

        }

        private void UpdateBanner(object source, ElapsedEventArgs e)
        {

            Application.Current.Dispatcher.Invoke(() =>
            AdvertisingBanner.Source = new BitmapImage(new Uri(gameArray[indexGame_1[countUpdateBanner]].FullPath)));
            countUpdateBanner++;
            if(countUpdateBanner == countNeedGames)
                countUpdateBanner = 0;
        }

        public double StartNeuralNetWork(Games alalysisGames)
        {



            double[] inputs = { 0, 0, 0, 0 };

            inputs[0] = AnalysisUserGamesDiscount(alalysisGames);
            inputs[1] = AnalysisUserGamesPrice(alalysisGames);
            inputs[2] = AnalisisUserGamesFavorite(alalysisGames);
            inputs[3] = AnalysisUserGamesGenre(alalysisGames);

            double[] result = neuralNetwork.FeedForward(inputs);

            return result[0];

        }

        private void BtnOpenFormGame_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GameForm((sender as Button).DataContext as Games));
        }

        public void LearningNeuralNetwork()
        {
            // 1) Совпадает по скидке
            // 2) Совпадает по цене
            // 3) Есть в избранном
            // 4) Совпадает по жанру

            var dataset = new List<Tuple<double, double[]>>
            {

                new Tuple<double, double[]>(0, new double[]{0, 0, 0, 0}),
                new Tuple<double, double[]>(0.1, new double[]{0, 0, 0, 1}),
                new Tuple<double, double[]>(0.1, new double[]{0, 0, 1, 0}),
                new Tuple<double, double[]>(0.45, new double[]{0, 0, 1, 1}),
                new Tuple<double, double[]>(0.2, new double[]{0, 1, 0, 0}),
                new Tuple<double, double[]>(0.5, new double[]{0, 1, 0, 1}),
                new Tuple<double, double[]>(0.6, new double[]{0, 1, 1, 0}),
                new Tuple<double, double[]>(0.8, new double[]{0, 1, 1, 1 }),
                new Tuple<double, double[]>(0.15, new double[]{1, 0, 0, 0 }),
                new Tuple<double, double[]>(0.55, new double[]{1, 0, 0, 1 }),
                new Tuple<double, double[]>(0.4, new double[]{1, 0, 1, 0 }),
                new Tuple<double, double[]>(0.9, new double[]{1, 0, 1, 1 }),
                new Tuple<double, double[]>(0.25, new double[]{1, 1, 0, 0 }),
                new Tuple<double, double[]>(0.7, new double[]{1, 1, 0, 1 }),
                new Tuple<double, double[]>(0.65, new double[]{1, 1, 1, 0 }),
                new Tuple<double, double[]>(1, new double[]{1, 1, 1, 1 })

            };

            for (int i = 0; i < 50000; i++)
            {
                for (int j = 0; j < dataset.Count; j++)
                {
                    neuralNetwork.Train(dataset[j].Item2, dataset[j].Item1);
                }
            }


        }

        public int AnalysisUserGamesPrice(Games alalysisGames)
        {
            int sumBuyGames = contextBD.OrderList.Where(x => x.Orders.idUsers == MainWindow.IdUser).Count<OrderList>();
            var ArrayGames = contextBD.OrderList.Where(x => x.Orders.idUsers == MainWindow.IdUser).ToArray();
            double AVG = 0;
            for (int i =0; i < sumBuyGames; i++)
            {
                AVG += Convert.ToDouble(ArrayGames[i].price);
                
            }
            AVG = AVG / sumBuyGames;

            double sumGamesLessAVG = 0;
            for(int i =0;i < sumBuyGames; i++)
            {
                if (Convert.ToDouble(ArrayGames[i].price) < AVG)
                    sumGamesLessAVG++;
            }

            if (sumGamesLessAVG > sumBuyGames / 2 || sumGamesLessAVG == sumBuyGames / 2)
            {
                if (Convert.ToDouble(alalysisGames.price) < AVG)
                    return 1;
                else
                    return 0;
            }
            else 
            { 
                if(Convert.ToDouble(alalysisGames.price) > AVG)
                    return 1;
                else 
                    return 0;
            }
            
        }

        public int AnalysisUserGamesDiscount(Games alalysisGames)
        {

            int sumBuyGames = contextBD.OrderList.Where(x => x.Orders.idUsers == MainWindow.IdUser).Count<OrderList>();
            int sumGamesDiscount = 0;

            for (int i = 0; i < sumBuyGames; i++)
            {
                if(contextBD.OrderList.Where(x => x.Orders.idUsers == MainWindow.IdUser).ToArray()[i].discount != null)
                {
                    sumGamesDiscount++;
                }
            }

            if(sumGamesDiscount > sumBuyGames/2)
            {
                if(alalysisGames.discount != null)
                {
                    return 1;
                }
                else
                { 
                    return 0; 
                }
            }
            else 
            { 
                
                if(alalysisGames.discount == null)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

        }

        public int AnalisisUserGamesFavorite(Games alalysisGames)
        {
            var obgArrayFavorite = contextBD.Favorites.Where(x => x.idUser == MainWindow.IdUser).ToArray();
            bool favoriteTrue = false;
            for(int i = 0; i < obgArrayFavorite.Length; i++)
            {
                if(alalysisGames.id == obgArrayFavorite[i].Games.id)
                {
                    favoriteTrue = true;       
                }
                
            }

            if(favoriteTrue)
                return 1;
            else
                return 0;
        }

        public int AnalysisUserGamesGenre(Games alalysisGames)
        {
            var objArrayGames = contextBD.OrderList.Where(x => x.Orders.idUsers == MainWindow.IdUser).ToArray();
            int sumBuyGames = objArrayGames.Length;
            
            string[] arrayGenres = new string[contextBD.GameGenre.Count<GameGenre>()];
            //string[] arrayGenresUser;

            for(int i = 0; i < arrayGenres.Length; i++)
            {
                arrayGenres[i] = contextBD.GameGenre.ToArray()[i].genre;
            }

            //int sumGenres = 0;
            List<OrderList> genreGameUser = new List<OrderList>();
            bool controlGenres = false;

            for (int i = 0; i < sumBuyGames; i++)
            {
                controlGenres = false;
                    if (i == 0)
                        genreGameUser.Add(objArrayGames[i]);
                    else
                    {
                        for(int k = 0; k < genreGameUser.Count; k++)
                        {
                            if (objArrayGames[i].Games.GameGenre.genre == genreGameUser[k].Games.GameGenre.genre)
                                controlGenres = true;
                        }

                        if (!controlGenres)
                            genreGameUser.Add(objArrayGames[i]);
                    }
                
            }

            int[] countEachGenres = new int[genreGameUser.Count]; // Количество каждого жанра

            
            for(int i =0; i < sumBuyGames; i++)
            {
                for(int j = 0; j < genreGameUser.Count; j++)
                {
                    if (genreGameUser[j].Games.GameGenre.genre == objArrayGames[i].Games.GameGenre.genre)
                    {
                        countEachGenres[j]++;
                    }
                }
            }

            int MaxCount = countEachGenres[0];
            int numberMaxCount = 0;//Индекс максимального количества жанров

            for(int i = 1; i < countEachGenres.Length; i++)
            {
                if (countEachGenres[i] > MaxCount)
                {
                    MaxCount = countEachGenres[i];
                    numberMaxCount = i;
                }
            }


            if (alalysisGames.GameGenre.genre == genreGameUser[numberMaxCount].Games.GameGenre.genre)
                return 1;
            else
                return 0;

        }

       

        public void SetDataSet()
        {
            // 1) Совпадает по скидке
            // 2) Совпадает по цене
            // 3) Есть в избранном
            // 4) Совпадает по жанру

            var dataset = new List<Tuple<double, double[]>>
            {

                new Tuple<double, double[]>(0, new double[]{0, 0, 0, 0}),
                new Tuple<double, double[]>(0.1, new double[]{0, 0, 0, 1}),
                new Tuple<double, double[]>(0.1, new double[]{0, 0, 1, 0}),
                new Tuple<double, double[]>(0.45, new double[]{0, 0, 1, 1}),
                new Tuple<double, double[]>(0.2, new double[]{0, 1, 0, 0}),
                new Tuple<double, double[]>(0.5, new double[]{0, 1, 0, 1}),
                new Tuple<double, double[]>(0.6, new double[]{0, 1, 1, 0}),
                new Tuple<double, double[]>(0.8, new double[]{0, 1, 1, 1 }),
                new Tuple<double, double[]>(0.15, new double[]{1, 0, 0, 0 }),
                new Tuple<double, double[]>(0.55, new double[]{1, 0, 0, 1 }),
                new Tuple<double, double[]>(0.4, new double[]{1, 0, 1, 0 }),
                new Tuple<double, double[]>(0.9, new double[]{1, 0, 1, 1 }),
                new Tuple<double, double[]>(0.25, new double[]{1, 1, 0, 0 }),
                new Tuple<double, double[]>(0.7, new double[]{1, 1, 0, 1 }),
                new Tuple<double, double[]>(0.65, new double[]{1, 1, 1, 0 }),
                new Tuple<double, double[]>(1, new double[]{1, 1, 1, 1 })

            };
            
                for (int i = 0; i < 50000; i++)
                {
                    for (int j = 0; j < dataset.Count; j++)
                    {
                        neuralNetwork.Train(dataset[j].Item2, dataset[j].Item1);
                    }
                }
               
            
        }

      

        private void ВtnApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            var filtersGames = contextBD.Games.ToList();

            if (TxtPriceFrom.Text != "")
            {
                filtersGames = filtersGames.Where(x => x.price > Convert.ToDecimal(TxtPriceFrom.Text)).ToList();

            }
            if (TxtPriceTo.Text != "")
            {
                filtersGames = filtersGames.Where(x => x.price < Convert.ToDecimal(TxtPriceTo.Text)).ToList();
            }
            if (CmBGenre.SelectedIndex > -1)
            {
                filtersGames = filtersGames.Where(x => x.idGameGenre == Convert.ToInt32(CmBGenre.SelectedValue)).ToList();
            }
            if (CmBMode.SelectedIndex > -1)
            {
                filtersGames = filtersGames.Where(x => x.idGameMode == Convert.ToInt32(CmBMode.SelectedValue)).ToList();
            }

            listGame.ItemsSource = filtersGames.ToList();

        }

        private void TxtFilterNameAndDeveloper_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filtersGames = contextBD.Games.ToList();

            if (TxtFilterNameAndDeveloper.Text != "")
            {
                filtersGames = filtersGames.Where(x => x.NameGameAndDeveloper.Contains(TxtFilterNameAndDeveloper.Text)).ToList();
                listGame.ItemsSource = filtersGames.ToList();
            }
            else
                listGame.ItemsSource = contextBD.Games.ToList();
        }

        private void BtnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            TxtPriceFrom.Text = "";
            TxtPriceTo.Text = "";
            CmBGenre.SelectedIndex = -1;
            CmBMode.SelectedIndex = -1;
            ListFavoriteGame.Visibility = Visibility.Hidden;
            listGame.ItemsSource = contextBD.Games.ToList();
            TxtDontFavorite.Visibility = Visibility.Hidden;
        }

        private void BtnFavorites_Click(object sender, RoutedEventArgs e)
        {
            //listGame.ItemsSource = contextBD.Games.Where(x => x.Favorites. == MainWindow.IdUser).ToList();
            ListFavoriteGame.Visibility = Visibility.Visible;
            ListFavoriteGame.DataContext = contextBD.Games;
            ListFavoriteGame.ItemsSource = contextBD.Favorites.Where(x => x.idUser == MainWindow.IdUser).ToList();
            var CheckFavorite = contextBD.Favorites.FirstOrDefault(x => x.idUser == MainWindow.IdUser);
            if(CheckFavorite == null)
                TxtDontFavorite.Visibility = Visibility.Visible;


        }

        private void BtnFavoritesOpenForm_Click(object sender, RoutedEventArgs e)
        {
            if (controlDelete == true)
            {
                var favoritesGame = (sender as Button).DataContext as Favorites;
                var game = contextBD.Games.FirstOrDefault(x => x.id == favoritesGame.Games.id);
                NavigationService.Navigate(new GameForm(game));
            }
            controlDelete = true;
        }

        private void BtnDeleteFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            var deleteGameFromFavorites = (sender as Button).DataContext as Favorites;
            contextBD.Favorites.Remove(deleteGameFromFavorites);

            try
            {
                contextBD.SaveChanges();
                ListFavoriteGame.ItemsSource = contextBD.Favorites.Where(x => x.idUser == MainWindow.IdUser).ToList();
                controlDelete = false;
                var CheckFavorite = contextBD.Favorites.FirstOrDefault(x => x.idUser == MainWindow.IdUser);
                if (CheckFavorite == null)
                    TxtDontFavorite.Visibility = Visibility.Visible;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

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
using System.Windows.Threading;

namespace mastermind2PEMaikoVosWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        ComboBox[] guess = new ComboBox[4];
        Ellipse[] selectedEllipse = new Ellipse[4];
        string[] randomNumberColor = new string[4];

        string randomColorSolution;
        int points = 100;
        int rows = 0;
        int attempts = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindowLoader(object sender, RoutedEventArgs e)
        {
            StartingGame();
        }

        private void ToggleDebug(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.F12)
            {
                showRandomColors.Visibility = Visibility.Visible;
                showRandomColors.Text = randomColorSolution;
            }
            else
            {
                showRandomColors.Visibility = Visibility.Hidden;
            }
        }

        private void StartingGame()
        {
            randomNumberColor[0] = PickingRandomColor(rnd.Next(0, 6));
            randomNumberColor[1] = PickingRandomColor(rnd.Next(0, 6));
            randomNumberColor[2] = PickingRandomColor(rnd.Next(0, 6));
            randomNumberColor[3] = PickingRandomColor(rnd.Next(0, 6));

            randomColorSolution = $"{randomNumberColor[0]}, {randomNumberColor[1]}, {randomNumberColor[2]}, {randomNumberColor[3]}";
            this.Title = $"MasterMind";
            totalScore.Content = $"Score: {points}/100";
            totalAttempts.Content = $"Attempts: {attempts}/10";
            showRandomColors.Text = randomColorSolution;
        }

        private string PickingRandomColor(int randomNumber)
        {
            switch (randomNumber)
            {
                case 0:
                    return "Red";
                case 1:
                    return "Yellow";
                case 2:
                    return "Orange";
                case 3:
                    return "White";
                case 4:
                    return "Green";
                case 5:
                    return "Blue";
                default:
                    return "Black";
            }
        }

        private void ColorChange(object sender, EventArgs e)
        {
            ComboBox changedComboBox = sender as ComboBox;

            if (changedComboBox == colorOneComboBox)
            {
                colorFieldOne.Fill = Colorindex(changedComboBox.SelectedIndex);
            }
            else if (changedComboBox == colorTwoComboBox)
            {
                colorFieldTwo.Fill = Colorindex(changedComboBox.SelectedIndex);
            }
            else if (changedComboBox == colorThreeComboBox)
            {
                colorFieldThree.Fill = Colorindex(changedComboBox.SelectedIndex);
            }
            else if (changedComboBox == colorFourComboBox)
            {
                colorFieldFour.Fill = Colorindex(changedComboBox.SelectedIndex);
            }
        }

        private Brush Colorindex(int selectedindex)
        {
            switch (selectedindex)
            {
                case 0:
                    return Brushes.Red;
                case 1:
                    return Brushes.Yellow;
                case 2:
                    return Brushes.Orange;
                case 3:
                    return Brushes.White;
                case 4:
                    return Brushes.Green;
                case 5:
                    return Brushes.Blue;
                default:
                    return Brushes.Black;
            }
        }

        private void LabelColorCheck(Ellipse colorChecker, string[] randomNumberColor, int position, ComboBox input)
        {
            string solution;
            switch (position)
            {
                case 0:
                    solution = randomNumberColor[0];
                    break;
                case 1:
                    solution = randomNumberColor[1];
                    break;
                case 2:
                    solution = randomNumberColor[2];
                    break;
                case 3:
                    solution = randomNumberColor[3];
                    break;
                default:
                    return;
            }

            if (input.Text == "" || !randomNumberColor.Contains(input.Text))
            {
                points -= 2;
                colorChecker.StrokeThickness = 5;
            }
            else if (randomNumberColor.Contains(input.Text) && input.Text != "" && input.Text != solution)
            {
                points -= 1;
                colorChecker.Stroke = Brushes.Wheat;
                colorChecker.StrokeThickness = 4;
            }
            else
            {
                colorChecker.Stroke = Brushes.DarkRed;
                colorChecker.StrokeThickness = 4;
            }
            totalScore.Content = $"Score: {points}/100";

            if (input.Text == "")
            {
                MessageBox.Show("Invalid color", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowGuess()
        {
            RowDefinition newRow = new RowDefinition();
            newRow.Height = GridLength.Auto;
            addRows.RowDefinitions.Add(newRow);

            guess[0] = colorOneComboBox;
            guess[1] = colorTwoComboBox;
            guess[2] = colorThreeComboBox;
            guess[3] = colorFourComboBox;

            selectedEllipse[0] = colorFieldOne;
            selectedEllipse[1] = colorFieldTwo;
            selectedEllipse[2] = colorFieldThree;
            selectedEllipse[3] = colorFieldFour;

            for (int i = 0; i < guess.Length; i++)
            {
                Ellipse makingNewEllipse = new Ellipse();
                makingNewEllipse.Fill = Colorindex(guess[i].SelectedIndex);
                makingNewEllipse.Stroke = selectedEllipse[i].Stroke;
                makingNewEllipse.StrokeThickness = selectedEllipse[i].StrokeThickness;
                makingNewEllipse.Height = 30;
                makingNewEllipse.Width = 30;
                makingNewEllipse.Margin = new Thickness(2.2);

                Grid.SetRow(makingNewEllipse, rows);
                Grid.SetColumn(makingNewEllipse, i);

                addRows.Children.Add(makingNewEllipse);
            }
            rows++;
        }

        private void CheckCodeButton_Click(object sender, RoutedEventArgs e)
        {
            LabelColorCheck(colorFieldOne, randomNumberColor, 0, colorOneComboBox);
            LabelColorCheck(colorFieldTwo, randomNumberColor, 1, colorTwoComboBox);
            LabelColorCheck(colorFieldThree, randomNumberColor, 2, colorThreeComboBox);
            LabelColorCheck(colorFieldFour, randomNumberColor, 3, colorFourComboBox);

            ShowGuess();
            attempts++;

            totalAttempts.Content = $"Attempts: {attempts}/10";
            if (attempts == 10)
            {
                MessageBox.Show("Max attempts reached", $"Attempts {attempts}/10", MessageBoxButton.OK);
                this.Close();
            }
        }
    }
}

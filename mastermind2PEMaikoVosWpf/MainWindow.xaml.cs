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
        string randomColorOne, randomColorTwo, randomColorThree, randomColorFour;
        string randomNumberColor;
        int attempts = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindowLoader(object sender, RoutedEventArgs e)
        {
            randomColorOne = PickingRandomColor(rnd.Next(0, 6));
            randomColorTwo = PickingRandomColor(rnd.Next(0, 6));
            randomColorThree = PickingRandomColor(rnd.Next(0, 6));
            randomColorFour = PickingRandomColor(rnd.Next(0, 6));

            this.Title = $"MasterMind: attempts:{attempts}/10";

            randomNumberColor = $"{randomColorOne}, {randomColorTwo}, {randomColorThree}, {randomColorFour}";
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
                colorFieldOne.Background = Colorindex(changedComboBox.SelectedIndex);
            }
            else if (changedComboBox == colorTwoComboBox)
            {
                colorFieldTwo.Background = Colorindex(changedComboBox.SelectedIndex);
            }
            else if (changedComboBox == colorThreeComboBox)
            {
                colorFieldThree.Background = Colorindex(changedComboBox.SelectedIndex);
            }
            else if (changedComboBox == colorFourComboBox)
            {
                colorFieldFour.Background = Colorindex(changedComboBox.SelectedIndex);
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

        private void LabelColorCheck(Label colorChecker, string randomNumberColor, int position, ComboBox input)
        {
            string solution;
            switch (position)
            {
                case 0:
                    solution = randomColorOne;
                    break;
                case 1:
                    solution = randomColorTwo;
                    break;
                case 2:
                    solution = randomColorThree;
                    break;
                case 3:
                    solution = randomColorFour;
                    break;
                default:
                    return;
            }

            if (randomNumberColor.Contains(input.Text) && input.Text != "")
            {
                colorChecker.BorderBrush = Brushes.Wheat;
                if (input.Text == solution)
                {
                    colorChecker.BorderBrush = Brushes.DarkRed;
                }
                colorChecker.BorderThickness = new Thickness(4);
            }
            else
            {
                colorChecker.BorderThickness = new Thickness(0);
            }
        }

        private void checkCodeButton_Click(object sender, RoutedEventArgs e)
        {
            LabelColorCheck(colorFieldOne, randomNumberColor, 0, colorOneComboBox);
            LabelColorCheck(colorFieldTwo, randomNumberColor, 1, colorTwoComboBox);
            LabelColorCheck(colorFieldThree, randomNumberColor, 2, colorThreeComboBox);
            LabelColorCheck(colorFieldFour, randomNumberColor, 3, colorFourComboBox);
            attempts++;
            this.Title = $"MasterMind: attempts:{attempts}/10";
            if (attempts == 10)
            {
                MessageBox.Show("Max attempts reached", $"Attempts {attempts}/10", MessageBoxButton.OK);
                this.Close();
            }

            

        }
    }
}

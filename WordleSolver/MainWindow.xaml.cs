using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WordleSolverWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public bool EnableFocusChanging { get; set; } = true;

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show(this, "Вы уверены, что хотите выйти?", "Подтвердите выход", MessageBoxButton.YesNo);

            if (answer == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void TextBoxLetter_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.Key == Key.Right)
                {
                    var focusDirection = FocusNavigationDirection.Next;
                    ChangeFocus(focusDirection);
                }
                else if (e.Key == Key.Left)
                {
                    var focusDirection = FocusNavigationDirection.Previous;
                    ChangeFocus(focusDirection);
                }
            }
        }

        private void TextBoxLetter_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender is TextBox)
            {
                if (e.Key == Key.Back && EnableFocusChanging)
                {
                    var focusDirection = FocusNavigationDirection.Previous;
                    ChangeFocus(focusDirection);
                }

                if (e.Key == Key.Right)
                {
                    var focusDirection = FocusNavigationDirection.Next;
                    ChangeFocus(focusDirection);
                }
                else if (e.Key == Key.Left)
                {
                    var focusDirection = FocusNavigationDirection.Previous;
                    ChangeFocus(focusDirection);
                }
            }
        }

        private void TextBoxLetter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && textBox != null)
            {
                var text = textBox.Text;

                EnableFocusChanging = string.IsNullOrEmpty(text);

                if (!string.IsNullOrEmpty(text))
                {
                    if (text.Length == 1)
                    {

                    }
                    else if (text.Length > 1)
                    {
                        textBox.Text = text[0].ToString();

                        var focusDirection = FocusNavigationDirection.Next;
                        ChangeFocus(focusDirection);
                    }
                }
            }
        }

        private void ChangeFocus(FocusNavigationDirection direction)
        {
            var request = new TraversalRequest(direction);
            var elementWithFocus = Keyboard.FocusedElement as UIElement;
            elementWithFocus?.MoveFocus(request);
        }
    }
}

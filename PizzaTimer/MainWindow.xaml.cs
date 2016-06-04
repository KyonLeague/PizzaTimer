using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PizzaTimer.Utilities;

namespace PizzaTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
    
        private static TimeSpan myTimerTime;
        private static Timer pizzerTimer = new Timer();

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var text = this.textBox;
                myTimerTime = TimeSpan.Parse(text.Text);
                label.Content = "Set current time: " + myTimerTime.Hours.ToString("00") + ":" + myTimerTime.Minutes.ToString("00") + ":" +
                                myTimerTime.Seconds.ToString("00");
                Console.WriteLine(myTimerTime.ToString());
            }
            catch (Exception w)
            {
                Console.WriteLine(w.Message);
            }

            // debugging
            //ConsoleManager.Show();
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (pizzerTimer.Enabled)
            {
                MessageBox.Show("Der Timer ist bereits am laufen PLS ... ", "Error", MessageBoxButton.OK);
                return;
            }

            try
            {
                var text = (TextBox)sender;
                myTimerTime = TimeSpan.Parse(text.Text);
                label.Content = "Set current time: " + myTimerTime.Hours.ToString("00") + ":" + myTimerTime.Minutes.ToString("00") + ":" +
                                myTimerTime.Seconds.ToString("00");
                Console.WriteLine(myTimerTime.ToString());
            }
            catch (Exception w)
            {
                Console.WriteLine(w.Message);
            }
            
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (pizzerTimer.Enabled) return;

            pizzerTimer.Start();
            pizzerTimer.Interval = 1000;
            pizzerTimer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object o, ElapsedEventArgs args)
        {
            myTimerTime -= TimeSpan.FromSeconds(1);
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.label.Content = "Timer is running: " + myTimerTime.Hours.ToString("00") + ":" + myTimerTime.Minutes.ToString("00") + ":" +
                                     myTimerTime.Seconds.ToString("00");
            }));
        }

        private void Button_Copy_OnClick(object sender, RoutedEventArgs e)
        {
            pizzerTimer.Stop();
            pizzerTimer.Elapsed -= Timer_Elapsed;
            TextBox_OnTextChanged(textBox, null);
        }
    }
}

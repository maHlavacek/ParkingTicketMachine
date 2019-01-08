using ParkingTicketMachine.Core;
using System;
using System.Text;
using System.Windows;

namespace ParkingTicketMachine.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            FastClock.Instance.Time = DateTime.Now;
            FastClock.Instance.Factor = 360;
            FastClock.Instance.OneMinuteIsOver += Update;
            FastClock.Instance.IsRunning = true;
            Update(this, FastClock.Instance.Time);
        }

        private void Update(object sender, DateTime time)
        {
            Title = $"Parkscheinzentrale {time.ToShortTimeString()}";
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            SlotMachineWindow slotMachineWindow = new SlotMachineWindow(TextBoxAddress.Text, TicketPrinted);
            slotMachineWindow.Owner = this;
            slotMachineWindow.Show();
        }
        private void TicketPrinted(object sender, Booking booking)
        {
            TextBlockLog.Text += $"{booking.ToString()}\n";
        }



    }
}

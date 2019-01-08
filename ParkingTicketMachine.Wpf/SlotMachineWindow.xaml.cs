using System;
using System.Windows;
using ParkingTicketMachine.Core;

namespace ParkingTicketMachine.Wpf
{
    /// <summary>
    /// Interaction logic for SlotMachineWindow.xaml
    /// </summary>
    public partial class SlotMachineWindow
    {

        public SlotMachineWindow(string name, EventHandler<Booking> ticketReady)
        {
            InitializeComponent();
        }

        private void ButtonInsertCoin_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonPrintTicket_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}

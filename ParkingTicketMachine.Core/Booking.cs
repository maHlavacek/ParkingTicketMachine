using System;

namespace ParkingTicketMachine.Core
{
    public class Booking
    {
        private string _address;
        private int _credit;
        private int _donation;
        private DateTime _startParkingTime;
        private DateTime _endParkingTime;

        public Booking(string address, int credit, int donation, DateTime startParkingTime, DateTime endParkingTime)
        {
            _address = address;
            _credit = credit;
            _donation = donation;
            _startParkingTime = startParkingTime;
            _endParkingTime = endParkingTime;
        }
        public override string ToString()
        {
            string donation = null;
            if(_donation > 0)
            {
                donation = $"Vielen dank für die großzügige Spende von {_donation / 100:f2} Euro";
            }
            return $"{_address}: New Ticket created Price: {_credit / 100:f2} Euro Time: {_startParkingTime.ToShortTimeString()} - {_endParkingTime.ToShortTimeString()}, {donation}";
        }
    }
}

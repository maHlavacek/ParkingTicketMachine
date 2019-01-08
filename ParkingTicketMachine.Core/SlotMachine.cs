using System;

namespace ParkingTicketMachine.Core
{
    public class SlotMachine
    {
        #region Fields
        const int CREDITLIMIT = 150;

        private DateTime _endParkingTime;
        private DateTime _startParkingTime;
        private string _address;
        private int _credit;
        private string[] _validCoins = new string[] { "10 Cent", "20 Cent", "50 Cent", "1 Euro", "2 Euro" };
        private int[] _coins = new int[] { 10, 20, 50, 100, 200 };
        #endregion

        #region Events
        public event EventHandler<DateTime?> ParkingTimeSet;
        public event EventHandler<Booking> TicketPrinted;
        #endregion

        #region Constructor
        public SlotMachine(string adress)
        {
            _address = adress;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Coins werden eingelesen und auf min und max Betrag geprüft
        /// Sollte die Parkzeit in einer kostenfreien Zeit liegen wird die Parkdauer um diese Zeit verlängert
        /// </summary>
        /// <param name="coinsText"></param>
        private void Insert(string coinsText)
        {
            int coinsInserted = GetCoin(coinsText);

            if (coinsInserted == -1)
                return;

            FastClock.Instance.IsRunning = false;
            _credit += coinsInserted;

            if (_credit < 50)
                return;

            int validCredit = Math.Min(Math.Max(0, _credit), CREDITLIMIT);
            int parkindDration = validCredit * 30 / 50;
            _startParkingTime = FastClock.Instance.Time;
            _endParkingTime = _startParkingTime;

            if(_startParkingTime.Hour >= 18 || _startParkingTime.Hour < 8)
            {
                _endParkingTime = DateTime.Parse("08:00").AddMinutes(_endParkingTime.Minute);
                _endParkingTime = _endParkingTime.AddMinutes(parkindDration);
            }
            if(_endParkingTime.Hour >= 18)
            {
                _endParkingTime = DateTime.Parse("08:00").AddHours(_endParkingTime.Hour - 18).AddMinutes(parkindDration);
            }
            ParkingTimeSet?.Invoke(this,_endParkingTime);
        }

        /// <summary>
        /// Coins werde auf Gültigkeit geprüft und deren Wert zurückgegeben 
        /// Ungültiger einwurf gibt den Wert -1 zurück
        /// </summary>
        /// <param name="coinsText"></param>
        /// <returns></returns>
        private int GetCoin(string coinsText)
        {
            for (int i = 0; i < _validCoins.Length; i++)
            {
                if (coinsText.Contains(_validCoins[i])) return _coins[i];
            }
            return -1;
        }
        /// <summary>
        /// Ticket wird gedruckt und an Zentrale übertragen
        /// </summary>
        private void Print()
        {
            if (_credit == 0)
                return;

            int price = Math.Min(_credit, 150);
            TicketPrinted?.Invoke(this, new Booking(_address, _credit,_credit - price, _startParkingTime, _endParkingTime));
            FastClock.Instance.IsRunning = true;
            ParkingTimeSet?.Invoke(this, null);
            _credit = 0;
        }
        /// <summary>
        /// Aktion wird abgebroche
        /// </summary>
        private void Cancel()
        {
            _credit = 0;
            ParkingTimeSet?.Invoke(this, null);
            FastClock.Instance.IsRunning = true;
        }

        #endregion
    }
}

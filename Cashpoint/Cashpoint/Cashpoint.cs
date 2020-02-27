namespace CashpointModel
{
    using System;
    using System.Collections.Generic;

    public sealed class Cashpoint
    {
        private readonly List<uint> _banknotes = new List<uint>();

        private uint _total;

        private bool[] _granted = { true };

        public uint Total => _total;

        // value - это номинал банкноты
        public void AddBanknote(uint value)
        {
            _banknotes.Add(value);
            _total += value;

            GrantsWhenAdd(value);
        }

        public void AddBanknote(uint[] value)
        {
            foreach (var el in value)
            {
                _banknotes.Add(el);
                _total += el;
                GrantsWhenAdd(el);
            }
        }

        public void RemoveBanknote(uint value)
        {
            if (_banknotes.Remove(value))
            {
                _total -= value;
                _granted = new bool[]{ true };
                foreach (var i in _banknotes)
                {
                    GrantsWhenAdd(i);
                }

                //или CalculateGrants();
                //вероятно, он будет быстрее, т.к. не будет использоваться постоянный
                //Array.Resize()
            }
        }

        public void RemoveBanknote(uint[] value)
        {
            foreach(var el in value)
            {
                RemoveBanknote(el);
            }
        }

        public bool CanGrant(uint value)
        {

            if (value > _total)
            {
                return false;
            }

            return _granted[(int)value];
        }

        private void GrantsWhenAdd(uint aBanknote)
        {
            int theBanknote = (int)aBanknote;

            var cells = new List<int>();

            for(var i = 0; i < _granted.Length; i++)
            {
                if (_granted[i])
                    cells.Add(i + theBanknote);
            }

            Array.Resize(ref _granted, _granted.Length + theBanknote);

            foreach(var i in cells)
            {
                _granted[i] = true;
            }
        }

        private void CalculateGrants()
        {
            _granted = new bool[_total + 1];
            _granted[0] = true;

            foreach (var b in _banknotes)
            {
                for (var i = (int)_total; i >= 0; i--)
                {
                    if (_granted[i])
                    {
                        _granted[i + b] = true;
                    }
                }
            }
        }
    }
}
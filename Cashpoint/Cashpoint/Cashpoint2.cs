namespace CashpointModel
{
    using System;
    using System.Collections.Generic;

    public sealed class Cashpoint2
    {
        //                                                              //номинал/количество
        private readonly Dictionary<uint, uint> _banknotes = new Dictionary<uint, uint>();

        private uint _total;

        private uint[] _granted = { 1 };

        public uint Total => _total;

        private bool CellIsFull(uint nominal)
        {
            if(_banknotes.TryGetValue(nominal, out uint count))
            {
                if (count == 255)
                    return true;
            }
            return false;
        }

        public void AddBanknote(uint nominal)
        {
            if (CellIsFull(nominal))
            {
                throw new CellIsFullExeption(nominal.ToString());
            }

            if (_banknotes.ContainsKey(nominal))
            {
                _banknotes[nominal] += 1;
            }
            else
            {
                _banknotes.Add(nominal, 1);
            }
            
            _total += nominal;

            GrantsWhenAdd(nominal);
        }

        public void AddBanknote(uint[] banknotes)
        {
            foreach (var el in banknotes)
            {
                AddBanknote(el);
            }
        }

        public void RemoveBanknote(uint[] banknotes)
        {
            foreach (var el in banknotes)
            {
                RemoveBanknote(el);
            }
        }

        public void RemoveBanknote(uint nominal)
        {
            if (nominal == 0)
            {
                return;
            }

            if (!_banknotes.ContainsKey(nominal))
            {
                throw new BadNominalExeption(nominal.ToString());
            }

            _total -= nominal;

            GrantsWhenDelete(nominal);
        }

        public bool CanGrant(uint value)
        {
            if(value == 0)
            {
                return true;
            }

            if (value > _total)
            {
                return false;
            }

            if (_granted[(int)value] > 0)
            {
                return true;
            }

            return false;
        }

        private void GrantsWhenAdd(uint aBanknote)
        {
            int theBanknote = (int)aBanknote;

            Array.Resize(ref _granted, _granted.Length + theBanknote);

            // добавляем от конца к началу

            for (var i = _granted.Length - 1; i >= 0; i--)
            {
                if (_granted[i] > 0)
                {
                    _granted[i + theBanknote] += _granted[i];
                }
            }
        }

        private void GrantsWhenDelete(uint aBanknote)
        {
            int theBanknote = (int)aBanknote;

            Array.Resize(ref _granted, _granted.Length - theBanknote);

            for (var i = 0; i < _granted.Length - theBanknote; i++)
            {
                if (_granted[i] > 0)
                {
                    _granted[i + theBanknote] -= _granted[i];
                }
            }
        }
    }
}
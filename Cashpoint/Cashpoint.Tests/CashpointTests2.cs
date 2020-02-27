namespace CashpointModel
{
    using NUnit.Framework;
    using CashpointModel;
    using System;
    using System.Reflection;

    [TestFixture]
    public class CashpointTests2
    {

        [TestCase(new uint[] { 2, 3 }, 2, ExpectedResult = new uint[] { 1, 0, 0, 1 })]
        [TestCase(new uint[] { 2, 3, 2 }, 3, ExpectedResult = new uint[] { 1, 0, 2, 0, 1 })]
        [TestCase(new uint[] { 2, 3, 2, 1 }, 1, ExpectedResult = new uint[] { 1, 0, 2, 1, 1, 2, 0, 1 })]
        [TestCase(new uint[] { 2, 3, 2, 1, 4 }, 3, ExpectedResult = new uint[] { 1, 1, 2, 2, 2, 2, 2, 2, 1, 1 })]
        public uint[] GrantsWhenDelete_With_One_Banknotes(uint[] array, int nominal)
        {
            // 0,1,2,3,4,5
            // 1,0,0,1 = 2,3
            //
            // 0,1,2,3,4,5,6,7
            // 1,0,2,0,1, = 2,2
            //
            // 0,1,2,3,4,5,6,7,8
            // 1,0,2,1,1,2,0,1 = 2,3,2
            //
            // 0,1,2,3,4,5,6,7,8,9,10,11,12
            // 1,1,2,2,2,2,2,2,1,1 = 2,2,1,4

            // т.е. считаем сначала от 0 индекса
            // и удаляем это значение индекса через n шагов, что является номиналом

            var cashpoint = new Cashpoint2();

            cashpoint.AddBanknote(array);
            cashpoint.RemoveBanknote((uint)nominal);

            Type theType = typeof(Cashpoint2);

            FieldInfo theField = theType.GetField("_granted", BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public);

            return (uint[])theField.GetValue(cashpoint);

        }

        [TestCase(new uint[] { 2, 3 }, ExpectedResult = new uint[] { 1, 0, 1, 1, 0, 1 })]
        [TestCase(new uint[] { 2, 3, 2 }, ExpectedResult = new uint[] { 1, 0, 2, 1, 1, 2, 0, 1 })]
        [TestCase(new uint[] { 2, 3, 2, 1 }, ExpectedResult = new uint[] { 1, 1, 2, 3, 2, 3, 2, 1, 1 })]
        [TestCase(new uint[] { 2, 3, 2, 1, 4 }, ExpectedResult = new uint[] { 1, 1, 2, 3, 3, 4, 4, 4, 3, 3, 2, 1, 1 })]
        public uint[] GrantsWhenAdd_With_Some_Banknotes(uint[] array)
        {
            // 0,1,2,3,4,5
            // 1,0,1,1,0,1 = 2,3
            //
            // 0,1,2,3,4,5,6,7
            // 1,0,2,1,1,2,0,1 = 2,3,2
            //
            // 0,1,2,3,4,5,6,7,8
            // 1,1,2,3,2,3,2,1,1 = 2,3,2,1
            //
            // 0,1,2,3,4,5,6,7,8,9,10,11,12
            // 1,1,2,3,3,4,4,4,3,3,02,01,01 = 2,3,2,1,4

            var cashpoint = new Cashpoint2();

            cashpoint.AddBanknote(array);

            Type theType = typeof(Cashpoint2);

            FieldInfo theField = theType.GetField("_granted", BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public);

            return (uint[])theField.GetValue(cashpoint);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(1)]
        [TestCase(4)]
        public void CanGrant_With_Some_Banknotes(int nominal)
        {
            uint[] array = { 2, 3, 2, 1, 4 };

            var cashpoint = new Cashpoint2();

            cashpoint.AddBanknote(array);

            Assert.That(cashpoint.CanGrant((uint)nominal), Is.True);
        }

        [Test]
        public void Total_With_Add_Remove_Banknotes()
        {
            uint[] array = { 2, 3, 2, 1, 4 };
            uint[] sum = { 2, 5, 7, 8, 12 };
            uint[] afterRemove = { 10, 7, 5, 4, 0 };

            var cashpoint = new Cashpoint2();

            int i = 0;
            foreach(var el in array)
            {
                cashpoint.AddBanknote(array[i]);
                Assert.That(cashpoint.Total, Is.EqualTo(sum[i]));
                i++;
            }

            i = 0;
            foreach (var el in array)
            {
                cashpoint.RemoveBanknote(array[i]);
                Assert.That(cashpoint.Total, Is.EqualTo(afterRemove[i]));
                i++;
            }
        }

        [Test]
        public void AddBanknote_CellIsFullExeption()
        {
            var cashpoint = new Cashpoint2();

            int counter = 255;
            while(counter > 0)
            {
                cashpoint.AddBanknote(1);
                counter--;
            }

            Assert.Throws<CellIsFullExeption>(() => cashpoint.AddBanknote(1));

        }

        [Test]
        public void AddBanknote_BadNominalExeption()
        {
            var cashpoint = new Cashpoint2();

            Assert.Throws<BadNominalExeption>(() => cashpoint.RemoveBanknote(1));

        }
    }
}
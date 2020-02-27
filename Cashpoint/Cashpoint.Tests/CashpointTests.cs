namespace CashpointModel.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class CashpointTests
    {
        [Test]
        public void AddBanknote_SingleBanknote_ShouldIncrementTotal()
        {
            var cashpoint = new Cashpoint();
            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(5u),
                "Добавление единственной банкноты не было произведено");
        }

        [Test]
        public void AddBanknote_MultipleBanknotes_ShouldIncrementTotal()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(10);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(15u),
                "Добавление второй банкноты не было произведено");
        }

        [Test]
        public void RemoveBanknote_CashpointIsEmpty_ShouldPreserveTotal()
        {
            var cashpoint = new Cashpoint();

            cashpoint.RemoveBanknote(5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(0),
                "Извлечена несуществующая купюра из пустого банкомата");
        }

        [Test]
        public void RemoveBanknote_UnknownBanknote_ShouldPreserveTotal()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(7);
            cashpoint.RemoveBanknote(5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(7),
                "Извлечена несуществующая купюра");
        }

        [Test]
        public void RemoveBanknote_ExistingBanknote_ShouldDecrementTotal()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(10);
            cashpoint.RemoveBanknote(5);

            Assert.That(
                cashpoint.Total,
                Is.EqualTo(10),
                "Купюра извлечена некорректно");
        }

        [Test]
        public void Total_InitialState_ShouldBeZero()
        {
            var cashpoint = new Cashpoint();
            Assert.That(
                cashpoint.Total,
                Is.EqualTo(0u),
                "Новый банкомат оказался не пустой");
        }

        [Test]
        public void CanGrant_SumIsZero_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            Assert.That(
                cashpoint.CanGrant(0),
                "Банкомат не смог выдать 0");

            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.CanGrant(0),
                "Банкомат не смог выдать 0 после добавления купюры");
        }

        [Test]
        public void CanGrant_SumEqualsToSingleBanknote_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.CanGrant(5),
                "Банкомат не смог выдать единственную банкноту");
        }

        [Test]
        public void CanGrant_SumNotEqualToSingleBanknote_ShouldNotGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);

            Assert.That(
                cashpoint.CanGrant(4),
                Is.False,
                "Банкомат смог выдать значение меньше номинала единственной купюры");

            Assert.That(
                cashpoint.CanGrant(6),
                Is.False,
                "Банкомат смог выдать значение больше номинала единственной купюры");
        }

        [Test]
        public void CanGrant_SumEqualsToBanknotesTotal_ShouldGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(3);

            Assert.That(
                cashpoint.CanGrant(8),
                "Банкомат не смог выдать сумму двух купюр");
        }

        [Test]
        public void CanGrant_MultipleBanknotesIntermediateValues_ShouldNotGrant()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(5);
            cashpoint.AddBanknote(3);

            Assert.That(
                cashpoint.CanGrant(6),
                Is.False,
                "Банкомат смог выдать значение между номиналами купюр");
        }

        [Test]
        [Ignore("Результат теста зависит от мощности машины")]
        [Timeout(20000)]
        public void CanGrant_PerformanceTest()
        {
            var cashpoint = new Cashpoint();
            for (var i = 0; i < 2; i++)
            {
                cashpoint.AddBanknote(10);
                cashpoint.AddBanknote(50);
                cashpoint.AddBanknote(100);
                cashpoint.AddBanknote(200);
                cashpoint.AddBanknote(500);
                cashpoint.AddBanknote(1000);
                cashpoint.AddBanknote(2000);
                cashpoint.AddBanknote(5000);
            }

            Assert.That(cashpoint.CanGrant(3350));
            Assert.That(cashpoint.CanGrant(3980), Is.False);
        }

        [Test]
        public void FindCells_With_Add_Some_Banknotes()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(new uint[] { 1, 2, 6 });


            Assert.That(cashpoint.CanGrant(6), Is.True);
            Assert.That(cashpoint.CanGrant(2), Is.True);
            Assert.That(cashpoint.CanGrant(1), Is.True);
            Assert.That(cashpoint.CanGrant(3), Is.True);
            Assert.That(cashpoint.CanGrant(7), Is.True);
            Assert.That(cashpoint.CanGrant(9), Is.True);
        }

        [Test]
        public void FindCells_With_Add_One_Banknotes()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(1);

            Assert.That(cashpoint.CanGrant(0), Is.True);
            Assert.That(cashpoint.CanGrant(1), Is.True);

            cashpoint.AddBanknote(2);

            Assert.That(cashpoint.CanGrant(2), Is.True);
            Assert.That(cashpoint.CanGrant(3), Is.True);

            cashpoint.AddBanknote(6);

            Assert.That(cashpoint.CanGrant(6), Is.True);
            Assert.That(cashpoint.CanGrant(7), Is.True);
            Assert.That(cashpoint.CanGrant(9), Is.True);
        }

        [Test]
        public void FindCells_With_Remove_Some_Banknotes()
        {
            var cashpoint = new Cashpoint();

            cashpoint.AddBanknote(new uint[] { 1, 2, 6, 10});

            cashpoint.RemoveBanknote(new uint[] { 1, 10 });

            Assert.That(cashpoint.CanGrant(2), Is.True);
            Assert.That(cashpoint.CanGrant(6), Is.True);
            Assert.That(cashpoint.CanGrant(8), Is.True);

            cashpoint.RemoveBanknote(2);
            Assert.That(cashpoint.CanGrant(6), Is.True);
        }
    }
}
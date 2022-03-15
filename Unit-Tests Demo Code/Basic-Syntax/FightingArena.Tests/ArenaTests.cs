namespace Tests
{
    using System;
    
    using NUnit.Framework;
    
    using FightingArena;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        private Warrior warrior1;
        private Warrior attacker;
        private Warrior defender;

        [SetUp]
        public void Setup()
        {
            this.arena = new Arena();
            this.warrior1 = new Warrior("Kesho", 5, 50);
            this.attacker = new Warrior("Kesho", 10, 80);
            this.defender = new Warrior("Fesho", 5, 60);
        }

        [Test]
        public void TestConstructorWorksCorrectly()
        {
            Assert.IsNotNull(this.arena.Warriors);
            //Assert.DoesNotThrow(() => this.arena = new Arena());
        }

        [Test]
        public void EnrollShouldAddWarriorToTheArena()
        {
            this.arena.Enroll(this.warrior1);
            Assert.That(this.arena.Warriors, Has.Member(warrior1));
        }

        [Test]
        public void SuccessfullEnrollShouldIncreaseArenaCount()
        {
            int expectedCount = 2;

            this.arena.Enroll(this.warrior1);
            this.arena.Enroll(new Warrior("Fesho", 40, 40));

            int actualCount = this.arena.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void EnrollingSameWarriorMoreThanOnceShouldThrowException()
        {
            this.arena.Enroll(this.warrior1);
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(this.warrior1);
            });
        }

        [Test]
        public void EnrollingWarriorsWithSameNameShouldThrowException()
        {
            Warrior warrior1Copy = new Warrior(warrior1.Name, warrior1.Damage, warrior1.HP);

            this.arena.Enroll(this.warrior1);
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(warrior1Copy);
            });
        }

        [Test]
        public void TestFightMethodWithMissingAttacker()
        {
            this.arena.Enroll(this.defender);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(this.attacker.Name, this.defender.Name);
            });
        }

        [Test]
        public void TestFightMethodWithMissingDefender()
        {
            this.arena.Enroll(this.attacker);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(this.attacker.Name, this.defender.Name);
            });
        }

        [Test]
        public void TestFightMethodWithAttackerAndDefender()
        {
            this.arena.Enroll(this.attacker);
            this.arena.Enroll(this.defender);

            int expectedAttackerHP = this.attacker.HP - this.defender.Damage;
            int expectedDefenderHP = this.defender.HP - this.attacker.Damage;

            this.arena.Fight(this.attacker.Name, this.defender.Name);

            Assert.AreEqual(expectedAttackerHP, this.attacker.HP);
            Assert.AreEqual(expectedDefenderHP, this.defender.HP);
        }
    }
}

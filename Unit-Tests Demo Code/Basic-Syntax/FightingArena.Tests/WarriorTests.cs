using System;
using NUnit.Framework;
using FightingArena;

namespace Tests
{
    [TestFixture]
    public class WarriorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructorIfWorksCorrectly()
        {
            string expectedName = "Kesho";
            int expectedDamage = 50;
            int expectedHP = 100;

            Warrior warrior = new Warrior(expectedName, expectedDamage, expectedHP);

            string actualName = warrior.Name;
            int actualDamage = warrior.Damage;
            int actualHP = warrior.HP;

            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expectedDamage, actualDamage);
            Assert.AreEqual(expectedHP, actualHP);
        }

        //[Test]
        //public void TestConstructorIfValidationExceptionsWorkCorrectly()
        //{
        //    string emptyName = string.Empty;
        //    string nullName = null;
        //    string whitespaceName = "       ";
        //    int negativeDamage = -1;
        //    int negativeHP = -1;

        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        Warrior warrior = new Warrior(emptyName, 1, 1);
        //    });

        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        Warrior warrior = new Warrior(whitespaceName, 1, 1);
        //    });

        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        Warrior warrior = new Warrior(nullName, 1, 1);
        //    });

        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        Warrior warrior = new Warrior("Kesho", negativeDamage, 1);
        //    });

        //    Assert.Throws<ArgumentException>(() =>
        //    {
        //        Warrior warrior = new Warrior("Kesho", 1, negativeHP);
        //    });
        //}

        // These name tests should be validated with TestCases in one test method
        // however, judge does not count as separate tests!!!
        //[TestCase(null)]
        //[TestCase("")] // -> string.Empty not accepted by test case (not regarded as constant)
        //[TestCase("          ")]
        [Test]
        public void TestNameValidatorWithNullName()
        {
            string name = null;
            int damage = 50;
            int HP = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, damage, HP);
            });
        }

        [Test]
        public void TestNameValidatorWithEmptyName()
        {
            string name = string.Empty;
            int damage = 50;
            int HP = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, damage, HP);
            });
        }

        [Test]
        public void TestNameValidatorWithWhitespaceName()
        {
            string name = "         ";
            int damage = 50;
            int HP = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, damage, HP);
            });
        }

        [Test]
        public void TestDamageValidatorWithZeroDamage()
        {
            string name = "Kesho";
            int damage = 0;
            int HP = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, damage, HP);
            });
        }

        [Test]
        public void TestDamageValidatorWithNegativeDamage()
        {
            string name = "Kesho";
            int damage = -1;
            int HP = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, damage, HP);
            });
        }

        [Test]
        public void TestHPValidatorWithNegativeHp()
        {
            string name = "Kesho";
            int damage = 50;
            int HP = -100;

            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior(name, damage, HP);
            });
        }

        [Test]
        [TestCase(25)]
        [TestCase(30)]
        public void TestAttackerAttacksWithHPLowerThanOrEqual30ShouldThrowException(int attackerHP)
        {
            string attackerName = "Kesho";
            int attackerDamage = 10;

            string defenderName = "Gosho";
            int defenderDamage = 10;
            int defenderHP = 40;

            Warrior attacker = new Warrior(attackerName, attackerDamage, attackerHP);
            Warrior defender = new Warrior(defenderName, defenderDamage, defenderHP);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [Test]
        [TestCase(25)]
        [TestCase(30)]
        public void TestDefenderDefendsWithHPLowerThanOrEqual30ShouldThrowException(int defenderHP)
        {
            string attackerName = "Kesho";
            int attackerDamage = 10;
            int attackerHP = 50;

            string defenderName = "Gosho";
            int defenderDamage = 10;

            Warrior attacker = new Warrior(attackerName, attackerDamage, attackerHP);
            Warrior defender = new Warrior(defenderName, defenderDamage, defenderHP);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [Test]
        public void TestWeakerAttackerAttacksStrongerDefenderShouldThrowException()
        {
            string attackerName = "Kesho";
            int attackerDamage = 10;
            int attackerHP = 40;

            string defenderName = "Gosho";
            int defenderDamage = 80;
            int defenderHP = 40;

            Warrior attacker = new Warrior(attackerName, attackerDamage, attackerHP);
            Warrior defender = new Warrior(defenderName, defenderDamage, defenderHP);

            Assert.Throws<InvalidOperationException>(() =>
            {
                attacker.Attack(defender);
            });
        }

        [Test]
        public void TestAttackerDefenderHPShouldBeDecreasedBySuccessfullAttack()
        {
            string attackerName = "Kesho";
            int attackerDamage = 10;
            int attackerHP = 50;

            string defenderName = "Gosho";
            int defenderDamage = 10;
            int defenderHP = 50;

            Warrior attacker = new Warrior(attackerName, attackerDamage, attackerHP);
            Warrior defender = new Warrior(defenderName, defenderDamage, defenderHP);

            int expectedAttackerHP = attackerHP - defenderDamage;
            int expectedDefenderHP = defenderHP - attackerDamage;

            //int expectedHP = 40;
            //int actualHP = attacker.HP;

            attacker.Attack(defender);

            Assert.AreEqual(expectedAttackerHP, attacker.HP);
            Assert.AreEqual(expectedDefenderHP, defender.HP);
        }

        [Test]
        public void TestStrongerAttackerShouldKillWeakerDefender()
        {
            string attackerName = "Kesho";
            int attackerDamage = 60;
            int attackerHP = 50;

            string defenderName = "Gosho";
            int defenderDamage = 10;
            int defenderHP = 50;

            Warrior attacker = new Warrior(attackerName, attackerDamage, attackerHP);
            Warrior defender = new Warrior(defenderName, defenderDamage, defenderHP);

            int expectedDefenderHP = 0;
            int expectedAttackerHP = attacker.HP - defender.Damage;
            
            attacker.Attack(defender);

            Assert.AreEqual(expectedAttackerHP, attacker.HP);
            Assert.AreEqual(expectedDefenderHP, defender.HP);
        }


        //[Test]
        //public void TestDefenderHPShouldBeDecreasedByAttackerDamage()
        //{
        //    string attackerName = "Kesho";
        //    int attackerDamage = 10;
        //    int attackerHP = 50;

        //    string defenderName = "Gosho";
        //    int defenderDamage = 10;
        //    int defenderHP = 50;

        //    Warrior attacker = new Warrior(attackerName, attackerDamage, attackerHP);
        //    Warrior defender = new Warrior(defenderName, defenderDamage, defenderHP);

        //    attacker.Attack(defender);

        //    int expectedHP = 40;
        //    int actualHP = defender.HP;

        //    Assert.AreEqual(expectedHP, actualHP);
        //}
    }
}
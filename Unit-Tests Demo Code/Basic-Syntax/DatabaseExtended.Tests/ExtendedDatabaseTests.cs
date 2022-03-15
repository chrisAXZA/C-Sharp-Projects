namespace Tests
{
    using System;

    using NUnit.Framework;

    // using ExtendedDatabase;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private ExtendedDatabase database;
        private Person[] largePersonList17;
        private Person[] largePersonList16;
        [SetUp]
        public void Setup()
        {
            Person person1 = new Person(1L, "Pesho1");
            Person person2 = new Person(2L, "Pesho2");
            Person person3 = new Person(3L, "Pesho3");
            Person person4 = new Person(4L, "Pesho4");
            Person person5 = new Person(5L, "Pesho5");
            Person person6 = new Person(6L, "Pesho6");
            Person person7 = new Person(7L, "Pesho7");
            Person person8 = new Person(8L, "Pesho8");
            Person person9 = new Person(9L, "Pesho9");
            Person person10 = new Person(10L, "Pesho10");
            Person person11 = new Person(11L, "Pesho11");
            Person person12 = new Person(12L, "Pesho12");
            Person person13 = new Person(13L, "Pesho13");
            Person person14 = new Person(14L, "Pesho14");
            Person person15 = new Person(15L, "Pesho15");
            Person person16 = new Person(16L, "Pesho16");
            Person person17 = new Person(17L, "Pesho17");

            largePersonList17 = new Person[] { person1, person2, person3, person4, person5, person6,
            person7, person8, person9, person10, person11, person12, person13, person14, person15,
            person16, person17};

            largePersonList16 = new Person[] { person1, person2, person3, person4, person5, person6,
            person7, person8, person9, person10, person11, person12, person13, person14, person15,
            person16};
        }

        //[TestCase(new int[] { 1, 2 })]
        //[TestCase(new Person[] { new Person (1L, "Pesho" ), new Person(2L, "Fesho") })]
        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Kesho");
            Person[] personList = new Person[] { person1, person2};
            this.database = new ExtendedDatabase(personList);

            Assert.IsNotNull(this.database);
        }

        //[Test]
        //public void DatabaseInitializeConstructorWithNullLeadsToEmptyDB()
        //{
        //    // Assert
        //    Assert.DoesNotThrow(() => this.database = new Database(null));
        //}

        [Test]
        public void TestIfConstructorWorksCorrectlyWithArrayOf16Persons()
        {
            this.database = new ExtendedDatabase(this.largePersonList16);

            Assert.AreEqual(this.largePersonList16.Length, this.database.Count);
        }

        [Test]
        public void ConstructorShouldThrowArgumentExceptionWhenGivenPersonArrayLargerThan16()
        {
            Assert.Throws<ArgumentException>(()
                => new ExtendedDatabase(this.largePersonList17));
        }

        [Test]
        public void ConstructorShouldThrowInvalidOperationExceptionWhenGiven2PersonsWithSameName()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Pesho");
            Person[] personList = new Person[] { person1, person2 };

            Assert.Throws<InvalidOperationException>(()
                => new ExtendedDatabase(personList));
        }

        [Test]
        public void ConstructorShouldThrowInvalidOperationExceptionWhenGiven2PersonsWithSameId()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(1L, "Kesho");
            Person[] personList = new Person[] { person1, person2 };

            Assert.Throws<InvalidOperationException>(()
                => new ExtendedDatabase(personList));
        }

        [Test]
        public void TestIfCountWorksCorrectly()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Kesho");
            Person[] personList = new Person[] { person1, person2 };
            this.database = new ExtendedDatabase(personList);

            Assert.AreEqual(personList.Length, this.database.Count);
        }

        [Test]
        public void AddMethodShouldThrowInvalidOperationExceptionWhenDatabaseCountIs16()
        {
            this.database = new ExtendedDatabase(this.largePersonList16);
            Person person1 = new Person(22L, "Fesho");
            Assert.Throws<InvalidOperationException>(()
                => this.database.Add(person1));
        }

        [Test]
        public void AddMethodShouldThrowInvalidOperationExceptionWhenAddingPersonWithSameNameThatIsAlreadyInDatabase()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Pesho");
            this.database = new ExtendedDatabase(person1);

            Assert.Throws<InvalidOperationException>(()
                => this.database.Add(person2));
        }

        [Test]
        public void AddMethodShouldThrowInvalidOperationExceptionWhenAddingPersonWithSameIdThatIsAlreadyInDatabase()
        {
            Person person1 = new Person(2L, "Kesho");
            Person person2 = new Person(2L, "Pesho");
            this.database = new ExtendedDatabase(person1);

            Assert.Throws<InvalidOperationException>(()
                => this.database.Add(person2));
        }

        [Test]
        public void TestAddMethodIfWorksCorrectly()
        {
            Person person1 = new Person(2L, "Kesho");
            Person person2 = new Person(1L, "Pesho");
            this.database = new ExtendedDatabase(person1);
            this.database.Add(person2);

            Assert.AreEqual(2, this.database.Count);
        }

        [Test]
        public void RemoveMethodShouldThrowInvalidOperationExceptionWhenDatabseIsEmpty()
        {
            this.database = new ExtendedDatabase();
            Assert.Throws<InvalidOperationException>(()
                => this.database.Remove());
        }

        [Test]
        public void TestIfRemoveMethodWorksCorrectly()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Kesho");
            Person[] personList = new Person[] { person1, person2 };
            this.database = new ExtendedDatabase(personList);
            this.database.Remove();

            Assert.AreEqual(1, this.database.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void FindByUserNameShouldThrowArgumentNullExceptionWhenNameIsNullEmptyWhiteSpace(string name)
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Kesho");
            Person[] personList = new Person[] { person1, person2 };
            this.database = new ExtendedDatabase(personList);

            Assert.Throws<ArgumentNullException>(()
                => this.database.FindByUsername(name));
        }

        [Test]
        public void FindByUserNameShouldThrowInvalidOperationExceptionWhenNameIsNotInDatabse()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Kesho");
            Person[] personList = new Person[] { person1, person2 };
            this.database = new ExtendedDatabase(personList);

            Assert.Throws<InvalidOperationException>(()
                => this.database.FindByUsername("Sasho"));
        }

        [Test]
        public void TestIfFindByUserNameWorksCorrectly()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Kesho");
            Person[] personList = new Person[] { person1, person2 };
            this.database = new ExtendedDatabase(personList);

            Person personTarget = this.database.FindByUsername("Pesho");

            Assert.AreEqual(person1, personTarget);
        }

        [Test]
        public void FindByIdShouldThrowArgumenOutOfRangeExceptionWhenIdIsNegative()
        {
            Person person1 = new Person(-1L, "Pesho");
            Person person2 = new Person(-2L, "Kesho");
            Person[] personList = new Person[] { person1, person2 };
            this.database = new ExtendedDatabase(personList);

            Assert.Throws<ArgumentOutOfRangeException>(()
                => this.database.FindById(-1));
        }

        [Test]
        public void FindByIdShouldThrowInvalidOperationExceptionWhenIdIsNotPresent()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Kesho");
            Person[] personList = new Person[] { person1, person2 };
            this.database = new ExtendedDatabase(personList);

            Assert.Throws<InvalidOperationException>(()
                => this.database.FindById(3));
        }

        [Test]
        public void TestFindByIdShouldIfWorksCorrectly()
        {
            Person person1 = new Person(1L, "Pesho");
            Person person2 = new Person(2L, "Kesho");
            Person[] personList = new Person[] { person1, person2 };
            this.database = new ExtendedDatabase(personList);

            Person personTarget = this.database.FindById(2L);

            Assert.AreEqual(person2, personTarget);
        }
    }
}
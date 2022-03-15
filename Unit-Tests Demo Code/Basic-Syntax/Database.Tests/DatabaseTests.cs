namespace Tests
{
    using System;

    using NUnit.Framework;
    
    [TestFixture]
    public class DatabaseTests
    {
        private Database.Database database;
        private readonly int[] initialData = new int[] { 1, 2 };

        [SetUp]
        public void Setup()
        {
            this.database = new Database.Database(initialData);
        }

        [TestCase(new int[] { 1, 2, 3 })]
        [TestCase(new int[] { })]
        public void TestConsructorIfCountWorksCorrectly(int[] data)
        {
            //int[] data = new int[] { 1, 2, 3 };
            this.database = new Database.Database(data);

            int expectedCount = data.Length;
            int actualCount = this.database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ConstructorShouldThrowExceptionWhenInitializedWithCollectionLargerThan16Elements()
        {
            int[] data = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database = new Database.Database(data);
            });
        }

        [Test]
        public void AddMethodShouldIncreaseCountOfDatabaseWhenElementIsAddedSuccesfully()
        {
            this.database.Add(3);

            int expectedCount = 3;
            int actualCount = this.database.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddMethodShouldThrowExceptionWhenAddingNewElementToDatabseThatHas16Elements()
        {
            //int[] data = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            //this.database = new Database.Database(data);
            // Arrange
            for (int i = 3; i <= 16; i++)
            {
                this.database.Add(i);
            }
            //Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                //Act
                this.database.Add(17);
            });
        }

        [Test]
        public void RemoveMethodShouldDecreaseCountOfDatabaseWhenElementIsRemovedSuccesfully()
        {
            // Arrange
            int expectedCount = 1;

            // Act
            this.database.Remove();
            int actualCount = this.database.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveMethodShouldThrowExceptionWhenRemovingElementFromEmptyDatabase()
        {
            // or 2x this.database.Remove();

            this.database = new Database.Database(new int[] { });

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.database.Remove();
            });
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2})]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void FetchMethodShouldReturnCopyOfDatabase(int[] expectedData)
        {
            this.database = new Database.Database(expectedData);

            int[] actualData = this.database.Fetch();

            CollectionAssert.AreEqual(expectedData, actualData);
        }
    }
}
//using ExtendedDatabase;
using System;
using NUnit.Framework;

namespace DatabaseExtendedTests
{
    [TestFixture]
    public class PersonTests
    {
        [TestCase(1, "Pesho")]
        [TestCase(long.MaxValue, "Max")]
        [TestCase(0, null)]
        [TestCase(2, "")]
        public void TestIfConstructorWorksCorrectly(long expectedId, string expectedName)
        {
            //long expectedId = 1;
            //string expectedName = "Pesho";

            Person person = new Person(expectedId, expectedName);

            Assert.AreEqual(expectedId, person.Id);
            Assert.AreEqual(expectedName, person.UserName);
        }
    }

    //[Test]
    //public void AllPropertieeNonPublicSetters()
    //{
    //    // Arrange
    //    var personType = typeof(Person);
    //    var propertiesWithPublicSetters = personType
    //        .GetProperties()
    //        .Where(p => p.SetMethod.IsPublic)
    //        .ToArray();

    //    // Assert
    //    Assert.AreEqual(0, propertiesWithPublicSetters.Length);
    //}
}

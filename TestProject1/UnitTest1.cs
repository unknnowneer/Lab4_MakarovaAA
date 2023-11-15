using NUnit.Framework;
using DatabaseLibrary;

namespace TestProject1
{
    [TestFixture]
    public class DatabaseTests
    {
        private string testFilePath = "test_database.txt";

        [SetUp]
        public void Setup()
        {
            // Create an empty test database file
            File.WriteAllText(testFilePath, string.Empty);
        }

        [TearDown]
        public void TearDown()
        {
            // Delete the test database file
            File.Delete(testFilePath);
        }

        [Test]
        public void Add_RecordIsAdded()
        {
            // Arrange
            Database database = new Database(testFilePath);
            int id = 1;
            string name = "John";
            string message = "Hello, World!";

            // Act
            database.Add(id, name, message);

            // Assert
            var records = database.GetByID(id);
            Assert.AreEqual(1, records.Count);
            Assert.AreEqual($"{id},{name},{message}", records[0]);
        }

        [Test]
        public void Add_MultipleRecordsAreAdded()
        {
            // Arrange
            Database database = new Database(testFilePath);
            int id1 = 1;
            string name1 = "John";
            string message1 = "Hello, World!";
            int id2 = 2;
            string name2 = "Jane";
            string message2 = "Goodbye, World!";

            // Act
            database.Add(id1, name1, message1);
            database.Add(id2, name2, message2);

            // Assert
            var records1 = database.GetByID(id1);
            Assert.AreEqual(1, records1.Count);
            Assert.AreEqual($"{id1},{name1},{message1}", records1[0]);

            var records2 = database.GetByID(id2);
            Assert.AreEqual(1, records2.Count);
            Assert.AreEqual($"{id2},{name2},{message2}", records2[0]);
        }

        [Test]
        public void GetByID_RecordExists()
        {
            // Arrange
            Database database = new Database(testFilePath);
            int id = 1;
            string name = "John";
            string message = "Hello, World!";
            database.Add(id, name, message);

            // Act
            var records = database.GetByID(id);

            // Assert
            Assert.AreEqual(1, records.Count);
            Assert.AreEqual($"{id},{name},{message}", records[0]);
        }

        [Test]
        public void GetByID_RecordDoesNotExist()
        {
            // Arrange
            Database database = new Database(testFilePath);
            int id = 1;

            // Act
            var records = database.GetByID(id);

            // Assert
            Assert.AreEqual(0, records.Count);
        }

        [Test]
        public void GetByName_RecordExists()
        {
            // Arrange
            Database database = new Database(testFilePath);
            int id = 1;
            string name = "John";
            string message = "Hello, World!";
            database.Add(id, name, message);

            // Act
            var records = database.GetByName(name);

            // Assert
            Assert.AreEqual(1, records.Count);
            Assert.AreEqual($"{id},{name},{message}", records[0]);
        }

        [Test]
        public void GetByName_RecordDoesNotExist()
        {
            // Arrange
            Database database = new Database(testFilePath);
            string name = "John";

            // Act
            var records = database.GetByName(name);

            // Assert
            Assert.AreEqual(0, records.Count);
        }

        
        [Test]
        public void Update_RecordDoesNotExist()
        {
            // Arrange
            Database database = new Database(testFilePath);
            int id = 1;
            string newMessage = "New message";

            // Act
            database.Update(id, newMessage);

            // Assert
            var records = database.GetByID(id);
            Assert.AreEqual(0, records.Count);
        }

        [Test]
        public void Update_EmptyFile_NoUpdatePerformed()
        {
            // Arrange
            Database database = new Database(testFilePath);
            int id = 1;
            string newMessage = "New message";

            // Act
            database.Update(id, newMessage);

            // Assert
            var records = File.ReadAllLines(testFilePath);
            Assert.AreEqual(0, records.Length);
        }

        [Test]
        public void Delete_RecordIsDeleted()
        {
            // Arrange
            Database database = new Database(testFilePath);
            int id = 1;
            string name = "John";
            string message = "Hello, World!";
            database.Add(id, name, message);

            // Act
            database.Delete(id);

            // Assert
            var records = database.GetByID(id);
            Assert.AreEqual(0, records.Count);
        }

        [Test]
        public void Delete_RecordDoesNotExist()
        {
            // Arrange
            Database database = new Database(testFilePath);
            int id = 1;

            // Act
            database.Delete(id);

            // Assert
            var records = database.GetByID(id);
            Assert.AreEqual(0, records.Count);
        }
    }
}
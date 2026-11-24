using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnalaizerClassLibrary;

namespace AnalaizerTests
{
    [TestClass]
    public class ReplaceUnaryPlusMinusTests
    {
        private const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=CalculatorTestsDB;Integrated Security=True;";

        public static IEnumerable<object[]> GetTestData()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(
                    "SELECT Input, Expected FROM TestData ORDER BY Id", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        yield return new object[] { reader.GetString(0), reader.GetString(1) };
                }
            }
        }

        [TestMethod]
        [DynamicData(nameof(GetTestData))]
        public void ReplaceUnaryPlusMinus_ReturnsExpected(string input, string expected)
        {
            string actual = AnalaizerClass.ReplaceUnaryPlusMinus(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
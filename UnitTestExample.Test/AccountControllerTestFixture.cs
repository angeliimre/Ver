using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidateEmail(email);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

           
        }
        [
            Test,
            TestCase("almaaaa",false),
            TestCase("ALMAAAAA1",false),
            TestCase("almaaaaa1", false),
            TestCase("Alm", false),
            TestCase("Almaaa1234", true)
        ]
        public void TestValidatePassword(string pass, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.ValidatePassword(pass);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);


        }
    }
}

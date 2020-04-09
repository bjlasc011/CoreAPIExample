using Models;
using System;
using Xunit;

namespace CoreAPIExample.TEST.Models.TEST
{
    public class Employee_Should
    {
        // run tests using CTRL+R, A
        private void SetupTestBed()
        {
            // TODO: moq Employee if needed for scenarios
        }

        [Theory]
        [InlineData("ben.lascurain@gmail.com")]
        [InlineData("bjlasc01@gmail.com")]
        [InlineData("yourFrien_.d@mydomain.net")]
        public void Set_Valid_Emails(string email)
        {
            Employee sut = new Employee();
            try
            {
                sut.Email = email;
            }
            catch
            {

            }
            Assert.Equal(email, sut.Email);
        }

        [Theory]
        [InlineData("email@.com")]
        public void Fail_With_Invalid_Emails(string email)
        {
            Employee sut = new Employee();
            Exception exResult = null;
            try
            {
                sut.Email = email;
            }
            catch (Exception ex)
            {
                exResult = ex;
            }
            Assert.NotNull(exResult);
            Assert.True(email != sut.Email);
        }

    }
}

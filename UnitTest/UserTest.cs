using sport_event.Services;
using sport_event.ViewModels;
using sport_event.ViewModels.Auth;
using System.Reflection;
using System.Security.Claims;
using Xunit.Abstractions;
using Xunit.Sdk;
using Xunit;

namespace UnitTest
{
    public class UserTest
    {
        private string email;
        private string password;
        private string firstName;
        private string lastName;
        private string unitNumber;
        private UserService _userService;

        public UserTest() {
            firstName = "rifki";
            lastName = "unittest";
            unitNumber = Convert.ToString(DateTimeOffset.Now.ToUnixTimeMilliseconds());
            email = "testunittest3@rifki.id";
            password = "MajuJaya2!";
            _userService = new UserService();
        }   

        [Fact]
        public async Task Login_postitif()
        {
            var loginData = new LoginModel { email = email, password = password };

            // Act
            var result = await _userService.Login(loginData); 

            // Assert
            if (!string.IsNullOrEmpty(result.token))
            {
                Assert.True(true);
            }
            else {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task Login_negatif()
        {
            var loginData = new LoginModel { email = email, password = password + "!!!!" };

            var result = await _userService.Login(loginData);

            if (string.IsNullOrEmpty(result.token))
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task updateData_positif()
        {
            var loginData = new LoginModel { email = email, password = password };
            var result = await _userService.Login(loginData);
            if (!string.IsNullOrEmpty(result.token))
            {
                var dataUser = new UserModel
                {
                    id = Convert.ToInt32(result.id),
                    firstName = "rifki",
                    lastName = "maju",
                    email = email
                };

                var updateData = await _userService.UpdateDetailUser(result.token, dataUser);
                Assert.Equal(true, updateData);
            }
            else
            {
                Assert.True(false);
            }
        }


        [Fact]
        public async Task updateData_negatif()
        {
            // login
            var loginData = new LoginModel { email = email, password = password };
            var result = await _userService.Login(loginData);
            if (!string.IsNullOrEmpty(result.token))
            {
                // login berhasil
                var dataUser = new UserModel
                {
                    id = Convert.ToInt32(result.id),
                    firstName = "rifki",
                    lastName = " maju",
                    email = "emailrifki.my.id"
                };
                
                var updateData = await _userService.UpdateDetailUser(result.token, dataUser);
                Assert.Equal(false, updateData);

            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task create_negatif()
        {
            var data = new UserModel { 
                email = email, 
                password = password, 
                firstName = firstName, 
                lastName = lastName,
                repeatPassword = password + "!"
            };
            
            var result = await _userService.createUser(data);
            
            if (!string.IsNullOrEmpty(result.Message))
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task create_login_delete_positif()
        {
            var data = new UserModel
            {
                email = unitNumber + email,
                password = password,
                firstName = firstName,
                lastName = lastName,
                repeatPassword = password
            };

            var result = await _userService.createUser(data);

            if (string.IsNullOrEmpty(result.Message))
            {
                var loginData = new LoginModel { email = unitNumber + email, password = password };
                var resultLogin = await _userService.Login(loginData);
                if (!string.IsNullOrEmpty(resultLogin.token))
                {
                    var dataDelete = await _userService.deletelUser(result.token, result.id);
                    if (dataDelete)
                    {
                        Assert.True(true);
                    }
                }
                else
                {
                    Assert.True(false);
                }
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task delete_negatif()
        {
            var loginData = new LoginModel { email = email, password = password };
            var result = await _userService.Login(loginData);
            if (!string.IsNullOrEmpty(result.token))
            {
                var data = await _userService.deletelUser(result.token,0);
                if (!data)
                {
                    Assert.True(true);
                }
            }
            else
            {
                Assert.True(false);
            }
        }

        //[Fact]
        //public async Task delete_positif()
        //{
        //    var loginData = new LoginModel { email = unitNumber+email, password = password };
        //    var result = await _userService.Login(loginData);
        //    if (!string.IsNullOrEmpty(result.token))
        //    {
        //        var data = await _userService.deletelUser(result.token, result.id);
        //        if (data)
        //        {
        //            Assert.True(true);
        //        }
        //    }
        //    else
        //    {
        //        Assert.True(false);
        //    }
        //}

        [Fact]
        public async Task updatePassword_positif()
        {
            // login
            var loginData = new LoginModel { email = email, password = password };
            var result = await _userService.Login(loginData);
            if (!string.IsNullOrEmpty(result.token))
            {
                // login berhasil
                var dataUser = new UserModel
                {
                    id = Convert.ToInt32(result.id),
                    newPassword = password,
                    oldPassword = password,
                    repeatPassword = password,
                };

                var updateData = await _userService.UpdatePasswordUser(result.token, dataUser);
                Assert.Equal(true, updateData);
            }
            else
            {
                Assert.True(false);
            }
        }


        [Fact]
        public async Task updatePassword_negatif()
        {
            // login
            var loginData = new LoginModel { email = email, password = password };
            var result = await _userService.Login(loginData);
            if (!string.IsNullOrEmpty(result.token))
            {
                var dataUser = new UserModel
                {
                    id = Convert.ToInt32(result.id),
                    newPassword = password,
                    oldPassword = password,
                    repeatPassword = password+"!!! ",
                };

                var updateData = await _userService.UpdatePasswordUser(result.token, dataUser);
                Assert.Equal(false, updateData);

            }
            else
            {
                Assert.True(false);
            }
        }

    }

}


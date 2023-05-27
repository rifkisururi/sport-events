using sport_event.Services;
using sport_event.ViewModels.Auth;
using sport_event.ViewModels.Organizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class OrganizerTest
    {
        private string email;
        private string password; 
        private int orgId = 900;
        private string organizerName = "rifki-organizer " + Convert.ToString(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        private string imageLocation = "https://img.freepik.com/free-vector/creative-barbecue-logo-template_23-2149017951.jpg";
        private UserService _userService;
        private OrganizerService _organizerService;
        public OrganizerTest()
        {
            email = "testunittest3@rifki.id";
            password = "MajuJaya2!";
            _userService = new UserService();
            _organizerService = new OrganizerService();
        }

        [Fact]
        public async Task Organizer_GetData()
        {
            var loginData = new LoginModel { email = email, password = password };
            // Act
            var result = await _userService.Login(loginData);
            var data = _organizerService.getData(result.token, 1, 1);
            // Assert
            if (data != null)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task Organizer_CreateDelete()
        {
            var loginData = new LoginModel { email = email, password = password };
            var result = await _userService.Login(loginData);

            var data = new OrganizerDto { imageLocation = imageLocation, organizerName = organizerName };
            var dataOrg = await _organizerService.createData(result.token, data);
            // Assert
            if (dataOrg.id != 0)
            {
                // delete org
                var deleteOrg = await _organizerService.DeleteData(result.token, dataOrg.id);
                Assert.True(deleteOrg);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task Organizer_GetDataDetail()
        {
            var loginData = new LoginModel { email = email, password = password };
            var result = await _userService.Login(loginData);

            var dataOrg = await _organizerService.GetDataDetail(result.token, orgId);
            if (dataOrg.id != 0)
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
          public async Task Organizer_Update()
        {
            var loginData = new LoginModel { email = email, password = password };
            var result = await _userService.Login(loginData);

            var data = new OrganizerDto { id = orgId, imageLocation = imageLocation, organizerName = organizerName };
            var dataOrg = await _organizerService.UpdateData(result.token, data);
            Assert.True(true);
        }

    }
}

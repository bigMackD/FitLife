using System.Collections.Generic;
using System.Linq;
using FitLife.DB.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace FitLife.Tests.Helpers
{
    public static class MockUserManager
    {
        public static Mock<UserManager<AppUser>> Build(List<AppUser> usersStub)
        {
            var store = new Mock<IUserStore<AppUser>>();
            var mockUserManager = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Object.UserValidators.Add(new UserValidator<AppUser>());
            mockUserManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());
            mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<AppUser, string>((x, y) => usersStub.Add(x));
            mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success);
            return mockUserManager;
        }

        public static Mock<UserManager<AppUser>> Build(List<AppUser> usersStub, string idStub)
        {
            var store = new Mock<IUserStore<AppUser>>();
            var mockUserManager = new Mock<UserManager<AppUser>>(store.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(x => x.FindByIdAsync(It.Is<string>(i => i == idStub))).ReturnsAsync(usersStub.First(u => u.Id == idStub));
            return mockUserManager;
        }
    }
}

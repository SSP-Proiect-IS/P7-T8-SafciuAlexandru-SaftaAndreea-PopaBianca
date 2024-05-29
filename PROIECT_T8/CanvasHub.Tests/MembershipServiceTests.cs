using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CanvasHub.Services;
using CanvasHub.Models;
using System.Collections.Generic;

namespace CanvasHub.Tests
{
    public class MembershipServiceTests
    {
        [Fact]
        public async Task AddMemberAsync_Success()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            var user = new User();

            userManagerMock.Setup(x => x.AddToRoleAsync(user, "Member"))
                .ReturnsAsync(IdentityResult.Success);

            var membershipService = new MembershipService(userManagerMock.Object);

            // Act
            var result = await membershipService.AddMemberAsync(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddMemberAsync_Failure()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            var user = new User();

            userManagerMock.Setup(x => x.AddToRoleAsync(user, "Member"))
                .ReturnsAsync(IdentityResult.Failed());

            var membershipService = new MembershipService(userManagerMock.Object);

            // Act
            var result = await membershipService.AddMemberAsync(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AddAdminAsync_Success()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            var user = new User();

            userManagerMock.Setup(x => x.AddToRoleAsync(user, "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            var membershipService = new MembershipService(userManagerMock.Object);

            // Act
            var result = await membershipService.AddAdminAsync(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddAdminAsync_Failure()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            var user = new User();

            userManagerMock.Setup(x => x.AddToRoleAsync(user, "Admin"))
                .ReturnsAsync(IdentityResult.Failed());

            var membershipService = new MembershipService(userManagerMock.Object);

            // Act
            var result = await membershipService.AddAdminAsync(user);

            // Assert
            Assert.False(result);
        }
    }
}

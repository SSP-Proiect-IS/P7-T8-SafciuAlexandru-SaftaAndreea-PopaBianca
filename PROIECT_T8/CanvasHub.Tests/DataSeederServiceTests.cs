using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using CanvasHub.Models;
using CanvasHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CanvasHub.Tests
{
    public class DataSeederServiceTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
        private readonly Mock<UserManager<User>> _userManagerMock;

        public DataSeederServiceTests()
        {
            var services = new ServiceCollection();

            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                Mock.Of<IRoleStore<IdentityRole>>(),
                new IRoleValidator<IdentityRole>[0],
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                null
            );

            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<IPasswordHasher<User>>(),
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                null,
                null
            );

            services.AddSingleton(_roleManagerMock.Object);
            services.AddSingleton(_userManagerMock.Object);

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task SeedRolesAndAdminAsync_RolesDoNotExist_AdminUserDoesNotExist_Success()
        {
            // Arrange
            var dataSeederService = new DataSeederService(_serviceProvider);

            _roleManagerMock.Setup(x => x.RoleExistsAsync("User")).ReturnsAsync(false);
            _roleManagerMock.Setup(x => x.RoleExistsAsync("Member")).ReturnsAsync(false);
            _roleManagerMock.Setup(x => x.RoleExistsAsync("Administrator")).ReturnsAsync(false);
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            await dataSeederService.SeedRolesAndAdminAsync();

            // Assert
            _roleManagerMock.Verify(x => x.CreateAsync(It.IsAny<IdentityRole>()), Times.Exactly(3)); // Three roles should be created
            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once); // Admin user should be created
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once); // Admin user should be assigned "Administrator" role
        }

        [Fact]
        public async Task SeedRolesAndAdminAsync_RolesExist_AdminUserDoesNotExist_Success()
        {
            // Arrange
            var dataSeederService = new DataSeederService(_serviceProvider);

            _roleManagerMock.Setup(x => x.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            await dataSeederService.SeedRolesAndAdminAsync();

            // Assert
            _roleManagerMock.Verify(x => x.CreateAsync(It.IsAny<IdentityRole>()), Times.Never); // No roles should be created
            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once); // Admin user should be created
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once); // Admin user should be assigned "Administrator" role
        }

        [Fact]
        public async Task SeedRolesAndAdminAsync_RolesExist_AdminUserExists_Success()
        {
            // Arrange
            var dataSeederService = new DataSeederService(_serviceProvider);

            _roleManagerMock.Setup(x => x.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());

            // Act
            await dataSeederService.SeedRolesAndAdminAsync();

            // Assert
            _roleManagerMock.Verify(x => x.CreateAsync(It.IsAny<IdentityRole>()), Times.Never); // No roles should be created
            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never); // Admin user should not be created
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never); // Admin user role assignment should not happen
        }

        [Fact]
        public async Task SeedRolesAndAdminAsync_RolesDoNotExist_AdminUserExists_Success()
        {
            // Arrange
            var dataSeederService = new DataSeederService(_serviceProvider);

            _roleManagerMock.Setup(x => x.RoleExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new User());

            // Act
            await dataSeederService.SeedRolesAndAdminAsync();

            // Assert
            _roleManagerMock.Verify(x => x.CreateAsync(It.IsAny<IdentityRole>()), Times.Exactly(3)); // Three roles should be created
            _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never); // Admin user should not be created
            _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never); // Admin user role assignment should not happen
        }
    }
}

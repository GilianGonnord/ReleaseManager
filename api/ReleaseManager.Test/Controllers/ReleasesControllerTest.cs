using Xunit;
using Moq;
using ReleaseManager.Api.Repositories;
using System.Collections.Generic;
using ReleaseManager.Model.Models;
using ReleaseManager.Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReleaseManager.Api.Commons;
using ReleaseManager.Api.ViewModels;

namespace ReleaseManager.Test.Controllers;

public class ReleasesControllerTest
{
    #region GetReleases

    [Fact]
    public async Task GetReleases_200()
    {
        // Arrange
        var releaseMockRepo = new Mock<IReleaseRepository>();
        releaseMockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Release>());

        var controller = new ReleasesController(releaseMockRepo.Object);

        // Act
        var result = await controller.GetReleases();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<ReleaseViewModel>>>(result);
        Assert.IsType<OkObjectResult>(actionResult.Result);
        releaseMockRepo.VerifyAll();
    }

    #endregion

    #region GetRelease

    [Fact]
    public async Task GetRelease_200()
    {
        // Arrange
        var release = new Release { Id = 0, Name = "0.0.1" };

        var releaseMockRepo = new Mock<IReleaseRepository>();
        releaseMockRepo.Setup(repo => repo.FindAsync(release.Id)).ReturnsAsync(Result<Release>.Ok(release));

        var controller = new ReleasesController(releaseMockRepo.Object);

        // Act
        var result = await controller.GetRelease(release.Id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ReleaseViewModel>>(result);
        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var releaseVm = Assert.IsType<ReleaseViewModel>(objectResult.Value);

        Assert.StrictEqual(release.Id, releaseVm.Id);
        Assert.Equal(release.Name, releaseVm.Name);
        releaseMockRepo.VerifyAll();
    }

    [Fact]
    public async Task GetRelease_404()
    {
        // Arrange
        var release = new Release { Id = 0, Name = "0.0.1" };

        var releaseMockRepo = new Mock<IReleaseRepository>();
        releaseMockRepo.Setup(repo => repo.FindAsync(release.Id)).ReturnsAsync(Result<Release>.Fail(CommonErrors.NotFound));

        var controller = new ReleasesController(releaseMockRepo.Object);

        // Act
        var result = await controller.GetRelease(release.Id);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ReleaseViewModel>>(result);
        Assert.IsType<NotFoundResult>(actionResult.Result);
        releaseMockRepo.VerifyAll();
    }

    #endregion

    #region PutRelease

    [Fact]
    public async Task PutRelease_400_BadIds()
    {
        // Arrange
        var releaseVM = new ReleaseViewModel { Id = 1, Name = "0.0.1" };

        var releaseMockRepo = new Mock<IReleaseRepository>();

        var controller = new ReleasesController(releaseMockRepo.Object);

        // Act
        var result = await controller.PutRelease(0, releaseVM);

        // Assert
        Assert.IsType<BadRequestResult>(result);
        releaseMockRepo.VerifyAll();
    }

    [Fact]
    public async Task PutRelease_200_Ok()
    {
        // Arrange
        var releaseVm = new ReleaseViewModel { Id = 1, Name = "new name" };
        var releaseModel = releaseVm.ToModel();

        var releaseMockRepo = new Mock<IReleaseRepository>();
        releaseMockRepo
            .Setup(repo => repo.Update(releaseVm.Id.Value, It.Is<Release>(r => r.Name == releaseVm.Name)))
            .ReturnsAsync(Result<Release>.Ok(releaseModel));

        var controller = new ReleasesController(releaseMockRepo.Object);

        // Act
        var result = await controller.PutRelease(1, releaseVm);

        // Assert
        Assert.IsType<NoContentResult>(result);
        releaseMockRepo.VerifyAll();
    }

    #endregion

    #region PostRelease

    [Fact]
    public async Task PostRelease_200_Ok()
    {
        // Arrange
        var releaseVm = new ReleaseViewModel { Name = "name" };

        var releaseMockRepo = new Mock<IReleaseRepository>();
        releaseMockRepo
            .Setup(repo => repo.Add(It.Is<Release>(r => r.Name == releaseVm.Name)))
            .ReturnsAsync(new Release { Id = 1, Name = "name" });

        var controller = new ReleasesController(releaseMockRepo.Object);

        // Act
        var result = await controller.PostRelease(releaseVm);

        // Assert
        var actionResult = Assert.IsType<ActionResult<ReleaseViewModel>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var gotReleaseVm = Assert.IsType<ReleaseViewModel>(createdAtActionResult.Value);

        Assert.NotNull(gotReleaseVm.Id);
        Assert.Equal(releaseVm.Name, gotReleaseVm.Name);

        releaseMockRepo.VerifyAll();
    }

    #endregion

    #region DeleteRelease

    [Fact]
    public async Task DeleteRelease_404_NotFound()
    {
        // Arrange
        var id = -1;

        var releaseMockRepo = new Mock<IReleaseRepository>();
        releaseMockRepo
            .Setup(repo => repo.Delete(id))
            .ReturnsAsync(Result<Release>.Fail(CommonErrors.NotFound));

        var controller = new ReleasesController(releaseMockRepo.Object);

        // Act
        var result = await controller.DeleteRelease(id);

        // Assert
        Assert.True(false);
        Assert.IsType<NotFoundResult>(result);
        releaseMockRepo.VerifyAll();
    }

    #endregion
}
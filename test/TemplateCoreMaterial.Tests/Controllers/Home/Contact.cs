﻿//-----------------------------------------------------------------------
// <copyright file="Contact.cs" company="Without name">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TemplateCoreMaterial.Tests.Controllers.Home
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using Moq;
  using TemplateCoreMaterial.Controllers;
  using Xunit;

  /// <summary>
  /// Class test that tests the method Contact of the class <see cref="HomeController"/>.
  /// </summary>
  public class Contact
  {
    /// <summary>
    /// The controller
    /// </summary>
    private HomeController controller = null;

    /// <summary>
    /// The localizer
    /// </summary>
    private Mock<IStringLocalizer<HomeController>> localizer = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="Contact"/> class.
    /// </summary>
    public Contact()
    {
      this.localizer = new Mock<IStringLocalizer<HomeController>>();
    }

    /// <summary>
    /// Tests the method Contact of the class <see cref="HomeController"/>
    /// Asserts the invoke of the method returns an instance of the class <see cref="ViewResult"/>
    /// </summary>
    [Fact]
    public void ContactOk()
    {
      // setup
      this.controller = new HomeController(this.localizer.Object);

      // action
      var result = this.controller.Contact();

      // assert
      Assert.IsType(typeof(ViewResult), result);
    }
  }
}
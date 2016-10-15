//-----------------------------------------------------------------------
// <copyright file="ApplicationUser.cs" company="Without name">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TemplateCoreMaterial.Model
{
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

  /// <summary>
  /// Class ApplicationUser.
  /// </summary>
  public class ApplicationUser : IdentityUser
  {
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }
  }
}

//-----------------------------------------------------------------------
// <copyright file="UserCreateViewModel.cs" company="Without name">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TemplateCoreMaterial.ViewModels.User
{
  using Newtonsoft.Json;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Class UserCreateViewModel.
  /// </summary>
  public class UserCreateViewModel
  {
    /// <summary>
    /// User Id
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    [Required(ErrorMessage = "The name is a required field.")]
    [Display(Name = "Name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    /// <value>The name of the user.</value>
    [Display(Name = "Username")]
    [Required(ErrorMessage = "The username is a required field.")]
    [JsonProperty("username")]
    public string UserName { get; set; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>The email.</value>
    [Display(Name = "Email")]
    [Required(ErrorMessage = "The email is a required field.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [JsonProperty("email")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    /// <value>The roles.</value>
    [JsonProperty("roles")]
    public IEnumerable<UserRoleCreateViewModel> Roles { get; set; }
  }
}

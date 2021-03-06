﻿//-----------------------------------------------------------------------
// <copyright file="RegisterViewModel.cs" company="Without name">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TemplateCoreMaterial.ViewModels.Account
{
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Class RegisterViewModel.
  /// </summary>
  public class RegisterViewModel
  {
    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>The email.</value>
    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>The password.</value>
    [Required(ErrorMessage = "The Password field is required.")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the confirm password.
    /// </summary>
    /// <value>The confirm password.</value>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}

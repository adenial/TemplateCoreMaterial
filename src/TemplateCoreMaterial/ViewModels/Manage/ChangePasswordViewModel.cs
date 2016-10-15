﻿//-----------------------------------------------------------------------
// <copyright file="ChangePasswordViewModel.cs" company="Without name">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TemplateCoreMaterial.ViewModels.Manage
{
  using System.ComponentModel.DataAnnotations;

  /// <summary>
  /// Class ChangePasswordViewModel.
  /// </summary>
  public class ChangePasswordViewModel
  {
    /// <summary>
    /// Gets or sets the old password.
    /// </summary>
    /// <value>The old password.</value>
    [Required(ErrorMessage = "The current password is a required field.")]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string OldPassword { get; set; }

    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    /// <value>The new password.</value>
    [Required(ErrorMessage = "The new password is a required field.")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    /// <summary>
    /// Gets or sets the confirm password.
    /// </summary>
    /// <value>The confirm password.</value>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}

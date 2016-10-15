﻿//-----------------------------------------------------------------------
// <copyright file="IRoleService.cs" company="Without name">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TemplateCoreMaterial.Service.Interfaces
{
  using System.Collections.Generic;
  using System.Security.Claims;

  /// <summary>
  /// Interface IRoleService
  /// </summary>
  public interface IRoleService
  {
    /// <summary>
    /// Get all the role names.
    /// </summary>
    /// <returns>List of type <see cref="string"/>.</returns>
    IEnumerable<string> GetAllRoleNames();

    /// <summary>
    /// Determines whether this instance can insert the specified name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns><c>true</c> if this instance can insert the specified name; otherwise, <c>false</c>.</returns>
    bool CanInsert(string name);

    /// <summary>
    /// Deletes a role with the specified name.
    /// </summary>
    /// <param name="name">The name of the role to delete.</param>
    void DeleteRoleByName(string name);

    /// <summary>
    /// Inserts a new role with the specified name
    /// </summary>
    /// <param name="name">The name of the role to Insert.</param>
    /// <param name="claims">The claims.</param>
    void Insert(string name, IEnumerable<Claim> claims);

    /// <summary>
    /// Gets the claims.
    /// </summary>
    /// <returns>Dictionary of type string, Claim.</returns>
    Dictionary<string, Claim> GetClaims();
  }
}

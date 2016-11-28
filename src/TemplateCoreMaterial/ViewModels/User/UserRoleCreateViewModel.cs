//-----------------------------------------------------------------------
// <copyright file="UserRoleCreateViewModel.cs" company="Without name">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using Newtonsoft.Json;

namespace TemplateCoreMaterial.ViewModels.User
{
  /// <summary>
  /// Class UserRoleCreateViewModel.
  /// </summary>
  public class UserRoleCreateViewModel
  {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="UserRoleCreateViewModel"/> is check.
    /// </summary>
    /// <value><c>true</c> if check; otherwise, <c>false</c>.</value>
    [JsonProperty("check")]
    public bool Check { get; set; }
  }
}

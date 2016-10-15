//-----------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Without name">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TemplateCoreMaterial.Model
{
  /// <summary>
  /// Interface IEntity
  /// </summary>
  /// <typeparam name="T">Entity Type</typeparam>
  public interface IEntity<T>
  {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    T Id { get; set; }
   }
}

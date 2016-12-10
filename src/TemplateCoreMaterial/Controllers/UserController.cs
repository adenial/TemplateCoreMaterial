//-----------------------------------------------------------------------
// <copyright file="UserController.cs" company="Without name">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace TemplateCoreMaterial.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Localization;
  using TemplateCoreMaterial.Model;
  using TemplateCoreMaterial.Service.Interfaces;
  using TemplateCoreMaterial.ViewModels.User;
  using System.Dynamic;
  using Newtonsoft.Json;

  /// <summary>
  /// Class UserController.
  /// </summary>
  [Authorize(Policy = "View Administrator Menu")]
  public class UserController : Controller
  {
    /// <summary>
    /// The user service
    /// </summary>
    private IUserService userService = null;

    /// <summary>
    /// The localizer
    /// </summary>
    private IStringLocalizer<UserController> localizer = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController" /> class.
    /// </summary>
    /// <param name="userService">The user service.</param>
    /// <param name="localizer">The localizer.</param>
    /// <exception cref="System.ArgumentNullException">userService</exception>
    public UserController(IUserService userService, IStringLocalizer<UserController> localizer)
    {
      if (userService == null)
      {
        throw new ArgumentNullException("userService");
      }

      if (localizer == null)
      {
        throw new ArgumentNullException("localizer");
      }

      this.localizer = localizer;
      this.userService = userService;
    }

    /// <summary>
    /// Create User Action
    /// </summary>
    /// <returns>View to create a new User</returns>
    //[Authorize(Roles = "Administrator")]
    [HttpGet]
    [Route("/api/users/create")]
    [Authorize(Policy = "Create Users")]
    public IActionResult Create()
    {
      var roles = this.userService.GetAllRoles();
      var rolesViewmodel = this.GetRolesForViewModel(roles);
      return this.Json(rolesViewmodel);
    }

    /// <summary>
    /// Gets this instance.
    /// </summary>
    /// <returns>IActionResult.</returns>
    [HttpGet]
    [Route("api/users")]
    public IEnumerable<UserIndexViewModel> GetAll()
    {
      List<AspNetUser> users = this.userService.GetAll().ToList();
      AspNetUser loggedUser = users.Single(x => x.UserName.Equals(this.HttpContext.User.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
      users.Remove(loggedUser);
      var orderUsers = users.OrderBy(x => x.Name);

      // create view model.
      var model = this.GetIndexModelFromUsers(orderUsers).ToList();

      return model;
    }

    /// <summary>
    /// Creates the specified model.
    /// </summary>
    /// <param name="model">The model.</param>
    /// <returns>Microsoft.AspNetCore.Mvc.IActionResult.</returns>
    [HttpPost]
    [Route("/api/users/create")]
    public IActionResult Insert([FromBody] UserCreateViewModel model)
    {
      var usernameExists = this.userService.CanInsertUserName(model.UserName);
      if (!usernameExists)
      {
        dynamic expando = new ExpandoObject();
        expando.attribute = "username";
        expando.message = localizer["Username already exists"].Value;
        var json = JsonConvert.SerializeObject(expando);
        return BadRequest(json);
      }

      var canInsertEmail = this.userService.CanInsertEmail(model.Email);
      if (!canInsertEmail)
      {
        dynamic expando = new ExpandoObject();
        expando.attribute = "email";
        expando.message = localizer["Email already exists"].Value;
        var json = JsonConvert.SerializeObject(expando);
        return BadRequest(json);
      }

      List<string> selectedRoles = model.Roles.Where(x => x.Check).Select(x => x.Id).ToList();
      ApplicationUser user = this.userService.Insert(model.Email, model.UserName, model.Name, selectedRoles);
      var newUser = new UserIndexViewModel { Email = user.Email, Id = new Guid(user.Id), Name = user.Name, UserName = user.UserName };
      return CreatedAtRoute("GetUser", new { id = newUser.Id }, newUser);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("api/users/{id}", Name = "GetUser")]
    public IActionResult GetById(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
      {
        return BadRequest();
      }

      // GET UserCreateViewModel
      var user = this.userService.GetUserById(id);

      // universe of roles.
      var allRoles = this.userService.GetAllRoles();

      // roles the user already has.
      var userRoles = this.userService.GetRolesByUserId(id);

      // compare all the roles against the roles the user has, mark as true.
      var rolesViewModel = GetRolesForViewModel(allRoles);

      var test = this.GetUserRolesForEditViewModel(userRoles, allRoles);

      var returnUser = new UserCreateViewModel
      {
        Id = user.Id,
        Email = user.Email,
        Name = user.Name,
        UserName = user.UserName,
        Roles = test
      };

      return new ObjectResult(returnUser);
    }

    /// <summary>
    /// Deletes the user by its Id
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns>Redirects to Index Page or HttpNotFound (404).</returns>
    [HttpDelete]
    [Route("/api/users/{id}")]
    [Authorize(Policy = "Delete Users")]
    public IActionResult Delete(string id)
    {
      try
      {
        this.userService.DeleteById(id);
        string message = this.localizer["User Deleted"];
        return this.Json(message);
      }
      catch
      {
        throw;
      }
    }

    /// <summary>
    /// Updates the specified identifier.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="model">The model.</param>
    /// <returns>IActionResult.</returns>
    [HttpPut]
    [Route("/api/users/{id}")]
    //[Authorize(Policy = "Update Users")]
    public IActionResult Update(string id, [FromBody]UserCreateViewModel model)
    {
      // validate inputs.
      if (!string.IsNullOrWhiteSpace(model.Email))
      {
        // validate if user email can be updated.
        var emailUpdate = this.userService.CanUpdateEmail(model.Id, model.Email);
        if (!emailUpdate)
        {
          // return 400 and message.
          dynamic expando = new ExpandoObject();
          expando.attribute = "email";
          expando.message = localizer["Email already exists"].Value;
          var json = JsonConvert.SerializeObject(expando);
          return BadRequest(json);
        }
      }

      if (!string.IsNullOrWhiteSpace(model.UserName))
      {
        var usernameUpdate = this.userService.CanUpdateUsername(model.Id, model.UserName);

        if (!usernameUpdate)
        {
          dynamic expando = new ExpandoObject();
          expando.message = localizer["Username already exists"].Value;
          var json = JsonConvert.SerializeObject(expando);
          return BadRequest(json);
        }
      }


      var userRoles = this.userService.GetUserRolesByUserId(model.Id);

      var selectedRoles = model.Roles.Where(x => x.Check == true).ToList();
      var unselectedRoles = model.Roles.Where(x => x.Check == false).ToList();

      List<IdentityUserRole<string>> rolesToDelete = new List<IdentityUserRole<string>>();
      List<IdentityUserRole<string>> newRolesToInsert = new List<IdentityUserRole<string>>();

      var roles = this.userService.GetAllRoles();

      // query against the universe of roles.
      foreach (var roleDb in roles)
      {
        foreach (var selectedRole in selectedRoles)
        {
          if (roleDb.Id.Equals(selectedRole.Id))
          {
            var query = userRoles.Where(x => x.RoleId.Equals(selectedRole.Id)).SingleOrDefault();

            if (query == null)
            {
              // add
              newRolesToInsert.Add(new IdentityUserRole<string> { UserId = model.Id, RoleId = selectedRole.Id });
            }
          }
        }
      }

      // roles to delete.
      foreach (var userRole in userRoles)
      {
        foreach (var unselectedRole in unselectedRoles)
        {
          if (userRole.RoleId.Equals(unselectedRole.Id))
          {
            // roles to delete.
            rolesToDelete.Add(new IdentityUserRole<string> { UserId = model.Id, RoleId = userRole.RoleId });
          }
        }
      }

      this.userService.UpdateUserRoles(newRolesToInsert, rolesToDelete);
      this.userService.UpdateUserInfo(model.Id, model.Name, model.UserName, model.Email);

      // taken from https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api 
      return new NoContentResult();
    }

    /// <summary>
    /// Index Page
    /// </summary>
    /// <returns>Index Page.</returns>
    //[Authorize(Roles = "Administrator")]
    [Authorize(Policy = "View Users")]
    public IActionResult Index()
    {
      ViewBag.CreatedUserMessage = localizer["User created"].Value;
      ViewBag.UpdatedUserMessage = localizer["User updated"].Value;
      return this.View();
    }

    /// <summary>
    /// Gets the index model from users.
    /// </summary>
    /// <param name="users">The users.</param>
    /// <returns>List <see cref="UserIndexViewModel"/>.</returns>
    private IEnumerable<UserIndexViewModel> GetIndexModelFromUsers(IEnumerable<AspNetUser> users)
    {
      return users.Select(x => new UserIndexViewModel { Id = x.Id, UserName = x.UserName, Name = x.Name, Roles = x.Roles, Email = x.Email }).ToList();
    }

    /// <summary>
    /// Gets the roles for view model.
    /// </summary>
    /// <param name="roles">The roles.</param>
    /// <returns>List of type <see cref="UserRoleCreateViewModel"/>.</returns>
    private IEnumerable<UserRoleCreateViewModel> GetRolesForViewModel(IEnumerable<IdentityRole> roles)
    {
      return roles.Select(x => new UserRoleCreateViewModel { Id = x.Id, Name = x.Name }).ToList();
    }

    /// <summary>
    /// Gets the user roles for edit view model.
    /// </summary>
    /// <param name="userRoles">The user roles.</param>
    /// <param name="roles">The roles.</param>
    /// <returns>List of type <see cref="UserRoleCreateViewModel"/>.</returns>
    private IEnumerable<UserRoleCreateViewModel> GetUserRolesForEditViewModel(IEnumerable<IdentityRole> userRoles, IEnumerable<IdentityRole> roles)
    {
      // this will create the view for all the roles... (the grid..)
      var rolesViewModel = roles.Select(x => new UserRoleCreateViewModel { Check = false, Id = x.Id, Name = x.Name }).ToList();

      // now preselect the property check = true against the roles the user already has
      foreach (var role in rolesViewModel)
      {
        foreach (var userRole in userRoles)
        {
          if (role.Id.Equals(userRole.Id))
          {
            role.Check = true;
          }
        }
      }

      return rolesViewModel;
    }
  }
}
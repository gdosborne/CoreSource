using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using System;
using Territory;
using System.Linq;
using System.Threading.Tasks;
using Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
namespace Territory
{
	public class ApplicationUser : IdentityUser
	{
		public Congregation Congregation { get; set; }
		public new Role Role { get; set; }
		public User DBUser { get; set; }
	}
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection")
		{
		}
	}
	public class UserIdentityResult : IdentityResult
	{
		public UserIdentityResult(string[] errors)
			: base(errors) { }
		public UserIdentityResult(List<string> errors)
			: base(errors) { }
		public UserIdentityResult(bool success)
			: base(success) { }
	}
	public class IdentityPrinciple : IPrincipal
	{
		public IdentityPrinciple(IIdentity id)
		{
			Identity = id;
		}
		public IIdentity Identity { get; set; }
		public bool IsInRole(string role)
		{
			return false;
		}
	}
	public class UserManager : UserManager<ApplicationUser>
	{
		public UserManager()
			: base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
		{
		}
		public UserManager(TerritoryDataContext ctx)
			: base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
		{
			this.dataContext = ctx;
		}
		public UserManager(TerritoryDataContext ctx, string congregationKey, string roleKey, string password, string firstName, string lastName)
			: this(ctx)
		{
			this.congregationKey = congregationKey;
			this.roleKey = roleKey;
			this.password = password;
			this.firstName = firstName;
			this.lastName = lastName;
		}
		private TerritoryDataContext dataContext = null;
		private string congregationKey = null;
		private string roleKey = null;
		private string password = null;
		private string firstName = null;
		private string lastName = null;
		public async override System.Threading.Tasks.Task<ApplicationUser> FindAsync(string userName, string password)
		{
			return await Task.FromResult<ApplicationUser>(GetUser(userName, password));
		}
		public async override Task<IdentityResult> CreateAsync(ApplicationUser user)
		{
			return await Task.FromResult<IdentityResult>(CreateUser(user));
		}
		public async override Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
		{
			return await Task.FromResult<ClaimsIdentity>(CreateIdentity(user, authenticationType));
		}
		private IdentityResult CreateUser(ApplicationUser user)
		{
			var result = new UserIdentityResult(true);
			if (dataContext.Users.Any(x => x.EMail.Equals(user.Email)))
			{
				result = new UserIdentityResult(new string[] { "Email is already registered" });
				return result;
			}
			if (!dataContext.Congregations.Any(x => x.Code.Equals(congregationKey)))
			{
				result = new UserIdentityResult(new string[] { "The congregation code is invalid" });
				return result;
			}
			if (!dataContext.Roles.Any(x => x.Code.Equals(roleKey)))
			{
				result = new UserIdentityResult(new string[] { "The role code is invalid" });
				return result;
			}
			var usr = new User
			{
				UserId = user.Email,
				FirstName = firstName,
				LastName = lastName,
				EMail = user.Email,
				Password = password,
				LastLogin = DateTime.Now,
				LastRequest = DateTime.Now,
				Created = DateTime.Now,
				RegionId = 1,
				PasswordChangeRequired = false,
				IsLoggedIn = false,
				IsAdmin = dataContext.Roles.FirstOrDefault(x => x.Code.Equals(roleKey)).Name.Equals("Administrator", StringComparison.InvariantCultureIgnoreCase)
			};
			usr.UserCongregationRoles.Add(
				new UserCongregationRole
				{
					CongregationId = dataContext.Congregations.FirstOrDefault(x => x.Code.Equals(congregationKey)).Id,
					RoleId = dataContext.Roles.FirstOrDefault(x => x.Code.Equals(roleKey)).Id
				});
			dataContext.Users.InsertOnSubmit(usr);
			dataContext.SubmitChanges();
			user.Id = usr.Id.ToString();
			user.EmailConfirmed = false;
			user.Congregation = dataContext.Congregations.FirstOrDefault(x => x.Code.Equals(congregationKey));
			user.Role = dataContext.Roles.FirstOrDefault(x => x.Code.Equals(roleKey));
			user.PasswordHash = PasswordHasher.HashPassword(password);
			user.UserName = usr.EMail;
			return result;
		}
		private ClaimsIdentity CreateIdentity(ApplicationUser user, string authenticationType)
		{
			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.Email, user.Email));
			var result = new ClaimsIdentity(claims, authenticationType);
			return result;
		}
		private ApplicationUser GetUser(string userName, string password)
		{
			var thisUser = dataContext.Users.FirstOrDefault(x => x.EMail.Equals(userName));
			if (thisUser == null || !thisUser.Password.Equals(password, StringComparison.InvariantCulture))
				return null;
			var user = new ApplicationUser
			{
				DBUser = thisUser,
				Email = thisUser.EMail,
				EmailConfirmed = true,
				Id = thisUser.Id.ToString(),
				Congregation = thisUser.UserCongregationRoles.First().Congregation,
				Role = thisUser.UserCongregationRoles.First().Role,
				UserName = thisUser.EMail,
				PasswordHash = PasswordHasher.HashPassword(password)
			};
			return user;
		}
	}
}
namespace Territory
{
	public static class IdentityHelper
	{
		public const string XsrfKey = "XsrfId";
		public static void SignIn(UserManager manager, ApplicationUser user, bool isPersistent)
		{
			IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
			authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
			var identity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
			var id = new IdentityPrinciple(identity);
			HttpContext.Current.User = id;
			authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
		}
		public const string ProviderNameKey = "providerName";
		public static string GetProviderNameFromRequest(HttpRequest request)
		{
			return request[ProviderNameKey];
		}
		public static string GetExternalLoginRedirectUrl(string accountProvider)
		{
			return "/Account/RegisterExternalLogin?" + ProviderNameKey + "=" + accountProvider;
		}
		private static bool IsLocalUrl(string url)
		{
			return !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
		}
		public static void RedirectToReturnUrl(string returnUrl, HttpResponse response)
		{
			if (!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl))
			{
				response.Redirect(returnUrl);
			}
			else
			{
				response.Redirect("~/");
			}
		}
	}
}

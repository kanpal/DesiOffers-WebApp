using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebLogic.Models;
using System.Data.Entity;
using System.Security.Cryptography;

namespace WebLogic.Authentication
{
    public class WebUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(WebUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
                        
            userIdentity.AddClaim(new Claim(CustomClaimTypes.Permission, Permissions.ProductMakeOffer));
            userIdentity.AddClaim(new Claim(CustomClaimTypes.CustomerId, Id));
            return userIdentity;
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                base.PasswordHash = PasswordHasher.HashPassword(_password);
            }
        }

        public WebUser(Customer customer)
        {
            Id = customer.ID.ToString();
            Email = customer.EmailId;
            Password = customer.Password;
            PhoneNumber = customer.PhoneNumber;
            UserName = FormatName(customer.FirstName, customer.LastName);
            // TODO: Set these properly
            EmailConfirmed = true;
        }

        internal string FormatName(string firstName, string lastName)
        {
            string fullName = firstName;
            if (!string.IsNullOrWhiteSpace(fullName))
                fullName += " ";
            fullName += lastName;
            return fullName;
        }
    }

    public class WebUserStore : IUserStore<WebUser>,
        IUserPasswordStore<WebUser>,
        IUserLoginStore<WebUser>,
        IUserLockoutStore<WebUser, string>,
        IUserTwoFactorStore<WebUser, string>
    {
        private DesiOfferEntities _entities;

        public WebUserStore(DesiOfferEntities entities)
        {
            _entities = entities ?? new DesiOfferEntities();
        }

        #region IUserStore

        public Task CreateAsync(WebUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(WebUser user)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            if (_entities != null)
            {
                _entities.Dispose();
                _entities = null;
            }
        }

        public async Task<WebUser> FindByIdAsync(string userId)
        {
            long id = 0;
            if (!string.IsNullOrEmpty(userId))
                long.TryParse(userId, out id);

            if (id == 0)
            {
                throw new ArgumentException("Null or empty/invalid argument: userId");
            }

            Customer customer = await _entities.Customers.Where(c => c.ID == id).FirstOrDefaultAsync();
            return customer != null ? new WebUser(customer) : null;
        }

        public async Task<WebUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }
            Customer customer = await _entities.Customers.Where(c => c.UserId == userName).FirstOrDefaultAsync();
            return customer != null ? new WebUser(customer) : null;
        }

        public Task UpdateAsync(WebUser user)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserPasswordStore

        public Task SetPasswordHashAsync(WebUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            UpdateAsync(user);
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(WebUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(WebUser user)
        {
            return Task.FromResult(false); //!string.IsNullOrEmpty(user.PasswordHash));
        }

        #endregion

        #region IUserLoginStore

        public Task AddLoginAsync(WebUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(WebUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(WebUser user)
        {
            throw new NotImplementedException();
        }

        public Task<WebUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IUserLockoutStore
        public Task<DateTimeOffset> GetLockoutEndDateAsync(WebUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(WebUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(WebUser user)
        {
            user.AccessFailedCount++;
            UpdateAsync(user);
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(WebUser user)
        {
            user.AccessFailedCount = 0;
            UpdateAsync(user);
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(WebUser user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(WebUser user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(WebUser user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            UpdateAsync(user);
            return Task.FromResult(0);
        }

        #endregion

        #region IUserTwoFactorStore
        public Task SetTwoFactorEnabledAsync(WebUser user, bool enabled)
        {
            user.TwoFactorEnabled = enabled;
            UpdateAsync(user);
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(WebUser user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }
        #endregion
    }
}

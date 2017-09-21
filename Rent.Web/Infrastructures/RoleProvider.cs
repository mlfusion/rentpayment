using System;
using System.Linq;
using System.Linq.Expressions;
using Rent.Entities;
using User = Rent.Business.Services.User;

namespace Rent.Web.Infrastructures
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        private Rent.Business.Interfaces.IUser objUser = new User(new RentEntities());

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var roles = objUser.Select().Where(x => x.Email.Contains(username.ToLower())).FirstOrDefault();

            if (roles == null)
                return new[] {"User"};

            return new[] { roles.UsersRoles.FirstOrDefault().Role1.Name };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}

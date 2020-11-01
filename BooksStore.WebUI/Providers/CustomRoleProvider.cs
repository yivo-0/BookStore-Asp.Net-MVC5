using BooksStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BooksStore.Domain.Entities;
using System.Web.Security;

namespace BooksStore.WebUI.Providers
{
    public class CustomRoleProvider: RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (EFDbContext db = new EFDbContext())
            { 
                User user = db.Users.FirstOrDefault(u => u.Email == username);
                if (user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleId);
                    if(userRole != null)  roles = new string[] { userRole.Name };
                }
                return roles;
            }
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResualt = false;
            using (EFDbContext db = new EFDbContext())
            {
                User user = db.Users.FirstOrDefault(u => u.Email == username);
                if (user != null)
                {
                    Role userRole = db.Roles.Find(user.RoleId);
                    if (userRole != null && userRole.Name == roleName) outputResualt = true ;
                }
            }
            return outputResualt;

        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
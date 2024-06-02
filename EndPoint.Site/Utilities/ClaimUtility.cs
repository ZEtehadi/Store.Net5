using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace EndPoint.Site.Utilities
{
    public static class ClaimUtility
    {
        public static long? GetUserId(ClaimsPrincipal User)
        {
            try
            {
                var ClaimIdentity = User.Identity as ClaimsIdentity;
                if (ClaimIdentity.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    long UserId = long.Parse(ClaimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    return UserId;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal static object GetUserId(IPrincipal user)
        {
            throw new NotImplementedException();
        }

        public static List<string> GetRoles(ClaimsPrincipal USer)
        {
            try
            {
                var claimIdentity = USer.Identity as ClaimsIdentity;
                List<string> roles = new List<string>();
                
                foreach (var item in claimIdentity.Claims.Where(p => p.Type.EndsWith("role")))
                {
                    roles.Add(item.Value);
                }

                return roles;
            }
            catch(Exception)
            {
                return null;
            }

        }

        public static string GetUserEmail(ClaimsPrincipal User)
        {
            try
            {
                var ClaimIdentity = User.Identity as ClaimsIdentity;

                return ClaimIdentity.FindFirst(ClaimTypes.Email).Value;

            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
namespace phoneBill.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        private static byte[] Avatar { get; set; }
        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (typeof(T) == typeof(string))
            {
                return (T)Convert.ChangeType(loggedInUserId, typeof(T));
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(long))
            {
                return loggedInUserId != null ? (T)Convert.ChangeType(loggedInUserId, typeof(T)) : (T)Convert.ChangeType(0, typeof(T));
            }
            else if (typeof(T) == typeof(Guid))
            {
                return loggedInUserId != null ? (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(loggedInUserId) : (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(Guid.Empty);
            }
            else
            {
                throw new Exception("Invalid type provided");
            }
        }

        public static string GetLoggedInEmpID(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst("EmpID").Value.ToString();
        }

        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Name).Value.ToString();
        }

        public static string GetLoggedInUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Email).Value.ToString();
        }
        public static string GetLoggedInPosition(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst("Position").Value.ToString();
        }
        public static string GetLoggedInImgProfile(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst("ImgProfile").Value.ToString();
        }
        public static string GetLoggedInUserAvatar(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));


            if (Avatar != null)
            {
                string base64String = Convert.ToBase64String(Avatar, 0, Avatar.Length);
                byte[] temp_backToBytes = Convert.FromBase64String(base64String);
                return Convert.ToBase64String(Avatar);
            }

            return string.Empty;
        }
        public static void SetLoggedInUserAvatar(this byte[] avatar)
        {
            Avatar = avatar;
        }

        public static List<string> GetLoggedInRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
        }
    }
}

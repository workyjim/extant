//-----------------------------------------------------------------------
// <copyright file="EmailExtensions.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Text;

namespace Extant.Web.Helpers
{
    public static class EmailExtensions
    {

        private const int DefaultRotation = 9;

        public static string Rotate(this string email)
        {
            return email.Rotate(DefaultRotation);
        }

        public static string Rotate(this string email, int rotateBy)
        {
            var encoded = new StringBuilder();
            foreach (var c in email)
            {
                if (c < 65 || c > 122 || (c > 90 && c < 97))
                {
                    encoded.Append(c);
                }
                else
                {
                    var ec = c + rotateBy;
                    if (c <= 'z' && ec > 'z')
                        ec -= 26;
                    if (c <= 'Z' && ec > 'Z')
                        ec -= 26;
                    encoded.Append((char)ec);
                }
            }
            return encoded.ToString();
        }

        public static string Reverse(this string email)
        {
            var array = email.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
    }
}
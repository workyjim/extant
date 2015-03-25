//-----------------------------------------------------------------------
// <copyright file="HttpUser.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Web;

namespace Extant.Web.Infrastructure
{
    public class HttpUser : ICurrentUser
    {
        public string UserName
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }

        public string IpAddress
        {
            get
            {
                var request = HttpContext.Current.Request;
                var ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(ip))
                {
                    var addresses = ip.Split(',');
                    if (addresses.Length > 0)
                        return addresses[0];
                }
                return request.ServerVariables["REMOTE_ADDR"];
            }
        }

    }
}
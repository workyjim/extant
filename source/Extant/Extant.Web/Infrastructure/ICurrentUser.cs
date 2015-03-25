//-----------------------------------------------------------------------
// <copyright file="ICurrentUser.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2009. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Extant.Web.Infrastructure
{
    public interface ICurrentUser
    {
        string UserName { get; }
        string IpAddress { get; }
    }
}
//-----------------------------------------------------------------------
// <copyright file="FileSizeFormatter.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace Extant.Web.Infrastructure
{
    public static class FileSizeFormatter
    {
        /// <summary>
        /// http://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-using-net
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string FileSize(this int size)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB" };
            int place = Convert.ToInt32(Math.Floor(Math.Log(size, 1024)));
            double num = Math.Round(size / Math.Pow(1024, place), 1);
            return num.ToString() + " " + suf[place];
        }
    }
}
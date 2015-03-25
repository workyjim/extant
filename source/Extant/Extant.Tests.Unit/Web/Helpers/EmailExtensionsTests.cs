//-----------------------------------------------------------------------
// <copyright file="EmailExtensionsTests.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using Extant.Web.Helpers;
using NUnit.Framework;

namespace Extant.Tests.Unit.Web.Helpers
{
    [TestFixture]
    public class EmailExtensionsTests
    {
        [Test]
        public void Rotate_Test()
        {
            var email = "robert.s.harper@manchester.ac.uk";
            var result = email.Rotate();
            Assert.AreEqual("axknac.b.qjayna@vjwlqnbcna.jl.dt", result);
        }

        [Test]
        public void Rotate_Test2()
        {
            var email = "r3o+be-rt.s.har=per@ma9nch!ester.a?c.uk";
            var result = email.Rotate();
            Assert.AreEqual("a3x+kn-ac.b.qja=yna@vj9wlq!nbcna.j?l.dt", result);
        }

        [Test]
        public void Reverse_Test()
        {
            var email = "robert.s.harper@manchester.ac.uk";
            var result = email.Reverse();
            Assert.AreEqual("ku.ca.retsehcnam@reprah.s.trebor", result);
        }


    }
}
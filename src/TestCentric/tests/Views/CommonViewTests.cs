﻿// ***********************************************************************
// Copyright (c) 2016 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NUnit.UiKit.Elements;

namespace TestCentric.Gui.Views
{
    [TestFixture(typeof(MainForm))]
    [TestFixture(typeof(TestTreeView))]
	[Platform(Exclude = "Linux", Reason = "Uninitialized form causes an error in Travis-CI")]
    public class CommonViewTests<T> where T: new()
    {
        protected T View { get; private set; }

        [SetUp]
        public void CreateView()
        {
            this.View = new T();
        }

        [TestCaseSource("GetViewElementProperties")]
        public void ViewElementsAreInitialized(PropertyInfo prop)
        {
            if (prop.GetValue(View, new object[0]) == null)
                Assert.Fail("{0} was not initialized", prop.Name);
        }

        [Test]
        public void ViewElementsHaveUniqueNames()
        {
            var names = new List<string>();

            foreach (PropertyInfo prop in GetViewElementProperties())
            {
                IViewElement element = (IViewElement)prop.GetValue(View, new object[0]);
                if (element != null)
                    names.Add(element.Name); 
            }

            Assert.That(names, Is.Unique);
        }

        static protected IEnumerable<PropertyInfo> GetViewElementProperties()
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                if (typeof(IViewElement).IsAssignableFrom(prop.PropertyType))
                    yield return prop;
            }
        }
    }
}

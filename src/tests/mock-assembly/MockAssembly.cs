// ***********************************************************************
// Copyright (c) 2014 Charlie Poole
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

using System;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestCentric.Gui.Tests
{
    namespace Assemblies
    {
        /// <summary>
        /// Constant definitions for the mock-assembly dll.
        /// </summary>
        public class MockAssembly
        {
            public const int Classes = 9;
            public const int NamespaceSuites = 6; // assembly, NUnit, Tests, Assemblies, Singletons, TestAssembly

            public const int Tests = MockTestFixture.Tests
                        + Singletons.OneTestCase.Tests
                        + TestAssembly.MockTestFixture.Tests
                        + IgnoredFixture.Tests
                        + ExplicitFixture.Tests
                        + BadFixture.Tests
                        + FixtureWithTestCases.Tests
                        + ParameterizedFixture.Tests
                        + GenericFixtureConstants.Tests;
            
            public const int Suites = MockTestFixture.Suites 
                        + Singletons.OneTestCase.Suites
                        + TestAssembly.MockTestFixture.Suites 
                        + IgnoredFixture.Suites
                        + ExplicitFixture.Suites
                        + BadFixture.Suites
                        + FixtureWithTestCases.Suites
                        + ParameterizedFixture.Suites
                        + GenericFixtureConstants.Suites
                        + NamespaceSuites;
            
            public const int Nodes = Tests + Suites;
            
            public const int ExplicitFixtures = 1;
            public const int SuitesRun = Suites - ExplicitFixtures;

            public const int Passed = MockTestFixture.Passed
                        + Singletons.OneTestCase.Tests
                        + TestAssembly.MockTestFixture.Tests
                        + FixtureWithTestCases.Tests
                        + ParameterizedFixture.Tests
                        + GenericFixtureConstants.Tests;

            public const int Skipped_Ignored = MockTestFixture.Skipped_Ignored + IgnoredFixture.Tests;
            public const int Skipped_Explicit = MockTestFixture.Skipped_Explicit + ExplicitFixture.Tests;
            public const int Skipped = Skipped_Ignored + Skipped_Explicit;
            
            public const int Failed_Errors = MockTestFixture.Failed_Errors;
            public const int Failed_Failures = MockTestFixture.Failed_Failures;
            public const int Failed_NotRunnable = MockTestFixture.Failed_NotRunnable + BadFixture.Tests;
            public const int Failed = Failed_Errors + Failed_Failures + Failed_NotRunnable;

            public const int Warnings = MockTestFixture.Warnings;

            public const int Inconclusive = MockTestFixture.Inconclusive;

            public const int Categories = MockTestFixture.Categories;
        }

        [TestFixture(Description="Fake Test Fixture")]
        [Category("FixtureCategory")]
        public class MockTestFixture
        {
            public const int Tests = 12;
            public const int Suites = 1;

            public const int Passed = 1;

            public const int Skipped_Ignored = 1;
            public const int Skipped_Explicit = 1;

            public const int Failed_Failures = 1;
            public const int Failed_Errors = 1;
            public const int Failed_NotRunnable = 2;
            public const int Failed = Failed_Errors + Failed_Failures + Failed_NotRunnable;

            public const int Warnings = 1;

            public const int Inconclusive = 1;

            public const int Categories = 5;
            public const int MockCategoryTests = 2;

            [Test(Description="Mock Test #1")]
            public void MockTest1()
            {}

            [Test]
            [Category("MockCategory")]
            [Property("Severity","Critical")]
            [Description("This is a really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really, really long description")]
            public void MockTest2()
            {}

            [Test]
            [Category("MockCategory")]
            [Category("AnotherCategory")]
            public void MockTest3()
            { Assert.Pass("Succeeded!"); }

            [Test]
            protected static void MockTest5()
            {}

            [Test]
            public void FailingTest()
            {
                Assert.Fail("Intentional failure");
            }

            [Test]
            public void WarningTest()
            {
                Assert.Warn("Warning Message");
            }

            [Test, Property("TargetMethod", "SomeClassName"), Property("Size", 5), /*Property("TargetType", typeof( System.Threading.Thread ))*/]
            public void TestWithManyProperties()
            {}

            [Test]
            [Ignore("ignoring this test method for now")]
            [Category("Foo")]
            public void MockTest4()
            {}

            [Test, Explicit]
            [Category( "Special" )]
            public void ExplicitlyRunTest()
            {}

            [Test]
            public void NotRunnableTest( int a, int b)
            {
            }

            [Test]
            public void InconclusiveTest()
            {
                Assert.Inconclusive("No valid data");
            }

            [Test]
            public void TestWithException()
            {
                MethodThrowsException();
            }

            private void MethodThrowsException()
            {
                throw new Exception("Intentional Exception");
            }
        }
    }

    namespace Singletons
    {
        [TestFixture]
        public class OneTestCase
        {
            public const int Tests = 1;
            public const int Suites = 1;

            [Test]
            public virtual void TestCase() 
            {}
        }
    }

    namespace TestAssembly
    {
        [TestFixture]
        public class MockTestFixture
        {
            public const int Tests = 1;
            public const int Suites = 1;

            [Test]
            public void MyTest()
            {
            }
        }
    }

    [TestFixture, Ignore("BECAUSE")]
    public class IgnoredFixture
    {
        public const int Tests = 3;
        public const int Suites = 1;

        [Test]
        public void Test1() { }

        [Test]
        public void Test2() { }
        
        [Test]
        public void Test3() { }
    }

    [TestFixture,Explicit]
    public class ExplicitFixture
    {
        public const int Tests = 2;
        public const int Suites = 1;
        public const int Nodes = Tests + Suites;

        [Test]
        public void Test1() { }

        [Test]
        public void Test2() { }
    }

    [TestFixture]
    public class BadFixture
    {
        public const int Tests = 1;
        public const int Suites = 1;

        public BadFixture(int val) { }

        [Test]
        public void SomeTest() { }
    }
    
    [TestFixture]
    public class FixtureWithTestCases
    {
        public const int Tests = 4;
        public const int Suites = 3;
        
        [TestCase(2, 2, ExpectedResult=4)]
        [TestCase(9, 11, ExpectedResult=20)]
        public int MethodWithParameters(int x, int y)
        {
            return x+y;
        }
        
        [TestCase(2, 4)]
        [TestCase(9.2, 11.7)]
        public void GenericMethod<T>(T x, T y)
        {
        }
    }
    
    [TestFixture(5)]
    [TestFixture(42)]
    public class ParameterizedFixture
    {
        public const int Tests = 4;
        public const int Suites = 3;

        public ParameterizedFixture(int num) { }
        
        [Test]
        public void Test1() { }
        
        [Test]
        public void Test2() { }
    }
    
    public class GenericFixtureConstants
    {
        public const int Tests = 4;
        public const int Suites = 3;
    }
    
    [TestFixture(5)]
    [TestFixture(11.5)]
    public class GenericFixture<T>
    {
        public GenericFixture(T num){ }
        
        [Test]
        public void Test1() { }
        
        [Test]
        public void Test2() { }
    }
}
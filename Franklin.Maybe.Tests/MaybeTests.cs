// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaybeTests.cs" company="Daniel Franklin">
//   Copyright © 2017 Daniel Franklin.
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"),
//   to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
//   and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//   IN THE SOFTWARE.
// </license>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable InconsistentNaming
namespace Franklin.Maybe
{
    using Xunit;

    public class MaybeTests
    {
        [Fact]
        public void None_is_type_of_Maybe()
        {
            Assert.IsType<Maybe<TestClass>>(Maybe<TestClass>.None);
        }

        [Fact]
        public void None_HasValue_should_be_false()
        {
            Maybe<TestClass> maybe = Maybe<TestClass>.None;

            Assert.False(maybe.HasValue);
        }

        [Fact]
        public void Has_is_type_of_Maybe()
        {
            Assert.IsType<Maybe<TestClass>>(Maybe<TestClass>.Has(new TestClass()));
        }

        [Fact]
        public void Has_HasValue_should_be_true()
        {
            Maybe<TestClass> maybe = Maybe<TestClass>.Has(new TestClass());

            Assert.True(maybe.HasValue);
        }

        [Fact]
        public void Has_Value_should_equal_set_value()
        {
            TestClass testClass = new TestClass();

            Maybe<TestClass> maybe = Maybe<TestClass>.Has(testClass);

            Assert.Equal(testClass, maybe.Value);
        }

        [Fact]
        public void Has_set_to_null_then_HasValue_should_be_false()
        {
            Maybe<TestClass> maybe = Maybe<TestClass>.Has(null);

            Assert.False(maybe.HasValue);
        }

        [Fact]
        public void When_Maybe_used_incorrectly_HasValue_should_return_false()
        {
            Maybe<TestClass> maybe = new Maybe<TestClass>();

            Assert.False(maybe.HasValue);
        }

        [Fact]
        public void Cast_to_Maybe_T_should_return_HasValue_true()
        {
            TestClass testClass = new TestClass();

            Maybe<TestClass> maybe = (Maybe<TestClass>)testClass;

            Assert.True(maybe.HasValue);
        }

        [Fact]
        public void Cast_to_Maybe_T_should_return_set_value()
        {
            TestClass testClass = new TestClass();

            Maybe<TestClass> maybe = (Maybe<TestClass>)testClass;

            Assert.Equal(testClass, maybe.Value);
        }

        [Fact]
        public void Cast_to_null_should_return_HasValue_false()
        {
            Maybe<TestClass> maybe = (Maybe<TestClass>)null;

            Assert.False(maybe.HasValue);
        }

        [Fact]
        public void Maybe_cast_to_T_should_return_value_of_T()
        {
            TestClass testClass = new TestClass();

            Maybe<TestClass> maybe = Maybe<TestClass>.Has(testClass);

            TestClass returned = (TestClass)maybe;

            Assert.Equal(testClass, returned);
        }

        [Fact]
        public void Equals_should_compare_values()
        {
            string value1 = "1";
            string value2 = "1";
            string value3 = "2";

            Maybe<string> maybe1 = Maybe<string>.Has(value1);
            Maybe<string> maybe2 = Maybe<string>.Has(value2);
            Maybe<string> maybe3 = Maybe<string>.Has(value3);

            bool result1 = maybe1.Equals(maybe2);
            bool result2 = maybe2.Equals(maybe3);

            Assert.True(result1);
            Assert.False(result2);
        }

        [Fact]
        public void Equals_should_return_false_if_HasValue_false_and_HasValue_true_even_if_values_are_equal()
        {
            Maybe<int> maybe1 = Maybe<int>.Has(0);
            Maybe<int> maybe2 = Maybe<int>.None;

            bool result = maybe1.Equals(maybe2);

            Assert.False(result);
        }

        [Fact]
        public void Maybe_None_and_Maybe_None_are_equal()
        {
            Maybe<TestClass> maybe1 = Maybe<TestClass>.None;
            Maybe<TestClass> maybe2 = Maybe<TestClass>.None;

            bool result = maybe1.Equals(maybe2);

            Assert.True(result);
        }

        [Fact]
        public void Do_not_throw_on_abuse_of_Equals()
        {
            Maybe<TestClass> maybe1 = Maybe<TestClass>.None;

            // ReSharper disable once SuspiciousTypeConversion.Global
            bool result = maybe1.Equals("Bad");

            Assert.False(result);
        }

        [Fact]
        public void Equality_operator_should_compare_values()
        {
            string value1 = "1";
            string value2 = "1";
            string value3 = "2";

            Maybe<string> maybe1 = Maybe<string>.Has(value1);
            Maybe<string> maybe2 = Maybe<string>.Has(value2);
            Maybe<string> maybe3 = Maybe<string>.Has(value3);

            bool results1 = maybe1 == maybe2;
            bool results2 = maybe2 == maybe3;

            Assert.True(results1);
            Assert.False(results2);
        }

        [Fact]
        public void Non_equality_operator_should_compare_values()
        {
            string value1 = "1";
            string value2 = "2";
            string value3 = "2";

            Maybe<string> maybe1 = Maybe<string>.Has(value1);
            Maybe<string> maybe2 = Maybe<string>.Has(value2);
            Maybe<string> maybe3 = Maybe<string>.Has(value3);

            bool results1 = maybe1 != maybe2;
            bool results2 = maybe2 != maybe3;

            Assert.True(results1);
            Assert.False(results2);
        }

        [Fact]
        public void ToString_with_HasValue_false_returns_empty_string()
        {
            Maybe<string> maybe1 = Maybe<string>.Has(null);
            Maybe<string> maybe2 = Maybe<string>.None;

            Assert.Equal(string.Empty, maybe1.ToString());
            Assert.Equal(string.Empty, maybe2.ToString());
        }

        [Fact]
        public void ToString_with_HasValue_true_returns_value_as_string()
        {
            Maybe<string> maybe = Maybe<string>.Has("Hello");

            Assert.Equal("Hello", maybe.ToString());
        }

        [Fact]
        public void ToDebugString_returns_debug_string()
        {
            Maybe<string> maybe1 = Maybe<string>.Has(null);
            Maybe<string> maybe2 = Maybe<string>.None;
            Maybe<string> maybe3 = Maybe<string>.Has("test");

            Assert.Equal("HasValue: False, Value: ", maybe1.ToDebugString());
            Assert.Equal("HasValue: False, Value: ", maybe2.ToDebugString());
            Assert.Equal("HasValue: True, Value: test", maybe3.ToDebugString());
        }

        private class TestClass
        {
        }
    }
}
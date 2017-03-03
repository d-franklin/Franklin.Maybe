// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Maybe.cs" company="Daniel Franklin">
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
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable UseNameofExpression
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable ConvertToAutoPropertyWhenPossible
// ReSharper disable ArrangeRedundantParentheses
// ReSharper disable UseStringInterpolation
// ReSharper disable RedundantToStringCallForValueType
namespace Franklin.Maybe
{
    using System.Collections.Generic;

    /// <summary>
    /// Maybe is a value type (struct) that is guaranteed to never be null and either contain an object of the specified type or nothing at all.
    /// </summary>
    /// <typeparam name="T">Type of the object Maybe contains</typeparam>
    public struct Maybe<T>
    {
        private readonly bool maybeHasValue;

        private Maybe(T obj, bool hasValue) : this()
        {
            this.Value = obj;
            this.maybeHasValue = obj != null && hasValue;
        }

        /// <summary>
        /// Gets an empty Maybe.
        /// Value will be default of T and HasValue will be null.
        /// </summary>
        public static Maybe<T> None
        {
            get
            {
                return new Maybe<T>(default(T), false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether Maybe contains a Value.
        /// </summary>
        public bool HasValue
        {
            get
            {
                return this.maybeHasValue;
            }
        }

        /// <summary>
        /// Gets the contained value of the Maybe.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Sets the Value property of the Maybe to the supplied object.
        /// </summary>
        /// <param name="obj">The object Maybe contains</param>
        /// <returns>Maybe containing the supplied object.</returns>
        public static Maybe<T> Has(T obj)
        {
            return new Maybe<T>(obj, true);
        }

        public static explicit operator Maybe<T>(T obj)
        {
            return Has(obj);
        }

        public static explicit operator T(Maybe<T> maybe)
        {
            return maybe.Value;
        }

        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            return left.Equals(right) == false;
        }
        
        public override bool Equals(object obj)
        {
            return obj is Maybe<T> && this.Equals((Maybe<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.maybeHasValue.GetHashCode() * 31) ^ EqualityComparer<T>.Default.GetHashCode(this.Value);
            }
        }

        public bool Equals(Maybe<T> maybe)
        {
            return (this.HasValue == false && maybe.HasValue == false) || (this.HasValue == maybe.HasValue && EqualityComparer<T>.Default.Equals(this.Value, maybe.Value));
        }

        public override string ToString()
        {
            return this.HasValue == false ? string.Empty : this.Value.ToString();
        }

        public string ToDebugString()
        {
            return string.Format("HasValue: {0}, Value: {1}", this.HasValue.ToString(), this.Value);
        }
    }
}
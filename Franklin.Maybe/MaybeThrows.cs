// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaybeThrows.cs" company="Daniel Franklin">
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
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// MaybeThrows is a value type (struct) that is guaranteed to never be null and either contain an object of the specified type or nothing at all.
    /// </summary>
    /// <typeparam name="T">Type of the object Maybe contains</typeparam>
    public struct MaybeThrows<T> : IEquatable<MaybeThrows<T>>
    {
        private readonly bool maybeHasValue;

        private MaybeThrows(T obj, bool hasValue) : this()
        {
            this.Value = obj;
            this.maybeHasValue = obj != null && hasValue;
        }

        /// <summary>
        /// Gets an empty MaybeThrows.
        /// Value will be default of T and HasValue will be null.
        /// </summary>
        public static MaybeThrows<T> None
        {
            get
            {
                return new MaybeThrows<T>(default(T), false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether MaybeThrows contains a Value.
        /// </summary>
        public bool HasValue
        {
            get
            {
                return this.maybeHasValue;
            }
        }

        /// <summary>
        /// Gets the contained value of the MaybeThrows.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Sets the Value property of the MaybeThrows to the supplied object.
        /// </summary>
        /// <param name="obj">The object MaybeThrows contains</param>
        /// <returns>MaybeThrows containing the supplied object.</returns>
        /// <exception cref="NullReferenceException">Object is null.</exception>
        public static MaybeThrows<T> Has(T obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            return new MaybeThrows<T>(obj, true);
        }

        public static explicit operator MaybeThrows<T>(T obj)
        {
            return Has(obj);
        }

        public static explicit operator T(MaybeThrows<T> maybe)
        {
            return maybe.Value;
        }

        public static bool operator ==(MaybeThrows<T> left, MaybeThrows<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MaybeThrows<T> left, MaybeThrows<T> right)
        {
            return left.Equals(right) == false;
        }

        public override bool Equals(object obj)
        {
            return obj is MaybeThrows<T> && this.Equals((MaybeThrows<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.maybeHasValue.GetHashCode() * 31) ^ EqualityComparer<T>.Default.GetHashCode(this.Value);
            }
        }

        public bool Equals(MaybeThrows<T> maybe)
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
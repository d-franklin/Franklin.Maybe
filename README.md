## ![Icon](MaybeIconSmall.png) Maybe

_Maybe is a value type (struct) that is guaranteed to never be NULL and either contain an object of the specified type or nothing at all._

[![Programmers Oath](https://img.shields.io/badge/programmers-oath-brightgreen.svg)](http://blog.cleancoder.com/uncle-bob/2015/11/18/TheProgrammersOath.html) [![NuGet](https://img.shields.io/nuget/v/Franklin.Maybe.svg)](https://www.nuget.org/packages/Franklin.Maybe/) ![License](https://img.shields.io/github/license/d-franklin/Franklin.Maybe.svg)

#### NuGet

There are three versions of Maybe available on NuGet.

[Franklin.Maybe](https://www.nuget.org/packages/Franklin.Maybe/) - Contains the source file for Maybe and MaybeThrows, useful if you want to include Maybe in your project without a reference to an assembly.

[Franklin.Maybe.Tests](https://www.nuget.org/packages/Franklin.Maybe.Tests/) - Contains the unit tests for Maybe and MaybeThrows, useful for adding to your test project so coverage software doesn't report inaccurate results because of Franklin.Maybe.

[Franklin.Maybe.Assembly](https://www.nuget.org/packages/Franklin.Maybe.Assembly/) - Contains Franklin.Maybe as a compiled assembly, useful for LINQPad.

#### Usage

##### Maybe - Null Example

````
string value = null;

Maybe<string> maybe = Maybe<string>.Has(value);

maybe.HasValue // will be false
````

##### MaybeThrows - Null Example

````
string value = null;

MaybeThrows<string> maybe = MaybeThrows<string>.Has(value); // Throws NullReferenceException
````

MaybeThrows will only throw a NullReferenceException on the creation of a MaybeThrows via the Has static method.

It will **not** throw an exception if the Value property is NULL, which is possible if created via the None static method.

````
MaybeThrows<string> maybe = MaybeThrows<string>.None;

maybe.HasValue; // will be false
maybe.Value;    // null
````

##### Maybe - Normal Example

````
MyClass myClass = new MyClass();

Maybe<MyClass> maybe = Maybe<MyClass>.Has(myClass);

maybe.HasValue // will be true
maybe.Value    // myClass object
````

##### MaybeThrows - Normal Example (operates same as Maybe)

````
MyClass myClass = new MyClass();

MaybeThrows<MyClass> maybeThrows = MaybeThrows<MyClass>.Has(myClass);

maybeThrows.HasValue // will be true
maybeThrows.Value    // myClass object
````

##### Maybe - Cast Example

````
MyClass myClass = new MyClass();

Maybe<MyClass> maybe = Maybe<MyClass>.Has(myClass);

MyClass value = (MyClass)maybe; // Maybe to value
````

````
MyClass testClass = new MyClass();

Maybe<MyClass> maybe = (Maybe<MyClass>)testClass; // Value to Maybe
````

````
string value = null;

Maybe<string> maybe = (Maybe<string>)value;

maybe.HasValue // will be false
````

##### MaybeThrows - Cast Example

````
MyClass myClass = new MyClass();

MaybeThrows<MyClass> maybeThrows = MaybeThrows<MyClass>.Has(myClass);

MyClass value = (MyClass)maybeThrows; // MaybeThrows to value
````

````
MyClass testClass = new MyClass();

MaybeThrows<MyClass> maybe = (MaybeThrows<MyClass>)testClass; // Value to MaybeThrows
````

````
string value = null;

MaybeThrows<string> maybe = (MaybeThrows<string>)value; // Throws NullReferenceException
````

#### Why

Maybe is useful in cases where NULL is **_not_** an acceptable value and/or it needs to be clear to people reading the code that the expected reponse is possilby NULL.

For example, imagine the following.

````
MyNewObject value = myClass.DoSomething();
````

What is known about the returned value of DoSomething? Very little, in fact almost nothing.

Does it return an object or null or throw an exception? Is there a need for a guard clause or a try catch or is it fine like it is? We don't know and because we don't know it means we either need to dig deeper in to the code (time wasting) or wrap in a guard clause _and_ a try catch (to be safe) or ignore it and hope for the best (error prone).

Now imagine the example with Maybe

````
Maybe<MyNewObject> maybe = myClass.DoSomething();
````

What do we now about the above? We know quite a lot more than the previous example.

We know the object returned is a Maybe which will never be null (Maybe is a struct), will either contain a value or it won't, we know it will never throw an exception&sup1; and we also know the Maybe has a property that can be used to check if a value exists or not.

Maybes not only prevent errors caused by values that are unexpectly NULL, they are also clear sign when reading the code that the method being called returns a value that is ambiguous.

&sup1; _Maybe will not throw on null but if exceptions are desired this can be achived by using MaybeThrows instead_

#### Using Maybe with value types

While it is possible to use both Maybe and MaybeThrows with a value types, it is essentailly pointless since Maybe and MaybeThrows only check for NULL and **do not** consider the default value for a value type to be an error.

#### License

MIT License

Copyright (c) 2017 Daniel Franklin

Permission is hereby granted, free of charge, to any person obtaining a copy of this
software and associated documentation files (the "Software"), to deal in the Software
without restriction, including without limitation the rights to use, copy, modify,
merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be included in all copies
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
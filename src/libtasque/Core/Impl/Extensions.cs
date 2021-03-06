//
// Extensions.cs
//
// Author:
//       Antonius Riha <antoniusriha@gmail.com>
//
// Copyright (c) 2013 Antonius Riha
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasque.Core
{
	static class Extensions
	{
		static List<Tuple<object, string>> updatingProperties
			= new List<Tuple<object, string>> ();
		
		public static void SetProperty<TProperty, T> (
			this T source, string name, TProperty val, TProperty curVal,
			Action<TProperty> setVal, Func<T, TProperty, TProperty> update)
			where T : ITasqueObject, IBackendDetachable, INotifying
		{
			if (source == null)
				throw new NullReferenceException ("source");
			
			if (Equals (val, curVal))
				return;
			
			if (!source.IsBackendDetached) {
				if (!updatingProperties.Any (t => Equals (t.Item1, source) &&
				                             t.Item2 == name)) {
					var pair = new Tuple<object, string> (source, name);
					updatingProperties.Add (pair);
					if (Equals (curVal, val = update (source, val)))
						return;
					updatingProperties.Remove (pair);
				}
			}
			
			source.OnPropertyChanging (name);
			setVal (val);
			source.OnPropertyChanged (name);
		}
	}
}

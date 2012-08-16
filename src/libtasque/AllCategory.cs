// AllCategory.cs created with MonoDevelop
// User: boyd at 3:45 PM 2/12/2008
// 
// AllCategory.cs
//  
// Author:
//       Antonius Riha <antoniusriha@gmail.com>
// 
// Copyright (c) 2012 Antonius Riha
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
using System.Collections.Generic;

namespace Tasque
{
	public class AllCategory : Category
	{
		// A "set" of categories specified by the user to show when the "All"
		// category is selected in the TaskWindow.  If the list is empty, tasks
		// from all categories will be shown. Otherwise, only tasks from the
		// specified lists will be shown.
		List<string> categoriesToHide;
		
		public AllCategory () : base ("All")
		{
			var preferences = Application.Instance.Preferences;
			categoriesToHide = preferences.GetStringList (Preferences.HideInAllCategory);
			Application.Instance.Preferences.SettingChanged += OnSettingChanged;
		}
		
		void OnSettingChanged (Preferences preferences, string settingKey)
		{
			if (settingKey.CompareTo (Preferences.HideInAllCategory) == 0)
				categoriesToHide = preferences.GetStringList (Preferences.HideInAllCategory);
		}

		public override int CompareTo (Category other)
		{
			return -1;
		}
	}
}
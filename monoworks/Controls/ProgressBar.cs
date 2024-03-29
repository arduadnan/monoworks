// 
//  ProgressBar.cs - MonoWorks Project
//  
//  Author:
//       Andy Selvig <ajselvig@gmail.com>
// 
//  Copyright (c) 2010 Andy Selvig
// 
//  This library is free software; you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 2.1 of the
//  License, or (at your option) any later version.
// 
//  This library is distributed in the hope that it will be useful, but
//  WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System;

using MonoWorks.Base;
using MonoWorks.Rendering;

namespace MonoWorks.Controls
{
	/// <summary>
	/// Progress indicator that represents progress as a linear distance.
	/// </summary>
	public class ProgressBar : ProgressIndicator
	{
		public ProgressBar()
		{
		}
		
		
		private Orientation _orientation;
		/// <summary>
		/// The orientation of the progress bar.
		/// </summary>
		[MwxProperty]
		public Orientation Orientation {
			get {return _orientation;}
			set {
				_orientation = value;
				MakeDirty();
			}
		}
		
		
		private const double MinThickness = 18;
		
		private const double MinLength = 100;
				
		public override void ComputeGeometry()
		{
			base.ComputeGeometry();
			
			if (Orientation == Orientation.Horizontal)
			{
				MinHeight = MinThickness;
				MinWidth = MinLength;
			}
			else
			{
				MinHeight = MinLength;
				MinWidth = MinThickness;
			}
			ApplyUserSize();
		}
		
		

	}
}


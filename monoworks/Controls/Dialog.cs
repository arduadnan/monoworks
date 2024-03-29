// 
//  Dialog.cs
//  
//  Author:
//       Andy Selvig <ajselvig@gmail.com>
// 
//  Copyright (c) 2009 Andy Selvig
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


	public class Dialog : GenericModalControlOverlay<DialogFrame>
	{

		public Dialog() : base(new DialogFrame())
		{
			Control.Closed += (sender, e) => Close();
			GrayScene = true;                 
			CloseOnOutsideClick = false;
		}		
		
		public override void AddChild(IMwxObject child)
		{
			if (child is Renderable2D)
				Children.AddChild(child as Renderable2D);
			else
				throw new Exception("Children of Dialog must be a Control2D.");
		}

		
		/// <summary>
		/// The title displayed in the title bar.
		/// </summary>
		[MwxProperty]
		public string Title
		{
			get {return Control.Title;}
			set { Control.Title = value; }
		}		
		
		/// <summary>
		/// The contents of the dialog.
		/// </summary>
		public Container Children
		{
			get { return Control; }
		}

		public override void OnShown(Scene scene)
		{
			base.OnShown(scene);
			Center(scene);
		}

		
	}
}

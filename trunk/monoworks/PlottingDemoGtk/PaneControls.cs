// PaneControls.cs - MonoWorks Project
//
//  Copyright (C) 2008 Andy Selvig
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 

using System;

using System.IO;

using MonoWorks.Base;
using MonoWorks.Rendering;
using MonoWorks.Rendering.Controls;
using MonoWorks.GuiGtk;
using MonoWorks.GuiGtk.Framework;

namespace MonoWorks.PlottingDemoGtk
{
	
	/// <summary>
	/// Contains the rendering controls demo.
	/// </summary>
	public class PaneControls : Gtk.HBox
	{
		
		public PaneControls() : base()
		{
			// add the viewport
			TooledViewport tooledViewport = new TooledViewport(ViewportUsage.Plotting, false);
			PackEnd(tooledViewport);

			Viewport viewport = tooledViewport.Viewport;
			
			ToolBar toolbar = new ToolBar();
			toolbar.Position = new Coord(300, 300);
			toolbar.Orientation = Orientation.Vertical;
			toolbar.StyleClassName = "grad-hori";
			viewport.RenderList.AddOverlay(toolbar);

			string iconPath = Directory.GetCurrentDirectory() + "/../../../Resources/icons48/3d.png";
			Image image1 = new Image(iconPath);
			Button button1 = new Button("3d Button", image1);
			toolbar.AppendChild(button1);

			iconPath = Directory.GetCurrentDirectory() + "/../../../Resources/icons48/arc.png";
			Image image2 = new Image(iconPath);
			Button button2 = new Button("Arc", image2);
			toolbar.AppendChild(button2);

			iconPath = Directory.GetCurrentDirectory() + "/../../../Resources/icons48/refplane.png";
			Image image3 = new Image(iconPath);
			Button button3 = new Button("Ref Plane", image3);
			toolbar.AppendChild(button3);


		}
	}
}

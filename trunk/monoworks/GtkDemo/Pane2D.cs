// Pane2D.cs - MonoWorks Project
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

using MonoWorks.Rendering;
using MonoWorks.Rendering.Interaction;
using MonoWorks.Plotting;
using MonoWorks.GtkBackend;

namespace MonoWorks.GtkDemo
{
	
	/// <summary>
	/// Pane that contains the 3D portion of the plotting demo.
	/// </summary>
	public class Pane2D : Gtk.HBox
	{
		
		public Pane2D()
		{			
			// add the viewport
			adapter = new ViewportAdapter();
			PackEnd(adapter);
			TestAxes2D axes = new TestAxes2D();
			Viewport.RenderList.AddActor(axes);
			
			Viewport.Camera.Projection = Projection.Parallel;
			Viewport.Use2dInteraction = true;
//			Viewport.UsePrimaryInteractor = true;
			Viewport.Camera.SetViewDirection(ViewDirection.Front);
			new PlotController(Viewport);
			
			
			// add the control pane
//			PlotPane controlPane = new PlotPane(axes);
//			PackStart(controlPane, false, true, 6);
//			controlPane.ControlChanged += OnControlChanged;
		}
		
		protected ViewportAdapter adapter;
		
		/// <summary>
		/// The viewport.
		/// </summary>
		protected Viewport Viewport
		{
			get {return adapter.Viewport;}
		}
		
		/// <summary>
		/// Handler for state changed events from the control pane.
		/// </summary>
		protected void OnControlChanged()
		{
			Viewport.PaintGL();
		}
	}
}
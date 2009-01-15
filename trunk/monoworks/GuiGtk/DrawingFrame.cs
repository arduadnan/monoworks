// DrawingFrame.cs - MonoWorks Project
//
//    Copyright Andy Selvig 2008
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General Public License as published 
//    by the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public 
//    License along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

using MonoWorks.Rendering;
using MonoWorks.Rendering.Controls;
using MonoWorks.Model;
using MonoWorks.Model.ViewportControls;
using MonoWorks.Model.Interaction;
using MonoWorks.GuiGtk.Tree;

namespace MonoWorks.GuiGtk
{
	
	/// <summary>
	/// The frame containing a drawing and it's tree.
	/// </summary>
	public class DrawingFrame : Gtk.HPaned
	{
		
		public DrawingFrame() : base()
		{
			// create the tree
			treeView = new TreeView();
			Add1(treeView);
			
			// create the viewport			
			adapter = new ViewportAdapter();
			Add2(adapter);
			
			Controller = new Controller(Viewport);
			Controller.SetUsage(ViewportUsage.CAD);
		}
		
		
		protected Drawing drawing = null;
		/// <value>
		/// The drawing associated with this frame.
		/// </value>
		public Drawing Drawing
		{
			get {return drawing;}
			set
			{
				if (drawing != null)
					Viewport.RenderList.RemoveRenderable(drawing);
				drawing = value;
				Viewport.RenderList.AddRenderable(drawing);
				treeView.Drawing = drawing;
			
				// add the drawing interactor
				DrawingInteractor interactor = new DrawingInteractor(Viewport, drawing);
				Viewport.PrimaryInteractor = interactor;
				
				// connect the viewport to the drawing
				drawing.EntityManager.SelectionChanged += Controller.OnSelectionChanged;
				drawing.EntityManager.RaiseSelectionChanged();
			}
		}

		protected ViewportAdapter adapter;
		
		/// <value>
		/// The viewport.
		/// </value>
		public Viewport Viewport
		{
			get {return adapter.Viewport;}
		}
		
		public Controller Controller {get; private set;}
		
		protected TreeView treeView;
		/// <value>
		/// The tree view widget.
		/// </value>
		public TreeView Treeview
		{
			get {return treeView;}
			set {treeView = value;}
		}
	}
}
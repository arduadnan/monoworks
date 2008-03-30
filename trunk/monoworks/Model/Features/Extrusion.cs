// Extrusion.cs - MonoWorks Project
//
// Copyright (C) 2008 Andy Selvig
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
using System.Collections.Generic;

using gl = Tao.OpenGl.Gl;

using MonoWorks.Base;

namespace MonoWorks.Model
{
		
	
	/// <summary>
	/// An Extrusion is a feature that extrudes a 2D sketch into 3D along an axis.
	/// </summary>
	public class Extrusion : Feature
	{		
		
		/// <summary>
		/// Constructor that initializes the sketch.
		/// </summary>
		/// <param name="sketch"> A <see cref="Sketch"/> to extrude. </param>
		public Extrusion(Sketch sketch) : base(sketch)
		{
		}
		
	

#region Momentos
				
		/// <summary>
		/// Appends a momento to the momento list.
		/// </summary>
		protected override void AddMomento()
		{
			base.AddMomento();
			Momento momento = momentos[momentos.Count-1];
			momento["path"] = new RefLine();
			momento["sweep"] = new Angle();
			momento["scale"] = 1.0;
			momento["travel"] = new Length();
		}
		
#endregion
		

		
#region Attributes
		

		/// <value>
		/// Extrusion path.
		/// </value>
		public RefLine Path
		{
			get {return (RefLine)CurrentMomento["path"];}
			set {CurrentMomento["path"] = value;}
		}		
		

		/// <value>
		/// Sweep angle.
		/// </value>
		public Angle Sweep
		{
			get {return (Angle)CurrentMomento["sweep"];}
			set {CurrentMomento["sweep"] = value;}
		}
		
		
		/// <value>
		/// Scaling factor.
		/// </value>
		public double Scale
		{
			get {return (double)CurrentMomento["scale"];}
			set {CurrentMomento["scale"] = value;}
		}
		
		
		/// <value>
		/// Travel distance.
		/// </value>
		public Length Travel
		{
			get {return (Length)CurrentMomento["travel"];}
			set {CurrentMomento["travel"] = value;}
		}
		
#endregion
		
		
		
#region Rendering

		/// <summary>
		/// Renders the extrusion to the given viewport.
		/// </summary>
		/// <param name="viewport"> A <see cref="IViewport"/> to render to. </param>
		public override void Render(IViewport viewport)
		{
			// determine sweep and scaling factors
			int N = 1;
//			Angle dSweep;
//			double dScale;
//			bool isSweeped = false;
//			bool isScaled = false;
//			if (Sweep.Value != 0.0 || Scale != 1)
//			{
//				N = 24; // number of divisions
//				
//				if (Sweep.Value != 0.0)
//				{
//					isSweeped = true;
//					dSweep = Sweep / (double)N;
//				}
//				if (Scale != 1.0)
//				{
//					isScaled= true;
//					dScale = Scale / (double)N;
//				}
//			}
			double dTravel = Travel.Value / (double)N;
			Vector direction = Path.Direction;
			
			// cycle through sketch children
			gl.glColor3f(0.5f, 0.5f, 0.5f);
			foreach (Sketchable sketchable in this.Sketch.Children)
			{
				sketchable.ComputeGeometry();
				Vector[] verts = sketchable.RawPoints;
				for (int n=0; n<N; n++)
				{
					gl.glBegin(gl.GL_QUAD_STRIP);
//					gl.glBegin(gl.GL_POINTS);
					foreach (Vector vert in verts)
					{
						gl.glVertex3d(vert[0], vert[1], vert[2]);	
						gl.glVertex3d(vert[0]+direction[0]*dTravel, vert[1]+direction[1]*dTravel, vert[2]+direction[2]*dTravel); 
//						gl.glTranslated(direction[0]*dTravel, direction[1]*dTravel, direction[2]*dTravel);
//						gl.glVertex3d(vert[0], vert[1], vert[2]);		
//						gl.glTranslated(-direction[0]*dTravel, -direction[1]*dTravel, -direction[2]*dTravel); 
					}
					gl.glEnd();
					gl.glTranslated(direction[0]*dTravel, direction[1]*dTravel, direction[2]*dTravel);
				}
				gl.glTranslated(-direction[0]*Travel.Value, -direction[1]*Travel.Value, -direction[2]*Travel.Value);
			}
			
		}

		
		
#endregion
		
	}
}
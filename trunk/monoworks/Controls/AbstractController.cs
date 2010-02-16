// AbstractController.cs - MonoWorks Project
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
using System.Collections.Generic;
using System.Reflection;

using MonoWorks.Base;
using MonoWorks.Rendering;

namespace MonoWorks.Controls
{

	/// <summary>
	/// Key event delegate.
	/// </summary>
	public delegate void KeyHandler(int key);

	/// <summary>
	/// Base class for Framework controllers.
	/// </summary>
    public abstract class AbstractController<T> where T : Scene
    {
		/// <summary>
		/// Default constructor (sets the scene).
		/// </summary>
        public AbstractController(T scene)
        {
        	Scene = scene;
        	Mwx = new MwxSource();
			
			// get the actions
			MethodInfo[] methods = GetType().GetMethods();
			foreach (MethodInfo method in methods)
			{
				object[] attributes = method.GetCustomAttributes(typeof(ActionHandlerAttribute), true);

				if (attributes.Length > 0)
				{
                    // TODO: Make this work if Action() is not the first attribute
					ActionHandlerAttribute action = attributes[0] as ActionHandlerAttribute;

					// assign the method name as the name if one wasn't assigned in the attribute
					if (action.Name.Length == 0)
						action.Name = method.Name;

					// store the action
					action.MethodInfo = method;
					actions[action.Name] = action;
				}
			}
        }
		
		/// <summary>
		/// The scene this controller controls.
		/// </summary>
		public T Scene { get; private set; }

		/// <summary>
		/// The MWX source for the controller. 
		/// </summary>
		public MwxSource Mwx { get; private set; }
		
		/// <summary>
		/// The actions for this controller.
		/// </summary>
		protected Dictionary<string, ActionHandlerAttribute> actions = new Dictionary<string, ActionHandlerAttribute>();

		/// <summary>
		/// Returns true if the controller has the action of the given name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool HasMethod(string name)
		{
			return actions.ContainsKey(name);
		}

		/// <summary>
		/// Gets the action of the given name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public ActionHandlerAttribute GetAction(string name)
		{
			return actions[name];
		}

		/// <summary>
		/// Base key press handler.
		/// </summary>
		/// <param name="key"></param>
		public virtual void OnKeyPress(int key)
		{

		}


		#region Internal Updates

		/// <summary>
		/// This is true when the controller is in the middle of an internal update.
		/// </summary>
		protected bool InternalUpdate { get; private set; }

		/// <summary>
		/// Call at the beginning of an internal update.
		/// </summary>
		protected void BeginInternalUpdate()
		{
			InternalUpdate = true;
		}

		/// <summary>
		/// Call at the end of an internal update.
		/// </summary>
		protected void EndInternalUpdate()
		{
			InternalUpdate = false;
		}

		#endregion


	}
}
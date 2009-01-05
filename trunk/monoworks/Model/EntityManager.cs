// EntityManager.cs - MonoWorks Project
//
//  Copyright (C) 2009 Andy Selvig
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

namespace MonoWorks.Model
{
	
	/// <summary>
	/// Manages the entities in a drawing.
	/// </summary>
	public class EntityManager
	{
		
		public EntityManager(Drawing drawing)
		{
			this.drawing = drawing;
			
			Selected = new List<Entity>();
		}
		
		protected Drawing drawing;
		
#region Entity Registry
		
		
		protected Dictionary<long, Entity> entityRegistry = new Dictionary<long,Entity>();
		
		/// <summary>
		/// Registers an entity with the drawing.
		/// The drawing keeps a flat list of entities it contains that can 
		/// be looked by id.
		/// </summary>
		/// <param name="entity"> A <see cref="Entity"/> to add to the drawing. </param>
		public void RegisterEntity(Entity entity)
		{
			if (!entityRegistry.ContainsKey(entity.Id))
				entityRegistry[entity.Id] = entity;
		}
		
		/// <summary>
		/// Returns the entity with the given id.
		/// </summary>
		/// <param name="id"> A valid entity id. </param>
		/// <returns> The <see cref="Entity"/> with the id. </returns>
		public Entity GetEntity(long id)
		{
			if (entityRegistry.ContainsKey(id))
				return entityRegistry[id];
			else
				throw new Exception(String.Format("Drawing does not contain an entity with id {0}", id));
		}
		
#endregion
		
		
#region Selection Listeners	
		
		/// <summary>
		/// All the selection listeners to tell about selection events.
		/// </summary>
		protected List<ISelectionListener> listeners = new List<ISelectionListener>();
		
		/// <summary>
		/// Registers listener to be told about selection events.
		/// </summary>
		public void RegisterListener(ISelectionListener listener)
		{
			listeners.Add(listener);	
		}
		
		/// <summary>
		/// Unregisters listener to be told about selection events.
		/// </summary>
		public void UnregisterListener(ISelectionListener listener)
		{
			listeners.Remove(listener);	
		}
		
#endregion
		
		
#region Selection		
		
		/// <summary>
		/// The selected entities.
		/// </summary>
		public List<Entity> Selected {get; private set;}

		
		/// <summary>
		/// Selects an entity.
		/// </summary>
		/// <param name="sender"> A <see cref="ISelectionListener"/> that performed the action. </param>
		public void Select(ISelectionListener sender, Entity entity)
		{
			Selected.Add(entity);
			entity.Select();
			foreach (ISelectionListener listener in listeners)
			{
				if (listener != sender)
					listener.OnSelect(entity);
			}
			RaiseSelectionChanged();
		}
		
		/// <summary>
		/// Deselects an entity.
		/// </summary>
		/// <param name="sender"> A <see cref="ISelectionListener"/> that performed the action. </param>
		public void Deselect(ISelectionListener sender, Entity entity)
		{
			Selected.Remove(entity);
			entity.Deselect();
			foreach (ISelectionListener listener in listeners)
			{
				if (listener != sender)
					listener.OnDeselect(entity);
			}
			RaiseSelectionChanged();

		}

		/// <summary>
		/// Select all entitys.
		/// </summary>
		public void SelectAll(ISelectionListener sender)
		{
			Selected.Clear();
			foreach (Entity entity in entityRegistry.Values)
			{
			        entity.Select();
			        Selected.Add(entity);
			}
			foreach (ISelectionListener listener in listeners)
			{
			        if (listener != sender)
			                listener.OnSelectAll();
			}
			RaiseSelectionChanged();
		}
		
		/// <summary>
		/// Deselect all entitys.
		/// </summary>
		public void DeselectAll(ISelectionListener sender)
		{
			foreach (Entity entity in Selected)
			{
				entity.Deselect();
			}
			Selected.Clear();
			foreach (ISelectionListener listener in listeners)
			{
		        if (listener != sender)
		        	listener.OnDeselectAll();
			}
			RaiseSelectionChanged();
		}
		
		/// <summary>
		/// Event handler for the selection changing on the given drawing.
		/// </summary>
		public delegate void SelectionChangedHandler(Drawing drawing);
		
		/// <summary>
		/// Raised when the selection is changed at all.
		/// </summary>
		public SelectionChangedHandler SelectionChanged;
		
		/// <summary>
		/// Safely raise the SelectionChanged event.
		/// </summary>
		public void RaiseSelectionChanged()
		{
			if (SelectionChanged != null)
				SelectionChanged(drawing);
		}

		
#endregion
		
	}
}

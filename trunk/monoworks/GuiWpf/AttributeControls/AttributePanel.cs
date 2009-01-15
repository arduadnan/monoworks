﻿// AttributePanel.cs - MonoWorks Project
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

using System.Windows;
using System.Windows.Controls;

using MonoWorks.Model;
using MonoWorks.GuiWpf.Framework;

namespace MonoWorks.GuiWpf.AttributeControls
{
	/// <summary>
	/// Panel containing attribute controls for an entity.
	/// </summary>
	public class AttributePanel : StackPanel, Model.ViewportControls.IAttributePanel
	{

		public AttributePanel() : base()
		{
		}


		/// <summary>
		/// Adds the buttons to the top of the panel.
		/// </summary>
		protected void AddButtons()
		{
			Button applyButton = new Button();
			StackPanel panel = new StackPanel();
			panel.Orientation = Orientation.Horizontal;
			panel.Children.Add(ResourceManager.RenderIcon("apply", 22));
			panel.AddLabel("Apply");
			applyButton.Content = panel;
			Children.Add(applyButton);

			Button cancelButton = new Button();
			panel = new StackPanel();
			panel.Orientation = Orientation.Horizontal;
			panel.Children.Add(ResourceManager.RenderIcon("cancel", 22));
			panel.AddLabel("Cancel");
			cancelButton.Content = panel;
			Children.Add(cancelButton);
		}


		/// <summary>
		/// Show the panel with the given entity.
		/// </summary>
		/// <param name="entity"></param>
		public void Show(Entity entity)
		{
			Children.Clear();

			Visibility = Visibility.Visible;

			AddButtons();

			// create the attribute controls
			foreach (AttributeMetaData metaData in entity.MetaData.AttributeList)
			{
				AttributeControl control = AttributeControl.Generate(entity, metaData);
				Children.Add(control);
			}
		}

		/// <summary>
		/// Hide the panel.
		/// </summary>
		public void Hide()
		{
			Visibility = Visibility.Collapsed;
		}

	}
}

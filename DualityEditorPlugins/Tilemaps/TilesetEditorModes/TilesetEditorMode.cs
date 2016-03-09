﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Aga.Controls.Tree;

using Duality.Plugins.Tilemaps;

namespace Duality.Editor.Plugins.Tilemaps.TilesetEditorModes
{
	/// <summary>
	/// Describes an editing mode of the <see cref="TilesetEditor"/>. This is a
	/// base class that can be derived from in order to implement a new editing mode.
	/// </summary>
	public abstract class TilesetEditorMode
	{
		private TilesetEditor editor = null;
		private string xmlDocDesc = null;


		/// <summary>
		/// [GET] The <see cref="Tileset"/> that is currently selected in the
		/// editor.
		/// </summary>
		protected ContentRef<Tileset> SelectedTileset
		{
			get { return this.editor.SelectedTileset; }
		}

		/// <summary>
		/// [GET] The editing mode's unique and persistent id. As far as possible, this
		/// should be a constant across versions.
		/// </summary>
		public abstract string Id { get; }
		/// <summary>
		/// [GET] The editing mode's name that is displayed to the user.
		/// </summary>
		public abstract string Name { get; }
		/// <summary>
		/// [GET] An icon that is used to visually identify this editing mode.
		/// </summary>
		public abstract Image Icon { get; }
		/// <summary>
		/// [GET] An <see cref="ITreeModel"/> that describes the contents of the <see cref="TilesetEditor"/>
		/// layer view.
		/// </summary>
		public abstract ITreeModel LayerModel { get; }

		/// <summary>
		/// [GET] A user-targeted description of the purpose of this editing mode.
		/// </summary>
		public virtual string Description
		{
			get { return this.xmlDocDesc; }
		}
		/// <summary>
		/// [GET] When displaying various editing modes in a list, this defines the
		/// sorting order in which they are listed. Lower values are listed first,
		/// higher values last. The default is zero.
		/// </summary>
		public virtual int SortOrder
		{
			get { return 0; }
		}


		internal void Init(TilesetEditor editor)
		{
			this.editor = editor;

			// Derive the default description from available XML documentation
			Type type = this.GetType();
			XmlCodeDoc.Entry docEntry = HelpSystem.GetXmlCodeDoc(type);
			if (docEntry != null) this.xmlDocDesc = docEntry.Summary;
		}
		internal void RaiseOnEnter()
		{
			this.OnEnter();
		}
		internal void RaiseOnLeave()
		{
			this.OnLeave();
		}
		internal void RaiseOnTilesetSelectionChanged()
		{
			this.OnTilesetSelectionChanged();
		}
		internal void RaiseOnTilesetModified(ObjectPropertyChangedEventArgs args)
		{
			this.OnTilesetModified(args);
		}
		internal void RaiseOnLayerSelectionChanged(LayerSelectionChangedEventArgs args)
		{
			OnLayerSelectionChanged(args);
		}

		/// <summary>
		/// Called when the editing mode becomes active.
		/// </summary>
		protected virtual void OnEnter() { }
		/// <summary>
		/// Called when the editing mode is deactivated.
		/// </summary>
		protected virtual void OnLeave() { }
		/// <summary>
		/// Called when the <see cref="Tileset"/> selection changed.
		/// </summary>
		protected virtual void OnTilesetSelectionChanged() { }
		/// <summary>
		/// Called when the currently edited <see cref="Tileset"/> was modified.
		/// </summary>
		/// <param name="args"></param>
		protected virtual void OnTilesetModified(ObjectPropertyChangedEventArgs args) { }
		/// <summary>
		/// Called when the user item selection of the provided <see cref="LayerModel"/> has changed.
		/// </summary>
		protected virtual void OnLayerSelectionChanged(LayerSelectionChangedEventArgs args) { }
	}
}

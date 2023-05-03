// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// VTSTag
//
namespace SNC.OptiRamp
{
	using SNC.OptiRamp.PageObjects;
	using SNC.OptiRamp.Services.fDefs;
	using SNC.OptiRamp.Services.fRT;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Class VTSTag.
	/// </summary>
	public class VTSTag
	{
		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="VTSTag"/> class.
		/// </summary>
		public VTSTag() {
			LocalId = Guid.NewGuid();
		}
		#endregion Public Constructors
		public static VTSTag FromElement(WAProject project, IElement element, WAObject owner) {
			WAComputer c = null;
			WAServer s = null;
			string u = null;
			project.GetComputerForTag(element, out c, out s, out u);
			var tag = new VTSTag {
				ElementId = element.Id,
				Name = element.Name,
				UniqueId = element.GetPropertyValue<int>(TypeIDs.UID, 0),
				LocalId = Guid.NewGuid(),
				Owner = owner,
				Computer = c,
				Server = s,
				APIUrl = u
			};
			return tag;
		}
		public static VTSTag FromBlank(WAProject project, string name, WAObject owner, WAComputer c, WAServer s, string u) {
			var tag = new VTSTag {
				ElementId = -1,
				Name = name,
				UniqueId = -1,
				LocalId = Guid.NewGuid(),
				Owner = owner,
				Computer = c,
				Server = s,
				APIUrl = u
			};
			return tag;
		}
		#region Public Properties
		/// <summary>
		/// Gets or sets the API URL.
		/// </summary>
		/// <value>The API URL.</value>
		public string APIUrl { get; set; }
		/// <summary>
		/// Gets or sets the computer.
		/// </summary>
		/// <value>The computer.</value>
		public WAComputer Computer { get; set; }
		/// <summary>
		/// Gets or sets the element identifier.
		/// </summary>
		/// <value>The element identifier.</value>
		public int ElementId { get; set; }
		/// <summary>
		/// Gets or sets the engineering unit.
		/// </summary>
		/// <value>The engineering unit.</value>
		public string EngineeringUnit { get; set; }
		/// <summary>
		/// Gets or sets the local identifier.
		/// </summary>
		/// <value>The local identifier.</value>
		public Guid LocalId { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the owner.
		/// </summary>
		/// <value>The owner.</value>
		public WAObject Owner { get; set; }
		/// <summary>
		/// Gets or sets the server.
		/// </summary>
		/// <value>The server.</value>
		public WAServer Server { get; set; }
		/// <summary>
		/// Gets or sets the unique identifier.
		/// </summary>
		/// <value>The unique identifier.</value>
		public int UniqueId { get; set; }
		#endregion Public Properties
	}
}
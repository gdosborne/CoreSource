// -----------------------------------------------------------------------
// Copyright (c) Statistics & Controls, Inc. All rights reserved.
// Created by: Greg Osborne
// -----------------------------------------------------------------------
//
// Communicator
//
namespace SNC.OptiRamp.Services.fCommunication
{
	using SNC.OptiRamp.Services.Events;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Mail;

	/// <summary>
	/// Enum CommunicationTypes
	/// </summary>
	public enum CommunicationTypes
	{
		/// <summary>
		/// The email
		/// </summary>
		Email
	}

	/// <summary>
	/// Interface ICommunicator
	/// </summary>
	public interface ICommunicator
	{
		#region Public Methods
		/// <summary>
		/// Sends the message.
		/// </summary>
		/// <param name="message">The message.</param>
		void SendMessage(IMessage message);
		#endregion Public Methods

		#region Public Events
		/// <summary>
		/// Occurs when [action complete].
		/// </summary>
		event ActionCompleteHandler ActionComplete;
		#endregion Public Events
	}

	/// <summary>
	/// Interface IMessage
	/// </summary>
	public interface IMessage
	{
		#region Public Properties
		/// <summary>
		/// Gets or sets the body.
		/// </summary>
		/// <value>The body.</value>
		string Body { get; set; }
		/// <summary>
		/// Gets or sets the type of the communication.
		/// </summary>
		/// <value>The type of the communication.</value>
		CommunicationTypes CommunicationType { get; set; }
		/// <summary>
		/// Gets or sets the priority.
		/// </summary>
		/// <value>The priority.</value>
		MailPriority Priority { get; set; }
		/// <summary>
		/// Gets or sets the recipients.
		/// </summary>
		/// <value>The recipients.</value>
		IList<IPerson> Recipients { get; set; }
		/// <summary>
		/// Gets or sets the sender.
		/// </summary>
		/// <value>The sender.</value>
		IPerson Sender { get; set; }
		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		/// <value>The subject.</value>
		string Subject { get; set; }
		#endregion Public Properties
	}

	/// <summary>
	/// Interface IPerson
	/// </summary>
	public interface IPerson
	{
		#region Public Properties
		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		string Address { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; set; }
		#endregion Public Properties
	}
}
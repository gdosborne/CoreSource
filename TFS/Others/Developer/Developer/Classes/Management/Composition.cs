namespace SNC.OptiRamp.Application.Developer.Classes.Management {

	using MVVMFramework;
	using SNC.OptiRamp.Application.DeveloperEntities.Configuration;
	using SNC.OptiRamp.Application.DeveloperEntities.Management;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.ComponentModel.Composition.Hosting;

	using System.Linq;

	public class Composition {

		#region Public Constructors
		public Composition() {
			OptionsCategories = new List<Category>();
			ExtensionCommands = new List<DelegateCommand>();
			var extensionCatalog = new DirectoryCatalog(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Extensions"), "*.dll");
			_ExtensionContainer = new CompositionContainer(extensionCatalog);
			try {
				_ExtensionContainer.ComposeParts(this);
				foreach (var extension in Extensions) {
					ExtensionCommands.AddRange(extension.ExportedCommands);
				}
			}
			catch (Exception ex) {
				Console.WriteLine("Problem in the constructor: ", ex.ToString());
			}
		}
		#endregion Public Constructors

		#region Public Methods
		public void ShowSplashMessage(string format, params object[] args) {
			ShowSplashMessage(string.Format(format, args));
		}
		public void ShowSplashMessage(string message) {
			if (AddSplashMessage != null)
				AddSplashMessage(this, new AddSplashMessageEventArgs(message));
		}
		#endregion Public Methods

		#region Public Events
		public event AddSplashMessageHandler AddSplashMessage;
		#endregion Public Events

		#region Private Fields
		private List<DelegateCommand> _ExtensionCommands = null;
		private CompositionContainer _ExtensionContainer;
		private List<Category> _OptionsCategories = null;
		#endregion Private Fields

		#region Public Properties
		public List<DelegateCommand> ExtensionCommands {
			get {
				return _ExtensionCommands;
			}
			private set {
				_ExtensionCommands = value;
			}
		}

		[ImportMany(typeof(IExtender))]
		public IEnumerable<IExtender> Extensions {
			get;
			set;
		}
		public IList<Category> OptionsCategories {
			get {
				return _OptionsCategories;
			}
			private set {
				_OptionsCategories = (List<Category>)value;
			}
		}
		#endregion Public Properties
	}
}

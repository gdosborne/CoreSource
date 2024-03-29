namespace OzChat
{
	using GregOsborne.Application.Media;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;
	using System.Windows.Documents;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Imaging;
	using System.Windows.Navigation;
	using System.Windows.Shapes;
	using MVVMFramework;
	using OzChat.Views;
	using GregOsborne.Application.Primitives;
	using OzChat.Classes;

	internal partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		public MainWindowView View
		{
			get { return LayoutRoot.GetView<MainWindowView>(); }
		}

		private void MainWindowView_TextSent(object sender, EventArgs e)
		{
			SendTextBox.Focus();
		}

		private void SendTextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			sender.As<TextBox>().SelectAll();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var win = new IdentifyWindow
			{
				Owner = this
			};
			if (!win.ShowDialog().GetValueOrDefault())
				Environment.Exit(0);

			if (View == null)
				return;
			View.Myself = win.View.ThisPerson;
			View.InitView();
			Title = string.Format("Oz Chat ({0})", View.Myself.Name);
		}

		private void MainWindowView_ConversationItemReceived(object sender, ConversationItemReceivedEventArgs e)
		{
			if (Dispatcher.CheckAccess())
			{
				var fPerson = View.Persons.FirstOrDefault(x => x.UserName == e.Item.FromPerson);
				var b = new SolidColorBrush(e.Item.TextBrush.ToColor());
				var item = new Classes.ConversationItem
				{
					FromPerson = fPerson,
					ToPerson = View.Myself,
					Text = e.Item.Text,
					TimeRecieved = DateTime.Now,
					TimeSent = e.Item.TimeSent,
					TextBrush = b
				};
				item.FromText = string.Format(" ({0})", item.FromPerson.Name);
				View.ConversationItems.Add(item);
			}
			else
				Dispatcher.BeginInvoke(new ConversationItemReceivedHandler(MainWindowView_ConversationItemReceived), new object[] { sender, e }); ;
		}

	}
}

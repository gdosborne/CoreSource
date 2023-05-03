using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Reflection;
using System.Net.Mail;
using System.Net;
namespace KHSound
{
	public partial class ApplicationSettingsWindow : Window
	{
		public ApplicationSettingsWindow(Window owner)
		{
			Owner = owner;
			InitializeComponent();
			var assy = Assembly.GetExecutingAssembly();
			var attribs = assy.GetCustomAttributes(true);
			NameTextBlock.Text = (attribs.First(x => x is AssemblyTitleAttribute) as AssemblyTitleAttribute).Title;
			CompanyTextBlock.Text = (attribs.First(x => x is AssemblyCompanyAttribute) as AssemblyCompanyAttribute).Company;
			CopyrightTextBlock.Text = (attribs.First(x => x is AssemblyCopyrightAttribute) as AssemblyCopyrightAttribute).Copyright;
			VersionTextBlock.Text = assy.GetName().Version.ToString(3);
			var lamePath = System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "lame"), "lame.exe");
			FileVersionInfo lameFileVersionInfo = FileVersionInfo.GetVersionInfo(lamePath);
			LameVersionTextBlock.Text = lameFileVersionInfo.ProductVersion;
		}
		private void CancelCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			DialogResult = false;
		}
		private void LogFolderPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var p = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "explorer.exe",
					Arguments = Logger.LogFolder
				}
			};
			p.Start();
		}
		private void MusicFolderCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			var fbd = new FolderBrowserDialog
			{
				Description = "Select music folder...",
				RootFolder = System.Environment.SpecialFolder.MyMusic,
				ShowNewFolderButton = false,
				SelectedPath = MusicFolderTextBox.Text
			};
			var result = fbd.ShowDialog();
			if(result == System.Windows.Forms.DialogResult.Cancel)
				return;
			MusicFolderTextBox.Text = fbd.SelectedPath;
		}
		private void OKCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			App.MySettings.SetValue("Application", "MusicFolder", MusicFolderTextBox.Text);
			App.MySettings.SetValue("Application", "RecordingFolder", RecordingFolderTextBox.Text);
			App.MySettings.SetValue("Personal", "AllowJumpToPosition", AllowJumpToPositionCheckBox.IsChecked.HasValue && AllowJumpToPositionCheckBox.IsChecked.Value);
			App.MySettings.SetValue("Personal", "StopRemovesCurrent", StopRemovesSongCheckBox.IsChecked.HasValue && StopRemovesSongCheckBox.IsChecked.Value);
			App.MySettings.SetValue("Personal", "RemoveAfterPlayback", RemoveSongAfterPlaybackCheckBox.IsChecked.HasValue && RemoveSongAfterPlaybackCheckBox.IsChecked.Value);
			App.MySettings.SetValue("Personal", "AutoStartNextRecording", AutoStartNextRecordingCheckBox.IsChecked.HasValue && AutoStartNextRecordingCheckBox.IsChecked.Value);
			App.MySettings.SetValue("Personal", "ConvertImmediately", ConvertImmediatelyCheckBox.IsChecked.HasValue && ConvertImmediatelyCheckBox.IsChecked.Value);
			App.MySettings.SetValue("Personal", "MaxNumPlaylist", string.IsNullOrEmpty(MaxNumPlaylist.Text) || int.Parse(MaxNumPlaylist.Text) < 1
				? "3" : MaxNumPlaylist.Text);
			DialogResult = true;
		}
		private void RecordingFolderCommand_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			var fbd = new FolderBrowserDialog
			{
				Description = "Select recording folder...",
				RootFolder = System.Environment.SpecialFolder.MyDocuments,
				ShowNewFolderButton = true,
				SelectedPath = RecordingFolderTextBox.Text
			};
			var result = fbd.ShowDialog();
			if(result == System.Windows.Forms.DialogResult.Cancel)
				return;
			RecordingFolderTextBox.Text = fbd.SelectedPath;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			MusicFolderTextBox.Text = App.MySettings.GetValue("Application", "MusicFolder", null);
			RecordingFolderTextBox.Text = App.MySettings.GetValue("Application", "RecordingFolder", null);
			AllowJumpToPositionCheckBox.IsChecked = bool.Parse(App.MySettings.GetValue("Personal", "AllowJumpToPosition", "False"));
			StopRemovesSongCheckBox.IsChecked = bool.Parse(App.MySettings.GetValue("Personal", "StopRemovesCurrent", "True"));
			RemoveSongAfterPlaybackCheckBox.IsChecked = bool.Parse(App.MySettings.GetValue("Personal", "RemoveAfterPlayback", "True"));
			AutoStartNextRecordingCheckBox.IsChecked = bool.Parse(App.MySettings.GetValue("Personal", "AutoStartNextRecording", "True"));
			ConvertImmediatelyCheckBox.IsChecked = bool.Parse(App.MySettings.GetValue("Personal", "ConvertImmediately", "True"));
			MaxNumPlaylist.Text = App.MySettings.GetValue("Personal", "MaxNumPlaylist", "3");
		}
		private void EmailTextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var win = new SupportRequestWindow(this);
			var result = win.ShowDialog();
			if(!result.GetValueOrDefault())
				return;
			var msg = new MailMessage
			{
				Subject = win.Subject,
				Body = win.Body,
			};
			msg.To.Add(win.EmailAddress);
			try
			{
				using(var client = new SmtpClient())
				{
					client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
					client.Send(msg);
				}
			}
			catch(Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);				
			}
		}
		void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			
		}
	}
}

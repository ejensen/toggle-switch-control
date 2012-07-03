using System;
using System.Windows;
using System.Windows.Controls;

namespace Demo
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			Application.Current.UnhandledException += OnUnhandledException;
			InitializeComponent();
		}

		private void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			ShowError("Unhandled Exception:" + Environment.NewLine + e.ExceptionObject.Message);
			e.Handled = true;
		}

		private void ShowError(string errorMessage)
		{
			ErrorConsole.Text += errorMessage + Environment.NewLine;
			ErrorPanel.Visibility = Visibility.Visible;
		}
	}
}

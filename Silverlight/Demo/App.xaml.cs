using System.Windows;

namespace Demo
{
	public partial class App : Application
	{
		public App()
		{
			Startup += delegate { RootVisual = new MainPage(); };
			InitializeComponent();
		}
	}
}
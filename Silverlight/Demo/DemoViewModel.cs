using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Demo
{
	public class DemoViewModel : INotifyPropertyChanged
	{
		private const string RestartTextUnchecked = "Restart";
		private const string RestartTextChecked = "Restarting...";

		private readonly DispatcherTimer _restartEventTimer = new DispatcherTimer();

		public DemoViewModel()
		{
			Application.Current.Host.Content.Zoomed += delegate { ZoomFactor = Application.Current.Host.Content.ZoomFactor; };

			_restartEventTimer.Interval = TimeSpan.FromSeconds(1.5);
			_restartEventTimer.Tick += delegate { _restartEventTimer.Stop(); RestartToggleChecked = false; };
		}

		private double _zoomFactor = 1.0;
		public double ZoomFactor
		{
			get { return _zoomFactor; }
			set
			{
				_zoomFactor = value;
				InvokePropertyChanged("ZoomFactor");
			}
		}

		private bool _restartToggleChecked;
		public bool RestartToggleChecked
		{
			get
			{
				return _restartToggleChecked;
			}
			set
			{
				if (_restartToggleChecked != value)
				{
					_restartToggleChecked = value;
					InvokePropertyChanged("RestartToggleChecked");

					if (_restartToggleChecked)
					{
						RestartText = RestartTextChecked;
						_restartEventTimer.Start();
					}
					else
					{
						RestartText = RestartTextUnchecked;
					}
				}
			}
		}

		private string _restartText = RestartTextUnchecked;
		public string RestartText
		{
			get
			{
				return _restartText;
			}
			set
			{
				if (_restartText != value)
				{
					_restartText = value;
					InvokePropertyChanged("RestartText");
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void InvokePropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

using Demo.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Demo
{
    public class DemoViewModel : INotifyPropertyChanged
    {
        private RelayCommand _test_command;
        private readonly DispatcherTimer _restartEventTimer = new DispatcherTimer();

        public ICommand TestCommand { get { return _test_command; } }
        public DemoViewModel()
        {
            _restartEventTimer.Interval = TimeSpan.FromSeconds(1.5);
            _restartEventTimer.Tick += delegate { _restartEventTimer.Stop(); RestartToggleChecked = false; };
            _test_command = new RelayCommand(ExecuteTest);
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
                        _restartEventTimer.Start();
                    }
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

        public void ExecuteTest(object sender)
        {
            var message = "Command Executed." + Environment.NewLine;
            message += "TextBlock Content : " + Environment.NewLine;
            message += sender.ToString();
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

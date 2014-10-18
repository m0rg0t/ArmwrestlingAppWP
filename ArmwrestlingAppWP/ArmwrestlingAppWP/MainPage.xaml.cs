using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Devices.Sensors;
using Microsoft.Phone.Controls;

namespace ArmwrestlingAppWP
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Конструктор
        public MainPage()
        {
            InitializeComponent();

            _accel.ReadingChanged +=
                new EventHandler<AccelerometerReadingEventArgs>(OnReadingChanged);
            _accel.Start();
        }

        private void OnReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            // Get the acceleration in the Y direction
            // (Y rather than X since we're in landscape mode)
            double x = e.X;

            // Cap it at -0.5 and 0.5
            if (x > 0.5)
                x = 0.5;
            else if (x < -0.5)
                x = -0.5;

            // Pass the value to the UI thread
            Dispatcher.BeginInvoke(() => UpdateDisplay(x));
        }

        private void UpdateDisplay(double x)
        {
            // Move the ball to the specified position
            BallTransform.X = -x * (Track.Width - Ball.Width);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private const string _format = "0.000";
        private Accelerometer _accel = new Accelerometer();

        private bool _gameStarted;

        public bool GameStarted
        {
            get { return _gameStarted; }
            set
            {
                _gameStarted = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (GameStarted)
            {
                GameStarted = false;
                StartGameButton.Content = "Начать игру";
                _accel.Stop();
            }
            else
            {
                StartGameButton.Content = "Закончить игру";
                GameStarted = true;
                _accel.Start();
            }
            //throw new NotImplementedException();
        }
    }
}
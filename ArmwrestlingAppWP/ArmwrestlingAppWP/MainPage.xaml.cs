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
using Microsoft.Phone.Tasks;

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
            //_accel.Start();
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
            int gameVersion = _gameVersion;
            Dispatcher.BeginInvoke(() =>
            {
                if (gameVersion == _gameVersion)
                    UpdateDisplay(x);
            });
        }

        private void UpdateDisplay(double x)
        {
            // A reading already queued on the dispatcher can outlive Stop().
            if (!GameStarted)
                return;

            // Move the ball to the specified position
            BallTransform.X = -x * (Track.Width - Ball.Width);

            double timeDiff = (DateTime.Now - CurrentTime).TotalSeconds;
            if (x > 0)
            {
                FirstPlayer = Math.Round(FirstPlayer + timeDiff, 2);
            }
            else
            {
                SecondPlayer = Math.Round(SecondPlayer + timeDiff, 2);
            }
            this.FirstPlayerTime.Text = FirstPlayer.ToString();
            this.SecondPlayerTime.Text = SecondPlayer.ToString();
            CurrentTime = DateTime.Now;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private const string _format = "0.000";
        private Accelerometer _accel = new Accelerometer();

        private bool _gameStarted;
        private int _gameVersion;
        /// <summary>
        /// 
        /// </summary>
        public bool GameStarted
        {
            get { return _gameStarted; }
            set
            {
                _gameStarted = value;
            }
        }

        private double _firstPlayerTime = 0;
        /// <summary>
        /// 
        /// </summary>
        public double FirstPlayer
        {
            get { return _firstPlayerTime; }
            set { _firstPlayerTime = value; }
        }

        private double _secondPlayerTime = 0;
        /// <summary>
        /// 
        /// </summary>
        public double SecondPlayer
        {
            get { return _secondPlayerTime; }
            set { _secondPlayerTime = value; }
        }

        private DateTime _gameStartedTime;
        /// <summary>
        /// Time when game was started
        /// </summary>
        public DateTime GameStartedTime
        {
            get { return _gameStartedTime; }
            set { _gameStartedTime = value; }
        }

        private DateTime _currentTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; }
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
                _gameVersion++;
                StartGameButton.Content = "Закончить игру";
                GameStarted = true;
                GameStartedTime = DateTime.Now;
                CurrentTime = DateTime.Now;
                FirstPlayer = 0;
                SecondPlayer = 0;
                FirstPlayerTime.Text = "0";
                SecondPlayerTime.Text = "0";
                _accel.Start();
            }
            //throw new NotImplementedException();
        }

        private void ShareButton_OnClick(object sender, EventArgs e)
        {
            ShareStatusTask shareStatusTask = new ShareStatusTask();
            if (SecondPlayer > FirstPlayer)
            {
                shareStatusTask.Status = "Я победил в борьбе на руках по времени "+FirstPlayer+":"+SecondPlayer;
            }
            else
            {
                shareStatusTask.Status = "Я проиграл в борьбе на руках по времени " + FirstPlayer + ":" + SecondPlayer;
            }

            shareStatusTask.Show();
        }
    }
}
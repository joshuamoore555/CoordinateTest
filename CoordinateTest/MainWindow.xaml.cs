using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Tobii.EyeX.Framework;
using EyeXFramework;
using System.Threading;
using System.Windows.Media;

namespace CoordinateTest

{
    public partial class MainWindow : Window
    {
        private double x;
        private double y;
        private string previousPosition;
        private string currentPosition;
        int maxCount = 3;

        private int count = 0;

        List<int> xsizes = new List<int>() { 480, 240, 120 };
        List<int> ysizes = new List<int>() { 360, 180, 90 };


        public MainWindow()
        {
            InitializeComponent();

            StartEyeTracking();

            System.Timers.Timer t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += Check;
            t.AutoReset = true;
            t.Enabled = true;
        }

        void StartEyeTracking()
        {
            var eyeXHost = new EyeXHost();
            eyeXHost.Start();
            var stream = eyeXHost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);

            Task.Run(async () =>
            {
                while (true)
                {
                    stream.Next += (s, t) => SetXY(t.X, t.Y);
                    await Task.Delay(1000);
                }
            });
        }

        public void Check(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                currentPosition = CheckY() + " " + CheckX();

                if (currentPosition == previousPosition)
                {
                    Interlocked.Increment(ref count);
                    CheckLarge();
                }
                else
                {
                    count = 0;
                }

                PrintPos();

                previousPosition = currentPosition;

            }));

        }

        private void CheckLarge()
        {
            if (count > maxCount && currentPosition == "Top Left")
            {
                TopLeft.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Top Middle Left")
            {
                TopMiddleLeft.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Top Middle Right")
            {
                TopMiddleRight.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Top Right")
            {
                TopRight.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Middle Left")
            {
                MiddleLeft.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Middle Middle Left")
            {
                MiddleMiddleLeft.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Middle Middle Right")
            {
                MiddleMiddleRight.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Middle Right")
            {
                MiddleRight.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Bottom Left")
            {
                BottomLeft.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Bottom Middle Left")
            {
                BottomMiddleLeft.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Bottom Middle Right")
            {
                BottomMiddleRight.Background = Brushes.ForestGreen;
            }
            else if (count > maxCount && currentPosition == "Bottom Right")
            {
                BottomRight.Background = Brushes.ForestGreen;
            }
        }

        private void PrintPos()
        {
            TopLeft.Text = count + " \nPosition:  " + currentPosition;
            TopMiddleLeft.Text = count + " \n\nPosition:  " + currentPosition;
            TopMiddleRight.Text = count + " \n\nPosition:  " + currentPosition;
            TopRight.Text = count + " \n\nPosition:  " + currentPosition;
            MiddleLeft.Text = count + " \n\nPosition:  " + currentPosition;
            MiddleMiddleLeft.Text = count + " \n\nPosition:  " + currentPosition;
            MiddleMiddleRight.Text = count + " \n\nPosition:  " + currentPosition;
            MiddleRight.Text = count + " \n\nPosition:  " + currentPosition;
            BottomLeft.Text = count + " \n\nPosition:  " + currentPosition;
            BottomMiddleLeft.Text = count + " \n\nPosition:  " + currentPosition;
            BottomMiddleRight.Text = count + " \n\nPosition:  " + currentPosition;
            BottomRight.Text = count + " \n\nPosition:  " + currentPosition;
        }

        private void SetXY(double X, double Y)
        {
            x = X;
            y = Y;

        }

        private string CheckX()
        {
            if( x < 480)
            {
                return "Left";
            }
            else if (x > 480 && x < 960)
            {
                return "Middle Left";

            }
            else if (x > 960 && x < 1440)
            {
                return "Middle Right";

            }
            else if (x > 1440 && x < 1920)
            {
                return "Right";

            }
            else
            {
                return x.ToString();
            }
        }

        private string CheckY()
        {
            if(y < 360)
            {
                return "Top";
            }
            else if(y > 360 && y < 720)
            {
                return "Middle";
            }
            else if (y > 720 && y < 1080)
            {
                return "Bottom";
            }
            else
            {
                return y.ToString();
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tobii.Interaction;
using Tobii.EyeX.Framework;
using EyeXFramework;
using System.Threading;

namespace CoordinateTest

{
    public partial class MainWindow : Window
    {
        private double x;
        private double y;
        private string previousPosition;
        private string currentPosition;

        private int count = 0;

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

                    if (count > 3)
                    {
                        button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                }
                else
                {
                    count = 0;
                }

                PrintPos();

                previousPosition = currentPosition;

            }));

        }

        private void PrintPos()
        {
            XYBox.Text = count + " \nPosition:  " + currentPosition;
            XYBox_Copy1.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy2.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy3.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy4.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy5.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy6.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy7.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy8.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy9.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy10.Text = count + " \n\nPosition:  " + currentPosition;
            XYBox_Copy.Text = count + " \n\nPosition:  " + currentPosition;
        }

        private void PrintXY()
        {
            XYBox.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy1.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy2.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy3.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy4.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy5.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy6.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy7.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy8.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy9.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy10.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
            XYBox_Copy.Text = "x: " + x.ToString() + " \ny:  " + y.ToString() + " \nPosition:  " + currentPosition;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            finish.Text = "Button has been clicked!";
        }
    }
}
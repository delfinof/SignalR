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
using Microsoft.Phone.Controls;

namespace Microsoft.AspNet.SignalR.Client.WP7.Sample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var connection = new Connection("http://mxm-signalr.azurewebsites.net/raw-connection");

            connection.Received += data => Report(data);

            connection.Reconnected += () => Report("[{0}]: Connection restablished", DateTime.Now);

            connection.StateChanged += change => Report(change.OldState + " => " + change.NewState);

            connection.Error += ex =>
            {
                Report("========ERROR==========" + ex.Message + "=======================");
            };

            connection.Start();
        }

        void Report(string message)
        {
            Dispatcher.BeginInvoke(() =>
                                    listInfo.Items.Add(message));
        }

        void Report(string format, params object[] args)
        {
            Dispatcher.BeginInvoke(() =>
                                    listInfo.Items.Insert(0, string.Format(format, args)));
        }

    }
}
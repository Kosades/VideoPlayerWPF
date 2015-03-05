using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoviePlayerControlLibrary
{
    /// <summary>
    /// Interaction logic for MoviePlayerControl.xaml
    /// </summary>
    public partial class MoviePlayerControl : UserControl
    {
        // Specifies whether the movie is playing.
        private bool playing;

        // Used to update the position slider's current value.
        private System.Windows.Threading.DispatcherTimer timer =
            new System.Windows.Threading.DispatcherTimer();


        public MoviePlayerControl()
        {
            InitializeComponent();

            // Initialize the timer's Tick event handler:
            timer.Tick += new EventHandler(timer_Tick);
        }

        public void PlayMovie(Uri movie)
        {
            moviePlayer.Source = movie;
            PlayMovie();
        }

        public void Close()
        {
            StopMovie();
            moviePlayer.Close();
        }

        private void moviePlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            // Put code here that runs when the media
            // is first opened.

            // Set the media's starting Volume to the current 
            // value of the slider control.
            moviePlayer.Volume = (double)volumeSlider.Value;
            positionSlider.Maximum =
              moviePlayer.NaturalDuration.TimeSpan.TotalMilliseconds;

            // Update the position slider every second.
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void moviePlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            // Media playback is finished. 
            // Stop the media to seek to media start.
            StopMovie();
            timer.Stop();
        }

        private void positionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Create a TimeSpan with milliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(
              0, 0, 0, 0, (int)positionSlider.Value);
            moviePlayer.Position = ts;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            // Jump back 5 seconds:
            moviePlayer.Position =
              moviePlayer.Position.Subtract(new TimeSpan(0, 0, 0, 0, 5000));

            positionSlider.Value =
                moviePlayer.Position.TotalMilliseconds;
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            PlayMovie();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            StopMovie();
        }

        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {
            // Jump ahead 5 seconds:
            moviePlayer.Position =
              moviePlayer.Position.Add(new TimeSpan(0, 0, 0, 0, 5000));

            positionSlider.Value =
              moviePlayer.Position.TotalMilliseconds;
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            moviePlayer.Volume = (double)volumeSlider.Value;
        }

        #region Utility Methods

        void timer_Tick(object sender, EventArgs e)
        {
            // The DispatcherTimer's Tick event handler runs
            // in the UI thread, so you can work with the UI 
            // without worrying about cross-thread issues.
            positionSlider.Value =
              moviePlayer.Position.TotalMilliseconds;
        }

        private void PlayMovie()
        {
            if (!playing)
            {
                // The Play method will begin the media if it is not currently active or 
                // resume media if it is paused. This has no effect if the media is
                // already running.
                moviePlayer.Play();
                playButton.Content = "Pause";
                playing = true;
            }
            else
            {
                moviePlayer.Pause();
                playButton.Content = "Play";
                playing = false;
            }
        }

        private void StopMovie()
        {
            // The Stop method stops and resets the media to be played from
            // the beginning.
            moviePlayer.Stop();
            moviePlayer.Position = TimeSpan.Zero;
            playButton.Content = "Play";
            playing = false;
        }

        #endregion

    }
}

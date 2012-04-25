using System;
using System.Drawing;
using System.Windows.Forms;
using Common.Properties;
using Microsoft.DirectX.AudioVideoPlayback;

namespace Common.Libraries.Utility.Multimedia.VideoPlayer
{
    public partial class videoPlayerForm : Form
    {

        private Video video;

        // Instance variable for muted
        int muted = 0;

        public videoPlayerForm(string videoFile)
        {
            InitializeComponent();

            //store the original size of the panel
            int width = viewPortPanel.Width;
            int height = viewPortPanel.Height;

            // load the selected video file
            video = new Video(videoFile);

            // set up the scroll bar
            videoHSBar.Minimum = 0;
            videoHSBar.Maximum = Convert.ToInt32(video.Duration) * 10000;

            // set up volume information
            volumeTrackBar.Minimum = -60;
            volumeTrackBar.Maximum = 0;
            volumeTrackBar.Value = -20;
            volumeLabel.Text = "Volume: " + (volumeTrackBar.Value + 60);

            // set up the timer interval
            myTimer.Interval = 1;

            //set the label to blank.
            counterLabel.Text = "";

            // set the panel as the video object's owner
            video.Owner = viewPortPanel;

            // stop the video
            video.Stop();

            // resize the video to the size of the original size of the panel
            viewPortPanel.Size = new Size(width, height);

            // start the video at the right volume and start the timer
            //video.Play();
            //video.Audio.Volume = volumeTrackBar.Value * 100;
            //myTimer.Start();

            // setup the button images
            //playVideoButton.Image = CashlinxDesktop.ButtonResources.playbuttondepressed;

        } // end videoPlayerForm constructor


        private void playVideoButton_Click(object sender, EventArgs e)
        {
            if (video.State != StateFlags.Running)
            {
                video.Play();

                // Start the timer
                myTimer.Start();

                // setup the button images
                playVideoButton.Image = Common.Properties.Resources.playbuttondepressed;
                pauseVideoButton.Image = Common.Properties.Resources.pausebutton;
                stopVideoButton.Image = Common.Properties.Resources.stopbutton;

            } // end if video.State...
        } // end playVideoButton_Click method


        private void pauseVideoButton_Click(object sender, EventArgs e)
        {
            if (video.State == StateFlags.Running)
            {
                video.Pause();

                // Stop the timer
                myTimer.Stop();

                // Show status on counter
                counterLabel.Text = "PAUSED";

                // setup the button images
                playVideoButton.Image = Common.Properties.Resources.playbutton1;
                pauseVideoButton.Image = Common.Properties.Resources.pausebuttondepressed;
                stopVideoButton.Image = Common.Properties.Resources.stopbutton;

            } // end if video.State...
        } // end pauseVideoButton_Click


        private void stopVideoButton_Click(object sender, EventArgs e)
        {
            if (video.State != StateFlags.Stopped)
            {
                video.Stop();

                // Stop the timer and reset the label and scroll bar
                myTimer.Stop();
                videoHSBar.Value = 0;
                counterLabel.Text = "STOPPED";
            } // end if video.State...


            // setup the button images
            playVideoButton.Image = Common.Properties.Resources.playbutton1;
            pauseVideoButton.Image = Common.Properties.Resources.pausebutton;
            stopVideoButton.Image = Common.Properties.Resources.stopbuttondepressed;

        } // end stopVideoButton_Click


        private void myTimer_Tick(object sender, EventArgs e)
        {
            // update the counter and scroll bar
            counterLabel.Text = Convert.ToString(Convert.ToInt32(video.CurrentPosition));
            videoHSBar.Value = Convert.ToInt32(video.CurrentPosition) * 10000;

            // if video is complete, reset counter, scrollbar and timer

        } // end myTimer_Tick


        private void videoHSBar_Scroll(object sender, ScrollEventArgs e)
        {
            // when the user scrolls, update the video position and counter
            video.CurrentPosition = Convert.ToDouble(videoHSBar.Value / 10000);
            counterLabel.Text = Convert.ToString(Convert.ToInt32(video.CurrentPosition));
        } // end videoHSBar_Scroll


        private void volumeTrackBar_Scroll(object sender, EventArgs e)
        {
            // set the volume only if not muted.
            if (muted == 0)
            {
                video.Audio.Volume = volumeTrackBar.Value * 100;
                volumeLabel.Text = "Volume: " + (volumeTrackBar.Value + 60);
            }
        } // end volumTrackBar_Scroll


        private void exitButton_Click(object sender, EventArgs e)
        {
            //            Application.Exit();
            video.Stop();
            this.Hide();
            this.Close();
            this.Dispose();
        } // end exitButton_Click


        private void muteButton_Click(object sender, EventArgs e)
        {
            if (muted == 0)
            {
                // not muted, so mute when clicked
                video.Audio.Volume = -10000;
                volumeLabel.Text = "MUTED";
                muted = 1;
                muteButton.Image = Common.Properties.Resources.mutebuttondepressed;
            } // end if not muted
            else
            {
                // muted already, so un-mute
                video.Audio.Volume = volumeTrackBar.Value * 100;
                volumeLabel.Text = "Volume: " + (volumeTrackBar.Value + 60);
                muted = 0;
                muteButton.Image = Common.Properties.Resources.mutebutton;
            } // end else
        }

        public void playVideo()
        {
            this.BringToFront();
            this.Show();
            this.Focus();
            // start the video at the right volume and start the timer
            video.Play();
            video.Audio.Volume = volumeTrackBar.Value * 100;
            myTimer.Start();

            // setup the button images
            playVideoButton.Image = Common.Properties.Resources.playbuttondepressed;
        }

        public void stopVideo()
        {
            this.stopVideoButton_Click(this, EventArgs.Empty);
        }

        private void videoPlayerForm_Load(object sender, EventArgs e)
        {
            // start the video at the right volume and start the timer
//            video.Play();
            video.Audio.Volume = volumeTrackBar.Value * 100;
//            myTimer.Start();

            // setup the button images
//            playVideoButton.Image = CashlinxDesktop.ButtonResources.playbuttondepressed;

        } // end muteButton_Click


    } // end class videoPlayerForm
} // end namespace

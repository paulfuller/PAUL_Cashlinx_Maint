namespace Common.Libraries.Utility.Multimedia.VideoPlayer
{
   partial class videoPlayerForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          this.components = new System.ComponentModel.Container();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(videoPlayerForm));
          this.viewPortPanel = new System.Windows.Forms.Panel();
          this.myTimer = new System.Windows.Forms.Timer(this.components);
          this.videoHSBar = new System.Windows.Forms.HScrollBar();
          this.volumeTrackBar = new System.Windows.Forms.TrackBar();
          this.counterLabel = new System.Windows.Forms.Label();
          this.stopVideoButton = new System.Windows.Forms.Button();
          this.pauseVideoButton = new System.Windows.Forms.Button();
          this.playVideoButton = new System.Windows.Forms.Button();
          this.volumePanel = new System.Windows.Forms.Panel();
          this.titleLabel = new System.Windows.Forms.Label();
          this.scrollPanel = new System.Windows.Forms.Panel();
          this.exitButton = new System.Windows.Forms.Button();
          this.volumeLabel = new System.Windows.Forms.Label();
          this.muteButton = new System.Windows.Forms.Button();
          ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).BeginInit();
          this.volumePanel.SuspendLayout();
          this.SuspendLayout();
          // 
          // viewPortPanel
          // 
          this.viewPortPanel.BackColor = System.Drawing.Color.Black;
          resources.ApplyResources(this.viewPortPanel, "viewPortPanel");
          this.viewPortPanel.Name = "viewPortPanel";
          // 
          // myTimer
          // 
          this.myTimer.Tick += new System.EventHandler(this.myTimer_Tick);
          // 
          // videoHSBar
          // 
          resources.ApplyResources(this.videoHSBar, "videoHSBar");
          this.videoHSBar.Name = "videoHSBar";
          this.videoHSBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.videoHSBar_Scroll);
          // 
          // volumeTrackBar
          // 
          resources.ApplyResources(this.volumeTrackBar, "volumeTrackBar");
          this.volumeTrackBar.BackColor = System.Drawing.Color.SteelBlue;
          this.volumeTrackBar.LargeChange = 20;
          this.volumeTrackBar.Name = "volumeTrackBar";
          this.volumeTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
          this.volumeTrackBar.Scroll += new System.EventHandler(this.volumeTrackBar_Scroll);
          // 
          // counterLabel
          // 
          this.counterLabel.BackColor = System.Drawing.Color.Transparent;
          resources.ApplyResources(this.counterLabel, "counterLabel");
          this.counterLabel.ForeColor = System.Drawing.Color.Transparent;
          this.counterLabel.Image = global::Common.Properties.Resources.bluerectangleglossy;
          this.counterLabel.Name = "counterLabel";
          // 
          // stopVideoButton
          // 
          this.stopVideoButton.BackColor = System.Drawing.Color.Transparent;
          this.stopVideoButton.FlatAppearance.BorderSize = 0;
          this.stopVideoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
          this.stopVideoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
          resources.ApplyResources(this.stopVideoButton, "stopVideoButton");
          this.stopVideoButton.ForeColor = System.Drawing.Color.White;
          this.stopVideoButton.Image = global::Common.Properties.Resources.stopbutton;
          this.stopVideoButton.MaximumSize = new System.Drawing.Size(100, 50);
          this.stopVideoButton.MinimumSize = new System.Drawing.Size(100, 50);
          this.stopVideoButton.Name = "stopVideoButton";
          this.stopVideoButton.UseVisualStyleBackColor = false;
          this.stopVideoButton.Click += new System.EventHandler(this.stopVideoButton_Click);
          // 
          // pauseVideoButton
          // 
          this.pauseVideoButton.BackColor = System.Drawing.Color.Transparent;
          this.pauseVideoButton.FlatAppearance.BorderSize = 0;
          this.pauseVideoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
          this.pauseVideoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
          resources.ApplyResources(this.pauseVideoButton, "pauseVideoButton");
          this.pauseVideoButton.ForeColor = System.Drawing.Color.White;
          this.pauseVideoButton.Image = global::Common.Properties.Resources.pausebutton;
          this.pauseVideoButton.MaximumSize = new System.Drawing.Size(100, 50);
          this.pauseVideoButton.MinimumSize = new System.Drawing.Size(100, 50);
          this.pauseVideoButton.Name = "pauseVideoButton";
          this.pauseVideoButton.UseVisualStyleBackColor = false;
          this.pauseVideoButton.Click += new System.EventHandler(this.pauseVideoButton_Click);
          // 
          // playVideoButton
          // 
          this.playVideoButton.BackColor = System.Drawing.Color.Transparent;
          resources.ApplyResources(this.playVideoButton, "playVideoButton");
          this.playVideoButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
          this.playVideoButton.FlatAppearance.BorderSize = 0;
          this.playVideoButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
          this.playVideoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
          this.playVideoButton.ForeColor = System.Drawing.Color.White;
          this.playVideoButton.Image = global::Common.Properties.Resources.playbutton1;
          this.playVideoButton.MaximumSize = new System.Drawing.Size(100, 50);
          this.playVideoButton.MinimumSize = new System.Drawing.Size(100, 50);
          this.playVideoButton.Name = "playVideoButton";
          this.playVideoButton.UseVisualStyleBackColor = false;
          this.playVideoButton.Click += new System.EventHandler(this.playVideoButton_Click);
          // 
          // volumePanel
          // 
          this.volumePanel.BackColor = System.Drawing.Color.Transparent;
          this.volumePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          this.volumePanel.Controls.Add(this.volumeTrackBar);
          resources.ApplyResources(this.volumePanel, "volumePanel");
          this.volumePanel.Name = "volumePanel";
          // 
          // titleLabel
          // 
          resources.ApplyResources(this.titleLabel, "titleLabel");
          this.titleLabel.BackColor = System.Drawing.Color.Transparent;
          this.titleLabel.ForeColor = System.Drawing.Color.White;
          this.titleLabel.Name = "titleLabel";
          // 
          // scrollPanel
          // 
          this.scrollPanel.BackColor = System.Drawing.Color.Transparent;
          this.scrollPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
          resources.ApplyResources(this.scrollPanel, "scrollPanel");
          this.scrollPanel.Name = "scrollPanel";
          // 
          // exitButton
          // 
          this.exitButton.BackColor = System.Drawing.Color.Transparent;
          this.exitButton.FlatAppearance.BorderSize = 0;
          this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
          this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
          resources.ApplyResources(this.exitButton, "exitButton");
          this.exitButton.ForeColor = System.Drawing.Color.White;
          this.exitButton.Image = global::Common.Properties.Resources.exitbutton;
          this.exitButton.MaximumSize = new System.Drawing.Size(100, 50);
          this.exitButton.MinimumSize = new System.Drawing.Size(100, 50);
          this.exitButton.Name = "exitButton";
          this.exitButton.UseVisualStyleBackColor = false;
          this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
          // 
          // volumeLabel
          // 
          this.volumeLabel.BackColor = System.Drawing.Color.Transparent;
          resources.ApplyResources(this.volumeLabel, "volumeLabel");
          this.volumeLabel.ForeColor = System.Drawing.Color.Transparent;
          this.volumeLabel.Image = global::Common.Properties.Resources.bluerectangleglossy;
          this.volumeLabel.Name = "volumeLabel";
          // 
          // muteButton
          // 
          this.muteButton.BackColor = System.Drawing.Color.Transparent;
          this.muteButton.FlatAppearance.BorderSize = 0;
          this.muteButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
          this.muteButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
          resources.ApplyResources(this.muteButton, "muteButton");
          this.muteButton.ForeColor = System.Drawing.Color.White;
          this.muteButton.Image = global::Common.Properties.Resources.mutebutton;
          this.muteButton.Name = "muteButton";
          this.muteButton.UseVisualStyleBackColor = false;
          this.muteButton.Click += new System.EventHandler(this.muteButton_Click);
          // 
          // videoPlayerForm
          // 
          resources.ApplyResources(this, "$this");
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.Color.Black;
          this.BackgroundImage = global::Common.Properties.Resources.newDialog_600_BlueScale;
          this.ControlBox = false;
          this.Controls.Add(this.muteButton);
          this.Controls.Add(this.volumeLabel);
          this.Controls.Add(this.exitButton);
          this.Controls.Add(this.titleLabel);
          this.Controls.Add(this.videoHSBar);
          this.Controls.Add(this.counterLabel);
          this.Controls.Add(this.stopVideoButton);
          this.Controls.Add(this.pauseVideoButton);
          this.Controls.Add(this.playVideoButton);
          this.Controls.Add(this.viewPortPanel);
          this.Controls.Add(this.volumePanel);
          this.Controls.Add(this.scrollPanel);
          this.DoubleBuffered = true;
          this.ForeColor = System.Drawing.Color.Black;
          this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
          this.MaximizeBox = false;
          this.MinimizeBox = false;
          this.Name = "videoPlayerForm";
          this.ShowIcon = false;
          this.Load += new System.EventHandler(this.videoPlayerForm_Load);
          ((System.ComponentModel.ISupportInitialize)(this.volumeTrackBar)).EndInit();
          this.volumePanel.ResumeLayout(false);
          this.ResumeLayout(false);
          this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Panel viewPortPanel;
      private System.Windows.Forms.Button playVideoButton;
      private System.Windows.Forms.Button pauseVideoButton;
      private System.Windows.Forms.Button stopVideoButton;
      private System.Windows.Forms.Timer myTimer;
      private System.Windows.Forms.Label counterLabel;
      private System.Windows.Forms.HScrollBar videoHSBar;
      private System.Windows.Forms.TrackBar volumeTrackBar;
      private System.Windows.Forms.Panel volumePanel;
      private System.Windows.Forms.Label titleLabel;
      private System.Windows.Forms.Panel scrollPanel;
      private System.Windows.Forms.Button exitButton;
      private System.Windows.Forms.Label volumeLabel;
      private System.Windows.Forms.Button muteButton;
   }
}


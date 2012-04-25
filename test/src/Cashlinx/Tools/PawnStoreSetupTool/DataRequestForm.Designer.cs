namespace PawnStoreSetupTool
{
    partial class DataRequestForm
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
            this.doneButton = new System.Windows.Forms.Button();
            this.dataPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // doneButton
            // 
            this.doneButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.doneButton.Location = new System.Drawing.Point(322, 371);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(100, 33);
            this.doneButton.TabIndex = 0;
            this.doneButton.Text = "OK";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // dataPropertyGrid
            // 
            this.dataPropertyGrid.CategoryForeColor = System.Drawing.Color.Black;
            this.dataPropertyGrid.CommandsDisabledLinkColor = System.Drawing.Color.Gainsboro;
            this.dataPropertyGrid.CommandsLinkColor = System.Drawing.Color.Blue;
            this.dataPropertyGrid.CommandsVisibleIfAvailable = false;
            this.dataPropertyGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataPropertyGrid.HelpBackColor = System.Drawing.Color.WhiteSmoke;
            this.dataPropertyGrid.HelpForeColor = System.Drawing.Color.Black;
            this.dataPropertyGrid.LineColor = System.Drawing.Color.Black;
            this.dataPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.dataPropertyGrid.Name = "dataPropertyGrid";
            this.dataPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.dataPropertyGrid.Size = new System.Drawing.Size(434, 365);
            this.dataPropertyGrid.TabIndex = 1;
            this.dataPropertyGrid.ToolbarVisible = false;
            this.dataPropertyGrid.ViewBackColor = System.Drawing.Color.White;
            this.dataPropertyGrid.ViewForeColor = System.Drawing.Color.Black;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 371);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 33);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // DataRequestForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(434, 416);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.dataPropertyGrid);
            this.Controls.Add(this.doneButton);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataRequestForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Title Here";
            this.Load += new System.EventHandler(this.DataRequestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.PropertyGrid dataPropertyGrid;
        private System.Windows.Forms.Button cancelButton;
    }
}
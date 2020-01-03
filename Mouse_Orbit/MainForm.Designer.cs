namespace Mouse_Orbit
{
    partial class MainForm
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
            this.GL_Monitor = new OpenTK.GLControl();
            this.DrawTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // GL_Monitor
            // 
            this.GL_Monitor.BackColor = System.Drawing.Color.Black;
            this.GL_Monitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GL_Monitor.Location = new System.Drawing.Point(0, 0);
            this.GL_Monitor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GL_Monitor.Name = "GL_Monitor";
            this.GL_Monitor.Size = new System.Drawing.Size(782, 553);
            this.GL_Monitor.TabIndex = 0;
            this.GL_Monitor.VSync = false;
            this.GL_Monitor.Load += new System.EventHandler(this.GL_Monitor_Load);
            this.GL_Monitor.Paint += new System.Windows.Forms.PaintEventHandler(this.GL_Monitor_Paint);
            // 
            // DrawTimer
            // 
            this.DrawTimer.Interval = 20;
            this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 553);
            this.Controls.Add(this.GL_Monitor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Mouse Orbit App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl GL_Monitor;
        private System.Windows.Forms.Timer DrawTimer;
    }
}


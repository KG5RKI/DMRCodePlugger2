namespace DMRCodePlugger
{
    partial class FormMain
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
            this.gbDebug = new System.Windows.Forms.GroupBox();
            this.tbDebug = new System.Windows.Forms.TextBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.gbDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDebug
            // 
            this.gbDebug.Controls.Add(this.tbDebug);
            this.gbDebug.Location = new System.Drawing.Point(12, 21);
            this.gbDebug.Name = "gbDebug";
            this.gbDebug.Size = new System.Drawing.Size(333, 249);
            this.gbDebug.TabIndex = 0;
            this.gbDebug.TabStop = false;
            this.gbDebug.Text = "Debug";
            // 
            // tbDebug
            // 
            this.tbDebug.Location = new System.Drawing.Point(11, 28);
            this.tbDebug.Multiline = true;
            this.tbDebug.Name = "tbDebug";
            this.tbDebug.ReadOnly = true;
            this.tbDebug.Size = new System.Drawing.Size(297, 210);
            this.tbDebug.TabIndex = 0;
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(23, 285);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 23);
            this.btn1.TabIndex = 1;
            this.btn1.Text = "Test";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 317);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.gbDebug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMain";
            this.Text = "CodePlugger";
            this.gbDebug.ResumeLayout(false);
            this.gbDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDebug;
        private System.Windows.Forms.TextBox tbDebug;
        private System.Windows.Forms.Button btn1;
    }
}


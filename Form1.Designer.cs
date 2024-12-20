
namespace midi2obs
{
    partial class Form1
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
            this.connectButton = new System.Windows.Forms.Button();
            this.ConnectedCheckbox = new System.Windows.Forms.CheckBox();
            this.NoteFeedback = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(100, 23);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // ConnectedCheckbox
            // 
            this.ConnectedCheckbox.AutoSize = true;
            this.ConnectedCheckbox.Location = new System.Drawing.Point(119, 17);
            this.ConnectedCheckbox.Name = "ConnectedCheckbox";
            this.ConnectedCheckbox.Size = new System.Drawing.Size(78, 17);
            this.ConnectedCheckbox.TabIndex = 2;
            this.ConnectedCheckbox.Text = "Connected";
            this.ConnectedCheckbox.UseVisualStyleBackColor = true;
            this.ConnectedCheckbox.CheckedChanged += new System.EventHandler(this.ConnectedCheckbox_CheckedChanged);
            // 
            // NoteFeedback
            // 
            this.NoteFeedback.AutoSize = true;
            this.NoteFeedback.Location = new System.Drawing.Point(12, 38);
            this.NoteFeedback.Name = "NoteFeedback";
            this.NoteFeedback.Size = new System.Drawing.Size(30, 13);
            this.NoteFeedback.TabIndex = 3;
            this.NoteFeedback.Text = "Note";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NoteFeedback);
            this.Controls.Add(this.ConnectedCheckbox);
            this.Controls.Add(this.connectButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.CheckBox ConnectedCheckbox;
        private System.Windows.Forms.Label NoteFeedback;
    }
}


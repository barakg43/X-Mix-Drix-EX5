
namespace X_Mix_Drix_UI
{
    partial class ScoreDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.player1NameLabel = new System.Windows.Forms.Label();
            this.player2NameLabel = new System.Windows.Forms.Label();
            this.scorePlayer1Label = new System.Windows.Forms.Label();
            this.scorePlayer2Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // player1NameLabel
            // 
            this.player1NameLabel.AutoSize = true;
            this.player1NameLabel.Location = new System.Drawing.Point(10, 6);
            this.player1NameLabel.Name = "player1NameLabel";
            this.player1NameLabel.Size = new System.Drawing.Size(0, 17);
            this.player1NameLabel.TabIndex = 0;
            // 
            // player2NameLabel
            // 
            this.player2NameLabel.AutoSize = true;
            this.player2NameLabel.Location = new System.Drawing.Point(20, 6);
            this.player2NameLabel.Name = "player2NameLabel";
            this.player2NameLabel.Size = new System.Drawing.Size(0, 17);
            this.player2NameLabel.TabIndex = 1;
            // 
            // scorePlayer1Label
            // 
            this.scorePlayer1Label.AutoSize = true;
            this.scorePlayer1Label.Location = new System.Drawing.Point(20, 6);
            this.scorePlayer1Label.Name = "scorePlayer1Label";
            this.scorePlayer1Label.Size = new System.Drawing.Size(0, 17);
            this.scorePlayer1Label.TabIndex = 2;
            // 
            // scorePlayer2Label
            // 
            this.scorePlayer2Label.AutoSize = true;
            this.scorePlayer2Label.Location = new System.Drawing.Point(30, 6);
            this.scorePlayer2Label.Name = "scorePlayer2Label";
            this.scorePlayer2Label.Size = new System.Drawing.Size(0, 17);
            this.scorePlayer2Label.TabIndex = 3;
            // 
            // ScoreDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.scorePlayer2Label);
            this.Controls.Add(this.scorePlayer1Label);
            this.Controls.Add(this.player2NameLabel);
            this.Controls.Add(this.player1NameLabel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "ScoreDisplay";
            this.Size = new System.Drawing.Size(228, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label player1NameLabel;
        private System.Windows.Forms.Label player2NameLabel;
        private System.Windows.Forms.Label scorePlayer1Label;
        private System.Windows.Forms.Label scorePlayer2Label;
    }
}

namespace cs_go_wallhack {
    partial class GUI {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.EnableBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EnableBtn
            // 
            this.EnableBtn.Location = new System.Drawing.Point(12, 15);
            this.EnableBtn.Name = "EnableBtn";
            this.EnableBtn.Size = new System.Drawing.Size(260, 35);
            this.EnableBtn.TabIndex = 0;
            this.EnableBtn.Text = "Enable";
            this.EnableBtn.UseVisualStyleBackColor = true;
            this.EnableBtn.Click += new System.EventHandler(this.EnableBtn_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 71);
            this.Controls.Add(this.EnableBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.ShowIcon = false;
            this.Text = "Cheat";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EnableBtn;
    }
}


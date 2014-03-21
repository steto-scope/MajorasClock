namespace MajorasClock
{
    partial class CountdownItem
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.countdownName = new System.Windows.Forms.Label();
            this.iconsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.playpause = new System.Windows.Forms.PictureBox();
            this.edit = new System.Windows.Forms.PictureBox();
            this.reset = new System.Windows.Forms.PictureBox();
            this.notify = new System.Windows.Forms.PictureBox();
            this.exec = new System.Windows.Forms.PictureBox();
            this.delete = new System.Windows.Forms.PictureBox();
            this.remainingTime = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.iconsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playpause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notify)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.delete)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.countdownName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.iconsPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.remainingTime, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.endDate, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(356, 59);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // countdownName
            // 
            this.countdownName.AutoSize = true;
            this.countdownName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.countdownName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countdownName.ForeColor = System.Drawing.Color.White;
            this.countdownName.Location = new System.Drawing.Point(3, 0);
            this.countdownName.Name = "countdownName";
            this.countdownName.Size = new System.Drawing.Size(172, 29);
            this.countdownName.TabIndex = 0;
            this.countdownName.Text = "label1";
            this.countdownName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // iconsPanel
            // 
            this.iconsPanel.ColumnCount = 6;
            this.iconsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.iconsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.iconsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.iconsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.iconsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.iconsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.iconsPanel.Controls.Add(this.playpause, 0, 0);
            this.iconsPanel.Controls.Add(this.edit, 1, 0);
            this.iconsPanel.Controls.Add(this.reset, 2, 0);
            this.iconsPanel.Controls.Add(this.notify, 3, 0);
            this.iconsPanel.Controls.Add(this.exec, 4, 0);
            this.iconsPanel.Controls.Add(this.delete, 5, 0);
            this.iconsPanel.Location = new System.Drawing.Point(3, 32);
            this.iconsPanel.Name = "iconsPanel";
            this.iconsPanel.RowCount = 1;
            this.iconsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.iconsPanel.Size = new System.Drawing.Size(172, 24);
            this.iconsPanel.TabIndex = 1;
            // 
            // playpause
            // 
            this.playpause.Image = global::MajorasClock.Properties.Resources.play;
            this.playpause.InitialImage = global::MajorasClock.Properties.Resources.play;
            this.playpause.Location = new System.Drawing.Point(3, 3);
            this.playpause.Name = "playpause";
            this.playpause.Size = new System.Drawing.Size(16, 16);
            this.playpause.TabIndex = 0;
            this.playpause.TabStop = false;
            this.playpause.Click += new System.EventHandler(this.playpause_Click);
            // 
            // edit
            // 
            this.edit.Image = global::MajorasClock.Properties.Resources.edit;
            this.edit.Location = new System.Drawing.Point(25, 3);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(16, 16);
            this.edit.TabIndex = 1;
            this.edit.TabStop = false;
            this.edit.Click += new System.EventHandler(this.edit_Click);
            // 
            // reset
            // 
            this.reset.Image = global::MajorasClock.Properties.Resources.reset;
            this.reset.Location = new System.Drawing.Point(47, 3);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(16, 16);
            this.reset.TabIndex = 5;
            this.reset.TabStop = false;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // notify
            // 
            this.notify.Image = global::MajorasClock.Properties.Resources.nonotify;
            this.notify.Location = new System.Drawing.Point(69, 3);
            this.notify.Name = "notify";
            this.notify.Size = new System.Drawing.Size(16, 16);
            this.notify.TabIndex = 2;
            this.notify.TabStop = false;
            this.notify.Click += new System.EventHandler(this.notify_Click);
            // 
            // exec
            // 
            this.exec.Image = global::MajorasClock.Properties.Resources.noactions;
            this.exec.Location = new System.Drawing.Point(91, 3);
            this.exec.Name = "exec";
            this.exec.Size = new System.Drawing.Size(22, 18);
            this.exec.TabIndex = 3;
            this.exec.TabStop = false;
            this.exec.Click += new System.EventHandler(this.exec_Click);
            // 
            // delete
            // 
            this.delete.Image = global::MajorasClock.Properties.Resources.delete;
            this.delete.Location = new System.Drawing.Point(119, 3);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(22, 18);
            this.delete.TabIndex = 4;
            this.delete.TabStop = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // remainingTime
            // 
            this.remainingTime.BackColor = System.Drawing.Color.Transparent;
            this.remainingTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remainingTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remainingTime.ForeColor = System.Drawing.Color.White;
            this.remainingTime.Location = new System.Drawing.Point(181, 0);
            this.remainingTime.Name = "remainingTime";
            this.remainingTime.Size = new System.Drawing.Size(172, 29);
            this.remainingTime.TabIndex = 2;
            this.remainingTime.Text = "-";
            this.remainingTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // endDate
            // 
            this.endDate.AutoSize = true;
            this.endDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.endDate.ForeColor = System.Drawing.Color.White;
            this.endDate.Location = new System.Drawing.Point(181, 39);
            this.endDate.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(172, 20);
            this.endDate.TabIndex = 3;
            this.endDate.Text = "label2";
            this.endDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // CountdownItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CountdownItem";
            this.Size = new System.Drawing.Size(356, 59);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.iconsPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playpause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notify)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.delete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label countdownName;
        private System.Windows.Forms.TableLayoutPanel iconsPanel;
        private System.Windows.Forms.Label remainingTime;
        private System.Windows.Forms.Label endDate;
        private System.Windows.Forms.PictureBox playpause;
        private System.Windows.Forms.PictureBox edit;
        private System.Windows.Forms.PictureBox notify;
        private System.Windows.Forms.PictureBox exec;
        private System.Windows.Forms.PictureBox delete;
        private System.Windows.Forms.PictureBox reset;
    }
}

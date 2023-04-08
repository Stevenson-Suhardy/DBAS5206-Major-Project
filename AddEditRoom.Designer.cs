namespace DBAS5206_Major_Project
{
    partial class AddEditRoom
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
            this.labelRoomLocation = new System.Windows.Forms.Label();
            this.textBoxRoomLocation = new System.Windows.Forms.TextBox();
            this.labelRoomType = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxRoomType = new System.Windows.Forms.ComboBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelRoomLocation
            // 
            this.labelRoomLocation.AutoSize = true;
            this.labelRoomLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.labelRoomLocation.Location = new System.Drawing.Point(97, 70);
            this.labelRoomLocation.Name = "labelRoomLocation";
            this.labelRoomLocation.Size = new System.Drawing.Size(175, 29);
            this.labelRoomLocation.TabIndex = 0;
            this.labelRoomLocation.Text = "Room Location";
            // 
            // textBoxRoomLocation
            // 
            this.textBoxRoomLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.textBoxRoomLocation.Location = new System.Drawing.Point(102, 102);
            this.textBoxRoomLocation.Name = "textBoxRoomLocation";
            this.textBoxRoomLocation.Size = new System.Drawing.Size(170, 34);
            this.textBoxRoomLocation.TabIndex = 1;
            // 
            // labelRoomType
            // 
            this.labelRoomType.AutoSize = true;
            this.labelRoomType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.labelRoomType.Location = new System.Drawing.Point(97, 139);
            this.labelRoomType.Name = "labelRoomType";
            this.labelRoomType.Size = new System.Drawing.Size(139, 29);
            this.labelRoomType.TabIndex = 2;
            this.labelRoomType.Text = "Room Type";
            // 
            // buttonOK
            // 
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.buttonOK.Location = new System.Drawing.Point(50, 316);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(120, 42);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.buttonCancel.Location = new System.Drawing.Point(214, 316);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(120, 42);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxRoomType
            // 
            this.comboBoxRoomType.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.comboBoxRoomType.FormattingEnabled = true;
            this.comboBoxRoomType.Location = new System.Drawing.Point(102, 171);
            this.comboBoxRoomType.Name = "comboBoxRoomType";
            this.comboBoxRoomType.Size = new System.Drawing.Size(170, 37);
            this.comboBoxRoomType.TabIndex = 6;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.buttonDelete.Location = new System.Drawing.Point(104, 396);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(168, 42);
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "Delete Room";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // AddEditRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 450);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.comboBoxRoomType);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelRoomType);
            this.Controls.Add(this.textBoxRoomLocation);
            this.Controls.Add(this.labelRoomLocation);
            this.Name = "AddEditRoom";
            this.Text = "Add Or Edit Room";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRoomLocation;
        private System.Windows.Forms.TextBox textBoxRoomLocation;
        private System.Windows.Forms.Label labelRoomType;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxRoomType;
        private System.Windows.Forms.Button buttonDelete;
    }
}
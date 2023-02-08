namespace CarRepairShop
{
    partial class FormDetail
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.LabelCost = new System.Windows.Forms.Label();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(112, 9);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(240, 23);
            this.textBoxName.TabIndex = 0;
            // 
            // textBoxCost
            // 
            this.textBoxCost.Location = new System.Drawing.Point(112, 40);
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(138, 23);
            this.textBoxCost.TabIndex = 1;
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Location = new System.Drawing.Point(27, 11);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(59, 15);
            this.LabelName.TabIndex = 2;
            this.LabelName.Text = "Название";
            // 
            // LabelCost
            // 
            this.LabelCost.AutoSize = true;
            this.LabelCost.Location = new System.Drawing.Point(27, 43);
            this.LabelCost.Name = "LabelCost";
            this.LabelCost.Size = new System.Drawing.Size(35, 15);
            this.LabelCost.TabIndex = 3;
            this.LabelCost.Text = "Цена";
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(152, 77);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(97, 22);
            this.ButtonSave.TabIndex = 4;
            this.ButtonSave.Text = "Сохранить";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(270, 77);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(82, 22);
            this.ButtonCancel.TabIndex = 5;
            this.ButtonCancel.Text = "Отмена";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // FormDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 108);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.LabelCost);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.textBoxCost);
            this.Controls.Add(this.textBoxName);
            this.Name = "FormDetail";
            this.Text = "Деталь";
            this.Load += new System.EventHandler(this.FormDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxName;
        private TextBox textBoxCost;
        private Label LabelName;
        private Label LabelCost;
        private Button ButtonSave;
        private Button ButtonCancel;
    }
}
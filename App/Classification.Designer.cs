namespace App
{
    partial class Classification
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataButton = new System.Windows.Forms.Button();
            this.joblibButton = new System.Windows.Forms.Button();
            this.predictButton = new System.Windows.Forms.Button();
            this.serversBox = new System.Windows.Forms.ComboBox();
            this.pythonButton = new System.Windows.Forms.Button();
            this.graphBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.graphBox)).BeginInit();
            this.SuspendLayout();
            // 
            // dataButton
            // 
            this.dataButton.Location = new System.Drawing.Point(41, 35);
            this.dataButton.Name = "dataButton";
            this.dataButton.Size = new System.Drawing.Size(128, 44);
            this.dataButton.TabIndex = 0;
            this.dataButton.Text = "Check CSV";
            this.dataButton.UseVisualStyleBackColor = true;
            this.dataButton.Click += new System.EventHandler(this.dataButton_Click);
            // 
            // joblibButton
            // 
            this.joblibButton.Location = new System.Drawing.Point(41, 109);
            this.joblibButton.Name = "joblibButton";
            this.joblibButton.Size = new System.Drawing.Size(128, 44);
            this.joblibButton.TabIndex = 1;
            this.joblibButton.Text = "Check JOBLIB";
            this.joblibButton.UseVisualStyleBackColor = true;
            this.joblibButton.Click += new System.EventHandler(this.joblibButton_Click);
            // 
            // predictButton
            // 
            this.predictButton.Location = new System.Drawing.Point(41, 471);
            this.predictButton.Name = "predictButton";
            this.predictButton.Size = new System.Drawing.Size(128, 44);
            this.predictButton.TabIndex = 3;
            this.predictButton.Text = "Predict";
            this.predictButton.UseVisualStyleBackColor = true;
            this.predictButton.Click += new System.EventHandler(this.predictButton_Click);
            // 
            // serversBox
            // 
            this.serversBox.Items.AddRange(new object[] {
            "75.127.97.72",
            "97.74.144.108",
            "74.63.40.21",
            "208.113.162.153",
            "69.84.133.138",
            "67.220.214.50",
            "69.192.24.88",
            "203.73.24.75",
            "74.55.1.4",
            "97.74.104.201"});
            this.serversBox.Location = new System.Drawing.Point(41, 192);
            this.serversBox.Name = "serversBox";
            this.serversBox.Size = new System.Drawing.Size(149, 28);
            this.serversBox.TabIndex = 4;
            // 
            // pythonButton
            // 
            this.pythonButton.Location = new System.Drawing.Point(41, 260);
            this.pythonButton.Name = "pythonButton";
            this.pythonButton.Size = new System.Drawing.Size(128, 44);
            this.pythonButton.TabIndex = 5;
            this.pythonButton.Text = "Open Python";
            this.pythonButton.UseVisualStyleBackColor = true;
            this.pythonButton.Click += new System.EventHandler(this.pythonButton_Click);
            // 
            // graphBox
            // 
            this.graphBox.Location = new System.Drawing.Point(209, 25);
            this.graphBox.Name = "graphBox";
            this.graphBox.Size = new System.Drawing.Size(640, 480);
            this.graphBox.TabIndex = 6;
            this.graphBox.TabStop = false;
            // 
            // Classification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 546);
            this.Controls.Add(this.graphBox);
            this.Controls.Add(this.pythonButton);
            this.Controls.Add(this.serversBox);
            this.Controls.Add(this.predictButton);
            this.Controls.Add(this.joblibButton);
            this.Controls.Add(this.dataButton);
            this.Name = "Classification";
            this.Text = "Dos Attack Classification Program";
            this.Load += new System.EventHandler(this.Classification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.graphBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button dataButton;
        private System.Windows.Forms.Button joblibButton;
        private System.Windows.Forms.Button predictButton;
        private System.Windows.Forms.ComboBox serversBox;
        private System.Windows.Forms.Button pythonButton;
        private System.Windows.Forms.PictureBox graphBox;
    }
}


namespace Menu.UI
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
            this.storeSelect = new System.Windows.Forms.ComboBox();
            this.store = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sucaiCount = new System.Windows.Forms.NumericUpDown();
            this.huncaiCount = new System.Windows.Forms.NumericUpDown();
            this.minPrice = new System.Windows.Forms.TextBox();
            this.maxPrice = new System.Windows.Forms.TextBox();
            this.zhi = new System.Windows.Forms.Label();
            this.calc = new System.Windows.Forms.Button();
            this.calcResult = new System.Windows.Forms.TextBox();
            this.Clscr = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sucaiCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huncaiCount)).BeginInit();
            this.SuspendLayout();
            // 
            // storeSelect
            // 
            this.storeSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.storeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.storeSelect.FormattingEnabled = true;
            this.storeSelect.Location = new System.Drawing.Point(94, 12);
            this.storeSelect.Name = "storeSelect";
            this.storeSelect.Size = new System.Drawing.Size(152, 20);
            this.storeSelect.TabIndex = 0;
            // 
            // store
            // 
            this.store.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.store.AutoSize = true;
            this.store.Location = new System.Drawing.Point(43, 16);
            this.store.Name = "store";
            this.store.Size = new System.Drawing.Size(35, 12);
            this.store.TabIndex = 1;
            this.store.Text = "Store";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "素菜个数";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "荤菜个数";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "价格区间";
            // 
            // sucaiCount
            // 
            this.sucaiCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sucaiCount.Location = new System.Drawing.Point(107, 46);
            this.sucaiCount.Name = "sucaiCount";
            this.sucaiCount.Size = new System.Drawing.Size(127, 21);
            this.sucaiCount.TabIndex = 5;
            // 
            // huncaiCount
            // 
            this.huncaiCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.huncaiCount.Location = new System.Drawing.Point(107, 73);
            this.huncaiCount.Name = "huncaiCount";
            this.huncaiCount.Size = new System.Drawing.Size(127, 21);
            this.huncaiCount.TabIndex = 6;
            // 
            // minPrice
            // 
            this.minPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minPrice.Location = new System.Drawing.Point(95, 104);
            this.minPrice.MaxLength = 3;
            this.minPrice.Name = "minPrice";
            this.minPrice.Size = new System.Drawing.Size(57, 21);
            this.minPrice.TabIndex = 7;
            this.minPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.minPrice_KeyPress);
            // 
            // maxPrice
            // 
            this.maxPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maxPrice.Location = new System.Drawing.Point(195, 104);
            this.maxPrice.MaxLength = 3;
            this.maxPrice.Name = "maxPrice";
            this.maxPrice.Size = new System.Drawing.Size(51, 21);
            this.maxPrice.TabIndex = 8;
            this.maxPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maxPrice_KeyPress);
            // 
            // zhi
            // 
            this.zhi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zhi.AutoSize = true;
            this.zhi.Location = new System.Drawing.Point(170, 108);
            this.zhi.Name = "zhi";
            this.zhi.Size = new System.Drawing.Size(11, 12);
            this.zhi.TabIndex = 9;
            this.zhi.Text = "-";
            // 
            // calc
            // 
            this.calc.Location = new System.Drawing.Point(64, 139);
            this.calc.Name = "calc";
            this.calc.Size = new System.Drawing.Size(78, 23);
            this.calc.TabIndex = 10;
            this.calc.Text = "Calc";
            this.calc.UseVisualStyleBackColor = true;
            this.calc.Click += new System.EventHandler(this.calc_Click);
            // 
            // calcResult
            // 
            this.calcResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calcResult.Location = new System.Drawing.Point(12, 168);
            this.calcResult.Multiline = true;
            this.calcResult.Name = "calcResult";
            this.calcResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.calcResult.Size = new System.Drawing.Size(256, 230);
            this.calcResult.TabIndex = 12;
            // 
            // Clscr
            // 
            this.Clscr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Clscr.Location = new System.Drawing.Point(152, 139);
            this.Clscr.Name = "Clscr";
            this.Clscr.Size = new System.Drawing.Size(79, 23);
            this.Clscr.TabIndex = 13;
            this.Clscr.Text = "Clscr";
            this.Clscr.UseVisualStyleBackColor = true;
            this.Clscr.Click += new System.EventHandler(this.Clscr_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 410);
            this.Controls.Add(this.Clscr);
            this.Controls.Add(this.calcResult);
            this.Controls.Add(this.calc);
            this.Controls.Add(this.zhi);
            this.Controls.Add(this.maxPrice);
            this.Controls.Add(this.minPrice);
            this.Controls.Add(this.huncaiCount);
            this.Controls.Add(this.sucaiCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.store);
            this.Controls.Add(this.storeSelect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sucaiCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huncaiCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox storeSelect;
        private System.Windows.Forms.Label store;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown sucaiCount;
        private System.Windows.Forms.NumericUpDown huncaiCount;
        private System.Windows.Forms.TextBox minPrice;
        private System.Windows.Forms.TextBox maxPrice;
        private System.Windows.Forms.Label zhi;
        private System.Windows.Forms.Button calc;
        private System.Windows.Forms.TextBox calcResult;
        private System.Windows.Forms.Button Clscr;
    }
}
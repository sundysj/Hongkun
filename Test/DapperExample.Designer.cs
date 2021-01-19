namespace Test
{
    partial class DapperExample
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
            this.BookName = new System.Windows.Forms.TextBox();
            this.Sex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.TextBox();
            this.button69 = new System.Windows.Forms.Button();
            this.button68 = new System.Windows.Forms.Button();
            this.button67 = new System.Windows.Forms.Button();
            this.button66 = new System.Windows.Forms.Button();
            this.button64 = new System.Windows.Forms.Button();
            this.button63 = new System.Windows.Forms.Button();
            this.button62 = new System.Windows.Forms.Button();
            this.button60 = new System.Windows.Forms.Button();
            this.Result = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BookName
            // 
            this.BookName.Location = new System.Drawing.Point(409, 349);
            this.BookName.Name = "BookName";
            this.BookName.Size = new System.Drawing.Size(188, 21);
            this.BookName.TabIndex = 31;
            // 
            // Sex
            // 
            this.Sex.Location = new System.Drawing.Point(409, 306);
            this.Sex.Name = "Sex";
            this.Sex.Size = new System.Drawing.Size(188, 21);
            this.Sex.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 309);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 29;
            this.label1.Text = "姓名";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(196, 306);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(188, 21);
            this.UserName.TabIndex = 28;
            // 
            // button69
            // 
            this.button69.Location = new System.Drawing.Point(436, 252);
            this.button69.Name = "button69";
            this.button69.Size = new System.Drawing.Size(161, 23);
            this.button69.TabIndex = 27;
            this.button69.Text = "执行存储过程返回DataSet";
            this.button69.UseVisualStyleBackColor = true;
            this.button69.Click += new System.EventHandler(this.button69_Click);
            // 
            // button68
            // 
            this.button68.Location = new System.Drawing.Point(436, 203);
            this.button68.Name = "button68";
            this.button68.Size = new System.Drawing.Size(161, 23);
            this.button68.TabIndex = 26;
            this.button68.Text = "执行存储过程返回实体";
            this.button68.UseVisualStyleBackColor = true;
            this.button68.Click += new System.EventHandler(this.button68_Click);
            // 
            // button67
            // 
            this.button67.Location = new System.Drawing.Point(436, 154);
            this.button67.Name = "button67";
            this.button67.Size = new System.Drawing.Size(161, 23);
            this.button67.TabIndex = 25;
            this.button67.Text = "多表查询返回多个实体";
            this.button67.UseVisualStyleBackColor = true;
            this.button67.Click += new System.EventHandler(this.button67_Click);
            // 
            // button66
            // 
            this.button66.Location = new System.Drawing.Point(436, 107);
            this.button66.Name = "button66";
            this.button66.Size = new System.Drawing.Size(161, 23);
            this.button66.TabIndex = 24;
            this.button66.Text = "多表查询返回实体";
            this.button66.UseVisualStyleBackColor = true;
            this.button66.Click += new System.EventHandler(this.button66_Click);
            // 
            // button64
            // 
            this.button64.Location = new System.Drawing.Point(436, 61);
            this.button64.Name = "button64";
            this.button64.Size = new System.Drawing.Size(161, 23);
            this.button64.TabIndex = 23;
            this.button64.Text = "单表查询返回实体";
            this.button64.UseVisualStyleBackColor = true;
            this.button64.Click += new System.EventHandler(this.button64_Click);
            // 
            // button63
            // 
            this.button63.Location = new System.Drawing.Point(309, 154);
            this.button63.Name = "button63";
            this.button63.Size = new System.Drawing.Size(75, 23);
            this.button63.TabIndex = 22;
            this.button63.Text = "修改数据";
            this.button63.UseVisualStyleBackColor = true;
            this.button63.Click += new System.EventHandler(this.button63_Click);
            // 
            // button62
            // 
            this.button62.Location = new System.Drawing.Point(309, 107);
            this.button62.Name = "button62";
            this.button62.Size = new System.Drawing.Size(75, 23);
            this.button62.TabIndex = 21;
            this.button62.Text = "删除数据";
            this.button62.UseVisualStyleBackColor = true;
            this.button62.Click += new System.EventHandler(this.button62_Click);
            // 
            // button60
            // 
            this.button60.Location = new System.Drawing.Point(309, 61);
            this.button60.Name = "button60";
            this.button60.Size = new System.Drawing.Size(75, 23);
            this.button60.TabIndex = 20;
            this.button60.Text = "添加数据";
            this.button60.UseVisualStyleBackColor = true;
            this.button60.Click += new System.EventHandler(this.button60_Click);
            // 
            // Result
            // 
            this.Result.Location = new System.Drawing.Point(12, 383);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(932, 262);
            this.Result.TabIndex = 32;
            this.Result.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(637, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "添加数据(实体)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(637, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(161, 23);
            this.button2.TabIndex = 34;
            this.button2.Text = "事务操作";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(637, 163);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(161, 23);
            this.button3.TabIndex = 35;
            this.button3.Text = "单表查询返回实体";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // DapperExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 657);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.BookName);
            this.Controls.Add(this.Sex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.button69);
            this.Controls.Add(this.button68);
            this.Controls.Add(this.button67);
            this.Controls.Add(this.button66);
            this.Controls.Add(this.button64);
            this.Controls.Add(this.button63);
            this.Controls.Add(this.button62);
            this.Controls.Add(this.button60);
            this.Name = "DapperExample";
            this.Text = "DapperExample";
            this.Load += new System.EventHandler(this.DapperExample_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox BookName;
        private System.Windows.Forms.TextBox Sex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.Button button69;
        private System.Windows.Forms.Button button68;
        private System.Windows.Forms.Button button67;
        private System.Windows.Forms.Button button66;
        private System.Windows.Forms.Button button64;
        private System.Windows.Forms.Button button63;
        private System.Windows.Forms.Button button62;
        private System.Windows.Forms.Button button60;
        private System.Windows.Forms.RichTextBox Result;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}
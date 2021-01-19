namespace Test
{
    partial class BGYSendPaid
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
            this.label1 = new System.Windows.Forms.Label();
            this.txbURL = new System.Windows.Forms.TextBox();
            this.txbUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbpwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.rtxbSurr = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbcon = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请求路径：";
            // 
            // txbURL
            // 
            this.txbURL.Location = new System.Drawing.Point(101, 13);
            this.txbURL.Name = "txbURL";
            this.txbURL.Size = new System.Drawing.Size(556, 21);
            this.txbURL.TabIndex = 1;
            this.txbURL.Text = "http://10.187.23.136:18081/smart-order-webapp/api/appWorkingOrder/arVerify";
            // 
            // txbUsername
            // 
            this.txbUsername.Location = new System.Drawing.Point(101, 55);
            this.txbUsername.Name = "txbUsername";
            this.txbUsername.Size = new System.Drawing.Size(382, 21);
            this.txbUsername.TabIndex = 3;
            this.txbUsername.Text = "13800138000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名：";
            // 
            // txbpwd
            // 
            this.txbpwd.Location = new System.Drawing.Point(101, 102);
            this.txbpwd.Name = "txbpwd";
            this.txbpwd.Size = new System.Drawing.Size(382, 21);
            this.txbpwd.TabIndex = 5;
            this.txbpwd.Text = "bgy123";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(687, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(687, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = " 结束";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // rtxbSurr
            // 
            this.rtxbSurr.Location = new System.Drawing.Point(12, 212);
            this.rtxbSurr.Name = "rtxbSurr";
            this.rtxbSurr.Size = new System.Drawing.Size(571, 267);
            this.rtxbSurr.TabIndex = 8;
            this.rtxbSurr.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "连接字符串：";
            // 
            // txbcon
            // 
            this.txbcon.Location = new System.Drawing.Point(101, 148);
            this.txbcon.Name = "txbcon";
            this.txbcon.Size = new System.Drawing.Size(741, 21);
            this.txbcon.TabIndex = 10;
            this.txbcon.Text = "Pooling=false;Data Source=10.10.171.88;Initial Catalog=HM_wygl_new_1853;User ID=L" +
    "FUser;Password=LF123SPoss";
            // 
            // BGYSendPaid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 491);
            this.Controls.Add(this.txbcon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rtxbSurr);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txbpwd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbURL);
            this.Controls.Add(this.label1);
            this.Name = "BGYSendPaid";
            this.Text = "BGYSendPaid";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbURL;
        private System.Windows.Forms.TextBox txbUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbpwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox rtxbSurr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbcon;
    }
}
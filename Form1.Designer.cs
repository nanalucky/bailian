namespace bailian
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.richTextBoxStatus = new System.Windows.Forms.RichTextBox();
            this.dataGridViewInfo = new System.Windows.Forms.DataGridView();
            this.Telephone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SignIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Post = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MyNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelForum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxStatus
            // 
            this.richTextBoxStatus.Location = new System.Drawing.Point(26, 777);
            this.richTextBoxStatus.Name = "richTextBoxStatus";
            this.richTextBoxStatus.Size = new System.Drawing.Size(1204, 76);
            this.richTextBoxStatus.TabIndex = 41;
            this.richTextBoxStatus.Text = "";
            // 
            // dataGridViewInfo
            // 
            this.dataGridViewInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Telephone,
            this.Login,
            this.SignIn,
            this.Post,
            this.MyNote,
            this.DelForum});
            this.dataGridViewInfo.Location = new System.Drawing.Point(27, 219);
            this.dataGridViewInfo.Name = "dataGridViewInfo";
            this.dataGridViewInfo.RowTemplate.Height = 30;
            this.dataGridViewInfo.Size = new System.Drawing.Size(1204, 527);
            this.dataGridViewInfo.TabIndex = 40;
            // 
            // Telephone
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Telephone.DefaultCellStyle = dataGridViewCellStyle7;
            this.Telephone.HeaderText = "Telephone";
            this.Telephone.Name = "Telephone";
            this.Telephone.ReadOnly = true;
            // 
            // Login
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Login.DefaultCellStyle = dataGridViewCellStyle8;
            this.Login.HeaderText = "登录";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            this.Login.Width = 120;
            // 
            // SignIn
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SignIn.DefaultCellStyle = dataGridViewCellStyle9;
            this.SignIn.HeaderText = "签到";
            this.SignIn.Name = "SignIn";
            this.SignIn.ReadOnly = true;
            this.SignIn.Width = 120;
            // 
            // Post
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Post.DefaultCellStyle = dataGridViewCellStyle10;
            this.Post.HeaderText = "发帖";
            this.Post.Name = "Post";
            this.Post.ReadOnly = true;
            this.Post.Width = 120;
            // 
            // MyNote
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.MyNote.DefaultCellStyle = dataGridViewCellStyle11;
            this.MyNote.HeaderText = "获取帖子列表";
            this.MyNote.Name = "MyNote";
            this.MyNote.ReadOnly = true;
            this.MyNote.Width = 120;
            // 
            // DelForum
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DelForum.DefaultCellStyle = dataGridViewCellStyle12;
            this.DelForum.HeaderText = "删帖";
            this.DelForum.Name = "DelForum";
            this.DelForum.ReadOnly = true;
            this.DelForum.Width = 120;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 62);
            this.button1.TabIndex = 39;
            this.button1.Text = "运行";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1256, 890);
            this.Controls.Add(this.richTextBoxStatus);
            this.Controls.Add(this.dataGridViewInfo);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxStatus;
        private System.Windows.Forms.DataGridView dataGridViewInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telephone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Post;
        private System.Windows.Forms.DataGridViewTextBoxColumn MyNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn DelForum;
        private System.Windows.Forms.Button button1;
    }
}


namespace ProjectClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.textBox_enterSweet = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.button_sendSweet = new System.Windows.Forms.Button();
            this.button_requestAllSweets = new System.Windows.Forms.Button();
            this.button_sendUsername = new System.Windows.Forms.Button();
            this.richTextBoxFeed = new System.Windows.Forms.RichTextBox();
            this.button_reqUsername = new System.Windows.Forms.Button();
            this.textBoxFollowUser = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonFollowedUsersTweet = new System.Windows.Forms.Button();
            this.buttonFollow = new System.Windows.Forms.Button();
            this.button_block = new System.Windows.Forms.Button();
            this.button_FollowersReq = new System.Windows.Forms.Button();
            this.button_FollowedUsers = new System.Windows.Forms.Button();
            this.button_DeleteSweet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 15F);
            this.label1.Location = new System.Drawing.Point(128, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 15F);
            this.label2.Location = new System.Drawing.Point(110, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 15F);
            this.label3.Location = new System.Drawing.Point(56, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(23, 36);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(433, 270);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox_IP.Location = new System.Drawing.Point(183, 17);
            this.textBox_IP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(247, 38);
            this.textBox_IP.TabIndex = 5;
            // 
            // textBox_port
            // 
            this.textBox_port.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox_port.Location = new System.Drawing.Point(183, 59);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(247, 38);
            this.textBox_port.TabIndex = 6;
            // 
            // textBox_username
            // 
            this.textBox_username.Enabled = false;
            this.textBox_username.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox_username.Location = new System.Drawing.Point(183, 230);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(247, 38);
            this.textBox_username.TabIndex = 7;
            // 
            // textBox_enterSweet
            // 
            this.textBox_enterSweet.Enabled = false;
            this.textBox_enterSweet.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox_enterSweet.Location = new System.Drawing.Point(15, 49);
            this.textBox_enterSweet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_enterSweet.Name = "textBox_enterSweet";
            this.textBox_enterSweet.Size = new System.Drawing.Size(205, 38);
            this.textBox_enterSweet.TabIndex = 8;
            // 
            // button_connect
            // 
            this.button_connect.BackColor = System.Drawing.SystemColors.Control;
            this.button_connect.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.button_connect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_connect.Location = new System.Drawing.Point(183, 105);
            this.button_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(247, 41);
            this.button_connect.TabIndex = 9;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = false;
            this.button_connect.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_disconnect
            // 
            this.button_disconnect.BackColor = System.Drawing.SystemColors.Control;
            this.button_disconnect.Enabled = false;
            this.button_disconnect.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.button_disconnect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_disconnect.Location = new System.Drawing.Point(183, 150);
            this.button_disconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(247, 43);
            this.button_disconnect.TabIndex = 10;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = false;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // button_sendSweet
            // 
            this.button_sendSweet.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button_sendSweet.Enabled = false;
            this.button_sendSweet.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.button_sendSweet.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_sendSweet.Location = new System.Drawing.Point(15, 102);
            this.button_sendSweet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_sendSweet.Name = "button_sendSweet";
            this.button_sendSweet.Size = new System.Drawing.Size(95, 49);
            this.button_sendSweet.TabIndex = 11;
            this.button_sendSweet.Text = "Send Sweet";
            this.button_sendSweet.UseVisualStyleBackColor = false;
            this.button_sendSweet.Click += new System.EventHandler(this.button_sendSweet_Click);
            // 
            // button_requestAllSweets
            // 
            this.button_requestAllSweets.Enabled = false;
            this.button_requestAllSweets.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.button_requestAllSweets.Location = new System.Drawing.Point(11, 92);
            this.button_requestAllSweets.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_requestAllSweets.Name = "button_requestAllSweets";
            this.button_requestAllSweets.Size = new System.Drawing.Size(175, 49);
            this.button_requestAllSweets.TabIndex = 12;
            this.button_requestAllSweets.Text = "Request All Sweets";
            this.button_requestAllSweets.UseVisualStyleBackColor = true;
            this.button_requestAllSweets.Click += new System.EventHandler(this.button_requestAllSweets_Click);
            // 
            // button_sendUsername
            // 
            this.button_sendUsername.BackColor = System.Drawing.SystemColors.Control;
            this.button_sendUsername.Enabled = false;
            this.button_sendUsername.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.button_sendUsername.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_sendUsername.Location = new System.Drawing.Point(183, 272);
            this.button_sendUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_sendUsername.Name = "button_sendUsername";
            this.button_sendUsername.Size = new System.Drawing.Size(247, 49);
            this.button_sendUsername.TabIndex = 13;
            this.button_sendUsername.Text = "Send Username";
            this.button_sendUsername.UseVisualStyleBackColor = false;
            this.button_sendUsername.Click += new System.EventHandler(this.button_sendUsername_Click);
            // 
            // richTextBoxFeed
            // 
            this.richTextBoxFeed.Location = new System.Drawing.Point(23, 39);
            this.richTextBoxFeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBoxFeed.Name = "richTextBoxFeed";
            this.richTextBoxFeed.Size = new System.Drawing.Size(433, 270);
            this.richTextBoxFeed.TabIndex = 16;
            this.richTextBoxFeed.Text = "";
            this.richTextBoxFeed.TextChanged += new System.EventHandler(this.richTextBoxFeed_TextChanged);
            // 
            // button_reqUsername
            // 
            this.button_reqUsername.Enabled = false;
            this.button_reqUsername.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.button_reqUsername.Location = new System.Drawing.Point(11, 196);
            this.button_reqUsername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_reqUsername.Name = "button_reqUsername";
            this.button_reqUsername.Size = new System.Drawing.Size(175, 49);
            this.button_reqUsername.TabIndex = 17;
            this.button_reqUsername.Text = "Request Usernames";
            this.button_reqUsername.UseVisualStyleBackColor = true;
            this.button_reqUsername.Click += new System.EventHandler(this.button_reqUsername_Click);
            // 
            // textBoxFollowUser
            // 
            this.textBoxFollowUser.Enabled = false;
            this.textBoxFollowUser.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxFollowUser.Location = new System.Drawing.Point(15, 41);
            this.textBoxFollowUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxFollowUser.Name = "textBoxFollowUser";
            this.textBoxFollowUser.Size = new System.Drawing.Size(205, 38);
            this.textBoxFollowUser.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_IP);
            this.groupBox1.Controls.Add(this.textBox_port);
            this.groupBox1.Controls.Add(this.textBox_username);
            this.groupBox1.Controls.Add(this.button_connect);
            this.groupBox1.Controls.Add(this.button_sendUsername);
            this.groupBox1.Controls.Add(this.button_disconnect);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(437, 337);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // buttonFollowedUsersTweet
            // 
            this.buttonFollowedUsersTweet.Enabled = false;
            this.buttonFollowedUsersTweet.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.buttonFollowedUsersTweet.Location = new System.Drawing.Point(11, 144);
            this.buttonFollowedUsersTweet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonFollowedUsersTweet.Name = "buttonFollowedUsersTweet";
            this.buttonFollowedUsersTweet.Size = new System.Drawing.Size(175, 49);
            this.buttonFollowedUsersTweet.TabIndex = 22;
            this.buttonFollowedUsersTweet.Text = "Followed Users Sweets";
            this.buttonFollowedUsersTweet.UseVisualStyleBackColor = true;
            this.buttonFollowedUsersTweet.Click += new System.EventHandler(this.buttonFollowedUsersTweet_Click);
            // 
            // buttonFollow
            // 
            this.buttonFollow.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buttonFollow.Enabled = false;
            this.buttonFollow.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.buttonFollow.Location = new System.Drawing.Point(15, 92);
            this.buttonFollow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonFollow.Name = "buttonFollow";
            this.buttonFollow.Size = new System.Drawing.Size(99, 49);
            this.buttonFollow.TabIndex = 23;
            this.buttonFollow.Text = "Follow";
            this.buttonFollow.UseVisualStyleBackColor = false;
            this.buttonFollow.Click += new System.EventHandler(this.buttonFollow_Click);
            // 
            // button_block
            // 
            this.button_block.Enabled = false;
            this.button_block.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_block.Location = new System.Drawing.Point(120, 92);
            this.button_block.Name = "button_block";
            this.button_block.Size = new System.Drawing.Size(101, 49);
            this.button_block.TabIndex = 24;
            this.button_block.Text = "Block";
            this.button_block.UseVisualStyleBackColor = true;
            this.button_block.Click += new System.EventHandler(this.button_block_Click);
            // 
            // button_FollowersReq
            // 
            this.button_FollowersReq.Enabled = false;
            this.button_FollowersReq.Location = new System.Drawing.Point(11, 252);
            this.button_FollowersReq.Name = "button_FollowersReq";
            this.button_FollowersReq.Size = new System.Drawing.Size(175, 49);
            this.button_FollowersReq.TabIndex = 25;
            this.button_FollowersReq.Text = "Request Followers";
            this.button_FollowersReq.UseVisualStyleBackColor = true;
            this.button_FollowersReq.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button_FollowedUsers
            // 
            this.button_FollowedUsers.Enabled = false;
            this.button_FollowedUsers.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_FollowedUsers.Location = new System.Drawing.Point(11, 36);
            this.button_FollowedUsers.Name = "button_FollowedUsers";
            this.button_FollowedUsers.Size = new System.Drawing.Size(175, 49);
            this.button_FollowedUsers.TabIndex = 26;
            this.button_FollowedUsers.Text = "Request Followed Users";
            this.button_FollowedUsers.UseVisualStyleBackColor = true;
            this.button_FollowedUsers.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_DeleteSweet
            // 
            this.button_DeleteSweet.Enabled = false;
            this.button_DeleteSweet.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_DeleteSweet.Location = new System.Drawing.Point(120, 103);
            this.button_DeleteSweet.Name = "button_DeleteSweet";
            this.button_DeleteSweet.Size = new System.Drawing.Size(101, 48);
            this.button_DeleteSweet.TabIndex = 27;
            this.button_DeleteSweet.Text = "Delete Sweet";
            this.button_DeleteSweet.UseVisualStyleBackColor = true;
            this.button_DeleteSweet.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_sendSweet);
            this.groupBox2.Controls.Add(this.button_DeleteSweet);
            this.groupBox2.Controls.Add(this.textBox_enterSweet);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(13, 369);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 161);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enter or Delete Sweet";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxFollowUser);
            this.groupBox3.Controls.Add(this.buttonFollow);
            this.groupBox3.Controls.Add(this.button_block);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox3.Location = new System.Drawing.Point(13, 547);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(239, 151);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Follow or Block User";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.richTextBoxFeed);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox4.Location = new System.Drawing.Point(471, 369);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(480, 332);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Returns from the Requests";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.richTextBox1);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox5.Location = new System.Drawing.Point(471, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(480, 337);
            this.groupBox5.TabIndex = 31;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Server and Client Messages";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button_FollowersReq);
            this.groupBox6.Controls.Add(this.button_requestAllSweets);
            this.groupBox6.Controls.Add(this.buttonFollowedUsersTweet);
            this.groupBox6.Controls.Add(this.button_reqUsername);
            this.groupBox6.Controls.Add(this.button_FollowedUsers);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox6.Location = new System.Drawing.Point(257, 371);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(192, 329);
            this.groupBox6.TabIndex = 32;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Buttons";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 713);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.TextBox textBox_enterSweet;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Button button_sendSweet;
        private System.Windows.Forms.Button button_requestAllSweets;
        private System.Windows.Forms.Button button_sendUsername;
        private System.Windows.Forms.RichTextBox richTextBoxFeed;
        private System.Windows.Forms.Button button_reqUsername;
        private System.Windows.Forms.TextBox textBoxFollowUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonFollowedUsersTweet;
        private System.Windows.Forms.Button buttonFollow;
        private System.Windows.Forms.Button button_block;
        private System.Windows.Forms.Button button_FollowersReq;
        private System.Windows.Forms.Button button_FollowedUsers;
        private System.Windows.Forms.Button button_DeleteSweet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
    }
}


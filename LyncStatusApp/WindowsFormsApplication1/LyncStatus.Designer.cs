namespace WindowsFormsApplication1
{
    partial class LyncStatus
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
            this.components = new System.ComponentModel.Container();
            this.bExit = new System.Windows.Forms.Button();
            this.lComPorts = new System.Windows.Forms.ListBox();
            this.bConnect = new System.Windows.Forms.Button();
            this.bDisconnect = new System.Windows.Forms.Button();
            this.gLinkInfo = new System.Windows.Forms.GroupBox();
            this.cDebugWnd = new System.Windows.Forms.CheckBox();
            this.rOffline = new System.Windows.Forms.RadioButton();
            this.rDND = new System.Windows.Forms.RadioButton();
            this.rBusy = new System.Windows.Forms.RadioButton();
            this.rAway = new System.Windows.Forms.RadioButton();
            this.rAvailable = new System.Windows.Forms.RadioButton();
            this.cDebugMode = new System.Windows.Forms.CheckBox();
            this.bScan = new System.Windows.Forms.Button();
            this.tComCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.tSendCommands = new System.Windows.Forms.Timer(this.components);
            this.gLinkInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // bExit
            // 
            this.bExit.Location = new System.Drawing.Point(385, 163);
            this.bExit.Name = "bExit";
            this.bExit.Size = new System.Drawing.Size(75, 23);
            this.bExit.TabIndex = 0;
            this.bExit.Text = "&Exit";
            this.bExit.UseVisualStyleBackColor = true;
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // lComPorts
            // 
            this.lComPorts.FormattingEnabled = true;
            this.lComPorts.Location = new System.Drawing.Point(13, 13);
            this.lComPorts.Name = "lComPorts";
            this.lComPorts.Size = new System.Drawing.Size(120, 173);
            this.lComPorts.TabIndex = 1;
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(140, 163);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(75, 23);
            this.bConnect.TabIndex = 2;
            this.bConnect.Text = "&Connect";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // bDisconnect
            // 
            this.bDisconnect.Location = new System.Drawing.Point(222, 163);
            this.bDisconnect.Name = "bDisconnect";
            this.bDisconnect.Size = new System.Drawing.Size(75, 23);
            this.bDisconnect.TabIndex = 3;
            this.bDisconnect.Text = "&Disconnect";
            this.bDisconnect.UseVisualStyleBackColor = true;
            this.bDisconnect.Click += new System.EventHandler(this.bDisconnect_Click);
            // 
            // gLinkInfo
            // 
            this.gLinkInfo.Controls.Add(this.cDebugWnd);
            this.gLinkInfo.Controls.Add(this.rOffline);
            this.gLinkInfo.Controls.Add(this.rDND);
            this.gLinkInfo.Controls.Add(this.rBusy);
            this.gLinkInfo.Controls.Add(this.rAway);
            this.gLinkInfo.Controls.Add(this.rAvailable);
            this.gLinkInfo.Controls.Add(this.cDebugMode);
            this.gLinkInfo.Location = new System.Drawing.Point(140, 13);
            this.gLinkInfo.Name = "gLinkInfo";
            this.gLinkInfo.Size = new System.Drawing.Size(320, 144);
            this.gLinkInfo.TabIndex = 4;
            this.gLinkInfo.TabStop = false;
            this.gLinkInfo.Text = "LinkInfo";
            // 
            // cDebugWnd
            // 
            this.cDebugWnd.AutoSize = true;
            this.cDebugWnd.Location = new System.Drawing.Point(7, 98);
            this.cDebugWnd.Name = "cDebugWnd";
            this.cDebugWnd.Size = new System.Drawing.Size(99, 17);
            this.cDebugWnd.TabIndex = 6;
            this.cDebugWnd.Text = "Debug Conso&le";
            this.cDebugWnd.UseVisualStyleBackColor = true;
            // 
            // rOffline
            // 
            this.rOffline.AutoSize = true;
            this.rOffline.Location = new System.Drawing.Point(229, 116);
            this.rOffline.Name = "rOffline";
            this.rOffline.Size = new System.Drawing.Size(55, 17);
            this.rOffline.TabIndex = 5;
            this.rOffline.Text = "&Offline";
            this.rOffline.UseVisualStyleBackColor = true;
            // 
            // rDND
            // 
            this.rDND.AutoSize = true;
            this.rDND.Location = new System.Drawing.Point(229, 92);
            this.rDND.Name = "rDND";
            this.rDND.Size = new System.Drawing.Size(49, 17);
            this.rDND.TabIndex = 4;
            this.rDND.Text = "D&ND";
            this.rDND.UseVisualStyleBackColor = true;
            // 
            // rBusy
            // 
            this.rBusy.AutoSize = true;
            this.rBusy.Location = new System.Drawing.Point(229, 68);
            this.rBusy.Name = "rBusy";
            this.rBusy.Size = new System.Drawing.Size(48, 17);
            this.rBusy.TabIndex = 3;
            this.rBusy.Text = "Bu&sy";
            this.rBusy.UseVisualStyleBackColor = true;
            // 
            // rAway
            // 
            this.rAway.AutoSize = true;
            this.rAway.Location = new System.Drawing.Point(229, 44);
            this.rAway.Name = "rAway";
            this.rAway.Size = new System.Drawing.Size(51, 17);
            this.rAway.TabIndex = 2;
            this.rAway.Text = "A&way";
            this.rAway.UseVisualStyleBackColor = true;
            // 
            // rAvailable
            // 
            this.rAvailable.AutoSize = true;
            this.rAvailable.Checked = true;
            this.rAvailable.Location = new System.Drawing.Point(229, 20);
            this.rAvailable.Name = "rAvailable";
            this.rAvailable.Size = new System.Drawing.Size(68, 17);
            this.rAvailable.TabIndex = 1;
            this.rAvailable.TabStop = true;
            this.rAvailable.Text = "&Available";
            this.rAvailable.UseVisualStyleBackColor = true;
            // 
            // cDebugMode
            // 
            this.cDebugMode.AutoSize = true;
            this.cDebugMode.Location = new System.Drawing.Point(7, 121);
            this.cDebugMode.Name = "cDebugMode";
            this.cDebugMode.Size = new System.Drawing.Size(93, 17);
            this.cDebugMode.TabIndex = 0;
            this.cDebugMode.Text = "De&bug Modes";
            this.cDebugMode.UseVisualStyleBackColor = true;
            this.cDebugMode.CheckedChanged += new System.EventHandler(this.cDebug_CheckedChanged);
            // 
            // bScan
            // 
            this.bScan.Location = new System.Drawing.Point(304, 164);
            this.bScan.Name = "bScan";
            this.bScan.Size = new System.Drawing.Size(75, 23);
            this.bScan.TabIndex = 5;
            this.bScan.Text = "Scan&!";
            this.bScan.UseVisualStyleBackColor = true;
            this.bScan.Click += new System.EventHandler(this.bScan_Click);
            // 
            // tComCheckTimer
            // 
            this.tComCheckTimer.Interval = 1000;
            this.tComCheckTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tSendCommands
            // 
            this.tSendCommands.Interval = 1000;
            this.tSendCommands.Tick += new System.EventHandler(this.tSendCommands_Tick);
            // 
            // LyncStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 198);
            this.Controls.Add(this.bScan);
            this.Controls.Add(this.gLinkInfo);
            this.Controls.Add(this.bDisconnect);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.lComPorts);
            this.Controls.Add(this.bExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "LyncStatus";
            this.Text = "LyncStatus";
            this.Load += new System.EventHandler(this.LyncStatus_Load);
            this.gLinkInfo.ResumeLayout(false);
            this.gLinkInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bExit;
        private System.Windows.Forms.ListBox lComPorts;
        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.Button bDisconnect;
        private System.Windows.Forms.GroupBox gLinkInfo;
        private System.Windows.Forms.RadioButton rOffline;
        private System.Windows.Forms.RadioButton rDND;
        private System.Windows.Forms.RadioButton rBusy;
        private System.Windows.Forms.RadioButton rAway;
        private System.Windows.Forms.RadioButton rAvailable;
        private System.Windows.Forms.CheckBox cDebugMode;
        private System.Windows.Forms.Button bScan;
        private System.Windows.Forms.Timer tComCheckTimer;
        private System.Windows.Forms.CheckBox cDebugWnd;
        private System.Windows.Forms.Timer tSendCommands;
    }
}


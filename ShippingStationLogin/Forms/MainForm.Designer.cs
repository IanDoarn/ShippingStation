namespace ShippingStationLogin.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.employeeComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.stationComboBox = new System.Windows.Forms.ComboBox();
            this.sessionKeyTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clockInButton = new System.Windows.Forms.Button();
            this.clockOutButton = new System.Windows.Forms.Button();
            this.statusButton = new System.Windows.Forms.Button();
            this.mainFormToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Employee";
            // 
            // employeeComboBox
            // 
            this.employeeComboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.employeeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.employeeComboBox.Location = new System.Drawing.Point(70, 12);
            this.employeeComboBox.Name = "employeeComboBox";
            this.employeeComboBox.Size = new System.Drawing.Size(169, 21);
            this.employeeComboBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Station";
            // 
            // stationComboBox
            // 
            this.stationComboBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stationComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.stationComboBox.Location = new System.Drawing.Point(70, 39);
            this.stationComboBox.Name = "stationComboBox";
            this.stationComboBox.Size = new System.Drawing.Size(169, 21);
            this.stationComboBox.TabIndex = 7;
            // 
            // sessionKeyTextBox
            // 
            this.sessionKeyTextBox.Location = new System.Drawing.Point(70, 66);
            this.sessionKeyTextBox.Name = "sessionKeyTextBox";
            this.sessionKeyTextBox.ShortcutsEnabled = false;
            this.sessionKeyTextBox.Size = new System.Drawing.Size(169, 20);
            this.sessionKeyTextBox.TabIndex = 8;
            this.mainFormToolTip.SetToolTip(this.sessionKeyTextBox, "Pass key must be a 4 digit number");
            this.sessionKeyTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sessionKeyTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Pass Key";
            // 
            // clockInButton
            // 
            this.clockInButton.Location = new System.Drawing.Point(14, 104);
            this.clockInButton.Name = "clockInButton";
            this.clockInButton.Size = new System.Drawing.Size(225, 23);
            this.clockInButton.TabIndex = 10;
            this.clockInButton.Text = "Clock In";
            this.mainFormToolTip.SetToolTip(this.clockInButton, "Clock into selected station");
            this.clockInButton.UseVisualStyleBackColor = true;
            this.clockInButton.Click += new System.EventHandler(this.clockInButton_Click);
            // 
            // clockOutButton
            // 
            this.clockOutButton.Location = new System.Drawing.Point(14, 133);
            this.clockOutButton.Name = "clockOutButton";
            this.clockOutButton.Size = new System.Drawing.Size(225, 23);
            this.clockOutButton.TabIndex = 11;
            this.clockOutButton.Text = "Clock Out";
            this.mainFormToolTip.SetToolTip(this.clockOutButton, "Clock out of selected station");
            this.clockOutButton.UseVisualStyleBackColor = true;
            this.clockOutButton.Click += new System.EventHandler(this.clockOutButton_Click);
            // 
            // statusButton
            // 
            this.statusButton.Location = new System.Drawing.Point(14, 163);
            this.statusButton.Name = "statusButton";
            this.statusButton.Size = new System.Drawing.Size(225, 23);
            this.statusButton.TabIndex = 12;
            this.statusButton.Text = "Status";
            this.mainFormToolTip.SetToolTip(this.statusButton, "Check the status of the current selected Employee");
            this.statusButton.UseVisualStyleBackColor = true;
            this.statusButton.Click += new System.EventHandler(this.statusButton_Click);
            // 
            // mainFormToolTip
            // 
            this.mainFormToolTip.IsBalloon = true;
            this.mainFormToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 195);
            this.Controls.Add(this.statusButton);
            this.Controls.Add(this.clockOutButton);
            this.Controls.Add(this.clockInButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sessionKeyTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.employeeComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.stationComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shipping Station Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox employeeComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox stationComboBox;
        private System.Windows.Forms.TextBox sessionKeyTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button clockInButton;
        private System.Windows.Forms.Button clockOutButton;
        private System.Windows.Forms.Button statusButton;
        private System.Windows.Forms.ToolTip mainFormToolTip;
    }
}
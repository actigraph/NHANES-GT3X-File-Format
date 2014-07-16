namespace GT3X.Parsing.Examples
{
    partial class ParsingForm
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
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.textBoxFilename = new System.Windows.Forms.TextBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonActivity = new System.Windows.Forms.RadioButton();
            this.radioButtonLux = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonJSON = new System.Windows.Forms.RadioButton();
            this.radioButtonCSV = new System.Windows.Forms.RadioButton();
            this.checkBoxTimestamps = new System.Windows.Forms.CheckBox();
            this.panelOptions = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.AutoSize = true;
            this.buttonOpenFile.Location = new System.Drawing.Point(12, 12);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(102, 23);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "Open GT3X File...";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            // 
            // textBoxFilename
            // 
            this.textBoxFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilename.Location = new System.Drawing.Point(120, 14);
            this.textBoxFilename.Name = "textBoxFilename";
            this.textBoxFilename.ReadOnly = true;
            this.textBoxFilename.Size = new System.Drawing.Size(166, 20);
            this.textBoxFilename.TabIndex = 1;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.AutoSize = true;
            this.buttonExport.Location = new System.Drawing.Point(196, 124);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 2;
            this.buttonExport.Text = "Export...";
            this.buttonExport.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Data To Export:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonLux);
            this.panel1.Controls.Add(this.radioButtonActivity);
            this.panel1.Location = new System.Drawing.Point(124, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 29);
            this.panel1.TabIndex = 4;
            // 
            // radioButtonActivity
            // 
            this.radioButtonActivity.AutoSize = true;
            this.radioButtonActivity.Checked = true;
            this.radioButtonActivity.Location = new System.Drawing.Point(3, 7);
            this.radioButtonActivity.Name = "radioButtonActivity";
            this.radioButtonActivity.Size = new System.Drawing.Size(59, 17);
            this.radioButtonActivity.TabIndex = 0;
            this.radioButtonActivity.TabStop = true;
            this.radioButtonActivity.Text = "Activity";
            this.radioButtonActivity.UseVisualStyleBackColor = true;
            // 
            // radioButtonLux
            // 
            this.radioButtonLux.AutoSize = true;
            this.radioButtonLux.Location = new System.Drawing.Point(68, 7);
            this.radioButtonLux.Name = "radioButtonLux";
            this.radioButtonLux.Size = new System.Drawing.Size(42, 17);
            this.radioButtonLux.TabIndex = 1;
            this.radioButtonLux.Text = "Lux";
            this.radioButtonLux.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Export Format:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButtonJSON);
            this.panel2.Controls.Add(this.radioButtonCSV);
            this.panel2.Location = new System.Drawing.Point(124, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(143, 29);
            this.panel2.TabIndex = 5;
            // 
            // radioButtonJSON
            // 
            this.radioButtonJSON.AutoSize = true;
            this.radioButtonJSON.Location = new System.Drawing.Point(68, 5);
            this.radioButtonJSON.Name = "radioButtonJSON";
            this.radioButtonJSON.Size = new System.Drawing.Size(53, 17);
            this.radioButtonJSON.TabIndex = 1;
            this.radioButtonJSON.Text = "JSON";
            this.radioButtonJSON.UseVisualStyleBackColor = true;
            // 
            // radioButtonCSV
            // 
            this.radioButtonCSV.AutoSize = true;
            this.radioButtonCSV.Checked = true;
            this.radioButtonCSV.Location = new System.Drawing.Point(3, 5);
            this.radioButtonCSV.Name = "radioButtonCSV";
            this.radioButtonCSV.Size = new System.Drawing.Size(46, 17);
            this.radioButtonCSV.TabIndex = 0;
            this.radioButtonCSV.TabStop = true;
            this.radioButtonCSV.Text = "CSV";
            this.radioButtonCSV.UseVisualStyleBackColor = true;
            // 
            // checkBoxTimestamps
            // 
            this.checkBoxTimestamps.AutoSize = true;
            this.checkBoxTimestamps.Checked = true;
            this.checkBoxTimestamps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTimestamps.Location = new System.Drawing.Point(127, 94);
            this.checkBoxTimestamps.Name = "checkBoxTimestamps";
            this.checkBoxTimestamps.Size = new System.Drawing.Size(104, 17);
            this.checkBoxTimestamps.TabIndex = 6;
            this.checkBoxTimestamps.Text = "Add Timestamps";
            this.checkBoxTimestamps.UseVisualStyleBackColor = true;
            // 
            // panelOptions
            // 
            this.panelOptions.Controls.Add(this.buttonExport);
            this.panelOptions.Controls.Add(this.checkBoxTimestamps);
            this.panelOptions.Controls.Add(this.label1);
            this.panelOptions.Controls.Add(this.panel2);
            this.panelOptions.Controls.Add(this.panel1);
            this.panelOptions.Controls.Add(this.label2);
            this.panelOptions.Enabled = false;
            this.panelOptions.Location = new System.Drawing.Point(12, 50);
            this.panelOptions.Name = "panelOptions";
            this.panelOptions.Size = new System.Drawing.Size(274, 150);
            this.panelOptions.TabIndex = 7;
            // 
            // ParsingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 212);
            this.Controls.Add(this.panelOptions);
            this.Controls.Add(this.textBoxFilename);
            this.Controls.Add(this.buttonOpenFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ParsingForm";
            this.Text = "GT3X File Parsing Example";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelOptions.ResumeLayout(false);
            this.panelOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.TextBox textBoxFilename;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonLux;
        private System.Windows.Forms.RadioButton radioButtonActivity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonJSON;
        private System.Windows.Forms.RadioButton radioButtonCSV;
        private System.Windows.Forms.CheckBox checkBoxTimestamps;
        private System.Windows.Forms.Panel panelOptions;
    }
}


namespace Excelsior
{
    partial class excelsiorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(excelsiorForm));
            sourceFile1TextBox = new TextBox();
            sourceFile2TextBox = new TextBox();
            workFileTextBox = new TextBox();
            sourceFile1BrowseButton = new Button();
            sourceFile2BrowseButton = new Button();
            workFileBrowseButton = new Button();
            openFileDialog1 = new OpenFileDialog();
            compareAndExportButton = new Button();
            label1 = new Label();
            label2 = new Label();
            exportFilteredOnlyButton = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            sourceFile3TextBox = new TextBox();
            sourceFile3BrowseButton = new Button();
            SuspendLayout();
            // 
            // sourceFile1TextBox
            // 
            sourceFile1TextBox.Location = new Point(54, 54);
            sourceFile1TextBox.Name = "sourceFile1TextBox";
            sourceFile1TextBox.Size = new Size(212, 23);
            sourceFile1TextBox.TabIndex = 0;
            // 
            // sourceFile2TextBox
            // 
            sourceFile2TextBox.Location = new Point(54, 83);
            sourceFile2TextBox.Name = "sourceFile2TextBox";
            sourceFile2TextBox.Size = new Size(212, 23);
            sourceFile2TextBox.TabIndex = 1;
            // 
            // workFileTextBox
            // 
            workFileTextBox.Location = new Point(54, 153);
            workFileTextBox.Name = "workFileTextBox";
            workFileTextBox.Size = new Size(212, 23);
            workFileTextBox.TabIndex = 2;
            // 
            // sourceFile1BrowseButton
            // 
            sourceFile1BrowseButton.Location = new Point(278, 54);
            sourceFile1BrowseButton.Name = "sourceFile1BrowseButton";
            sourceFile1BrowseButton.Size = new Size(178, 23);
            sourceFile1BrowseButton.TabIndex = 3;
            sourceFile1BrowseButton.Text = "Load Master File";
            sourceFile1BrowseButton.UseVisualStyleBackColor = true;
            sourceFile1BrowseButton.Click += sourceFile1BrowseButton_Click;
            // 
            // sourceFile2BrowseButton
            // 
            sourceFile2BrowseButton.Location = new Point(278, 83);
            sourceFile2BrowseButton.Name = "sourceFile2BrowseButton";
            sourceFile2BrowseButton.Size = new Size(178, 23);
            sourceFile2BrowseButton.TabIndex = 4;
            sourceFile2BrowseButton.Text = "Load Pending Verification File";
            sourceFile2BrowseButton.UseVisualStyleBackColor = true;
            sourceFile2BrowseButton.Click += sourceFile2BrowseButton_Click;
            // 
            // workFileBrowseButton
            // 
            workFileBrowseButton.Location = new Point(278, 153);
            workFileBrowseButton.Name = "workFileBrowseButton";
            workFileBrowseButton.Size = new Size(178, 23);
            workFileBrowseButton.TabIndex = 5;
            workFileBrowseButton.Text = "Load Current File";
            workFileBrowseButton.UseVisualStyleBackColor = true;
            workFileBrowseButton.Click += workFileBrowseButton_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // compareAndExportButton
            // 
            compareAndExportButton.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            compareAndExportButton.ForeColor = SystemColors.HotTrack;
            compareAndExportButton.Location = new Point(53, 217);
            compareAndExportButton.Name = "compareAndExportButton";
            compareAndExportButton.Size = new Size(213, 29);
            compareAndExportButton.TabIndex = 6;
            compareAndExportButton.Text = "Compare-Export Full File";
            compareAndExportButton.UseVisualStyleBackColor = true;
            compareAndExportButton.Click += compareAndExportButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(85, 9);
            label1.Name = "label1";
            label1.Size = new Size(335, 32);
            label1.TabIndex = 7;
            label1.Text = "Excelsior - Verification Filter";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 6F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(391, 249);
            label2.Name = "label2";
            label2.Size = new Size(65, 9);
            label2.TabIndex = 8;
            label2.Text = "Developed by AR";
            // 
            // exportFilteredOnlyButton
            // 
            exportFilteredOnlyButton.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            exportFilteredOnlyButton.ForeColor = Color.SeaGreen;
            exportFilteredOnlyButton.Location = new Point(278, 217);
            exportFilteredOnlyButton.Name = "exportFilteredOnlyButton";
            exportFilteredOnlyButton.Size = new Size(178, 29);
            exportFilteredOnlyButton.TabIndex = 9;
            exportFilteredOnlyButton.Text = "Export Filtered Data Only";
            exportFilteredOnlyButton.UseVisualStyleBackColor = true;
            exportFilteredOnlyButton.Click += exportFilteredOnlyButton_Click;
            // 
            // sourceFile3TextBox
            // 
            sourceFile3TextBox.Location = new Point(54, 112);
            sourceFile3TextBox.Name = "sourceFile3TextBox";
            sourceFile3TextBox.Size = new Size(212, 23);
            sourceFile3TextBox.TabIndex = 10;
            // 
            // sourceFile3BrowseButton
            // 
            sourceFile3BrowseButton.Location = new Point(278, 112);
            sourceFile3BrowseButton.Name = "sourceFile3BrowseButton";
            sourceFile3BrowseButton.Size = new Size(178, 23);
            sourceFile3BrowseButton.TabIndex = 11;
            sourceFile3BrowseButton.Text = "Load First Term File";
            sourceFile3BrowseButton.UseVisualStyleBackColor = true;
            sourceFile3BrowseButton.Click += sourceFile3BrowseButton_Click;
            // 
            // excelsiorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(515, 267);
            Controls.Add(sourceFile3BrowseButton);
            Controls.Add(sourceFile3TextBox);
            Controls.Add(exportFilteredOnlyButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(compareAndExportButton);
            Controls.Add(workFileBrowseButton);
            Controls.Add(sourceFile2BrowseButton);
            Controls.Add(sourceFile1BrowseButton);
            Controls.Add(workFileTextBox);
            Controls.Add(sourceFile2TextBox);
            Controls.Add(sourceFile1TextBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "excelsiorForm";
            Text = "Excelsior Verification Filter";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox sourceFile1TextBox;
        private TextBox sourceFile2TextBox;
        private TextBox workFileTextBox;
        private Button sourceFile1BrowseButton;
        private Button sourceFile2BrowseButton;
        private Button workFileBrowseButton;
        private OpenFileDialog openFileDialog1;
        private Button compareAndExportButton;
        private Label label1;
        private Label label2;
        private Button exportFilteredOnlyButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private TextBox sourceFile3TextBox;
        private Button sourceFile3BrowseButton;
    }
}
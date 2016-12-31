namespace Graphics
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
            this.components = new System.ComponentModel.Container();
            this.minusExpControl = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.linearControl = new ZedGraph.ZedGraphControl();
            this.expControl = new ZedGraph.ZedGraphControl();
            this.minusLinearControl = new ZedGraph.ZedGraphControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.multiTrendControl = new ZedGraph.ZedGraphControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.spectreFileControl = new ZedGraph.ZedGraphControl();
            this.fileControl = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // minusExpControl
            // 
            this.minusExpControl.Location = new System.Drawing.Point(342, 195);
            this.minusExpControl.Name = "minusExpControl";
            this.minusExpControl.ScrollGrace = 0D;
            this.minusExpControl.ScrollMaxX = 0D;
            this.minusExpControl.ScrollMaxY = 0D;
            this.minusExpControl.ScrollMaxY2 = 0D;
            this.minusExpControl.ScrollMinX = 0D;
            this.minusExpControl.ScrollMinY = 0D;
            this.minusExpControl.ScrollMinY2 = 0D;
            this.minusExpControl.Size = new System.Drawing.Size(338, 208);
            this.minusExpControl.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.7076F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.2924F));
            this.tableLayoutPanel1.Controls.Add(this.linearControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.expControl, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.minusExpControl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.minusLinearControl, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.29064F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.70936F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 406);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // linearControl
            // 
            this.linearControl.Location = new System.Drawing.Point(3, 3);
            this.linearControl.Name = "linearControl";
            this.linearControl.ScrollGrace = 0D;
            this.linearControl.ScrollMaxX = 0D;
            this.linearControl.ScrollMaxY = 0D;
            this.linearControl.ScrollMaxY2 = 0D;
            this.linearControl.ScrollMinX = 0D;
            this.linearControl.ScrollMinY = 0D;
            this.linearControl.ScrollMinY2 = 0D;
            this.linearControl.Size = new System.Drawing.Size(333, 186);
            this.linearControl.TabIndex = 2;
            // 
            // expControl
            // 
            this.expControl.Location = new System.Drawing.Point(3, 195);
            this.expControl.Name = "expControl";
            this.expControl.ScrollGrace = 0D;
            this.expControl.ScrollMaxX = 0D;
            this.expControl.ScrollMaxY = 0D;
            this.expControl.ScrollMaxY2 = 0D;
            this.expControl.ScrollMinX = 0D;
            this.expControl.ScrollMinY = 0D;
            this.expControl.ScrollMinY2 = 0D;
            this.expControl.Size = new System.Drawing.Size(333, 208);
            this.expControl.TabIndex = 3;
            // 
            // minusLinearControl
            // 
            this.minusLinearControl.Location = new System.Drawing.Point(342, 3);
            this.minusLinearControl.Name = "minusLinearControl";
            this.minusLinearControl.ScrollGrace = 0D;
            this.minusLinearControl.ScrollMaxX = 0D;
            this.minusLinearControl.ScrollMaxY = 0D;
            this.minusLinearControl.ScrollMaxY2 = 0D;
            this.minusLinearControl.ScrollMinX = 0D;
            this.minusLinearControl.ScrollMinY = 0D;
            this.minusLinearControl.ScrollMinY2 = 0D;
            this.minusLinearControl.Size = new System.Drawing.Size(338, 186);
            this.minusLinearControl.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(693, 439);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(685, 413);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Тренды";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.multiTrendControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(685, 413);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Мульти тренд";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // multiTrendControl
            // 
            this.multiTrendControl.Location = new System.Drawing.Point(-4, 0);
            this.multiTrendControl.Name = "multiTrendControl";
            this.multiTrendControl.ScrollGrace = 0D;
            this.multiTrendControl.ScrollMaxX = 0D;
            this.multiTrendControl.ScrollMaxY = 0D;
            this.multiTrendControl.ScrollMaxY2 = 0D;
            this.multiTrendControl.ScrollMinX = 0D;
            this.multiTrendControl.ScrollMinY = 0D;
            this.multiTrendControl.ScrollMinY2 = 0D;
            this.multiTrendControl.Size = new System.Drawing.Size(693, 417);
            this.multiTrendControl.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.spectreFileControl);
            this.tabPage3.Controls.Add(this.fileControl);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(685, 413);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Чтение из файла";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // spectreFileControl
            // 
            this.spectreFileControl.Location = new System.Drawing.Point(391, 0);
            this.spectreFileControl.Name = "spectreFileControl";
            this.spectreFileControl.ScrollGrace = 0D;
            this.spectreFileControl.ScrollMaxX = 0D;
            this.spectreFileControl.ScrollMaxY = 0D;
            this.spectreFileControl.ScrollMaxY2 = 0D;
            this.spectreFileControl.ScrollMinX = 0D;
            this.spectreFileControl.ScrollMinY = 0D;
            this.spectreFileControl.ScrollMinY2 = 0D;
            this.spectreFileControl.Size = new System.Drawing.Size(294, 413);
            this.spectreFileControl.TabIndex = 1;
            // 
            // fileControl
            // 
            this.fileControl.Location = new System.Drawing.Point(-4, 0);
            this.fileControl.Name = "fileControl";
            this.fileControl.ScrollGrace = 0D;
            this.fileControl.ScrollMaxX = 0D;
            this.fileControl.ScrollMaxY = 0D;
            this.fileControl.ScrollMaxY2 = 0D;
            this.fileControl.ScrollMinX = 0D;
            this.fileControl.ScrollMinY = 0D;
            this.fileControl.ScrollMinY2 = 0D;
            this.fileControl.Size = new System.Drawing.Size(389, 417);
            this.fileControl.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 440);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl minusExpControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ZedGraph.ZedGraphControl minusLinearControl;
        private ZedGraph.ZedGraphControl linearControl;
        private ZedGraph.ZedGraphControl expControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ZedGraph.ZedGraphControl multiTrendControl;
        private System.Windows.Forms.TabPage tabPage3;
        private ZedGraph.ZedGraphControl fileControl;
        private ZedGraph.ZedGraphControl spectreFileControl;

    }
}
namespace Graphics
{
    partial class Form3
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
            this.HarmGraph = new ZedGraph.ZedGraphControl();
            this.PolyHarmGraph = new ZedGraph.ZedGraphControl();
            this.PolyHarmWithNoiseGraph = new ZedGraph.ZedGraphControl();
            this.CarrierGraph = new ZedGraph.ZedGraphControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Гармонический = new System.Windows.Forms.TabPage();
            this.SP_FMHarmGraph = new ZedGraph.ZedGraphControl();
            this.FMHarmGraph = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SP_FMPolyGraph = new ZedGraph.ZedGraphControl();
            this.SP_PolyGraph = new ZedGraph.ZedGraphControl();
            this.FMPolyGraph = new ZedGraph.ZedGraphControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.FM_PolyNoizeGraph = new ZedGraph.ZedGraphControl();
            this.SpectrePolyNoiseGraph = new ZedGraph.ZedGraphControl();
            this.Spectre_FMPolyNoiseGraph = new ZedGraph.ZedGraphControl();
            this.tabControl1.SuspendLayout();
            this.Гармонический.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // HarmGraph
            // 
            this.HarmGraph.Location = new System.Drawing.Point(6, 6);
            this.HarmGraph.Name = "HarmGraph";
            this.HarmGraph.ScrollGrace = 0D;
            this.HarmGraph.ScrollMaxX = 0D;
            this.HarmGraph.ScrollMaxY = 0D;
            this.HarmGraph.ScrollMaxY2 = 0D;
            this.HarmGraph.ScrollMinX = 0D;
            this.HarmGraph.ScrollMinY = 0D;
            this.HarmGraph.ScrollMinY2 = 0D;
            this.HarmGraph.Size = new System.Drawing.Size(734, 250);
            this.HarmGraph.TabIndex = 0;
            // 
            // PolyHarmGraph
            // 
            this.PolyHarmGraph.Location = new System.Drawing.Point(6, 6);
            this.PolyHarmGraph.Name = "PolyHarmGraph";
            this.PolyHarmGraph.ScrollGrace = 0D;
            this.PolyHarmGraph.ScrollMaxX = 0D;
            this.PolyHarmGraph.ScrollMaxY = 0D;
            this.PolyHarmGraph.ScrollMaxY2 = 0D;
            this.PolyHarmGraph.ScrollMinX = 0D;
            this.PolyHarmGraph.ScrollMinY = 0D;
            this.PolyHarmGraph.ScrollMinY2 = 0D;
            this.PolyHarmGraph.Size = new System.Drawing.Size(690, 259);
            this.PolyHarmGraph.TabIndex = 1;
            // 
            // PolyHarmWithNoiseGraph
            // 
            this.PolyHarmWithNoiseGraph.Location = new System.Drawing.Point(6, 6);
            this.PolyHarmWithNoiseGraph.Name = "PolyHarmWithNoiseGraph";
            this.PolyHarmWithNoiseGraph.ScrollGrace = 0D;
            this.PolyHarmWithNoiseGraph.ScrollMaxX = 0D;
            this.PolyHarmWithNoiseGraph.ScrollMaxY = 0D;
            this.PolyHarmWithNoiseGraph.ScrollMaxY2 = 0D;
            this.PolyHarmWithNoiseGraph.ScrollMinX = 0D;
            this.PolyHarmWithNoiseGraph.ScrollMinY = 0D;
            this.PolyHarmWithNoiseGraph.ScrollMinY2 = 0D;
            this.PolyHarmWithNoiseGraph.Size = new System.Drawing.Size(707, 259);
            this.PolyHarmWithNoiseGraph.TabIndex = 2;
            // 
            // CarrierGraph
            // 
            this.CarrierGraph.Location = new System.Drawing.Point(3, 6);
            this.CarrierGraph.Name = "CarrierGraph";
            this.CarrierGraph.ScrollGrace = 0D;
            this.CarrierGraph.ScrollMaxX = 0D;
            this.CarrierGraph.ScrollMaxY = 0D;
            this.CarrierGraph.ScrollMaxY2 = 0D;
            this.CarrierGraph.ScrollMinX = 0D;
            this.CarrierGraph.ScrollMinY = 0D;
            this.CarrierGraph.ScrollMinY2 = 0D;
            this.CarrierGraph.Size = new System.Drawing.Size(808, 300);
            this.CarrierGraph.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Гармонический);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1278, 688);
            this.tabControl1.TabIndex = 9;
            // 
            // Гармонический
            // 
            this.Гармонический.Controls.Add(this.SP_FMHarmGraph);
            this.Гармонический.Controls.Add(this.FMHarmGraph);
            this.Гармонический.Controls.Add(this.HarmGraph);
            this.Гармонический.Location = new System.Drawing.Point(4, 22);
            this.Гармонический.Name = "Гармонический";
            this.Гармонический.Padding = new System.Windows.Forms.Padding(3);
            this.Гармонический.Size = new System.Drawing.Size(1270, 662);
            this.Гармонический.TabIndex = 0;
            this.Гармонический.Text = "Гармонический";
            this.Гармонический.UseVisualStyleBackColor = true;
            // 
            // SP_FMHarmGraph
            // 
            this.SP_FMHarmGraph.Location = new System.Drawing.Point(747, 4);
            this.SP_FMHarmGraph.Name = "SP_FMHarmGraph";
            this.SP_FMHarmGraph.ScrollGrace = 0D;
            this.SP_FMHarmGraph.ScrollMaxX = 0D;
            this.SP_FMHarmGraph.ScrollMaxY = 0D;
            this.SP_FMHarmGraph.ScrollMaxY2 = 0D;
            this.SP_FMHarmGraph.ScrollMinX = 0D;
            this.SP_FMHarmGraph.ScrollMinY = 0D;
            this.SP_FMHarmGraph.ScrollMinY2 = 0D;
            this.SP_FMHarmGraph.Size = new System.Drawing.Size(517, 252);
            this.SP_FMHarmGraph.TabIndex = 2;
            // 
            // FMHarmGraph
            // 
            this.FMHarmGraph.Location = new System.Drawing.Point(6, 262);
            this.FMHarmGraph.Name = "FMHarmGraph";
            this.FMHarmGraph.ScrollGrace = 0D;
            this.FMHarmGraph.ScrollMaxX = 0D;
            this.FMHarmGraph.ScrollMaxY = 0D;
            this.FMHarmGraph.ScrollMaxY2 = 0D;
            this.FMHarmGraph.ScrollMinX = 0D;
            this.FMHarmGraph.ScrollMinY = 0D;
            this.FMHarmGraph.ScrollMinY2 = 0D;
            this.FMHarmGraph.Size = new System.Drawing.Size(734, 252);
            this.FMHarmGraph.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.SP_FMPolyGraph);
            this.tabPage2.Controls.Add(this.SP_PolyGraph);
            this.tabPage2.Controls.Add(this.FMPolyGraph);
            this.tabPage2.Controls.Add(this.PolyHarmGraph);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1270, 662);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Полигармонический";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SP_FMPolyGraph
            // 
            this.SP_FMPolyGraph.Location = new System.Drawing.Point(703, 272);
            this.SP_FMPolyGraph.Name = "SP_FMPolyGraph";
            this.SP_FMPolyGraph.ScrollGrace = 0D;
            this.SP_FMPolyGraph.ScrollMaxX = 0D;
            this.SP_FMPolyGraph.ScrollMaxY = 0D;
            this.SP_FMPolyGraph.ScrollMaxY2 = 0D;
            this.SP_FMPolyGraph.ScrollMinX = 0D;
            this.SP_FMPolyGraph.ScrollMinY = 0D;
            this.SP_FMPolyGraph.ScrollMinY2 = 0D;
            this.SP_FMPolyGraph.Size = new System.Drawing.Size(561, 238);
            this.SP_FMPolyGraph.TabIndex = 4;
            // 
            // SP_PolyGraph
            // 
            this.SP_PolyGraph.Location = new System.Drawing.Point(703, 6);
            this.SP_PolyGraph.Name = "SP_PolyGraph";
            this.SP_PolyGraph.ScrollGrace = 0D;
            this.SP_PolyGraph.ScrollMaxX = 0D;
            this.SP_PolyGraph.ScrollMaxY = 0D;
            this.SP_PolyGraph.ScrollMaxY2 = 0D;
            this.SP_PolyGraph.ScrollMinX = 0D;
            this.SP_PolyGraph.ScrollMinY = 0D;
            this.SP_PolyGraph.ScrollMinY2 = 0D;
            this.SP_PolyGraph.Size = new System.Drawing.Size(561, 259);
            this.SP_PolyGraph.TabIndex = 3;
            // 
            // FMPolyGraph
            // 
            this.FMPolyGraph.Location = new System.Drawing.Point(7, 272);
            this.FMPolyGraph.Name = "FMPolyGraph";
            this.FMPolyGraph.ScrollGrace = 0D;
            this.FMPolyGraph.ScrollMaxX = 0D;
            this.FMPolyGraph.ScrollMaxY = 0D;
            this.FMPolyGraph.ScrollMaxY2 = 0D;
            this.FMPolyGraph.ScrollMinX = 0D;
            this.FMPolyGraph.ScrollMinY = 0D;
            this.FMPolyGraph.ScrollMinY2 = 0D;
            this.FMPolyGraph.Size = new System.Drawing.Size(689, 238);
            this.FMPolyGraph.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Spectre_FMPolyNoiseGraph);
            this.tabPage1.Controls.Add(this.SpectrePolyNoiseGraph);
            this.tabPage1.Controls.Add(this.FM_PolyNoizeGraph);
            this.tabPage1.Controls.Add(this.PolyHarmWithNoiseGraph);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1270, 662);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Полигармонический с шумом";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.CarrierGraph);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1270, 662);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Несущая";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // FM_PolyNoizeGraph
            // 
            this.FM_PolyNoizeGraph.Location = new System.Drawing.Point(4, 272);
            this.FM_PolyNoizeGraph.Name = "FM_PolyNoizeGraph";
            this.FM_PolyNoizeGraph.ScrollGrace = 0D;
            this.FM_PolyNoizeGraph.ScrollMaxX = 0D;
            this.FM_PolyNoizeGraph.ScrollMaxY = 0D;
            this.FM_PolyNoizeGraph.ScrollMaxY2 = 0D;
            this.FM_PolyNoizeGraph.ScrollMinX = 0D;
            this.FM_PolyNoizeGraph.ScrollMinY = 0D;
            this.FM_PolyNoizeGraph.ScrollMinY2 = 0D;
            this.FM_PolyNoizeGraph.Size = new System.Drawing.Size(709, 239);
            this.FM_PolyNoizeGraph.TabIndex = 3;
            // 
            // SpectrePolyNoiseGraph
            // 
            this.SpectrePolyNoiseGraph.Location = new System.Drawing.Point(720, 7);
            this.SpectrePolyNoiseGraph.Name = "SpectrePolyNoiseGraph";
            this.SpectrePolyNoiseGraph.ScrollGrace = 0D;
            this.SpectrePolyNoiseGraph.ScrollMaxX = 0D;
            this.SpectrePolyNoiseGraph.ScrollMaxY = 0D;
            this.SpectrePolyNoiseGraph.ScrollMaxY2 = 0D;
            this.SpectrePolyNoiseGraph.ScrollMinX = 0D;
            this.SpectrePolyNoiseGraph.ScrollMinY = 0D;
            this.SpectrePolyNoiseGraph.ScrollMinY2 = 0D;
            this.SpectrePolyNoiseGraph.Size = new System.Drawing.Size(544, 258);
            this.SpectrePolyNoiseGraph.TabIndex = 4;
            // 
            // Spectre_FMPolyNoiseGraph
            // 
            this.Spectre_FMPolyNoiseGraph.Location = new System.Drawing.Point(720, 272);
            this.Spectre_FMPolyNoiseGraph.Name = "Spectre_FMPolyNoiseGraph";
            this.Spectre_FMPolyNoiseGraph.ScrollGrace = 0D;
            this.Spectre_FMPolyNoiseGraph.ScrollMaxX = 0D;
            this.Spectre_FMPolyNoiseGraph.ScrollMaxY = 0D;
            this.Spectre_FMPolyNoiseGraph.ScrollMaxY2 = 0D;
            this.Spectre_FMPolyNoiseGraph.ScrollMinX = 0D;
            this.Spectre_FMPolyNoiseGraph.ScrollMinY = 0D;
            this.Spectre_FMPolyNoiseGraph.ScrollMinY2 = 0D;
            this.Spectre_FMPolyNoiseGraph.Size = new System.Drawing.Size(544, 239);
            this.Spectre_FMPolyNoiseGraph.TabIndex = 5;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 547);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.tabControl1.ResumeLayout(false);
            this.Гармонический.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl HarmGraph;
        private ZedGraph.ZedGraphControl PolyHarmGraph;
        private ZedGraph.ZedGraphControl PolyHarmWithNoiseGraph;
        private ZedGraph.ZedGraphControl CarrierGraph;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Гармонический;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private ZedGraph.ZedGraphControl FMHarmGraph;
        private ZedGraph.ZedGraphControl SP_FMHarmGraph;
        private ZedGraph.ZedGraphControl FMPolyGraph;
        private ZedGraph.ZedGraphControl SP_PolyGraph;
        private ZedGraph.ZedGraphControl SP_FMPolyGraph;
        private ZedGraph.ZedGraphControl FM_PolyNoizeGraph;
        private ZedGraph.ZedGraphControl SpectrePolyNoiseGraph;
        private ZedGraph.ZedGraphControl Spectre_FMPolyNoiseGraph;
    }
}
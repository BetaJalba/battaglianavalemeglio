namespace battaglianavale
{
    partial class Form2
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
            dataGridView1 = new DataGridView();
            dataGridView2 = new DataGridView();
            btnPiazza = new Button();
            panel1 = new Panel();
            btnNave4 = new Button();
            btnNave3 = new Button();
            label1 = new Label();
            button3 = new Button();
            button4 = new Button();
            button2 = new Button();
            button1 = new Button();
            btnNave2 = new Button();
            btnNave1 = new Button();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeight = 29;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Location = new Point(173, 33);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(400, 400);
            dataGridView1.TabIndex = 0;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeight = 29;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView2.Location = new Point(637, 233);
            dataGridView2.Margin = new Padding(3, 4, 3, 4);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(200, 200);
            dataGridView2.TabIndex = 1;
            // 
            // btnPiazza
            // 
            btnPiazza.Location = new Point(33, 33);
            btnPiazza.Margin = new Padding(3, 4, 3, 4);
            btnPiazza.Name = "btnPiazza";
            btnPiazza.Size = new Size(91, 35);
            btnPiazza.TabIndex = 2;
            btnPiazza.Text = "Piazza Navi";
            btnPiazza.UseVisualStyleBackColor = true;
            btnPiazza.Click += btnPiazza_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnNave4);
            panel1.Controls.Add(btnNave3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(btnNave2);
            panel1.Controls.Add(btnNave1);
            panel1.Location = new Point(12, 95);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(142, 338);
            panel1.TabIndex = 3;
            // 
            // btnNave4
            // 
            btnNave4.Location = new Point(19, 161);
            btnNave4.Margin = new Padding(3, 4, 3, 4);
            btnNave4.Name = "btnNave4";
            btnNave4.Size = new Size(102, 31);
            btnNave4.TabIndex = 8;
            btnNave4.Text = "1x Nave da 4";
            btnNave4.UseVisualStyleBackColor = true;
            btnNave4.Click += btnNave4_Click;
            // 
            // btnNave3
            // 
            btnNave3.Location = new Point(19, 120);
            btnNave3.Margin = new Padding(3, 4, 3, 4);
            btnNave3.Name = "btnNave3";
            btnNave3.Size = new Size(102, 31);
            btnNave3.TabIndex = 7;
            btnNave3.Text = "2x Navi da 3";
            btnNave3.UseVisualStyleBackColor = true;
            btnNave3.Click += btnNave3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 220);
            label1.Name = "label1";
            label1.Size = new Size(79, 20);
            label1.TabIndex = 6;
            label1.Text = "Rotazione:";
            // 
            // button3
            // 
            button3.Font = new Font("SimSun", 9F);
            button3.Location = new Point(57, 304);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(23, 27);
            button3.TabIndex = 5;
            button3.Text = "▼";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button4.Location = new Point(37, 280);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(23, 27);
            button4.TabIndex = 4;
            button4.Text = "<";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button2.Location = new Point(77, 280);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(23, 27);
            button2.TabIndex = 3;
            button2.Text = ">";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Font = new Font("SimSun", 9F);
            button1.Location = new Point(56, 256);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(23, 27);
            button1.TabIndex = 2;
            button1.Text = "▲";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnNave2
            // 
            btnNave2.Location = new Point(19, 79);
            btnNave2.Margin = new Padding(3, 4, 3, 4);
            btnNave2.Name = "btnNave2";
            btnNave2.Size = new Size(102, 31);
            btnNave2.TabIndex = 1;
            btnNave2.Text = "2x Navi da 2";
            btnNave2.UseVisualStyleBackColor = true;
            btnNave2.Click += btnNave2_Click;
            // 
            // btnNave1
            // 
            btnNave1.Location = new Point(19, 37);
            btnNave1.Margin = new Padding(3, 4, 3, 4);
            btnNave1.Name = "btnNave1";
            btnNave1.Size = new Size(102, 31);
            btnNave1.TabIndex = 0;
            btnNave1.Text = "1x Nave da 1";
            btnNave1.UseVisualStyleBackColor = true;
            btnNave1.Click += btnNave1_Click;
            // 
            // label2
            // 
            label2.Location = new Point(637, 33);
            label2.Name = "label2";
            label2.Size = new Size(200, 181);
            label2.TabIndex = 4;
            label2.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 471);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(btnPiazza);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Button btnPiazza;
        private Panel panel1;
        private Button button3;
        private Button button4;
        private Button button2;
        private Button button1;
        private Button btnNave2;
        private Button btnNave1;
        private Button btnNave4;
        private Button btnNave3;
        private Label label1;
        private Label label2;
    }
}

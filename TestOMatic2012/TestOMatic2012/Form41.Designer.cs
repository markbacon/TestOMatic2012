namespace TestOMatic2012 {
	partial class Form41 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.SuspendLayout();
			// 
			// Form41
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(621, 520);
			this.Name = "Form41";
			this.Text = "Form41";
			this.ResumeLayout(false);
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox1.Location = new System.Drawing.Point(144, 64);
			this.textBox1.Size = new System.Drawing.Size(128, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			this.checkedListBox1.Location = new System.Drawing.Point(16, 64);
			this.checkedListBox1.Size = new System.Drawing.Size(120, 184);
			this.checkedListBox1.TabIndex = 0;
			this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
			this.listBox1.Location = new System.Drawing.Point(408, 64);
			this.listBox1.Size = new System.Drawing.Size(128, 186);
			this.listBox1.TabIndex = 3;
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(144, 104);
			this.button1.Size = new System.Drawing.Size(104, 32);
			this.button1.TabIndex = 2;
			this.button1.Text = "Add Fruit";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(288, 64);
			this.button2.Size = new System.Drawing.Size(104, 32);
			this.button2.TabIndex = 2;
			this.button2.Text = "Show Order";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(288, 104);
			this.button3.Size = new System.Drawing.Size(104, 32);
			this.button3.TabIndex = 2;
			this.button3.Text = "Save Order";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			this.ClientSize = new System.Drawing.Size(563, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {this.listBox1,
                                                        this.button3,
                                                        this.button2,
                                                        this.button1,
                                                        this.textBox1,
                                                        this.checkedListBox1});
			this.Text = "Fruit Order";

		}

		#endregion
	}
}
using System.Configuration;

namespace FlightSimulatorApp
{
    partial class NetSettings
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
            this.actual_IP_A = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.new_PORT_txt = new System.Windows.Forms.TextBox();
            this.new_IP_txt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.actual_IP_value = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.actual_PORT_value = new System.Windows.Forms.Label();
            this.change_net_settings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // actual_IP_A
            // 
            this.actual_IP_A.AutoSize = true;
            this.actual_IP_A.Location = new System.Drawing.Point(24, 23);
            this.actual_IP_A.Name = "actual_IP_A";
            this.actual_IP_A.Size = new System.Drawing.Size(72, 13);
            this.actual_IP_A.TabIndex = 0;
            this.actual_IP_A.Text = "Actual IP address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "PORT";
            // 
            // new_PORT_txt
            // 
            this.new_PORT_txt.Location = new System.Drawing.Point(172, 161);
            this.new_PORT_txt.Name = "new_PORT_txt";
            this.new_PORT_txt.Size = new System.Drawing.Size(129, 20);
            this.new_PORT_txt.TabIndex = 2;
            // 
            // new_IP_txt
            // 
            this.new_IP_txt.Location = new System.Drawing.Point(172, 64);
            this.new_IP_txt.Name = "new_IP_txt";
            this.new_IP_txt.Size = new System.Drawing.Size(129, 20);
            this.new_IP_txt.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Actual Value";
            // 
            // actual_IP_value
            // 
            this.actual_IP_value.AutoSize = true;
            this.actual_IP_value.Location = new System.Drawing.Point(24, 64);
            this.actual_IP_value.Name = "actual_IP_value";
            this.actual_IP_value.Size = new System.Drawing.Size(11, 13);
            this.actual_IP_value.TabIndex = 5;
            this.actual_IP_value.Text = ConfigurationManager.AppSettings.Get("IP_address");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Actual Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(169, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "New Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "New Value";
            // 
            // actual_PORT_value
            // 
            this.actual_PORT_value.AutoSize = true;
            this.actual_PORT_value.Location = new System.Drawing.Point(24, 168);
            this.actual_PORT_value.Size = new System.Drawing.Size(11, 13);
            this.actual_PORT_value.TabIndex = 9;
            this.actual_PORT_value.Text = ConfigurationManager.AppSettings.Get("PORT");
            // 
            // change_net_settings
            // 
            this.change_net_settings.Location = new System.Drawing.Point(94, 212);
            this.change_net_settings.Name = "change_net_settings";
            this.change_net_settings.Size = new System.Drawing.Size(121, 32);
            this.change_net_settings.TabIndex = 10;
            this.change_net_settings.Text = "Change";
            this.change_net_settings.UseVisualStyleBackColor = true;
            this.change_net_settings.Click += new System.EventHandler(this.change_net_settings_Click);
            // 
            // NetSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 266);
            this.Controls.Add(this.change_net_settings);
            this.Controls.Add(this.actual_PORT_value);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.actual_IP_value);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.new_IP_txt);
            this.Controls.Add(this.new_PORT_txt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.actual_IP_A);
            this.Name = "NetSettings";
            this.ShowIcon = false;
            this.Text = "NetSettings1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label actual_IP_A;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox new_PORT_txt;
        private System.Windows.Forms.TextBox new_IP_txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label actual_IP_value;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label actual_PORT_value;
        private System.Windows.Forms.Button change_net_settings;
    }
}
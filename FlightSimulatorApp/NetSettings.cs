using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightSimulatorApp
{
    public partial class NetSettings : Form
    {
        internal string actual_IP_Value;
        internal string actual_PORT_Value;

        public NetSettings()
        {
            InitializeComponent();
            actual_IP_Value = ConfigurationManager.AppSettings.Get("IP_address");
            actual_PORT_Value = ConfigurationManager.AppSettings.Get("PORT");
        }

        private void change_net_settings_Click(object sender, EventArgs e)
        {
            //Tomo los valores de ambos cuadros de texto
            string IPstr = new_IP_txt.Text.Trim();
            string PORTstr = new_PORT_txt.Text.Trim();

            if (IPstr == "")
            {
                string message = "Changes cannot be applied - IP Address empty";
                string title = "Error";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.Close();
                return;
            }

            if (PORTstr == "")
            {
                string message = "Changes cannot be applied - PORT empty";
                string title = "Error";
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.Close();
                return;
            }

            //Editar el App.config
            ConfigXmlDocument xmlDoc = new ConfigXmlDocument();

            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            foreach (System.Xml.XmlElement el in xmlDoc.DocumentElement)
            {
                if (el.Name.Equals("appSettings"))
                {  
                    foreach (System.Xml.XmlNode node in el.ChildNodes)
                    {
                        if (node.Attributes[0].Value == "IP_address")
                        {
                            node.Attributes[1].Value = IPstr;
                        }
                        if (node.Attributes[0].Value == "PORT")
                        {
                            node.Attributes[1].Value = PORTstr;
                        }
                    }
                }
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                ConfigurationManager.RefreshSection("appSettings");
            }
            MessageBox.Show("Changes applied successfully");
            this.Close();
        }
    }
}
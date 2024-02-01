using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace ArduinoWF
{
    public partial class ServoControl : Form
    {

        bool isConnected = false;
        bool isAutomated = false;
        String[] ports;
        SerialPort port;
        public ServoControl()
        {
            InitializeComponent();
            disableControls();
            getAvailablePorts();
            
            foreach(var port in ports)
            {
                comboBox1.Items.Add(port.ToString());
                if (ports[0] != null)
                {
                    comboBox1.SelectedItem = "COM4";
                }

            }
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void connectbutton_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                connectToArduino();
            }
            else
            {
                disconnectFromArduino();
            }
        }

        void getAvailablePorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void connectToArduino()
        {
            try
            {
                isConnected = true;
                string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
                port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
                port.Open();
                //port.Write("#STAR\n");
                connectbutton.Text = "Disconnect";
                Console.WriteLine("Connected to: " + port.PortName);
                enableControls();
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
                
            }

        }

        private void disconnectFromArduino()
        {
            try
            {
                // port.Write("#STOP\n");
                port.Write("2");
                port.Write("");
                checkBox1.Checked = false;
                connectbutton.Text = "Connect";
                isConnected = false;
                disableControls();
                port.Close();
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
               
        }
        
        // A way to send text to display in a 16x2 I2C LCD (currently unused)
        private void button1_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                string sentText = "#TEXT" + textBox1.Text + "\n";
                port.Write(sentText);
                Console.WriteLine("Message sent: " + sentText);
            }
        }
        private void enableControls()
        {
            button1.Enabled = false;
            textBox1.Enabled = false;
            checkBox1.Enabled = true;
        }

        private void disableControls()
        {
            button1.Enabled = false;
            textBox1.Enabled = false;
            checkBox1.Enabled = false;
            textBox1.Text = "";
        }
        // automated servo movement checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (checkBox1.Checked)
                {
                    string sentText = "1";
                    Console.WriteLine("Automated servo activated" + " " + sentText);
                    isAutomated = true;
                    disableServoControl();
                    port.Write(sentText);
                }
                else if (!checkBox1.Checked)
                {
                    string sentText = "2";
                    Console.WriteLine("Automated servo deactivated" + " " + sentText);
                    isAutomated = false;
                    enableServoControl();
                    port.Write(sentText);
                }
               
            }
        }
        private void laser_CheckedChanged(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (laser.Checked)
                {
                    string sentText = "Z";
                    port.Write(sentText);
                } else if (!laser.Checked)
                {
                    string sentText = "S";
                    port.Write(sentText);
                }

            }
        }

        private void disableServoControl()
        {
            upbutton.Enabled = false;
            downbutton.Enabled = false;
            rightbutton.Enabled = false;
            leftbutton.Enabled = false;

        }

        private void enableServoControl() {
            upbutton.Enabled = true;
            downbutton.Enabled = true;
            rightbutton.Enabled = true;
            leftbutton.Enabled = true;
        }

        // control
        private void upbutton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                try
                {
                    string sentText = "U";
                    Console.WriteLine("Sent up instruction: " + sentText);
                    port.Write(sentText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void downbutton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                try
                {
                    string sentText = "D";
                    Console.WriteLine("Sent down instruction: " + sentText);
                    port.Write(sentText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rightbutton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                try
                {
                    string sentText = "R";
                    Console.WriteLine("Sent right instruction: " + sentText);
                    port.Write(sentText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void leftbutton_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                try
                {
                    string sentText = "L";
                    Console.WriteLine("Sent left instruction: " + sentText);
                    port.Write(sentText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}

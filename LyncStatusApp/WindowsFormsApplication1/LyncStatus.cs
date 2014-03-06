using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsApplication1
{
    public partial class LyncStatus : Form
    {
        public Boolean bIsComConnected = false;
        private SerialPort _serialPort;
        private string tString = string.Empty;
        private byte _terminator = 0x13;

        public byte[] keepAlive = new byte[] {0x7c, 0x39 ,0x5f, 0x30, 0x5f, 0x7c};
        public byte[] lyncAvailableLeft = new byte[] {0x7c, 0x39, 0x5f, 0x31, 0x5f, 0x34, 0x5f, 0x30, 0x5f, 0x30, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x30, 0x5f, 0x7c };
        public byte[] lyncAvailableRight = new byte[] {0x7c, 0x39, 0x5f, 0x31, 0x5f, 0x34, 0x5f, 0x31, 0x5f, 0x30, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x30, 0x5f, 0x7c };
        public byte[] lyncBusyLeft = new byte[] {0x7c, 0x39, 0x5f, 0x31, 0x5f, 0x34, 0x5f, 0x30, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x30, 0x5f, 0x30, 0x5f, 0x7c };
        public byte[] lyncBusyRight = new byte[] {0x7c, 0x39, 0x5f, 0x31, 0x5f, 0x34, 0x5f, 0x31, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x30, 0x5f, 0x30, 0x5f, 0x7c };
        public byte[] lyncAwayLeft = new byte[] {0x7c, 0x39, 0x5f, 0x31, 0x5f, 0x34, 0x5f, 0x30, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x30, 0x5f, 0x7c };
        public byte[] lyncAwayRight = new byte[] {0x7c, 0x39, 0x5f, 0x31, 0x5f, 0x34, 0x5f, 0x31, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x30, 0x5f, 0x7c };
        public byte[] lyncDNDLeft = new byte[] {0x7c, 0x39, 0x5f, 0x31, 0x5f, 0x34, 0x5f, 0x30, 0x5f, 0x30, 0x5f, 0x30, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x7c };
        public byte[] lyncDNDRight = new byte[] {0x7c, 0x39, 0x5f, 0x31, 0x5f, 0x34, 0x5f, 0x31, 0x5f, 0x32, 0x35, 0x35, 0x5f, 0x30, 0x5f, 0x30, 0x5f, 0x7c };
        public byte[] lyncOfflineLeft = new byte[] {0x7c, 0x39, 0x5f, 0x32, 0x5f, 0x30, 0x5f, 0x31, 0x5f, 0x7c };
        public byte[] lyncOfflineRight = new byte[] {0x7c, 0x39, 0x5f, 0x32, 0x5f, 0x31, 0x5f, 0x31, 0x5f, 0x7c };
        

        public LyncStatus()
        {
            InitializeComponent();
            // Initialization of windows form
            // 0. Ready for com connect
            this.bDisconnect.Enabled = false;
            // 1. Debug will be disable
            this.cDebugMode.Checked = false;
            // 2. all radio lables will be disable
            this.rAvailable.Enabled = false;
            this.rAway.Enabled = false;
            this.rBusy.Enabled = false;
            this.rDND.Enabled = false;
            this.rOffline.Enabled = false;

            // 3. Need to update the com list - already updated (in timer module)
            // 4. (provision) lync connect

        }

        private void bExit_Click(object sender, EventArgs e)
        {
            this.tComCheckTimer.Enabled = false;
            this.tComCheckTimer.Dispose();
            this.tSendCommands.Enabled = false;
            this.tSendCommands.Dispose();

            this.Close();
        }

        private void cDebug_CheckedChanged(object sender, EventArgs e)
        {
            Boolean bEnableStatus = this.cDebugMode.Checked;

            this.rAvailable.Enabled = bEnableStatus;
            this.rAway.Enabled = bEnableStatus;
            this.rBusy.Enabled = bEnableStatus;
            this.rDND.Enabled = bEnableStatus;
            this.rOffline.Enabled = bEnableStatus;


        }

        private void bScan_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            this.lComPorts.Items.Clear();

            foreach (string port in ports)
            {
                this.lComPorts.Items.Add(port);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            Boolean isNeededUpdate = (this.bIsComConnected == false) && (ports.Length != this.lComPorts.Items.Count) ;

            for (int i = 0; isNeededUpdate && (i < this.lComPorts.Items.Count); i++)
            {
                isNeededUpdate = !(ports[i] == this.lComPorts.Items[i].ToString());              
            }

            if (isNeededUpdate == true)
            {
                //Console.Out.WriteLine("Need to update {0} {1}", ports.Length, this.lComPorts.Items.Count);
                this.lComPorts.Items.Clear();

                foreach (string port in ports)
                {
                    this.lComPorts.Items.Add(port);
                }
            }
        }

        private void LyncStatus_Load(object sender, EventArgs e)
        {
            this.tComCheckTimer.Interval = 1000;
            this.tComCheckTimer.Enabled = true;
            this.tSendCommands.Interval = 3000;
            this.tSendCommands.Enabled = false;
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _serialPort = new SerialPort();
                _serialPort.BaudRate = 9600;
                //_serialPort.DataBits = 8;
                //_serialPort.Handshake = Handshake.None;
                //_serialPort.Parity = Parity.None;
                _serialPort.PortName = this.lComPorts.SelectedItem.ToString();
                //_serialPort.StopBits = StopBits.None;
                //_serialPort.DtrEnable = true;
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
                _serialPort.Open();
            }
            catch { return; }
        
            // if connected ok disable the connect and enable the disconnect
            this.bConnect.Enabled = false;
            this.bDisconnect.Enabled = true;
            this.bIsComConnected = true;
            this.tSendCommands.Enabled = true;
        }

        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Initialize a buffer to hold the received data 
            byte[] buffer = new byte[_serialPort.ReadBufferSize];

            //There is no accurate method for checking how many bytes are read 
            //unless you check the return from the Read method 
            int bytesRead = _serialPort.Read(buffer, 0, buffer.Length);

            //For the example assume the data we are received is ASCII data. 
            tString += Encoding.ASCII.GetString(buffer, 0, bytesRead);
            //Check if string contains the terminator  
            if (tString.IndexOf((char)_terminator) > -1)
            {
                //If tString does contain terminator we cannot assume that it is the last character received 
                string workingString = tString.Substring(0, tString.IndexOf((char)_terminator));
                //Remove the data up to the terminator from tString 
                tString = tString.Substring(tString.IndexOf((char)_terminator));
                //Do something with workingString 
                Console.WriteLine(workingString);
            }
            else
            {
                tString = string.Empty;
            }

        }

        private void bDisconnect_Click(object sender, EventArgs e)
        {
            _serialPort.Close();
            this.bDisconnect.Enabled = false;
            this.bConnect.Enabled = true;
            this.bIsComConnected = false;
            this.tSendCommands.Enabled = false;
        }

        private void tSendCommands_Tick(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                Console.WriteLine("Start Of transmittion");
            }

            if (this.cDebugMode.Checked == true)
            {
                if (rAvailable.Checked == true)
                {
                    _serialPort.Write(this.lyncAvailableLeft, 0, this.lyncAvailableLeft.Length);
                    //_serialPort.Write(this.lyncAvailableRight, 0, this.lyncAvailableRight.Length);

                }
                else if (rAway.Checked == true)
                {
                    _serialPort.Write(this.lyncAwayLeft, 0, this.lyncAwayLeft.Length);
                    _serialPort.Write(this.lyncAwayRight, 0, this.lyncAwayRight.Length);
                }
                else if (rBusy.Checked == true)
                {
                    _serialPort.Write(this.lyncBusyLeft, 0, this.lyncBusyLeft.Length);
                    _serialPort.Write(this.lyncBusyRight, 0, this.lyncBusyRight.Length);
                }
                else if (rDND.Checked == true)
                {
                    _serialPort.Write(this.lyncDNDLeft, 0, this.lyncDNDLeft.Length);
                    _serialPort.Write(this.lyncDNDRight, 0, this.lyncDNDRight.Length);
                }
                else if (rOffline.Checked == true)
                {
                    _serialPort.Write(this.lyncOfflineLeft, 0, this.lyncOfflineLeft.Length);
                    _serialPort.Write(this.lyncOfflineRight, 0, this.keepAlive.Length);
                }
                else
                {
                    Console.WriteLine("Error: no debug option were selected");
                }

            }
            else
            {
                _serialPort.Write(this.keepAlive, 0, this.keepAlive.Length);
            }



        } 

    }
}

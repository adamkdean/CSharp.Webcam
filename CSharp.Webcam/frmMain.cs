using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Touchless.Vision.Camera;

namespace CSharp.Webcam
{
    public partial class frmMain : Form
    {
        private List<Camera> availableCameras;
        private Camera selectedCamera;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            WriteLog("Initializing...");

            var timer = new System.Timers.Timer { Interval = (1000f) };
            timer.Elapsed += new ElapsedEventHandler((o, a) => Start());
            timer.Elapsed += new ElapsedEventHandler((o, a) => timer.Enabled = false);
            timer.Enabled = true;
        }

        private void Start()
        {
            availableCameras = new List<Camera>(CameraService.AvailableCameras);
            availableCameras.ForEach(x => WriteLog("Camera found: {0}", x.Name));

            selectedCamera = availableCameras[0];
            WriteLog("Selected camera: {0}", selectedCamera.Name);
        }

        private void WriteLog(string format, params object[] args)
        {
            WriteLog(string.Format(format, args));
        }

        private void WriteLog(string s)
        {
            if (InvokeRequired)
            {
                Action<string> action = new Action<string>(WriteLog);
                Invoke(action, s);
                return;
            }

            string prefix = (txtLog.Text == "") ? "" : "\r\n";
            txtLog.Text += string.Format("{0}{1}", prefix, s);
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }
    }
}

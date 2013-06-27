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
using Touchless.Vision.Contracts;

namespace CSharp.Webcam
{
    public partial class frmMain : Form
    {
        private List<Camera> availableCameras;
        private Camera selectedCamera;
        private CameraFrameSource cameraFrameSource;
        private static Bitmap latestFrame;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            WriteLog("Initializing...");

            var timer = new System.Timers.Timer { Interval = (1000f) };
            timer.Elapsed += new ElapsedEventHandler((o, a) => timer.Enabled = false);
            timer.Elapsed += new ElapsedEventHandler((o, a) => Start());            
            timer.Enabled = true;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cameraFrameSource != null)
            {
                cameraFrameSource.NewFrame -= OnNewFrame;
                cameraFrameSource.Camera.Dispose();
                cameraFrameSource = null;                
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (cameraFrameSource != null)
                cameraFrameSource.Camera.ShowPropertiesDialog();
        }  

        private void Start()
        {
            availableCameras = new List<Camera>(CameraService.AvailableCameras);
            availableCameras.ForEach(x => WriteLog("Camera found: {0}", x.Name));

            selectedCamera = availableCameras[0];
            WriteLog("Selected camera: {0}", selectedCamera.Name);

            cameraFrameSource = new CameraFrameSource(selectedCamera);
            cameraFrameSource.Camera.CaptureWidth = 320;
            cameraFrameSource.Camera.CaptureHeight = 240;
            cameraFrameSource.Camera.Fps = 10;            
            cameraFrameSource.NewFrame += OnNewFrame;
            WriteLog("CameraFrameSource created {0}x{1} @ {2} fps",
                cameraFrameSource.Camera.CaptureWidth, 
                cameraFrameSource.Camera.CaptureHeight, 
                cameraFrameSource.Camera.Fps);

            WriteLog("Starting capture...");
            cameraDisplayBox.Paint += new PaintEventHandler(DrawLatestFrame);
            cameraFrameSource.StartFrameCapture();
        }

        private void DrawLatestFrame(object sender, PaintEventArgs e)
        {
            if (latestFrame != null)
            {
                e.Graphics.DrawImage(latestFrame, 0, 0, latestFrame.Width, latestFrame.Height);
                WriteLog("Size: {0},{1}", latestFrame.Width, latestFrame.Height);
            }
        }

        private void OnNewFrame(IFrameSource frameSource, Frame frame, double fps)
        {
            latestFrame = frame.Image;
            cameraDisplayBox.Invalidate();
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

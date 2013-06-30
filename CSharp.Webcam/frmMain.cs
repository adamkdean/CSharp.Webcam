using CSharp.Webcam.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Touchless.Vision.Camera;
using Touchless.Vision.Contracts;

namespace CSharp.Webcam
{
    public partial class frmMain : Form
    {
        private const int CAPTURE_WIDTH = 640;
        private const int CAPTURE_HEIGHT = 480;
        private const int CAPTURE_FPS = 30;
        private const float STARTUP_DELAY = 250f;
        
        private List<Camera> availableCameras;
        private Camera selectedCamera;
        private CameraFrameSource cameraFrameSource;
        private Bitmap lastFrame;
        private PictureBox maxPictureBox;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            WriteLog("Initializing...");

            var timer = new System.Timers.Timer { Interval = (STARTUP_DELAY) };
            timer.Elapsed += new ElapsedEventHandler((o, a) => timer.Enabled = false);
            timer.Elapsed += new ElapsedEventHandler((o, a) => Start());            
            timer.Enabled = true;
        }

        #region User Interface
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnExit_Click(this, null);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (cameraFrameSource != null)
                cameraFrameSource.Camera.ShowPropertiesDialog();
        }        

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (cameraFrameSource != null)
            {
                cameraFrameSource.NewFrame -= OnNewFrame;
                cameraFrameSource.Camera.Dispose();
                cameraFrameSource = null;
                Application.Exit();
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            TogglePictureBox(pictureBox1, true);
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            TogglePictureBox(pictureBox2, true);
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            TogglePictureBox(pictureBox3, true);
        }

        private void pictureBox4_DoubleClick(object sender, EventArgs e)
        {
            TogglePictureBox(pictureBox4, true);
        }

        private void pictureBox5_DoubleClick(object sender, EventArgs e)
        {
            TogglePictureBox(null, false);
        }

        private void TogglePictureBox(PictureBox box, bool maximize)
        {
            pictureBox1.Visible = !maximize;
            pictureBox2.Visible = !maximize;
            pictureBox3.Visible = !maximize;
            pictureBox4.Visible = !maximize;
            pictureBox5.Visible = maximize;
            maxPictureBox = box;
        }

        private void pictureBox5_Paint(object sender, PaintEventArgs e)
        {
            if (maxPictureBox != null) pictureBox5.Image = maxPictureBox.Image;
        }

        private void UpdateLabel(Label label, string s)
        {
            if (InvokeRequired)
            {
                var action = new Action<Label, string>(UpdateLabel);
                Invoke(action, label, s);
                return;
            }

            label.Text = s;
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
        #endregion

        #region Camera Function
        private void Start()
        {
            availableCameras = new List<Camera>(CameraService.AvailableCameras);
            availableCameras.ForEach(x => WriteLog("Camera found: {0}", x.Name));

            selectedCamera = availableCameras[0];
            WriteLog("Selected camera: {0}", selectedCamera.Name);

            cameraFrameSource = new CameraFrameSource(selectedCamera);
            cameraFrameSource.Camera.CaptureWidth = CAPTURE_WIDTH;
            cameraFrameSource.Camera.CaptureHeight = CAPTURE_HEIGHT;
            cameraFrameSource.Camera.Fps = CAPTURE_FPS;
            cameraFrameSource.NewFrame += OnNewFrame;
            WriteLog("CameraFrameSource created {0}x{1} @ {2} fps",
                cameraFrameSource.Camera.CaptureWidth, 
                cameraFrameSource.Camera.CaptureHeight, 
                cameraFrameSource.Camera.Fps);

            WriteLog("Starting capture...");
            cameraFrameSource.StartFrameCapture();
        }

        private void OnNewFrame(IFrameSource frameSource, Frame frame, double fps)
        {
            var timeStart = DateTime.Now;
            var currentFrame = (Bitmap)frame.Image.Clone();
            pictureBox1.Image = currentFrame; // (Bitmap)frame.Image.Clone();
            
            if (lastFrame != null)
            {                
                // http://www.codeproject.com/Articles/10248/Motion-Detection-Algorithms
            }

            var elapsed = DateTime.Now - timeStart;
            UpdateLabel(lblMS, string.Format("{0:0.00} ms", elapsed.TotalMilliseconds));
            UpdateLabel(lblFPS, string.Format("{0} FPS", FrameRateCounter.CalculateFrameRate()));
            lastFrame = currentFrame;
        }
        #endregion
    }
}

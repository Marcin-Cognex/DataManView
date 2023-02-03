using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Cognex.DataMan.SDK;
using Cognex.DataMan.SDK.Utils;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;


namespace DataManView
{
    public partial class frmMain : Form
    {
        private DataManSystem _system = null;
        private ResultCollector _results;
        private SynchronizationContext _syncContext = null;
        private object _currentResultInfoSyncLock = new object();

        //-------------------------------------------------------------------------------------------------------------------
        public frmMain()
        {
            InitializeComponent();

            // The SDK may fire events from arbitrary thread context. Therefore if you want to change
            // the state of controls or windows from any of the SDK' events, you have to use this
            // synchronization context to execute the event handler code on the main GUI thread.
            _syncContext = WindowsFormsSynchronizationContext.Current;

            //Properties.Settings.Default.Reload();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = Properties.Settings.Default.WindowLocation;
            this.Size = Properties.Settings.Default.WindowSize;
            txtIP.Text = Properties.Settings.Default.IP;
            lblStats.Text = "";

            tblMain.Dock = DockStyle.Fill;
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void frmMain_Load(object sender, EventArgs e)
        {
            //Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Version v = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }

            backgroundWorkerConnector.RunWorkerAsync();
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.WindowLocation = this.Location;
            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.IP = txtIP.Text.Trim();
            Properties.Settings.Default.Save();

            if (null != _system && _system.State == Cognex.DataMan.SDK.ConnectionState.Connected)
                _system.Disconnect();
        }
        //-------------------------------------------------------------------------------------------------------------------
        public static bool PingHost(IPAddress ip)
        {
            bool pingable = false;
            System.Net.NetworkInformation.Ping pinger = new System.Net.NetworkInformation.Ping();
            try
            {
                System.Net.NetworkInformation.PingReply reply = pinger.Send(ip, 50);
                pingable = (reply.Status == System.Net.NetworkInformation.IPStatus.Success);
            }
            catch (System.Net.NetworkInformation.PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void CleanupConnection()
        {
            if (null != _system)
            {
                _system.SystemConnected -= OnSystemConnected;
                _system.SystemDisconnected -= OnSystemDisconnected;
            }

            _system = null;
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void OnSystemConnected(object sender, EventArgs args)
        {
            _syncContext.Post(
                delegate
                {
                    //AddDebugListItem("System connected");

                    backgroundWorkerConnector.CancelAsync();

                    try { picResultImage.Image = _system.GetLastReadImage(); }
                    catch { }
                },
                null);
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void OnSystemDisconnected(object sender, EventArgs args)
        {
            _syncContext.Post(
                delegate
                {
                    picResultImage.Image = null;
                    backgroundWorkerConnector.RunWorkerAsync();
                },
                null);
        }
        //-------------------------------------------------------------------------------------------------------------------
        void Results_ComplexResultArrived(object sender, ResultInfo e)
        {
            _syncContext.Post(
                delegate
                {
                    ShowResult(e);
                    GetStatsFromResultXml(e.XmlResult);
                },
                null);
        }
        //-------------------------------------------------------------------------------------------------------------------
        private string GetReadStringFromResultXml(string resultXml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(resultXml);

                XmlNode full_string_node = doc.SelectSingleNode("result/general/full_string");

                if (full_string_node != null)
                {
                    XmlAttribute encoding = full_string_node.Attributes["encoding"];
                    if (encoding != null && encoding.InnerText == "base64")
                    {
                        byte[] code = Convert.FromBase64String(full_string_node.InnerText);
                        return _system.Encoding.GetString(code, 0, code.Length);
                    }

                    return full_string_node.InnerText;
                }
            }
            catch
            {
            }

            return "";
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void GetStatsFromResultXml(string resultXml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(resultXml);

                XmlNode node = doc.SelectSingleNode("result/statistics/read_stats");

                if (node != null)
                {
                    var good = node.ChildNodes[0].InnerText;
                    var noread = node.ChildNodes[1].InnerText;
                    lblStats.Text = $"Good reads: {good}   No reads: {noread}";
                }
            }
            catch
            {
                lblStats.Text = "Error.";
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void ShowResult(ResultInfo e)
        {
            List<Image> images = new List<Image>();
            List<string> image_graphics = new List<string>();
            string read_result = null;

            // Take a reference or copy values from the locked result info object.
            // This is done so that the lock is used only for a short period of time.
            lock (_currentResultInfoSyncLock)
            {
                read_result = !String.IsNullOrEmpty(e.ReadString) ? e.ReadString : GetReadStringFromResultXml(e.XmlResult);

                if (e.Image != null)
                    images.Add(e.Image);

                if (e.ImageGraphics != null)
                    image_graphics.Add(e.ImageGraphics);

                if (e.SubResults != null)
                {
                    foreach (var item in e.SubResults)
                    {
                        if (item.Image != null)
                            images.Add(item.Image);

                        if (item.ImageGraphics != null)
                            image_graphics.Add(item.ImageGraphics);
                    }
                }
            }

            //Display full string
            lblResult.Text = read_result;

            //Draw image
            if (images.Count > 0)
            {
                Image first_image = images[0];

                Size image_size = Gui.FitImageInControl(first_image.Size, picResultImage.Size);
                Image fitted_image = Gui.ResizeImageToBitmap(first_image, image_size);

                if (image_graphics.Count > 0)
                {
                    using (Graphics g = Graphics.FromImage(fitted_image))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        foreach (var graphics in image_graphics)
                        {
                            ResultGraphics rg = GraphicsResultParser.Parse(graphics, new Rectangle(0, 0, image_size.Width, image_size.Height));

                            foreach (ResultPolygon poly in rg.Polygons)
                            {
                                //Code graphics
                                if (poly.Color.R == 0 && poly.Color.G == 255 && poly.Color.B == 0)
                                {
                                    drawThicker(poly, Color.Lime, 3, g);
                                }

                                //ROI graphics
                                if (poly.Color.R == 0 && poly.Color.G == 0 && poly.Color.B == 255)
                                {
                                    drawThicker(poly, Color.Blue, 2, g);
                                }
                            }
                        }
                    }
                }

                if (picResultImage.Image != null)
                    picResultImage.Image.Dispose();

                picResultImage.Image = fitted_image;
                picResultImage.Invalidate();
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void drawThicker(ResultPolygon Polygon, Color LineColor, float LineWidth, Graphics g)
        {
            //extend
            List<List<ClipperLib.IntPoint>> solution = new List<List<ClipperLib.IntPoint>>();
            List<List<ClipperLib.IntPoint>> solution2 = new List<List<ClipperLib.IntPoint>>();
            List<ClipperLib.IntPoint> pg = new List<ClipperLib.IntPoint>();
            foreach (Point p in Polygon.Points)
            {
                pg.Add(new ClipperLib.IntPoint(p.X, p.Y));
            }
            solution.Add(pg);

            ClipperLib.ClipperOffset co = new ClipperLib.ClipperOffset();
            co.AddPaths(solution, ClipperLib.JoinType.jtRound, ClipperLib.EndType.etClosedPolygon);
            co.Execute(ref solution2, LineWidth * 0.75);

            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                foreach (List<ClipperLib.IntPoint> pg2 in solution2)
                {
                    PointF[] pts = PolygonToPointFArray(pg2, 1);
                    if (pts.Count() > 2)
                        path.AddPolygon(pts);
                    pts = null;
                }

                g.DrawPath(new Pen(LineColor, LineWidth), path);
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        static private PointF[] PolygonToPointFArray(List<ClipperLib.IntPoint> pg, float scale)
        {
            PointF[] result = new PointF[pg.Count];
            for (int i = 0; i < pg.Count; ++i)
            {
                result[i].X = (float)pg[i].X / scale;
                result[i].Y = (float)pg[i].Y / scale;
            }
            return result;
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (lblInfo.Visible == false) { lblInfo.Visible = true; }

            if(e.UserState is string)
            {
                lblInfo.Text = (string)e.UserState;
            }
            else if (e.UserState is Exception)
            {
                lblInfo.Text = "Exception: " + ((Exception)e.UserState).Message;
            }
        }
        int connectorTickCount = 0;
        //-------------------------------------------------------------------------------------------------------------------
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            backgroundWorkerConnector.ReportProgress(1, "Started.");

            while (backgroundWorkerConnector.CancellationPending == false)
            {
                Thread.Sleep(1000);

                connectorTickCount++;
                if (connectorTickCount > 5) { connectorTickCount = 1; }

                //copy, because those might change any time
                System.Net.IPAddress ip;
                try
                {
                    ip = IPAddress.Parse(txtIP.Text);
                }
                catch { ip = null; }

                if (ip == null)
                {
                    backgroundWorkerConnector.ReportProgress(1, "No address specified" + new String('.', connectorTickCount));
                    continue;
                }

                //Check if device is ready to connect
                bool readyToConnect = false;
                if (ip != null)
                {
                    readyToConnect = PingHost(ip);

                    if (!readyToConnect)
                    {
                        backgroundWorkerConnector.ReportProgress(1, ip.ToString() + " not reachible" + new String('.', connectorTickCount));
                        continue;
                    }
                }

                backgroundWorkerConnector.ReportProgress(50, "Connecting" + new String('.', connectorTickCount));

                if (_system != null && _system.State == Cognex.DataMan.SDK.ConnectionState.Connecting)
                {
                    backgroundWorkerConnector.ReportProgress(50, new Exception("Warning. Connection in progress."));
                    continue;
                }

                if (_system != null && _system.State == Cognex.DataMan.SDK.ConnectionState.Connected)
                {
                    backgroundWorkerConnector.ReportProgress(50, new Exception("Warning. Second attempt to connect."));
                    continue;
                }

                try
                {
                    ISystemConnector _connector = null;
                    if (ip != null)
                    {
                        EthSystemConnector conn = new EthSystemConnector(ip);
                        _connector = conn;
                    }
                    else { return; }

                    _system = new DataManSystem(_connector);
                    _system.DefaultTimeout = 1000;

                    // Subscribe to events that are signalled when the system is connected / disconnected.
                    _system.SystemConnected += new SystemConnectedHandler(OnSystemConnected);
                    _system.SystemDisconnected += new SystemDisconnectedHandler(OnSystemDisconnected);

                    // Subscribe to events that are signalled when the deveice sends auto-responses.
                    ResultTypes requested_result_types = ResultTypes.ReadXml | ResultTypes.Image | ResultTypes.ImageGraphics;
                    _results = new ResultCollector(_system, requested_result_types);
                    _results.ComplexResultArrived += Results_ComplexResultArrived;

                    _system.SetKeepAliveOptions(true, 500, 1000);

                    _system.Connect();
                    _system.SetResultTypes(requested_result_types);
                }
                catch (Exception ex)
                {
                    CleanupConnection();
                    backgroundWorkerConnector.ReportProgress(50, new Exception("Failed to connect: " + ex.ToString()));
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void backgroundWorkerConnector_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            lblInfo.Text = "Connected";
        }
    }
}

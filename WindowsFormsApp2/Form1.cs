using System;
using System.Drawing;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

using System.Diagnostics;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using ABB.Robotics.Controllers.MotionDomain;

using AForge.Video;
using AForge.Video.DirectShow;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;
using AForge.Vision.Motion;
using AForge.Controls;
using System.Collections.Generic;
using ABB.Robotics.Controllers.IOSystemDomain;
using Point = System.Drawing.Point;
using TicTacToe;
using System.Drawing.Imaging;

namespace WindowsFormsApp2
{


    public partial class Form1 : Form
    {
        private NetworkScanner scanner = null;
        private Controller controller = null;
        private RapidData rd = null;
        private int working = 1;
        private double [,] dominos_inf = new double[9, 4] { { 0, 0, 0, 0 },{ 0, 0, 0, 0 },{ 0, 0, 0, 0 },{ 0, 0, 0, 0 },{ 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        private List<double> used_dominos = new List<double>();
        private ABB.Robotics.Controllers.RapidDomain.Task[] tasks = null;
        private TicTacToe.Game algorithm;

        Orient orient;
        ExtJoint extax;
        ConfData conf;
        private RobTarget robtarget;
        int[] oputary = new int[9];
        double offset = 10.0;
        int chess_sequence;

        public Form1()
        {
            InitializeComponent();
            System.Drawing.Point[] OK;    //current feature x,y
            System.Drawing.Point[] center; //四邊形中心的x,y
            System.Drawing.Point[] desired_center;
            System.Drawing.Point[] Current_Center = new System.Drawing.Point[1];
        }

        public FilterInfoCollection videoDevices = null;
        public VideoCaptureDevice videoSource = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Aforge 程式調用
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                comboBox1.Items.Add(device.Name);
            }
            //comboBox1.SelectedIndex = 0;
            videoSource = new VideoCaptureDevice();
            //

            this.scanner = new NetworkScanner();
            this.scanner.Scan();
            ControllerInfoCollection controllers = scanner.Controllers;
            foreach (ControllerInfo info in controllers)
            {
                comboBox2.Items.Add(info.ControllerName + " / " + info.IPAddress.ToString());
            }

           
        }

        private void GetPosition()//quaternion to euler
        {
            PosX.Text = robtarget.Trans.X.ToString();
            PosY.Text = robtarget.Trans.Y.ToString();
            PosZ.Text = robtarget.Trans.Z.ToString();

            double qw = robtarget.Rot.Q1;
            double qx = robtarget.Rot.Q2;
            double qy = robtarget.Rot.Q3;
            double qz = robtarget.Rot.Q4;

            double sinr = +2.0 * (qw * qx + qy * qz);
            double cosr = +1.0 - 2.0 * (qx * qx + qy * qy);
            double rx = Math.Atan2(sinr, cosr) * 180 / Math.PI;

            // pitch (y-axis rotation)
            double sinp = +2.0 * (qw * qy - qz * qx);
            double ry;
            if (Math.Abs(sinp) >= 1)
                ry = Math.Sign(sinp) * 180 / Math.PI;//(Math.PI / 2, sinp); // use 90 degrees if out of range
            else
                ry = Math.Asin(sinp) * 180 / Math.PI;

            // yaw (z-axis rotation)
            double siny = +2.0 * (qw * qz + qx * qy);
            double cosy = +1.0 - 2.0 * (qy * qy + qz * qz);
            double rz = Math.Atan2(siny, cosy) * 180 / Math.PI;

            PosRX.Text = rx.ToString();// q1 q2 q3 q4
            PosRY.Text = ry.ToString();
            PosRZ.Text = rz.ToString();
            //var tuple = quaterniontoeuler(qw, qx, qy, qz);

            //PosRX.Text = tuple.Item1.ToString();//ไปดูเรื่อง zyx กับ xyz กับ q1 q2 q3 q4
            //PosRY.Text = tuple.Item2.ToString();
            //PosRZ.Text = tuple.Item3.ToString();
        }
        
        // VideoSource
        void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                Bitmap image = (Bitmap)eventArgs.Frame.Clone();
                pictureBox2.Image = image;
            }
            catch(Exception)
            {
                MessageBox.Show("Please check the IP !");
            }
        }

        // Combo
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Picture Box
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }        

        // TextBox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // Image Processing 
        // Set Origin images
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap img = new Bitmap(pictureBox1.Image);
                pictureBox3.Image = img;
            }
            catch (Exception)
            {
                MessageBox.Show("Please load an image!");
            }
        }

        //Save and load image to process 
        private void save_load_image()
        {
            try
            {
                pictureBox2.Image.Save(@"C:\Users\cyc\Desktop\ARAA_test\WindowsFormsApp2\WindowsFormsApp2\test.jpeg", ImageFormat.Jpeg);
            }
            catch (Exception)
            {
                MessageBox.Show("Can not save the Images.");
            }

            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            VectorOfVectorOfPoint contours_red = new VectorOfVectorOfPoint();
            VectorOfVectorOfPoint contours_blue = new VectorOfVectorOfPoint();
            Image<Gray, System.Byte> imgProcessed;
            int index = 0;

            Image<Bgr, System.Byte> inputImage = new Image<Bgr, System.Byte>(@"C:\Users\cyc\Desktop\ARAA_test\WindowsFormsApp2\WindowsFormsApp2\test.jpeg");
            Image<Bgr, System.Byte> Image = inputImage.Clone();


            Image.ROI = new Rectangle(140, 80, 340, 340);
            pictureBox2.Image = Image.ToBitmap();
            Console.WriteLine("size:" + contours.Size);


            //----------------------------------------------------------------------------------------------------
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Image<Bgr, System.Byte> Image1 = new Image<Bgr, byte>(Image.ToBitmap());
                    Image1.ROI = new Rectangle(x * Image1.Width / 3, y * Image1.Height / 3, Image1.Width / 3, Image1.Height / 3);
                    pictureBox1.Image = Image1.ToBitmap();
                    imgProcessed = Image1.InRange(new Bgr(0, 0, 150), new Bgr(100, 125, 255));
                    CvInvoke.FindContours(imgProcessed, contours_red, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                    Console.WriteLine(Convert.ToString(index) + "red:" + contours_red.Size);
                    imgProcessed = Image1.InRange(new Bgr(50, 0, 0), new Bgr(255, 175, 50));
                    CvInvoke.FindContours(imgProcessed, contours_blue, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                    Console.WriteLine(Convert.ToString(index) + "blue:" + contours_blue.Size);
                    if (contours_red.Size > contours_blue.Size)
                    {
                        oputary[index] = 2;
                    }
                    else if (contours_blue.Size > contours_red.Size)
                    {
                        oputary[index] = 1;
                    }
                    else
                    {
                        oputary[index] = 0;
                    }
                    index++;

                }
            }
            for (int i = 0; i < oputary.Length; i++)
            {
                Console.WriteLine("位置" + i + ": " + oputary[i]);
            }


            label1.Text = Convert.ToString(oputary[0]);
            label3.Text = Convert.ToString(oputary[1]);
            label6.Text = Convert.ToString(oputary[2]);
            label7.Text = Convert.ToString(oputary[3]);
            label4.Text = Convert.ToString(oputary[4]);
            label5.Text = Convert.ToString(oputary[5]);
            label16.Text = Convert.ToString(oputary[6]);
            label15.Text = Convert.ToString(oputary[7]);
            label8.Text = Convert.ToString(oputary[8]);

        }

        // Load Images
        private void button1_Click(object sender, EventArgs e)
        {
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            VectorOfVectorOfPoint contours_red = new VectorOfVectorOfPoint();
            VectorOfVectorOfPoint contours_blue = new VectorOfVectorOfPoint();
            Image<Gray, System.Byte> imgProcessed;
            int index = 0;

                    Image<Bgr, System.Byte> inputImage = new Image<Bgr, System.Byte>(@"C:\Users\cyc\Desktop\ARAA_test\WindowsFormsApp2\WindowsFormsApp2\test.jpeg");
                    Image<Bgr, System.Byte> Image = inputImage.Clone();


                    //var temp = Image.SmoothGaussian(9).Convert<Gray, byte>().ThresholdBinary(new Gray(175), new Gray(255));
                    //CvInvoke.FindContours(temp, contours, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);


                    //for (int i = 0; i < contours.Size; i++)
                    //{
                    //    using (VectorOfPoint contour = contours[i])
                    //    {
                    //        // MinAreaRect 是此版本找尋最小面積矩形的方法。
                    //        RotatedRect BoundingBox = CvInvoke.MinAreaRect(contour);
                    //        var pts = BoundingBox.GetVertices();
                    //        for(int j = 0; j < pts.Length; j++)
                    //        {
                    //            pts.x=
                    //        }
                    //        CvInvoke.Polylines(Image, Array.ConvertAll(BoundingBox.GetVertices(), Point.Round), true, new Bgr(Color.DeepPink).MCvScalar, 7);
                    //        Console.WriteLine("BoundingBox:" + BoundingBox.Size);
                    //    }
                    //}
                    //pictureBox3.Image = temp.ToBitmap();


                    Image.ROI = new Rectangle(140, 80, 340, 340);
                    pictureBox2.Image = Image.ToBitmap();
                    Console.WriteLine("size:" + contours.Size);
                    

                    //----------------------------------------------------------------------------------------------------
                    for (int y = 0; y < 3; y++)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            Image<Bgr, System.Byte> Image1 = new Image<Bgr, byte>(Image.ToBitmap());
                            Image1.ROI = new Rectangle(x * Image1.Width / 3, y * Image1.Height / 3, Image1.Width / 3, Image1.Height / 3);
                            pictureBox1.Image = Image1.ToBitmap();
                            imgProcessed = Image1.InRange(new Bgr(0, 0, 125), new Bgr(125, 125, 255));
                            CvInvoke.FindContours(imgProcessed, contours_red, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                            Console.WriteLine(Convert.ToString(index) + "red:" + contours_red.Size);
                            imgProcessed = Image1.InRange(new Bgr(0, 0, 0), new Bgr(255, 175, 50));
                            CvInvoke.FindContours(imgProcessed, contours_blue, null, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                            Console.WriteLine(Convert.ToString(index) + "blue:" + contours_blue.Size);
                            if (contours_red.Size > contours_blue.Size)
                            {
                                oputary[index] = 2;
                            }
                            else if (contours_blue.Size > contours_red.Size)
                            {
                                oputary[index] = 1;
                            }
                            else
                            {
                                oputary[index] = 0;
                            }
                            index++;

                        }
                    }
                    for (int i = 0; i < oputary.Length; i++)
                    {
                        Console.WriteLine("位置" + i + ": " + oputary[i]);
                    }




                    //--------------------------------------------------------------------------------------------------------
                    //Bitmap img = Image1.ToBitmap();
                    //Image<Bgr, byte> imgInput = new Image<Bgr, byte>(img);

                    //pictureBox1.Image = Image1.ToBitmap();
                    //pictureBox3.Image = imgProcessed.ToBitmap();


                    //pictureBox3.Image = imgProcessed.ToBitmap();
                    //pictureBox2.Image = inputImage.ToBitmap();
                    //label6.Text = Convert.ToString(contours.Size);
                    //label7.Text = Convert.ToString(inputImage.Size);
                    label1.Text = Convert.ToString(oputary[0]);
                    label3.Text = Convert.ToString(oputary[1]);
                    label6.Text = Convert.ToString(oputary[2]);
                    label7.Text = Convert.ToString(oputary[3]);
                    label4.Text = Convert.ToString(oputary[4]);
                    label5.Text = Convert.ToString(oputary[5]);
                    label16.Text = Convert.ToString(oputary[6]);
                    label15.Text = Convert.ToString(oputary[7]);
                    label8.Text = Convert.ToString(oputary[8]);
                
            
        }
        
        // Device
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (videoSource.IsRunning)
                {
                    videoSource.Stop();
                    pictureBox1.Image = null;
                    pictureBox1.Invalidate();
                }
                else
                {
                    videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
                    videoSource.Start();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Device Lost!");
            }
        }

        // GrayScale
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap img = new Bitmap(pictureBox3.Image);
                Image<Bgr, byte> imgInput = new Image<Bgr, byte>(img);
                Image<Gray, byte> grayImage = imgInput.Convert<Gray, byte>();
                

                pictureBox3.Image = grayImage.ToBitmap();
            }
            catch (Exception)
            {
                MessageBox.Show("Please load an image!");
            }
        }

        // Save Image
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //using (SaveFileDialog filename = new SaveFileDialog())
                //{
                //    filename.Title = "Save Dialog";
                //    filename.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                //    if (filename.ShowDialog(this) == DialogResult.OK)
                //    {
                //        using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox2.Height))
                //        {
                //            pictureBox2.Image.Save(filename.FileName);
                //            MessageBox.Show("Saved Successfully.....");
                //        }
                //    }
                //}
                pictureBox2.Image.Save(@"C:\Users\cyc\Desktop\ARAA_test\WindowsFormsApp2\WindowsFormsApp2\test.jpeg", ImageFormat.Jpeg);
                // String path = "D:\\";
                // String iCount = "Detecting_Image";
                // pictureBox2.Image.Save(path + iCount.ToString() + ".jpeg");
                // Bitmap bitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                // pictureBox2.DrawToBitmap(mybmp, pictureBox1.Bounds);
                // bitmap.Save("C:\\someImage.bmp");
            }
            catch (Exception)
            {
                MessageBox.Show("Can not save the Images.");
            }
        }

        // Threshold
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap img = new Bitmap(pictureBox3.Image);
                Image<Bgr, byte> imgInput = new Image<Bgr, byte>(img);
                Threshold filter = new Threshold(100);
                CvInvoke.Threshold(imgInput, imgInput,
                         50, //阀值
                         255, //最大值
                         ThresholdType.Binary);//二进制阈值化
                Mat element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross,
                        new Size(3, 3), new System.Drawing.Point(-1, -1));
                CvInvoke.Dilate(imgInput, imgInput, element, new System.Drawing.Point(-1, -1), 5,
                        Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
                CvInvoke.Erode(imgInput, imgInput, element, new System.Drawing.Point(-1, -1), 5, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));

                pictureBox3.Image = imgInput.ToBitmap();
            }
            catch (Exception)
            {
                MessageBox.Show("Please load an image!");
            }
        }

        // Gaussium
        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox3.Image);
            Image<Bgr, byte> imgInput = new Image<Bgr, byte>(img);

            var temp = imgInput.SmoothGaussian(5).Convert<Gray, byte>().ThresholdBinaryInv(new Gray(230), new Gray(255));
            pictureBox3.Image = imgInput.Bitmap;
        }

        // Contours
        private void button9_Click(object sender, EventArgs e)
        {
            //从文件加载图像
            try
            {
                Bitmap img = new Bitmap(pictureBox3.Image);
                Image<Bgr, byte> imgInput = new Image<Bgr, byte>(img);

                Mat element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross,
                        new Size(3, 3), new System.Drawing.Point(-1, -1));
                
                CvInvoke.Erode(imgInput, imgInput, element, new System.Drawing.Point(-1, -1), 4, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));


                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                Mat m = new Mat();

                var temp = imgInput.SmoothGaussian(5).Convert<Gray, byte>().ThresholdBinaryInv(new Gray(230), new Gray(255));
                CvInvoke.FindContours(temp, contours, m, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

                

                int ksize = contours.Size;
                System.Drawing.Point[] Gravity = new System.Drawing.Point[ksize];
                System.Drawing.Point[] Gravity1 = new System.Drawing.Point[ksize];
                System.Drawing.Point[] Gravity2 = new System.Drawing.Point[ksize];
                System.Drawing.Point[] Gravity3 = new System.Drawing.Point[ksize];
                System.Drawing.Point[] Gravity4 = new System.Drawing.Point[ksize];

                MCvMoments[] hu = new MCvMoments[ksize];

                for (int i = 0; i < contours.Size; i++)
                {
                    double perimeter = CvInvoke.ArcLength(contours[i], true);
                    VectorOfPoint approx = new VectorOfPoint();
                    CvInvoke.ApproxPolyDP(contours[i], approx, 0.04 * perimeter, true);

                    CvInvoke.DrawContours(imgInput, contours, i, new MCvScalar(0, 0, 255), 4);



                    //moments  center of the shape
                    var moments = CvInvoke.Moments(contours[i]);

                    double x = (double)(moments.M10 / moments.M00);
                    double y = (double)(moments.M01 / moments.M00);

                    double a = (double)(moments.M20 / moments.M00)-x*x;
                    double b = (double)(moments.M11 / moments.M00) - y * x;
                    double c = (double)(moments.M02 / moments.M00)-y*x;

                    double shift = (Math.Atan2(b, a * c))/2;


                    label2.Text = Convert.ToString(Convert.ToInt64(x));
                    label4.Text = Convert.ToString(Convert.ToInt64(y));

                    Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);
                                                         
                    CvInvoke.DrawContours(imgInput, contours, i, new MCvScalar(0, 0, 255), 8);

                    double ar = (double)rect.Width / rect.Height;
                    
                    // CvInvoke.Line(imgInput, (new System.Drawing.Point((int)(rect.Right), (int)rect.Bottom)), (new System.Drawing.Point((int)(rect.Left), (int)rect.Top)), new MCvScalar(0, 0, 255), 2);

                    Gravity[i] = new System.Drawing.Point((int)x, (int)y);
                    
                    foreach (System.Drawing.Point cent in Gravity)
                    {
                        CvInvoke.Circle(imgInput, cent, 2, new MCvScalar(0, 0, 255), 2);
                    }


                    System.Drawing.Point RightTop = new System.Drawing.Point(rect.Right, rect.Top);
                    System.Drawing.Point RightBottom = new System.Drawing.Point(rect.Right, rect.Bottom);
                    System.Drawing.Point LeftTop = new System.Drawing.Point(rect.Left, rect.Top);
                    System.Drawing.Point LeftBottom = new System.Drawing.Point(rect.Left, rect.Bottom);
                                        
                    Bitmap imgs = imgInput.Bitmap;

                    int top_x = 0;
                    int top_y = 0;
                    int bottom_x = 0;
                    int bottom_y = 0;
                    int right_x = 0;
                    int right_y = 0;
                    int left_x = 0;
                    int left_y = 0;
                    // Get top corner
                    for (int left = rect.Left; left <= rect.Right; left++)
                    {
                        Color left_top = imgs.GetPixel(left, rect.Top);
                        if (left_top.R > 250 && left_top.G < 5 && left_top.B < 5)
                        {
                            top_x = left;
                            top_y = rect.Top;
                            break;
                        }
                    }

                    // Get bottom corner
                    for (int left = rect.Left; left <= rect.Right; left++)
                    {
                        Color left_bottom = imgs.GetPixel(left, rect.Bottom);
                        if (left_bottom.R > 250 && left_bottom.G < 5 && left_bottom.B < 5)
                        {
                            bottom_x = left;
                            bottom_y = rect.Top;
                            break;
                        }
                    }

                    // Get Right corner
                    for (int top = rect.Top; top <= rect.Bottom; top++)
                    {
                        Color top_Right = imgs.GetPixel(rect.Right, top);
                        if (top_Right.R > 250 && top_Right.G < 5 && top_Right.B < 5)
                        {
                            right_x = rect.Right;
                            right_y = top;
                            break;

                        }
                    }

                    // Get left corner

                    for (int top = rect.Top; top <= rect.Bottom; top++)
                    {
                        Color top_left = imgs.GetPixel(rect.Left, top);
                        if (top_left.R > 250 && top_left.G < 5 && top_left.B < 5)
                        {
                            left_x = rect.Left;
                            left_y = top;
                            break;
                        }
                    }
                    
                    if (top_x > bottom_x)
                    {
                        if (right_y > left_y)
                        {
                            double delta_x = right_x - top_x;
                            double delta_y = right_y - top_y;
                            double theta_radians = Math.Atan2(delta_y, delta_x) * 180 / 3.1415926;
                            label5.Text = Convert.ToString(Math.Round(90 - theta_radians, 2));
                        }
                        if (right_y < left_y)
                        {
                            double delta_x = top_x - left_x;
                            double delta_y = left_y - top_y;
                            double theta_radians = Math.Atan2(delta_y, delta_x) * 180 / 3.1415926;
                            label5.Text = Convert.ToString(Math.Round(theta_radians - 90, 2));
                        }
                    }

                    if (top_x < bottom_x)
                    {
                        if (right_y > left_y)
                        {
                            double delta_x = right_x - top_x;
                            double delta_y = right_y - top_y;
                            double theta_radians = Math.Atan2(delta_y, delta_x) * 180 / 3.1415926;
                            label5.Text = Convert.ToString(Math.Round(90 - theta_radians, 2));
                        }
                        if (right_y < left_y)
                        {
                            if (rect.Height > rect.Width)
                            {
                                double delta_x = top_x - left_x;
                                double delta_y = left_y - top_y;
                                double theta_radians = Math.Atan2(delta_y, delta_x) * 180 / 3.1415926;
                                label5.Text = Convert.ToString(Math.Round(theta_radians - 90, 2));
                            }
                            if (rect.Height < rect.Width)
                            {
                                double delta_x = right_x - top_x;
                                double delta_y = right_y - top_y;
                                double theta_radians = Math.Atan2(delta_y, delta_x) * 180 / 3.1415926;
                                label5.Text = Convert.ToString(Math.Round(90 - theta_radians, 2));
                            }

                        }
                    }


                    if (top_x == bottom_x)
                    {
                        if (rect.Width > rect.Height)
                        {
                            label5.Text = Convert.ToString(Convert.ToInt64(0));
                        }
                        else
                        {
                            label5.Text = Convert.ToString(Convert.ToInt64(90));
                        }
                    }

                    pictureBox3.Image = imgInput.Bitmap;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please load an image!");
            }

        }

        // Count each values
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap img = new Bitmap(pictureBox1.Image);
                for (int x = 0; x < img.Width; x++)
                {
                    for (int y = 0; y < img.Height; y++)
                    {
                        Color c = img.GetPixel(x, y);
                        if (c.R > 120 || c.R < 45 || c.G > 60 || c.B > 60)
                        {
                            img.SetPixel(x, y, Color.White);
                        }
                    }
                }
                Image<Bgr, byte> imgInput = new Image<Bgr, byte>(img);
                Mat element = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross,
                        new Size(3, 3), new System.Drawing.Point(-1, -1));
                CvInvoke.Dilate(imgInput, imgInput, element, new System.Drawing.Point(-1, -1), 4,
                        Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));
                CvInvoke.Erode(imgInput, imgInput, element, new System.Drawing.Point(-1, -1), 4, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(0, 0, 0));

                Image<Gray, byte> grayImage = imgInput.Convert<Gray, byte>();

                Mat edges = new Mat();

                CvInvoke.Canny(imgInput, edges, 120, 180);
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(edges, contours, null,
                    Emgu.CV.CvEnum.RetrType.External,
                    Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxNone);

                // 輪廓查找
                int ksize = contours.Size;
                Console.WriteLine(contours.Size);
                double[] m00 = new double[ksize];
                double[] m10 = new double[ksize];
                double[] m01 = new double[ksize];

                System.Drawing.Point[] Gravity = new System.Drawing.Point[ksize];
                MCvMoments[] hu = new MCvMoments[ksize];

                for (int i = 0; i < ksize; i++)
                {
                    VectorOfPoint contour = contours[i];
                    hu[i] = CvInvoke.Moments(contour, false);
                    m00[i] = hu[i].M00;
                    m10[i] = hu[i].M10;
                    m01[i] = hu[i].M01;
                    double X = m10[i] / m00[i]; //get center X
                    double Y = m01[i] / m00[i]; //get center y
                    Gravity[i] = new System.Drawing.Point((int)X, (int)Y);
                }
                int circle_number = 0;

                foreach (System.Drawing.Point cent in Gravity)
                {
                    CvInvoke.Circle(imgInput, cent, 2, new MCvScalar(0, 0, 255), 2);
                    circle_number++;
                }
                label8.Text = Convert.ToString(Convert.ToInt64(circle_number));
            }
            catch (Exception)
            {
                MessageBox.Show("Please load an image!");
            }

        }



        // Manipulator
        public void ConnectionChanged(object sender, EventArgs e)
        {
            if (this.controller.Connected == true) Connect.BackColor = Color.Green;
            else Connect.BackColor = Color.Red;
        }
        

        // Get Home
        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                //RobTarget robtarget = new RobTarget();
                robtarget.Trans.X = 353;
                robtarget.Trans.Y = -0.2F;
                robtarget.Trans.Z = 578.9F;
                                              
                orient.Q1 = 0.27063;
                orient.Q2 = -0.65362;
                orient.Q3 = 0.65277;
                orient.Q4 = -0.27096;
                robtarget.Rot = orient;
                

                extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
                robtarget.Extax = extax;

                conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
                robtarget.Robconf = conf;

                tasks = controller.Rapid.GetTasks();
                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    //tasks.SetProgramPointer("Module1", "main");
                    rd.Value = robtarget;
                    //controller.Rapid.Start(true);
                    tasks[0].Start(); //test real robot
                                      //tasks.Dispose();
                }
                GetPosition();
            }
            catch (Exception)
            {
                MessageBox.Show("Please connect the manipulator.");
            }

        }

        // Get Position
        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                RobTarget CurrPos = controller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.WorkObject);
                PosX.Text = CurrPos.Trans.X.ToString();
                PosY.Text = CurrPos.Trans.Y.ToString();
                PosZ.Text = CurrPos.Trans.Z.ToString();

                double qw = CurrPos.Rot.Q1;
                double qx = CurrPos.Rot.Q2;
                double qy = CurrPos.Rot.Q3;
                double qz = CurrPos.Rot.Q4;

                // roll (x-axis rotation)
                double sinr = +2.0 * (qw * qx + qy * qz);
                double cosr = +1.0 - 2.0 * (qx * qx + qy * qy);
                double rx = Math.Atan2(sinr, cosr) * 180 / Math.PI;

                // pitch (y-axis rotation)
                double sinp = +2.0 * (qw * qy - qz * qx);
                double ry;
                if (Math.Abs(sinp) >= 1)
                    ry = Math.Sign(sinp) * 180 / Math.PI;//(Math.PI / 2, sinp); // use 90 degrees if out of range
                else
                    ry = Math.Asin(sinp) * 180 / Math.PI;

                // yaw (z-axis rotation)
                double siny = +2.0 * (qw * qz + qx * qy);
                double cosy = +1.0 - 2.0 * (qy * qy + qz * qz);
                double rz = Math.Atan2(siny, cosy) * 180 / Math.PI;
                PosRX.Text = rx.ToString();//q1 q2 q3 q4
                PosRY.Text = ry.ToString();
                PosRZ.Text = rz.ToString();

                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    rd.Value = CurrPos;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please connect the manipulator.");
            }

        }

        // Connect
        private void Connect_Click(object sender, EventArgs e)
        {
            ControllerInfoCollection controllers = scanner.Controllers;
            foreach (ControllerInfo info in controllers)
            {
                if (comboBox2.Text.Equals(info.ControllerName + " / " + info.IPAddress.ToString()))
                {
                    if (info.Availability == Availability.Available)
                    {
                        if (this.controller != null)
                        {
                            this.controller.Logoff();
                            this.controller.Dispose(); // = LogOff
                            this.controller = null;
                        }
                        this.controller = ControllerFactory.CreateFrom(info);
                        this.controller.Logon(UserInfo.DefaultUser);
                        //this.controller.StartRapidProgram();
                        this.controller.ConnectionChanged += new EventHandler<ConnectionChangedEventArgs>(ConnectionChanged);
                        Connect.BackColor = Color.Green;
                        Disconnect.BackColor = SystemColors.Control;
                        break;
                    }
                }
                {
                    MessageBox.Show("Selected controller not available.");
                }
            }
            if (this.controller == null) MessageBox.Show("Selected controller not available. (comboBox String != controller info)");
        }

        // Disconnect
        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (this.controller != null)
            {
                controller.Rapid.Stop();
                this.controller.Logoff();
                this.controller.Dispose(); // = LogOff
                this.controller = null;
            }
            Disconnect.BackColor = Color.Red;
            Connect.BackColor = SystemColors.Control;
        }

        // Jog X up
        private void JogXup_Click(object sender, EventArgs e)
        {
            try
            {
                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                robtarget.Trans.X += 1;
                
                tasks = controller.Rapid.GetTasks();
                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    rd.Value = robtarget;
                    //controller.Rapid.Start(true);
                    tasks[0].Start(); //test real robot
                                      //tasks.Dispose();
                }
                GetPosition();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Connect to the robots!");
            }
        }

        // Jox X down
        private void JogXdown_Click(object sender, EventArgs e)
        {
            try
            {
                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                robtarget.Trans.X -= 1;

                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    rd.Value = robtarget;
                    controller.Rapid.Start(true);
                    //tasks[0].Start(); //test real robot
                    //tasks.Dispose();
                }
                GetPosition();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Connect to the robots!");
            }
        }

        // Jog Y up
        private void JogYup_Click(object sender, EventArgs e)
        {
            try
            {
                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                robtarget.Trans.Y += 5;

                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    rd.Value = robtarget;
                    controller.Rapid.Start(true);
                    //tasks[0].Start(); //test real robot
                    //tasks.Dispose();
                }
                GetPosition();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Connect to the robots!");
            }
        }

        // Jog Y down
        private void JogYdown_Click(object sender, EventArgs e)
        {
            try
            {
                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                robtarget.Trans.Y -= 5;

                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    rd.Value = robtarget;
                    controller.Rapid.Start(true);
                    //tasks[0].Start(); //test real robot
                    //tasks.Dispose();
                }
                GetPosition();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Connect to the robots!");
            }
        }

        // Jog Z up
        private void JogZup_Click(object sender, EventArgs e)
        {
            try
            {
                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                robtarget.Trans.Z += 1;

                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    rd.Value = robtarget;
                    controller.Rapid.Start(true);
                    //tasks[0].Start(); //test real robot
                    //tasks.Dispose();
                }
                GetPosition();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Connect to the robots!");
            }
        }

        // Jog Z down
        private void JogZdown_Click(object sender, EventArgs e)
        {
            try
            {
                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                robtarget.Trans.Z -= 1;

                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    rd.Value = robtarget;
                    controller.Rapid.Start(true);
                    //tasks[0].Start(); //test real robot
                    //tasks.Dispose();
                }
                GetPosition();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Connect to the robots!");
            }
        }


        // Move to Working Area
        private void Move2W_Click(object sender, EventArgs e)
        {
            try
            {
                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                //RobTarget robtarget = new RobTarget();
                robtarget.Trans.X = 388.65F;
                robtarget.Trans.Y = 211.05F;
                robtarget.Trans.Z = 579.9F;

                orient.Q1 = 0.27063;
                orient.Q2 = -0.65362;
                orient.Q3 = 0.65277;
                orient.Q4 = -0.27096;
                robtarget.Rot = orient;

                extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
                robtarget.Extax = extax;

                conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
                robtarget.Robconf = conf;

                tasks = controller.Rapid.GetTasks();
                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    //tasks.SetProgramPointer("Module1", "main");
                    rd.Value = robtarget;
                    //controller.Rapid.Start(true);
                    tasks[0].Start(); //test real robot
                                      //tasks.Dispose();
                }
                GetPosition();
            }
            catch (Exception)
            {
                MessageBox.Show("Please connect the manipulator.");
            }
        }
     
        // Move to Target Position
        private void Move2Target_Click(object sender, EventArgs e)
        {           
            try
            {
                int x = Convert.ToInt32(label2.Text);
                int y = Convert.ToInt32(label4.Text);

                Bitmap img = new Bitmap(pictureBox1.Image);

                int middle_x = img.Width / 2 - 1;
                int middle_y = img.Height / 2 - 1;

                Console.WriteLine("all range x" + middle_x);
                Console.WriteLine("all range y" + middle_y);

                // To compute relation from manipulator to images.
                int range_x = middle_x - x;
                int range_y = middle_y - y;
   

                int mrange_x = Convert.ToInt32(range_x / 9.01) + 15; //10
                int mrange_y = Convert.ToInt32(range_y / 8.93) + 42; //2.3
                //ratio at 05:00 p.m. : 6.7 for x and 1.2 for y
                // int mrange_x = Convert.ToDouble(range_x / ((img.Height+1)/10.2));
                // int mrange_y = Convert.ToDouble(range_y / ((img.Width + 1) / 14.3));
                
                Console.WriteLine("differance x:" + mrange_x);
                Console.WriteLine("differance y:" + mrange_y);

                
                try
                {
                    rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                    robtarget.Trans.X += mrange_y;
                    robtarget.Trans.Y += mrange_x;

                    tasks = controller.Rapid.GetTasks();
                    using (Mastership m = Mastership.Request(controller.Rapid))
                    {
                        rd.Value = robtarget;
                        //controller.Rapid.Start(true);
                        tasks[0].Start(); //test real robot
                                          //tasks.Dispose();
                    }
                    GetPosition();
                }
                catch (Exception)
                {
                    MessageBox.Show("Please Connect to the robots!");
                } 
            }
            
            catch (Exception)
            {
                MessageBox.Show("Please connect the manipulator.");
            } 
        }
            
        private void Jog_X_Click(object sender, EventArgs e)
        {
            
        }

        private void get_coordinations_Click(object sender, EventArgs e)
        {

        }

        private void JogRXup_Click(object sender, EventArgs e)
        {
            rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
            const float pi = 3.1415927F;

            double roll = Convert.ToDouble(PosRX.Text);
            double pitch = Convert.ToDouble(PosRY.Text);
            double yaw = Convert.ToDouble(PosRZ.Text);

            roll += 1;

            roll = roll / 180 * pi;
            yaw = yaw / 180 * pi;
            pitch = pitch / 180 * pi;

            Console.WriteLine(roll);
            
            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);
                       
            //RobTarget robtarget = new RobTarget();

            orient.Q1 = cy * cp * cr + sy * sp * sr;
            orient.Q2 = cy * cp * sr - sy * sp * cr;
            orient.Q3 = sy * cp * sr + cy * sp * cr;
            orient.Q4 = sy * cp * cr - cy * sp * sr;
            robtarget.Rot = orient;

            extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
            robtarget.Extax = extax;

            conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
            robtarget.Robconf = conf;

            tasks = controller.Rapid.GetTasks();
            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                //tasks.SetProgramPointer("Module1", "main");
                rd.Value = robtarget;
                //controller.Rapid.Start(true);
                tasks[0].Start(); //test real robot
                                  //tasks.Dispose();
            }
            GetPosition();            
        }

        private void JogRXdown_Click(object sender, EventArgs e)
        {
            rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
            const float pi = 3.1415927F;

            double roll = Convert.ToDouble(PosRX.Text);
            double pitch = Convert.ToDouble(PosRY.Text);
            double yaw = Convert.ToDouble(PosRZ.Text);

            roll -= 1;

            roll = roll / 180 * pi;
            yaw = yaw / 180 * pi;
            pitch = pitch / 180 * pi;

            Console.WriteLine(roll);

            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);

            //RobTarget robtarget = new RobTarget();

            orient.Q1 = cy * cp * cr + sy * sp * sr;
            orient.Q2 = cy * cp * sr - sy * sp * cr;
            orient.Q3 = sy * cp * sr + cy * sp * cr;
            orient.Q4 = sy * cp * cr - cy * sp * sr;
            robtarget.Rot = orient;

            extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
            robtarget.Extax = extax;

            conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
            robtarget.Robconf = conf;

            tasks = controller.Rapid.GetTasks();
            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                //tasks.SetProgramPointer("Module1", "main");
                rd.Value = robtarget;
                //controller.Rapid.Start(true);
                tasks[0].Start(); //test real robot
                                  //tasks.Dispose();
            }
            GetPosition();
        }

        private void JogRYup_Click(object sender, EventArgs e)
        {
            rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
            const float pi = 3.1415927F;

            double roll = Convert.ToDouble(PosRX.Text);
            double pitch = Convert.ToDouble(PosRY.Text);
            double yaw = Convert.ToDouble(PosRZ.Text);

            pitch += 1;

            roll = roll / 180 * pi;
            yaw = yaw / 180 * pi;
            pitch = pitch / 180 * pi;

            Console.WriteLine(roll);

            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);

            //RobTarget robtarget = new RobTarget();

            orient.Q1 = cy * cp * cr + sy * sp * sr;
            orient.Q2 = cy * cp * sr - sy * sp * cr;
            orient.Q3 = sy * cp * sr + cy * sp * cr;
            orient.Q4 = sy * cp * cr - cy * sp * sr;
            robtarget.Rot = orient;

            extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
            robtarget.Extax = extax;

            conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
            robtarget.Robconf = conf;

            tasks = controller.Rapid.GetTasks();
            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                //tasks.SetProgramPointer("Module1", "main");
                rd.Value = robtarget;
                //controller.Rapid.Start(true);
                tasks[0].Start(); //test real robot
                                  //tasks.Dispose();
            }
            GetPosition();
        }

        private void JogRYdown_Click(object sender, EventArgs e)
        {
            rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
            const float pi = 3.1415927F;

            double roll = Convert.ToDouble(PosRX.Text);
            double pitch = Convert.ToDouble(PosRY.Text);
            double yaw = Convert.ToDouble(PosRZ.Text);

            pitch -= 1;

            roll = roll / 180 * pi;
            yaw = yaw / 180 * pi;
            pitch = pitch / 180 * pi;

            Console.WriteLine(roll);

            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);

            //RobTarget robtarget = new RobTarget();

            orient.Q1 = cy * cp * cr + sy * sp * sr;
            orient.Q2 = cy * cp * sr - sy * sp * cr;
            orient.Q3 = sy * cp * sr + cy * sp * cr;
            orient.Q4 = sy * cp * cr - cy * sp * sr;
            robtarget.Rot = orient;

            extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
            robtarget.Extax = extax;

            conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
            robtarget.Robconf = conf;

            tasks = controller.Rapid.GetTasks();
            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                //tasks.SetProgramPointer("Module1", "main");
                rd.Value = robtarget;
                //controller.Rapid.Start(true);
                tasks[0].Start(); //test real robot
                                  //tasks.Dispose();
            }
            GetPosition();
        }

        private void JogRZup_Click(object sender, EventArgs e)
        {
            rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
            const float pi = 3.1415927F;

            double roll = Convert.ToDouble(PosRX.Text);
            double pitch = Convert.ToDouble(PosRY.Text);
            double yaw = Convert.ToDouble(PosRZ.Text);

            yaw += 1;

            roll = roll / 180 * pi;
            yaw = yaw / 180 * pi;
            pitch = pitch / 180 * pi;

            Console.WriteLine(roll);

            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);

            //RobTarget robtarget = new RobTarget();

            orient.Q1 = cy * cp * cr + sy * sp * sr;
            orient.Q2 = cy * cp * sr - sy * sp * cr;
            orient.Q3 = sy * cp * sr + cy * sp * cr;
            orient.Q4 = sy * cp * cr - cy * sp * sr;
            robtarget.Rot = orient;

            extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
            robtarget.Extax = extax;

            conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
            robtarget.Robconf = conf;

            tasks = controller.Rapid.GetTasks();
            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                //tasks.SetProgramPointer("Module1", "main");
                rd.Value = robtarget;
                //controller.Rapid.Start(true);
                tasks[0].Start(); //test real robot
                                  //tasks.Dispose();
            }
            GetPosition();
        }

        private void JogRZdown_Click(object sender, EventArgs e)
        {
            rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
            const float pi = 3.1415927F;
            // get the degree
            double roll = Convert.ToDouble(PosRX.Text);
            double pitch = Convert.ToDouble(PosRY.Text);
            double yaw = Convert.ToDouble(PosRZ.Text);

            // move by -
            yaw -= 1;

            // turn to 
            roll = roll / 180 * pi;
            yaw = yaw / 180 * pi;
            pitch = pitch / 180 * pi;

            Console.WriteLine(roll);

            double cy = Math.Cos(yaw * 0.5);
            double sy = Math.Sin(yaw * 0.5);
            double cp = Math.Cos(pitch * 0.5);
            double sp = Math.Sin(pitch * 0.5);
            double cr = Math.Cos(roll * 0.5);
            double sr = Math.Sin(roll * 0.5);

            //RobTarget robtarget = new RobTarget();

            orient.Q1 = cy * cp * cr + sy * sp * sr;
            orient.Q2 = cy * cp * sr - sy * sp * cr;
            orient.Q3 = sy * cp * sr + cy * sp * cr;
            orient.Q4 = sy * cp * cr - cy * sp * sr;
            robtarget.Rot = orient;

            extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
            robtarget.Extax = extax;

            conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
            robtarget.Robconf = conf;

            tasks = controller.Rapid.GetTasks();
            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                //tasks.SetProgramPointer("Module1", "main");
                rd.Value = robtarget;
                //controller.Rapid.Start(true);
                tasks[0].Start(); //test real robot
                                  //tasks.Dispose();
            }
            GetPosition();
        }

        private void Reach_Click(object sender, EventArgs e)
        {
            try
            {                
                rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                robtarget.Trans.Z -= 211;

                using (Mastership m = Mastership.Request(controller.Rapid))
                {
                    rd.Value = robtarget;
                    controller.Rapid.Start(true);
                    //tasks[0].Start(); //test real robot
                    //tasks.Dispose();
                }
                GetPosition();                
            }
            catch (Exception)
            {
                MessageBox.Show("Please Connect to the robots!");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Signal sig = controller.IOSystem.GetSignal("DO10_16");



            DigitalSignal digitalSig = (DigitalSignal)sig;
            int wValue = digitalSig.Get();
            Console.WriteLine(wValue);
            


            if (this.checkBox1.Checked)
            {
                digitalSig.Set();
            }
            else
            {
                digitalSig.Reset();
            }
            
        }

        private void Reach2DP_Click(object sender, EventArgs e)
        {
            List<double> x_pos = new List<double>(); ;
            List<double> y_pos = new List<double>(); ;
            
            double detect_num = Convert.ToDouble(label8.Text);
            double horizontal_sum;
            double vertical_sum;
            if (detect_num > 0)
            {

                

                if (working == 1)
                {
                    Console.WriteLine("Start First Program");
                    if (used_dominos.Contains(detect_num))
                        return;
                    used_dominos.Add(detect_num);

                    //rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                    //RobTarget robtarget = new RobTarget();
                    /*
                    robtarget.Trans.X = 388.65F;
                    robtarget.Trans.Y = 211.05F;
                    robtarget.Trans.Z = 579.9F;

                    orient.Q1 = 0.27063;
                    orient.Q2 = -0.65362;
                    orient.Q3 = 0.65277;
                    orient.Q4 = -0.27096;
                    robtarget.Rot = orient;

                    extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
                    robtarget.Extax = extax;

                    conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
                    robtarget.Robconf = conf;

                    tasks = controller.Rapid.GetTasks();
                    using (Mastership m = Mastership.Request(controller.Rapid))
                    {
                        //tasks.SetProgramPointer("Module1", "main");
                        rd.Value = robtarget;
                        //controller.Rapid.Start(true);
                        tasks[0].Start(); //test real robot
                                            //tasks.Dispose();
                    }*/
                    dominos_inf[0, 0] = 388.65F;
                    dominos_inf[0, 1] = 211.05F;
                    dominos_inf[0, 2] = 579.9F;
                    dominos_inf[0, 3] = detect_num;

                    working = working - 1;
                    //GetPosition();                
                }
                else
                {
                    Console.WriteLine("Start Second Program");
                    // to check detect number is right
                    if (!used_dominos.Contains(detect_num))
                    {
                        used_dominos.Add(detect_num);
                        // Get the possible position.
                        for (int i = 0; i < 6; i++)
                        {                            
                            Console.WriteLine("Times:" + dominos_inf[i, 3]);
                            if (dominos_inf[i, 3] != 0)
                            {
                                List<double> pos_x = new List<double>(); ;
                                List<double> pos_y = new List<double>(); ;
                                horizontal_sum = Check_Horizontal(detect_num, dominos_inf[i, 0]);                             
                                Console.WriteLine("horizontal_sum:" + horizontal_sum);
                                vertical_sum = Check_Vertical(detect_num, dominos_inf[i, 1]);
                                Console.WriteLine("vertical_sum:" + vertical_sum);

                                if (Convert.ToInt32(horizontal_sum) % 2 == 0)
                                {
                                    pos_y = judge_pos_y(dominos_inf[i, 1], 55, dominos_inf[i, 0], dominos_inf[i, 3]);
                                    foreach (double po_y in pos_y)
                                    {
                                        x_pos.Add(dominos_inf[i, 0]);
                                        y_pos.Add(po_y);
                                        Console.WriteLine("Get the parameter x: " + dominos_inf[i, 0]);
                                        Console.WriteLine("Get the position y: " + po_y);
                                    }
                                }

                                if (Convert.ToInt32(vertical_sum) % 2 == 1)
                                {
                                    pos_x = judge_pos_x(dominos_inf[i, 0], 71, dominos_inf[i, 1], dominos_inf[i, 3]);
                                    foreach (double po_x in pos_x)
                                    {
                                        x_pos.Add(po_x);
                                        y_pos.Add(dominos_inf[i, 1]);
                                        Console.WriteLine("Get the position x: " + po_x);
                                        Console.WriteLine("Get the parameter y: " + dominos_inf[i, 1]);
                                    }
                                }
                            }                            
                        }

                        // Grasp
                        double[] final_x = x_pos.ToArray();
                        double[] final_y = y_pos.ToArray();

                        if (x_pos.Count != 0)
                        {
                            Random rnd = new Random();
                            int rand_num = rnd.Next(0, x_pos.Count-1);

                            Console.WriteLine("random number : "+ rand_num);
                            //rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
                            Console.WriteLine("final position x : "+ final_x[rand_num]);
                            Console.WriteLine("final position y : " + final_y[rand_num]);

                            //RobTarget robtarget = new RobTarget();
                            int final_x_position = Convert.ToInt32(final_x[rand_num]);
                            int final_y_position = Convert.ToInt32(final_y[rand_num]);
                            /*
                            robtarget.Trans.X = final_x_position;
                            robtarget.Trans.Y = final_y_position;
                            robtarget.Trans.Z = 579.9F;

                            orient.Q1 = 0.27063;
                            orient.Q2 = -0.65362;
                            orient.Q3 = 0.65277;
                            orient.Q4 = -0.27096;
                            robtarget.Rot = orient;

                            extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
                            robtarget.Extax = extax;

                            conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
                            robtarget.Robconf = conf;

                            tasks = controller.Rapid.GetTasks();
                            using (Mastership m = Mastership.Request(controller.Rapid))
                            {
                                //tasks.SetProgramPointer("Module1", "main");
                                rd.Value = robtarget;
                                //controller.Rapid.Start(true);
                                tasks[0].Start(); //test real robot
                                                    //tasks.Dispose();

                            }*/
                            for (int domino_x = 0; domino_x < 6; domino_x++)
                            {
                                if (dominos_inf[domino_x, 3] != 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    dominos_inf[domino_x, 0] = final_x[rand_num];
                                    dominos_inf[domino_x, 1] = final_y[rand_num];
                                    dominos_inf[domino_x, 2] = 579.9F;
                                    dominos_inf[domino_x, 3] = detect_num;
                                    break;
                                }

                            }
                            //GetPosition();
                            
                        }
                        else
                        {
                            
                            // To put garbage area
/*                            rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");

                            //RobTarget robtarget = new RobTarget();
                            robtarget.Trans.X = 353;
                            robtarget.Trans.Y = -174.6F;
                            robtarget.Trans.Z = 579.9F;

                            orient.Q1 = 0.27063;
                            orient.Q2 = -0.65362;
                            orient.Q3 = 0.65277;
                            orient.Q4 = -0.27096;
                            robtarget.Rot = orient;

                            extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
                            robtarget.Extax = extax;

                            conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
                            robtarget.Robconf = conf;

                            tasks = controller.Rapid.GetTasks();
                            using (Mastership m = Mastership.Request(controller.Rapid))
                            {
                                //tasks.SetProgramPointer("Module1", "main");
                                rd.Value = robtarget;
                                //controller.Rapid.Start(true);
                                tasks[0].Start(); //test real robot
                                                    //tasks.Dispose();

                            }
                            GetPosition();*/
                            
                        }
                    }
                    else
                    {
                        
                        // To put garbage area
                        /*rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");

                        //RobTarget robtarget = new RobTarget();
                        robtarget.Trans.X = 353;
                        robtarget.Trans.Y = -174.6F;
                        robtarget.Trans.Z = 579.9F;

                        orient.Q1 = 0.27063;
                        orient.Q2 = -0.65362;
                        orient.Q3 = 0.65277;
                        orient.Q4 = -0.27096;
                        robtarget.Rot = orient;

                        extax.FillFromString2("[" + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "," + 9E+09 + "]");
                        robtarget.Extax = extax;

                        conf.FillFromString2("[" + 0 + "," + 0 + "," + 0 + "," + 0 + "]");
                        robtarget.Robconf = conf;

                        tasks = controller.Rapid.GetTasks();
                        using (Mastership m = Mastership.Request(controller.Rapid))
                        {
                            //tasks.SetProgramPointer("Module1", "main");
                            rd.Value = robtarget;
                            //controller.Rapid.Start(true);
                            tasks[0].Start(); //test real robot
                                                //tasks.Dispose();

                        }
                         GetPosition();           */                                 
                    }
                }
            }
        }

        double Check_Horizontal(double captured_num, double x)
        {
            // Check horizontals
            double sum = captured_num;
            for (int i = 0; i < 6; i++)
            {
                if (dominos_inf[i, 0] == x)
                {
                    sum += dominos_inf[i, 3];
                }
            }
            return sum;
        }

        double Check_Vertical(double captured_num, double y)
        {
            // Check vertical
            double sum = captured_num;
            for (int i = 0; i < 6; i++)
            {
                if (dominos_inf[i, 1] == y)
                {
                    sum += dominos_inf[i, 3];
                }
            }
            return sum;
        }

        List<double> judge_pos_x(double x ,double x_para, double y, double domino_num)
        {
            double detect_num = Convert.ToDouble(label8.Text);
            List<double> result = new List<double>();
            double x_plus = x + x_para;
            double x_minus = x - x_para;
            Boolean is_plus_good = false;
            Boolean is_minus_good = false;
            //for (int i = 0; i < 6; i++)
            {
                double current_x = x;
                double current_y = y;
                Console.WriteLine("======================");
                Console.WriteLine("current_x :" + current_x);
                Console.WriteLine("current_y :" + current_y);

                //if (current_y == y && current_x == x_minus)
                {
                    Console.WriteLine("in minus");

                    //double num_minus = dominos_inf[i, 3];
                    double num_minus = detect_num;

                    if (num_minus < domino_num)
                    {
                        is_minus_good = true;
                        result.Add(x_minus);

                    }
                }
                //else if (current_y == y && current_x == x_plus)
                {
                    Console.WriteLine("hey plus");
                    //double num_plus = dominos_inf[i, 3];
                    double num_plus = x_plus;

                    if (num_plus > domino_num)
                    {
                        is_plus_good = true;
                        result.Add(x_plus);
                    }
                }
            }
            ////Boolean is_good = is_minus_good && is_plus_good;
            //if (is_good)
            //{
            //    result.Add(x);
            //}
            foreach (var data in result)
            {
                Console.WriteLine("x_pos:" + data);
            }

            // to judge the position x
            return result;
        }

        List<double> judge_pos_y(double y, double y_para, double x, double domino_num)
        {
            double detect_num = Convert.ToDouble(label8.Text);

            List<double> result = new List<double>();
            double y_plus = y + y_para;
            double y_minus = y - y_para;
            Boolean is_plus_good = false;
            Boolean is_minus_good = false;
            Console.WriteLine("x         :"+ x);
            Console.WriteLine("y         :"+ y);
            Console.WriteLine("y_minus   :"+ y_minus);
            Console.WriteLine("y_plus    :"+ y_plus);
            Console.WriteLine("domino_num:" + domino_num);

            //for (int i = 0; i < 6; i++)
            {
                double current_x = x;
                double current_y = y;
                Console.WriteLine("======================");
                Console.WriteLine("current_x :"+ current_x);
                Console.WriteLine("current_y :"+ current_y);


                //if (current_x == x && current_y == y_minus)
                {
                    Console.WriteLine("in minus");

                    //double num_minus = dominos_inf[i, 3];
                    double num_minus = detect_num;

                    if (num_minus < domino_num)
                    {
                        is_minus_good = true;
                        result.Add(y_minus);

                    }                  
                }
                //else if (current_x == x && current_y == y_plus)
                {
                    Console.WriteLine("hey plus");
                    //double num_plus = dominos_inf[i, 3];
                    double num_plus = y_plus;

                    if (num_plus > domino_num)
                    {
                        is_plus_good = true;
                        result.Add(y_plus);
                    }
                } 
                //else
                {
                    //Console.WriteLine("not found");
                }

            }
            //Boolean is_good = is_minus_good && is_plus_good;
            //if (is_good)
            //{
            //    result.Add(y);
            //}
            foreach(var data in result)
            {
                Console.WriteLine("y_pos:" + data);
            }

            // to judge the position y
            return result;
        }

        private void play_move_robot(double x, double y, double z)
        {
            RobTarget CurrPos = controller.MotionSystem.ActiveMechanicalUnit.GetPosition(CoordinateSystemType.WorkObject);

            robtarget.Trans.X = Convert.ToInt32(x);
            robtarget.Trans.Y = Convert.ToInt32(y);
            robtarget.Trans.Z = Convert.ToInt32(z);

            tasks = controller.Rapid.GetTasks();
            using (Mastership m = Mastership.Request(controller.Rapid))
            {
                rd.Value = robtarget;
                controller.Rapid.Start(true);
                tasks[0].Start();

                //while ( offset > 1 )
                //{
                //    offset = 0.0;
                //    offset = Math.Abs((x - CurrPos.Trans.X) + (y - CurrPos.Trans.Y) + (z - CurrPos.Trans.Z));
                    
                //}
                                  
            }
        }

        private void catch_object()
        {
            Signal sig = controller.IOSystem.GetSignal("DO10_16");

            DigitalSignal digitalSig = (DigitalSignal)sig;
            int wValue = digitalSig.Get();
            Console.WriteLine(wValue);
            digitalSig.Set();
        }

        private void release_object()
        {
            Signal sig = controller.IOSystem.GetSignal("DO10_16");

            DigitalSignal digitalSig = (DigitalSignal)sig;
            int wValue = digitalSig.Get();
            Console.WriteLine(wValue);
            digitalSig.Reset();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            rd = controller.Rapid.GetRapidData("T_ROB1", "MainModule", "target01");
            int[] chessboard = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] x_position = new double[10];
            double[] y_position = new double[10];
            double[] x_chess = new double[5];
            int state = 0;

            //---robot_orientation
            orient.Q1 = 0.27085;
            orient.Q2 = 0.65306;
            orient.Q3 = 0.65348;
            orient.Q4 = 0.27043;
            robtarget.Rot = orient;
            //---chessboard_position
            x_position[2] = 266.7;
            y_position[2] = 32.6;
            x_position[5] = 360.0;
            y_position[5] = 32.6;
            x_position[8] = 467.9;
            y_position[8] = 32.6;
            x_position[1] = 266.7;
            y_position[1] = -62.3;           
            x_position[4] = 362.6; //---center position
            y_position[4] = -62.3; //---center position
            x_position[7] = 460.6;
            y_position[7] = -62.3;
            x_position[0] = 267.0;
            y_position[0] = -162.2;
            x_position[3] = 363.7;
            y_position[3] = -162.8;
            x_position[6] = 462.1;
            y_position[6] = -164.9;
            double fixed_z = 414.9;

            //---wait position
            x_position[9] = 38.7;
            y_position[9] = 383.2;
            double wait_z = 566.2;

            //---chess position
            x_chess[0] = 223.0;
            x_chess[1] = 291.4;
            x_chess[2] = 364.8;
            x_chess[3] = 434.9;
            x_chess[4] = 502.9;
            double y_chess = 126.6;


            //Step1. Move to wait position
            play_move_robot(x_position[9], y_position[9], wait_z);
            MessageBox.Show("1");
            Console.WriteLine("==== User's term ====");

            //Step2. Captured chessborad image
            save_load_image();

            //Step3. Judge chessboard status
            chessboard = (oputary);
            Game.print_chessboard(chessboard);
            state = Game.get_game_state(chessboard);
            Game.print_state(state);
            if (state < 0)
                return;
            Console.WriteLine("");

            //Step4. Compute the next step of computer turn

            Console.WriteLine("==== Computer's term ====");

            int[] candidates = Game.get_candidates(chessboard);
            int pos = Game.pick_candidate(candidates);

            //Step5. Move to get chess            
            play_move_robot(x_chess[chess_sequence], y_chess, fixed_z + 50);
            MessageBox.Show("2_1");
            play_move_robot(x_chess[chess_sequence], y_chess, 371.3);
            MessageBox.Show("2_2");
            catch_object();
            chess_sequence++;
            play_move_robot(x_chess[chess_sequence], y_chess, fixed_z + 50);
            MessageBox.Show("2_3");
            //Step6. Move to target position
            play_move_robot(x_position[pos], y_position[pos], fixed_z + 50);
            MessageBox.Show("3_1");
            play_move_robot(x_position[pos], y_position[pos], 371.3);
            MessageBox.Show("3_2");
            release_object();

            //Step7. Move to center position
            play_move_robot(x_position[4], y_position[4], fixed_z + 50);
            MessageBox.Show("4");
            GetPosition();

            //Step8. Judge chessboard status
            chessboard = Game.update_chessboard_from_computer(chessboard);
            Game.print_chessboard(chessboard);
            state = Game.get_game_state(chessboard);
            Game.print_state(state);
            if (state < 0)
                return;
            Console.WriteLine("");

        }
    }


}

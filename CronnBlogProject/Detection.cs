using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Alturos.Yolo;

namespace CronnBlogProject
{
    class Detection
    {
        private MainForm form;
        private ScreenCapture cap;
        private bool detect;

        public Detection(MainForm form)
        {
            this.form = form;

            cap = new ScreenCapture();
            detect = false;
        }

        public void run()
        {
            detect = true;

            const int screenWidth = 1920;
            const int screenHeight = 1080;
            const int captureRegionWidth = 1200;
            const int captureRegionHeight = 675;

            Console.WriteLine("Starting YOLO object detection ...");
            var gpuConfig = new GpuConfig();
            var yoloConfig =
                new YoloConfiguration("yolov4-valorant.cfg", "yolov4-valorant_last.weights", "valorant.names");
            using (var yoloWrapper = new YoloWrapper(yoloConfig, gpuConfig))
            {
                Console.WriteLine("Starting detection loop ...");
                Stopwatch watch = new Stopwatch();
                Rectangle capRegion = new Rectangle((screenWidth / 2) - (captureRegionWidth / 2),
                    (screenHeight / 2) - (captureRegionHeight / 2), captureRegionWidth, 675);
                while (detect)
                {
                    watch.Restart();
                    Bitmap bmp = cap.CaptureRegion(capRegion);

                    byte[] jpgBytes = (byte[]) new ImageConverter().ConvertTo(bmp, typeof(byte[]));
                    var detectedObjects = yoloWrapper.Detect(jpgBytes);

                    using (Graphics capturedScreen = Graphics.FromImage(bmp))
                    {
                        foreach (var detectedObject in detectedObjects)
                        {
                            Color color;
                            switch (detectedObject.Type)
                            {
                                case "player":
                                    color = Color.LawnGreen;
                                    break;
                                case "head_player":
                                    color = Color.Red;
                                    break;
                                default:
                                    color = Color.Black;
                                    break;
                            }

                            capturedScreen.DrawRectangle(new Pen(color), detectedObject.X, detectedObject.Y,
                                detectedObject.Width, detectedObject.Height);
                            capturedScreen.DrawString(
                                detectedObject.Type + "(" + Math.Round(detectedObject.Confidence * 100) + "%)",
                                new Font(FontFamily.GenericMonospace, 15), new SolidBrush(color), detectedObject.X,
                                detectedObject.Y);
                        }

                        watch.Stop();
                        capturedScreen.DrawString(
                            (1000 / watch.ElapsedMilliseconds) + "FPS (" + watch.ElapsedMilliseconds + "ms)",
                            new Font(FontFamily.GenericMonospace, 15), new SolidBrush(Color.LimeGreen), 10, 10);
                    }

                    form.pictureBox1.Image = bmp;
                }
            }
        }
    }
}
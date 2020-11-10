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

            Console.WriteLine("Starting YOLO object detection ...");
            var gpuConfig = new GpuConfig();
            var yoloConfig =
                new YoloConfiguration("yolov4-valorant.cfg", "yolov4-valorant_last.weights", "valorant.names");
            using (var yoloWrapper = new YoloWrapper(yoloConfig, gpuConfig))
            {
                Console.WriteLine("Starting detection loop ...");
                Stopwatch watch = new Stopwatch();
                while (detect)
                {
                    watch.Restart();
                    Rectangle capRegion = new Rectangle(1920 / 2 - 1200 / 2, 1080 / 2 - 675 / 2, 1200, 675);
                    Bitmap bmp = cap.CaptureRegion(capRegion);

                    byte[] jpgBytes = (byte[]) new ImageConverter().ConvertTo(bmp, typeof(byte[]));
                    var items = yoloWrapper.Detect(jpgBytes);

                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        if (items.GetEnumerator().MoveNext())
                        {
                            foreach (var item in items)
                            {
                                Color color = Color.Black;
                                switch (item.Type)
                                {
                                    case "player":
                                        color = Color.LawnGreen;
                                        break;
                                    case "head_player":
                                        color = Color.Red;
                                        break;
                                }

                                g.DrawRectangle(new Pen(color), item.X, item.Y, item.Width, item.Height);
                                g.DrawString(item.Type + "(" + Math.Round(item.Confidence * 100) + "%)",
                                    new Font(FontFamily.GenericMonospace, 15), new SolidBrush(color), item.X, item.Y);
                            }
                        }

                        watch.Stop();
                        g.DrawString((1000 / watch.ElapsedMilliseconds) + "FPS (" + watch.ElapsedMilliseconds + "ms)",
                            new Font(FontFamily.GenericMonospace, 15), new SolidBrush(Color.LimeGreen), 10, 10);
                    }

                    form.pictureBox1.Image = bmp;
                }
            }
        }
    }
}
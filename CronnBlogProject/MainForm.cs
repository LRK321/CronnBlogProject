using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace CronnBlogProject
{
    public partial class MainForm : Form
    {
        private Detection detection;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            detection = new Detection(this);

            Thread thread = new Thread(detection.run);
            thread.Start();
        }

    }
}

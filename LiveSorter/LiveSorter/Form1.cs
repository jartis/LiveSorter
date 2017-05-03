using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSorter
{
    public partial class Form1 : Form
    {
        public enum SortDirection
        {
            LeftToRight = 0,
            RightToLeft = 1,
            TopToBottom = 2,
            BottomToTop = 3,
        }

        Bitmap currentImg;

        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            imageBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            SetSrcFile(files[0]);
        }

        void SetSrcFile(string filename)
        {
            if (currentImg != null)
            {
                currentImg.Dispose();
            }
            currentImg = new Bitmap(Image.FromFile(filename));
            this.imageBox.Image = currentImg;
        }

        public void UpdatePictureBox()
        { }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            DialogResult fResult = fd.ShowDialog();
            if (fResult == DialogResult.OK)
            {
                SetSrcFile(fd.FileName);
            }
        }

        private void SortOneLine(Bitmap bmp, int lineNumber, SortDirection dir)
        {
            List<MyPixel> line = new List<MyPixel>();
            
            switch (dir)
            {
                case SortDirection.LeftToRight:
                case SortDirection.RightToLeft:
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        line.Add(new MyPixel(bmp.GetPixel(x, lineNumber)));
                    }
                    break;
                case SortDirection.BottomToTop:
                case SortDirection.TopToBottom:
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        line.Add(new MyPixel(bmp.GetPixel(lineNumber, y)));
                    }
                    break;
            }

            line.Sort();
            if (dir == SortDirection.RightToLeft || dir == SortDirection.BottomToTop)
            {
                line.Reverse();
            }

            switch(dir)
            {
                case SortDirection.LeftToRight:
                case SortDirection.RightToLeft:
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        bmp.SetPixel(x, lineNumber, line[x].mColor);
                    }
                    break;
                case SortDirection.BottomToTop:
                case SortDirection.TopToBottom:
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        bmp.SetPixel(lineNumber, y, line[y].mColor);
                    }
                    break;
            }
        }

        private void leftToRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < currentImg.Height; y++)
            {
                SortOneLine(currentImg, y, SortDirection.LeftToRight);
                imageBox.Image = currentImg;
                this.Update();
            }
        }

        private void rightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < currentImg.Height; y++)
            {
                SortOneLine(currentImg, y, SortDirection.RightToLeft);
                imageBox.Image = currentImg;
                this.Update();
            }
        }

        private void topToBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < currentImg.Width; x++)
            {
                SortOneLine(currentImg, x, SortDirection.TopToBottom);
                imageBox.Image = currentImg;
                this.Update();
            }
        }

        private void bottomToTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < currentImg.Width; x++)
            {
                SortOneLine(currentImg, x, SortDirection.BottomToTop);
                imageBox.Image = currentImg;
                this.Update();
            }
        }
    }
}

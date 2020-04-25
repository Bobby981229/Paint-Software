/******************************
 *
 * C# Developing Small Scale Standalone Outcome 03
 * 
 * Name: Liu Shangyuan
 * 
 * Class: 17 - SD
 * 
 * SNO: 197076658
 * 
 * Date: 30/05/2019
 * 
 * 
 ******************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace Outcome_03
{
    public partial class frmPaint : Form
    {
        public frmPaint()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        /// <Stack>
        /// Publicly represents a variable-size last-in, first-out (LOFT) set, 
        /// using Bitmap to process image pixels for recall and redo functions
        /// </Bitmap>
        public Stack<Bitmap> Undo, Redo;

        List<string> ls = new List<string>(); // The full path used to store music files
        List<Point> pList = new List<Point>(); // The path used to store the brushes, the graphics

        // Dtermine the coordinate when mouse down for picLocationX, picLocationY, picLocationX_Y.
        int xPos, yPos, xPos1, yPos1, xPos2, yPos2; 

        int Px, Py; // Determine the new picDrawArea's Size
        bool sizeFlag; // Bool sizeFlag to deal the control's event of picLocationY
        bool sizeFlag1; //Bool sizeFlag1 to deal the control's event of picLocationX
        bool sizeFlag2; //Bool sizeFlag2 to deal the control's event of picLocationX_Y
        bool flag = false; //start drawing or not

        // Represents a small rectangular pop-up window for storing controls and instructions
        ToolTip toolTip1 = new ToolTip(); 

        Pen pen; // Defines objects for drawing lines and curves
        Point beginPt, endPt; //Definition the the Point of begin and end
        Graphics g, g2; // Encapsulate a GUI + drawing surface, respectively is g, g2

        // Store the shape into image, and store the trace drawing into image2
        Image image, image2; 

        int type; // int type to select the different of drawing

        // A brush that defines a color to fill the shape and path of the graphic
        SolidBrush brush;
        
        // Exit the application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( pList != null && pList.Count > 0 )
            {
                DialogResult result = MessageBox.Show("Whether or not to save the picture before exit the application？",
                    "Warning", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    savePicture();
                    Application.Exit();
                }
                else if (result == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void exitEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pList != null && pList.Count > 0)
            {
                DialogResult result = MessageBox.Show("Whether or not to save the picture before exit the application？",
                    "Warning", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    savePicture();
                    Application.Exit();
                }
                else if (result == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void pastePToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtboxInfor.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtboxInfor.Cut();
        }

        private void copyCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtboxInfor.Copy();
        }

        private void picPencil_Click(object sender, EventArgs e)
        {
            type = 0;
            lblStart.Text = "Pencil !";
        }

        private void picLine_Click(object sender, EventArgs e)
        {
            showStatus();
            type = 1;
        }

        private void picRectangle_Click(object sender, EventArgs e)
        {
            showStatus();
            type = 2;
        }

        private void picCircle_Click(object sender, EventArgs e)
        {
            showStatus();
            type = 3;
        }

        private void picRight_Triangle_Click(object sender, EventArgs e)
        {
            showStatus();
            type = 4;
        }

        private void picTriangle_Click(object sender, EventArgs e)
        {
            showStatus();
            type = 5;
        }

        private void picRhombus_Click(object sender, EventArgs e)
        {
            showStatus();
            type = 6;
        }

        private void picRubber_Click_1(object sender, EventArgs e)
        {
            type = 7;
            lblStart.Text = "Rubber !";
        }

        private void picWord_Click(object sender, EventArgs e)
        {
            type = 8;
            lblStart.Text = "TextBox";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = new Icon("Paint 01.ico");
            image = new Bitmap(2000, 2000);

            image2 = new Bitmap(picDrawArea.Width, picDrawArea.Height);
            g = Graphics.FromImage(image);
            ToolTip toolTip1 = new ToolTip();
            NewMethod(toolTip1);
            Undo = new Stack<Bitmap>();
            Redo = new Stack<Bitmap>();

            beginPt = new Point(0, 0);
            endPt = new Point(0, 0);
            pen = new Pen(Color.Black, 5);
            pen.Width = 5;
            type = 0;
            brush = new SolidBrush(Color.Blue);

            picPenColor.BackColor = Color.Black;
            txtboxInfor.Text = "Comment: " + "\n" + "Name: Liu Shangyuan " + "\n" + "SCN: 197076658 " + "\n" + "Class: 17 - SD ";       
        }

        private static void NewMethod(ToolTip toolTip1)
        {
            toolTip1.ShowAlways = true;
        }


        void showStatus()
        {
            switch (type) 
            {
                case 1:
                    lblStart.Text = "Draw a line ! ";
                    break;
                case 2:
                    lblStart.Text = "Draw a Rectangle ! ";
                    break;
                case 3:
                    lblStart.Text = "Draw a Ellpise ! ";
                    break;
                case 4:
                    lblStart.Text = "Draw a Triangle ! ";
                    break;
                case 5:
                    lblStart.Text = "Draw a Isosceles ! ";
                    break;
                case 6:
                    lblStart.Text = "Draw a Diamond ! ";
                    break;
                case 7:
                    lblStart.Text = "Draw a Rectangle! ";
                    break;
                case 8:
                    lblStart.Text = "Draw a Ellpise ! ";
                    break;
                case 9:
                    lblStart.Text = "Draw a Triangle ! ";
                    break;
                case 10:
                    lblStart.Text = "Draw a Isosceles ! ";
                    break;
                case 11:
                    lblStart.Text = "Draw a Diamond ! ";
                    break;
            }
        }

        /// <summary>
        /// Draw some shaped graphics or lines which are auto-size and can be filled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingBoard_MouseUp(object sender, MouseEventArgs e)
        {
            // Definition the position of endPt.X and endPt.Y
            endPt.X = e.X;
            endPt.Y = e.Y;
            endPt = e.Location;

            // Determine the color of the SolidBrush to fill the figures
            SolidBrush brush = new SolidBrush(pen.Color);
            
            // Draw a line
            if (flag == true && type == 1)
            {
                g.DrawLine(pen, beginPt, endPt);
            }

            // Draw a rectangle
            if (flag == true && type == 2)
            {
                if (beginPt.X < endPt.X && beginPt.Y < endPt.Y)
                {
                    g.DrawRectangle(pen, beginPt.X, beginPt.Y, endPt.X - beginPt.X, endPt.Y - beginPt.Y);
                }
                else if (beginPt.X < endPt.X && beginPt.Y > endPt.Y)
                {
                    g.DrawRectangle(pen, beginPt.X, endPt.Y, endPt.X - beginPt.X, beginPt.Y - endPt.Y);
                }
                else if (beginPt.X > endPt.X && beginPt.Y < endPt.Y)
                {
                    g.DrawRectangle(pen, endPt.X, beginPt.Y, beginPt.X - endPt.X, endPt.Y - beginPt.Y);
                }
                else if (beginPt.X > endPt.X && beginPt.Y > endPt.Y)
                {
                    g.DrawRectangle(pen, endPt.X, endPt.Y, Math.Abs(endPt.X - beginPt.X), Math.Abs(endPt.Y - beginPt.Y));
                }
            }

            // Draw a ellipse
            if (flag == true && type == 3)
            {
                if (beginPt.X < endPt.X && beginPt.Y < endPt.Y)
                {
                    g.DrawEllipse(pen, beginPt.X, beginPt.Y, endPt.X - beginPt.X, endPt.Y - beginPt.Y);
                }
                else if (beginPt.X < endPt.X && beginPt.Y > endPt.Y)
                {
                    g.DrawEllipse(pen, beginPt.X, endPt.Y, endPt.X - beginPt.X, beginPt.Y - endPt.Y);
                }
                else if (beginPt.X > endPt.X && beginPt.Y < endPt.Y)
                {
                    g.DrawEllipse(pen, endPt.X, beginPt.Y, beginPt.X - endPt.X, endPt.Y - beginPt.Y);
                }
                else if (beginPt.X > endPt.X && beginPt.Y > endPt.Y)
                {
                    g.DrawEllipse(pen, endPt.X, endPt.Y, Math.Abs(endPt.X - beginPt.X), Math.Abs(endPt.Y - beginPt.Y));
                }
            }

            // Draw a Right-Triangle
            if (flag == true && type == 4)
            {
                Point tA = new Point(beginPt.X, beginPt.Y);
                Point tB = new Point(beginPt.X, endPt.Y);
                Point tC = new Point(endPt.X, endPt.Y);
                Point[] tr = { tA, tB, tC };
                g.DrawPolygon(pen, tr);
            }

            // Draw a triangle
            if (flag == true && type == 5)
            {
                Point a = new Point(beginPt.X, endPt.Y);
                Point b = new Point(beginPt.X + (endPt.X - beginPt.X) / 2, beginPt.Y);
                Point c = new Point(endPt.X, endPt.Y);
                Point[] array = { a, b, c };
                g.DrawPolygon(pen, array);
            }

            // Draw a rhombus
            if (flag == true && type == 6)
            {
                Point PtA = new Point(beginPt.X, beginPt.Y + (endPt.Y - beginPt.Y) / 2);
                Point PtB = new Point(beginPt.X + (endPt.X - beginPt.X) / 2, beginPt.Y);
                Point PtC = new Point(endPt.X, beginPt.Y + (endPt.Y - beginPt.Y) / 2);
                Point PtD = new Point(beginPt.X + (endPt.X - beginPt.X) / 2, endPt.Y);
                Point[] PtT = { PtA, PtB, PtC, PtD };
                g.DrawPolygon(pen, PtT);
            }

            // Create a auto-size textbox
            if (flag == true && type == 8)
            {
                    picDrawArea.Cursor = Cursors.SizeNWSE; // Change the cursor of picDrawArea
                    TextBox txt = new TextBox(); // New a textbnox
                    this.picDrawArea.Controls.Add(txt); // Add a Controls - TetxBox.
                    txt.Multiline = true;
                    txt.Location = beginPt; // Determine Textbox's location.
                    txt.Size = new Size(endPt.X - beginPt.X, endPt.Y - beginPt.Y); // Determine TextBox's Size
                    this.ActiveControl = txt;
                    txt.BorderStyle = BorderStyle.Fixed3D;
                    txt.Font = new Font("Times New Roman", 16); // Change the font of TextBox
                    // txt.BackColor = Color.Transparent;                
            }

            if (flag == true && ckbFill.Checked)
            {
                // Draw a fill rectangle
                if (type == 2)
                {
                    if (beginPt.X < endPt.X && beginPt.Y < endPt.Y)
                    {
                        g.FillRectangle(brush, beginPt.X, beginPt.Y, endPt.X - beginPt.X, endPt.Y - beginPt.Y);
                    }
                    else if (beginPt.X < endPt.X && beginPt.Y > endPt.Y)
                    {
                        g.FillRectangle(brush, beginPt.X, endPt.Y, endPt.X - beginPt.X, beginPt.Y - endPt.Y);
                    }
                    else if (beginPt.X > endPt.X && beginPt.Y < endPt.Y)
                    {
                        g.FillRectangle(brush, endPt.X, beginPt.Y, beginPt.X - endPt.X, endPt.Y - beginPt.Y);
                    }
                    else if (beginPt.X > endPt.X && beginPt.Y > endPt.Y)
                    {
                        g.FillRectangle(brush, endPt.X, endPt.Y, Math.Abs(endPt.X - beginPt.X), Math.Abs(endPt.Y - beginPt.Y));
                    }
                }

                // Draw a fill ellipse
                if (type == 3)
                {
                    if (beginPt.X < endPt.X && beginPt.Y < endPt.Y)
                    {
                        g.FillEllipse(brush, beginPt.X, beginPt.Y, endPt.X - beginPt.X, endPt.Y - beginPt.Y);
                    }
                    else if (beginPt.X < endPt.X && beginPt.Y > endPt.Y)
                    {
                        g.FillEllipse(brush, beginPt.X, endPt.Y, endPt.X - beginPt.X, beginPt.Y - endPt.Y);
                    }
                    else if (beginPt.X > endPt.X && beginPt.Y < endPt.Y)
                    {
                        g.FillEllipse(brush, endPt.X, beginPt.Y, beginPt.X - endPt.X, endPt.Y - beginPt.Y);
                    }
                    else if (beginPt.X > endPt.X && beginPt.Y > endPt.Y)
                    {
                        g.FillEllipse(brush, endPt.X, endPt.Y, Math.Abs(endPt.X - beginPt.X), Math.Abs(endPt.Y - beginPt.Y));
                    }
                }

                // Draw a fill Right-Triangle
                if (type == 4)
                {
                    Point PTA = new Point(beginPt.X, beginPt.Y);
                    Point PTB = new Point(beginPt.X, endPt.Y);
                    Point PTC = new Point(endPt.X, endPt.Y);
                    Point[] Ptr = { PTA, PTB, PTC };
                    g.FillPolygon(brush, Ptr);
                }

                // Draw a fill triangle
                if (type == 5)
                {
                    Point Pta = new Point(beginPt.X, endPt.Y);
                    Point Ptb = new Point(beginPt.X + (endPt.X - beginPt.X) / 2, beginPt.Y);
                    Point Ptc = new Point(endPt.X, endPt.Y);
                    Point[] Ptarray = { Pta, Ptb, Ptc };
                    g.FillPolygon(brush, Ptarray);
                }

                // Draw a fill rhombus
                if (type == 6)
                {
                    Point PTa = new Point(beginPt.X, beginPt.Y + (endPt.Y - beginPt.Y) / 2);
                    Point PTb = new Point(beginPt.X + (endPt.X - beginPt.X) / 2, beginPt.Y);
                    Point PTc = new Point(endPt.X, beginPt.Y + (endPt.Y - beginPt.Y) / 2);
                    Point PTd = new Point(beginPt.X + (endPt.X - beginPt.X) / 2, endPt.Y);
                    Point[] PTT = { PTa, PTb, PTc, PTd };
                    g.FillPolygon(brush, PTT);
                }

            } // end CheckBox_Fill

            picDrawArea.Image = image; // Store the picDrawArea's picture in image
            picDrawArea.Refresh();

        } // end void Drawing_Board_Mouseup

        /// <summary>
        /// Add a track to the process of drawing a graph or line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == true)
            {
                // Definition the position of endPt.X and endPt.Y
                endPt.X = e.X;
                endPt.Y = e.Y;
                endPt = e.Location;

                // Beautify the graphics track
                g.SmoothingMode = SmoothingMode.AntiAlias;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;

                // Add the track for the free curve
                if (type == 0)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        pList.Add(e.Location);
                        this.Refresh();
                        pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                        g.DrawCurve(pen, pList.ToArray());
                        picDrawArea.Image = image;
                    }
                }

                // Add the track for the line
                if (type == 1)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        image2 = new Bitmap(image);
                        g2 = Graphics.FromImage(image2);
                        g2.DrawLine(pen, beginPt, endPt);
                        picDrawArea.Image = image2; // Store the picture in image2
                    }
                }

                // Add the track for the Rectangle
                if (type == 2)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        image2 = new Bitmap(image);
                        g2 = Graphics.FromImage(image2);
                        if (beginPt.X < endPt.X && beginPt.Y < endPt.Y)
                        {
                            g2.DrawRectangle(pen, beginPt.X, beginPt.Y, endPt.X - beginPt.X, endPt.Y - beginPt.Y);
                        }
                        else if (beginPt.X < endPt.X && beginPt.Y > endPt.Y)
                        {
                            g2.DrawRectangle(pen, beginPt.X, endPt.Y, endPt.X - beginPt.X, beginPt.Y - endPt.Y);
                        }
                        else if (beginPt.X > endPt.X && beginPt.Y < endPt.Y)
                        {
                            g2.DrawRectangle(pen, endPt.X, beginPt.Y, beginPt.X - endPt.X, endPt.Y - beginPt.Y);
                        }
                        else if (beginPt.X > endPt.X && beginPt.Y > endPt.Y)
                        {
                            g2.DrawRectangle(pen, endPt.X, endPt.Y, Math.Abs(endPt.X - beginPt.X), Math.Abs(endPt.Y - beginPt.Y));
                        }
                        picDrawArea.Image = image2; // Store the picture in image2
                    }
                }

                // Add the track for the Ellipse
                if (type == 3)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        image2 = new Bitmap(image);
                        g2 = Graphics.FromImage(image2);
                        if (beginPt.X < endPt.X && beginPt.Y < endPt.Y)
                        {
                            g2.DrawEllipse(pen, beginPt.X, beginPt.Y, endPt.X - beginPt.X, endPt.Y - beginPt.Y);
                        }
                        else if (beginPt.X < endPt.X && beginPt.Y > endPt.Y)
                        {
                            g2.DrawEllipse(pen, beginPt.X, endPt.Y, endPt.X - beginPt.X, beginPt.Y - endPt.Y);
                        }
                        else if (beginPt.X > endPt.X && beginPt.Y < endPt.Y)
                        {
                            g2.DrawEllipse(pen, endPt.X, beginPt.Y, beginPt.X - endPt.X, endPt.Y - beginPt.Y);
                        }
                        else if (beginPt.X > endPt.X && beginPt.Y > endPt.Y)
                        {
                            g2.DrawEllipse(pen, endPt.X, endPt.Y, Math.Abs(endPt.X - beginPt.X), Math.Abs(endPt.Y - beginPt.Y));
                        }
                        picDrawArea.Image = image2; // Store the picture in image2
                    }
                }

                // Add the track for the Right-Triangle
                if (type == 4)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        image2 = new Bitmap(image);
                        g2 = Graphics.FromImage(image2);
                        Point tA = new Point(beginPt.X, beginPt.Y);
                        Point tB = new Point(beginPt.X, endPt.Y);
                        Point tC = new Point(endPt.X, endPt.Y);
                        Point[] tr = { tA, tB, tC };
                        g2.DrawPolygon(pen, tr);
                        picDrawArea.Image = image2; // Store the picture in image2
                    }
                }

                // Add the track for the Triangle
                if (type == 5)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        image2 = new Bitmap(image);
                        g2 = Graphics.FromImage(image2);
                        Point a = new Point(beginPt.X, endPt.Y);
                        Point b = new Point(beginPt.X + (endPt.X - beginPt.X) / 2, beginPt.Y);
                        Point c = new Point(endPt.X, endPt.Y);
                        Point[] array = { a, b, c };
                        g2.DrawPolygon(pen, array);
                        picDrawArea.Image = image2; // Store the picture in image2
                    }
                }

                // Add the track for the Rhombus
                if (type == 6)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        image2 = new Bitmap(image);
                        g2 = Graphics.FromImage(image2);
                        Point PtA = new Point(beginPt.X, beginPt.Y + (endPt.Y - beginPt.Y) / 2);
                        Point PtB = new Point(beginPt.X + (endPt.X - beginPt.X) / 2, beginPt.Y);
                        Point PtC = new Point(endPt.X, beginPt.Y + (endPt.Y - beginPt.Y) / 2);
                        Point PtD = new Point(beginPt.X + (endPt.X - beginPt.X) / 2, endPt.Y);
                        Point[] PtT = { PtA, PtB, PtC, PtD };
                        g2.DrawPolygon(pen, PtT);
                        picDrawArea.Image = image2; // Store the picture in image2
                    } 
                }

                // Add the track for the Rubber
                if (type == 7)
                {
                    //pen.Color = colorDialog1.Color;
                    pen.Color = Color.White;
                    if (e.Button == MouseButtons.Left)
                    {
                        pList.Add(e.Location);
                        this.Refresh();
                        pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                        g.DrawCurve(pen, pList.ToArray());
                        picDrawArea.Image = image; // Store the picture in image1
                    }

                    // Change the color of pen from the colorDialog
                    pen.Color = colorDialog1.Color; 
                }
                picDrawArea.Refresh();

            } // end if flag
        } // end void DrawingBoard_MouseMove

        /// <summary>
        /// Determine the coordinate of begine when DrawingBoard_MouseDown
        /// Add e.Location into pList and change Bitmap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawingBoard_MouseDown(object sender, MouseEventArgs e)
        {
            // Definition the position of beginPt.X and beginPt.Y
            beginPt.X = e.X;
            beginPt.Y = e.Y;

            pList.Clear();
            pList.Add(e.Location); // Add the e.Location into pList
            beginPt = e.Location;

            // New a Bitmap of Information to package the image
            Bitmap Information = new Bitmap(image); 

            // Insert an object at the top of Stack<Bitmap> - Information
            Undo.Push(Information); 
        } // end void DrawingBoard_MouseDown

        // Select the color of pen
        private void picColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            pen.Color = colorDialog1.Color;
            picPenColor.BackColor = colorDialog1.Color;
        }

        // Change the width of pen and rubber
        private void tckWidth_Scroll(object sender, EventArgs e)
        {
            if (tckWidth.Value == 0)
            {
                flag = false;
                MessageBox.Show("The Pen/Rubber Width Cannot Be Zero");
                lblWidth.Text = "Pen/Rubber-Width: " + tckWidth.Value.ToString();
            }
            else
            {
                flag = true;
                lblWidth.Text = "Pen/Rubber-Width: " + tckWidth.Value.ToString();
                pen.Width = tckWidth.Value;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" C# Outcome_03 V1.0 \n 2019 ©  Liu Shangyuan.All rights reserved. \n " +
                "© 729461836@qq.com. Tel: 13121561120");
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://baike.baidu.com/item/%E7%94%BB%E5%9B%BE/13024320?fr=aladdin");
        }

        // Clear the screen
        private void picNew_Click(object sender, EventArgs e)
        {
            picDrawArea.BackColor = Color.White; // Change the backcolor
            picDrawArea.BackgroundImage = null; // Cancel the DrawArea's BackgroundImage
            picDrawArea.Refresh();
            g.Clear(Color.White);
            picDrawArea.Controls.Clear(); // Clear the controls in draw area
        }

        // New a picture
        private void newNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pList != null && pList.Count > 0)
            {
                DialogResult result = MessageBox.Show("Whether or not to save the picture before new a file？",
                   "Warning", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    savePicture();

                    picDrawArea.BackColor = Color.White;
                    picDrawArea.BackgroundImage = null;
                    picDrawArea.Refresh();
                    g.Clear(Color.White);
                    picDrawArea.Controls.Clear();
                }
                else if (result == DialogResult.No)
                {
                    picDrawArea.BackColor = Color.White;
                    picDrawArea.BackgroundImage = null;
                    picDrawArea.Refresh();
                    g.Clear(Color.White);
                    picDrawArea.Controls.Clear();
                }
            }
        }

         // The method of save the picture as a file on computer
        void savePicture()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "All Bit Image（*.bmp）|*.bmp|Png File(*.png)|*.png"; // Select the file's formate.
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName; // Nmae the picture' name
                Bitmap bmp = new Bitmap(picDrawArea.Image);
                bmp.Save(file);
            }
        }

        // The mothod of openning a picture
        void openPicture()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file1 = openFileDialog1.FileName;
                this.picDrawArea.Image = Image.FromFile(file1);
                //this.DrawArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            }
            // Exception Handling
            try
            {
                string file = openFileDialog1.FileName;
                picDrawArea.Image = Image.FromFile(file);
                //DrawArea.SizeMode = PictureBoxSizeMode.Zoom;
                picDrawArea.BackgroundImageLayout = ImageLayout.None;
                image = Image.FromFile(file);
                g = Graphics.FromImage(image);

            }
            catch
            {

            }
        }

        // Save the picture in computer as a file
        private void saveSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pList.Count == 0)
            {
                MessageBox.Show("There is no pictures to save");
            }
            else
            {
            savePicture();
            }
        }

        // Open a picture to add into pictureBox
        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                openPicture();
            }
            else if (pList != null && pList.Count > 0)
            {
                // Open the file
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    DialogResult result = MessageBox.Show("Whether or not to save the picture before open a file？",
                       "Warning", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        savePicture();
                        // Open the file
                        string file = openFileDialog1.FileName;
                        picDrawArea.Image = Image.FromFile(file);
                        picDrawArea.BackgroundImageLayout = ImageLayout.None;
                        image = Image.FromFile(file);
                        g = Graphics.FromImage(image);
                    }
                    else if (result == DialogResult.No)
                    {
                        string file = openFileDialog1.FileName;
                        picDrawArea.Image = Image.FromFile(file);
                        //DrawArea.SizeMode = PictureBoxSizeMode.Zoom;
                        picDrawArea.BackgroundImageLayout = ImageLayout.None;
                        image = Image.FromFile(file);
                        g = Graphics.FromImage(image);
                    }
                }
                else
                {
                    openPicture();
                }
            }
        }

        // Add the musics into ListBox from computer.
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.InitialDirectory = @"C:\Desktop";  // The default path to the initial open file.
            of.Filter = @"Music Files|*.mp3|Audio File|*.wav|All the Files|*.*"; // The type of file opened is shown below.
            of.ShowDialog(); // Display the designed dialog box

            string[] musics = of.FileNames; // Gets the full path of all files selected in the file box.

            // Store the file name in the listbox and the full path to the generic.
            for (int i = 0; i < musics.Length; i++)
            {
                if (ls.Contains(musics[i]))
                {
                    continue;
                }
                ls.Add(musics[i]); // Store the music in list of ls
                MusicList.Items.Add(Path.GetFileName(musics[i]));
            }
        } // end void btnAdd_Click

        // Begin or Stop the program
        private void btnBeginDraw_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                flag = true;
                picDrawArea.BackColor = Color.White;
                MessageBox.Show("Begin to draw !");
                btnBeginDraw.Text = "Pause";
            }
            else if (flag == true)
            {
                flag = false;
                picDrawArea.BackColor = Color.Red;
                MessageBox.Show("Stop drawing");
                btnBeginDraw.Text = "Continue";
            }
        }

        // The event of DoubleClick the items in MusicList to play music
        private void MusicList_DoubleClick(object sender, EventArgs e)
        {
            if (MusicList.Items.Count <= 0)
            { 
                // Show the music's name in MusicList
                MessageBox.Show("Please select the music file ");
                return;
            }
            // Exception Handling
            try
            {
                MediaPlayer.URL = ls[MusicList.SelectedIndex];
                MediaPlayer.Ctlcontrols.play(); // Play the music
            }
            catch
            {

            }
        } // end void MusicList_DoubleClick

        // Show the color of the pen
        void penColor()
        {
            picPenColor.BackColor = pen.Color;
        }

        /// <summary>
        /// Shortcuts to select colors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblMagenta_Click(object sender, EventArgs e)
        {
            pen.Color = lblMagenta.BackColor;
            penColor();
        }

        private void lblWhite_Click(object sender, EventArgs e)
        {
            pen.Color = lblWhite.BackColor;
            penColor();            
        }

        private void lblBlack_Click(object sender, EventArgs e)
        {
            pen.Color = lblBlack.BackColor;
            penColor();           
        }

        private void lblRed_Click(object sender, EventArgs e)
        {
            pen.Color = lblRed.BackColor;
            penColor();         
        }

        private void lblBlue_Click(object sender, EventArgs e)
        {
            pen.Color = lblBlue.BackColor;
            penColor();           
        }

        private void lblCyan_Click(object sender, EventArgs e)
        {
            pen.Color = lblCyan.BackColor;
            penColor();
        }

        private void lblYellow_Click(object sender, EventArgs e)
        {
            pen.Color = lblYellow.BackColor;
            penColor();            
        }

        private void lblLime_Click(object sender, EventArgs e)
        {
            pen.Color = lblLime.BackColor;
            penColor();          
        }

        private void lblPink_Click(object sender, EventArgs e)
        {
            pen.Color = lblPink.BackColor;
            penColor();         
        }

        private void lblOrange_Click(object sender, EventArgs e)
        {
            pen.Color = lblOrange.BackColor;
            penColor();            
        }


        private void picPencil_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picPencil,"Pencil");
        }

        private void picRubber_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picRubber, "Rubber");
        }

        private void picLine_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picLine, "Line");
        }

        private void picRectangle_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picRectangle, "Rectangle");       
        }

        private void picCircle_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picCircle, "Circle");
        }

        private void picRight_Triangle_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picRight_Triangle, "Right_Triangle");
        }

        private void picTriangle_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picTriangle, "Triangle");
        }

        private void picRhombus_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picRhombus, "Rhombus");
        }

        private void picColor_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picColor, "Color");
        }

        private void picBackColor_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picBackColor, "BackColor");
        }

        private void picNew_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picNew, "New");
        }

        private void picWord_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picWord, "TextBox");
        }

        private void tckWidth_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(tckWidth, "Pen/Rubber-Width");
        }

        private void picUndo_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picUndo, "Undo (Ctrl+Z)");
        }

        private void picRedo_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picRedo, "Redo (Ctrl+R)");
        }

         // Undo the last step
        private void picUndo_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap Information = new Bitmap(image);
                Redo.Push(Information);
                image = Undo.Pop();
                picDrawArea.Image = image;
                g = Graphics.FromImage(image);
                picDrawArea.Refresh();
            }
            catch
            {
            }
        }

        // Redo the last step
        private void picRedo_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap Information = new Bitmap(image);
                Undo.Push(Information);
                image = Redo.Pop();
                picDrawArea.Image = image;
                g = Graphics.FromImage(image);
                picDrawArea.Refresh();
            }
            catch
            {
            }
        }

        private void UndoZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap Information = new Bitmap(image);
                Redo.Push(Information);
                image = Undo.Pop();
                picDrawArea.Image = image;
                g = Graphics.FromImage(image);
                picDrawArea.Refresh();
            }
            catch
            {
            }
        } 

         /// <summary>
         /// Change the width of canvas
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void picLocationY_MouseDown(object sender, MouseEventArgs e)
        {
                sizeFlag = true;
                xPos = e.X;
                yPos = e.Y;
        }

        private void picLocationY_MouseUp(object sender, MouseEventArgs e)
        {
                sizeFlag = false;
        }

        private void picLocationY_MouseMove(object sender, MouseEventArgs e)
        {
            Px = picDrawArea.Size.Width;
            Py = picLocationX_Y.Location.Y - 85;

            if (sizeFlag)
            {
                picLocationY.Top += Convert.ToInt16(e.Y - yPos);
                picDrawArea.Size = new Size(Px, Py);
            }
            picLocationX.Location = new Point(picDrawArea.Size.Width + 258, (picLocationX_Y.Location.Y / 2) + 38);
            picLocationX_Y.Location = new Point(picDrawArea .Size.Width +260, picLocationY.Location.Y);          
        }

        private void redoRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap Information = new Bitmap(image);
                Undo.Push(Information);
                image = Redo.Pop();
                picDrawArea.Image = image;
                g = Graphics.FromImage(image);
                picDrawArea.Refresh();
            }
            catch
            {
            }
        }

         /// <summary>
         /// Change the length of canvas
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void picLocationX_MouseDown(object sender, MouseEventArgs e)
        {
                sizeFlag1 = true;
                xPos1 = e.X;
                yPos1 = e.Y;           
        }

        private void picLocationX_MouseMove(object sender, MouseEventArgs e)
        {
            Px = picLocationX.Location.X - 260;
            Py = picDrawArea.Size.Height;

            if (sizeFlag1)
            {
                picLocationX.Left += Convert.ToInt16(e.X - xPos1);
                picDrawArea.Size = new Size(Px, Py);
            }
            picLocationY.Location = new Point((picLocationX.Location.X /2) + 128, picLocationX_Y.Location.Y);
            picLocationX_Y.Location = new Point(picLocationX.Location.X, picLocationX_Y.Location.Y);
        }

        private void picLocationX_MouseUp(object sender, MouseEventArgs e)
        {
                sizeFlag1 = false;
        }

        /// <summary>
        /// Change the size of canvas freely
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picLocationX_Y_MouseDown(object sender, MouseEventArgs e)
        {
                sizeFlag2 = true;
                xPos2 = e.X;
                yPos2 = e.Y;
        }

        private void picLocationX_Y_MouseUp(object sender, MouseEventArgs e)
        {
            sizeFlag2 = false;
        }

        private void picLocationX_Y_MouseMove(object sender, MouseEventArgs e)
        {
            Px = picLocationX_Y.Location.X - 260;
            Py = picLocationY.Location.Y - 85;

            if (sizeFlag2)
            {
                picLocationX_Y.Top += Convert.ToInt16(e.Y - xPos2);
                picLocationX_Y.Left += Convert.ToInt16(e.X - xPos2);
                picDrawArea.Size = new Size(Px, Py);
            }
            picLocationX.Location = new Point(picDrawArea.Size.Width + 258, (picLocationX_Y.Location.Y / 2) + 39);
            picLocationY.Location = new Point((picLocationX.Location.X / 2) + 128, picLocationX_Y.Location.Y);
        }

        private void DrawArea_Click(object sender, EventArgs e)
        {
            picLocationX.Visible = true;
            picLocationY.Visible = true;
            picLocationX_Y.Visible = true;
        }

        // Play the last music in MusicList
        private void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                // The currently selected item
                int newindex = MusicList.SelectedIndex; 
                MusicList.SelectedIndices.Clear();
                newindex--;
                if (newindex < 0)
                {
                    //count is the number of songs, not the subscript
                    newindex = MusicList.Items.Count - 1;
                }
                MediaPlayer.URL = ls[newindex];
                MusicList.SelectedIndex = newindex;
                MediaPlayer.Ctlcontrols.play();
            }
            catch
            {
            }

        } // end void btnLast_Click

        // Select the BackColor
        private void picBackColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            picDrawArea.BackColor = colorDialog1.Color;
        }

        // Play the next music in MusicList
        private void btnNext_Click(object sender, EventArgs e)
        {
            // The currently selected item
            int index = MusicList.SelectedIndex;
            MusicList.SelectedIndices.Clear();
            index++;
            if (index == MusicList.Items.Count)
            {
                index = 0;
            }
            MusicList.SelectedIndex = index;
            MediaPlayer.URL = ls[index];
            MediaPlayer.Ctlcontrols.play();
        } // end void btnNext_Click

    } // End Class

} // End Appplication



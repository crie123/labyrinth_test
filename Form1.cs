using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace labyrinth
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.picMaze = new System.Windows.Forms.PictureBox();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.solveBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picMaze)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Height";
            // 
            // picMaze
            // 
            this.picMaze.Location = new System.Drawing.Point(12, 96);
            this.picMaze.Name = "picMaze";
            this.picMaze.Size = new System.Drawing.Size(681, 329);
            this.picMaze.TabIndex = 6;
            this.picMaze.TabStop = false;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(34, 53);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(100, 20);
            this.txtWidth.TabIndex = 4;
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(140, 53);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(100, 20);
            this.txtHeight.TabIndex = 5;
            // 
            // solveBtn
            // 
            this.solveBtn.Location = new System.Drawing.Point(365, 50);
            this.solveBtn.Name = "solveBtn";
            this.solveBtn.Size = new System.Drawing.Size(75, 23);
            this.solveBtn.TabIndex = 7;
            this.solveBtn.Text = "Solve";
            this.solveBtn.UseVisualStyleBackColor = true;
            this.solveBtn.Click += new System.EventHandler(this.solveBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(446, 50);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 8;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(719, 456);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.solveBtn);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.txtWidth);
            this.Controls.Add(this.picMaze);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picMaze)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private int _cellhig, _cellwid;
        LabyrinthClass inLabyrinth = new LabyrinthClass(10, 10);
        Bitmap inBitmap = new Bitmap(1, 1);

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void solveBtn_Click(object sender, EventArgs e)
        {
            inLabyrinth.SolveLabyrinth();
            DrawSolve();
            void DrawSolve()
            {
                Brush bluebrush = new SolidBrush(Color.Blue);
                Brush yellowbrush = new SolidBrush(Color.Yellow);
                using (Graphics gr = Graphics.FromImage(inBitmap))
                {
                    gr.SmoothingMode = SmoothingMode.AntiAlias;
                    foreach (CellStruct cell in inLabyrinth._visited)
                    {
                        Point point = new Point(cell.X * _cellwid, cell.Y * _cellwid);
                        Size size = new Size(_cellwid, _cellwid);
                        Rectangle rectangle = new Rectangle(point, size);
                        gr.FillRectangle(yellowbrush, rectangle);
                    }
                    foreach (CellStruct cell in inLabyrinth._solve)
                    {
                        Point point = new Point(cell.X * _cellwid, cell.Y * _cellwid);
                        Size size = new Size(_cellwid, _cellwid);
                        Rectangle rectangle = new Rectangle(point, size);
                        gr.FillRectangle(bluebrush, rectangle);
                    }
                    gr.FillRectangle(new SolidBrush(Color.Green), new Rectangle(new Point(inLabyrinth.start.X * _cellwid, inLabyrinth.start.Y * _cellwid), new Size(_cellwid, _cellwid)));
                    gr.FillRectangle(new SolidBrush(Color.Red), new Rectangle(new Point(inLabyrinth.start.X * _cellwid, inLabyrinth.start.Y * _cellwid), new Size(_cellwid, _cellwid)));
                }
                picMaze.Image = inBitmap;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Сохранить как...";
            savefile.OverwritePrompt = true;
            savefile.CheckPathExists = true;
            savefile.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
            savefile.ShowHelp = true;
            if(savefile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picMaze.Image.Save(savefile.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch
                {
                    MessageBox.Show("Нельзя сохранить файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int wid = 0;
            int hig = 0;
            //проверка размеров
            try
            {
                wid = int.Parse(txtWidth.Text);
                hig = int.Parse(txtHeight.Text);

                if (wid == 0 || hig == 0)
                {
                    throw new FormatException();
                }

            }
            catch (System.FormatException)
            {
                string message = "Введи больше 0";
                string caption = "Ошибка ввода";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                txtWidth.Text = "10";
                txtHeight.Text = "10";

                return;
            }
            int oddW = 0;
            int oddH = 0;

            //случай с нечетными размерами
            if (wid % 2 != 0 && wid != 0)
            {
                oddW = 1;
            }
            if (hig % 2 != 0 && hig != 0)
            {
                oddH = 1;
            }
            
            //вычисляем ширину ячейки для масштаба

            _cellwid = picMaze.ClientSize.Width / (wid + 2);
            _cellhig = picMaze.ClientSize.Height / (hig + 2);

            //минимальный размер ячейки чтобы глаза не лопнули нахер
            int CellMin = 10;
            if (_cellwid < CellMin)
            {
                _cellwid = CellMin;
                _cellhig = _cellwid;
            }
            else if (_cellhig < CellMin)
            {
                _cellhig = CellMin;
                _cellwid = _cellhig;
            }
            else if (_cellwid > _cellhig) _cellwid = _cellhig;
            else _cellhig = _cellwid;


            LabyrinthClass lab = new LabyrinthClass(wid, hig);

            //обрабатываем прорисовку финиша при нечетных размерах
            lab.finish.X = lab.finish.X + oddW;
            lab.finish.Y = lab.finish.Y + oddH;
            lab.LabyrinthCreation();
            DrawMaze();

            inLabyrinth = lab;

            void DrawMaze()
            {
                inBitmap.Dispose();
                //захват стенки и финиша
                Bitmap bm = new Bitmap(
                    _cellwid * (lab.finish.X + 2),
                    _cellhig * (lab.finish.Y + 2), System.Drawing.Imaging.PixelFormat.Format16bppRgb555);

                Brush whiteBrush = new SolidBrush(Color.White);
                Brush blackBrush = new SolidBrush(Color.Black);

                using (Graphics gr = Graphics.FromImage(bm))
                {
                    gr.SmoothingMode = SmoothingMode.AntiAlias;
                    for (var i = 0; i < lab._cells.GetUpperBound(0); i++)
                        for (var j = 0; j < lab._cells.GetUpperBound(1); j++)
                        {
                            Point point = new Point(i * _cellwid, j * _cellwid);
                            Size size = new Size(_cellwid, _cellwid);
                            Rectangle rec = new Rectangle(point, size);
                            if (lab._cells[i, j]._isCell)
                            {
                                gr.FillRectangle(whiteBrush, rec);
                            }
                            else
                            {
                                gr.FillRectangle(blackBrush, rec);
                            }
                        }
                    gr.FillRectangle(new SolidBrush(Color.Green),                                   //заливаем старт зеленым
                        new Rectangle(new Point(lab.start.X * _cellwid, lab.start.Y * _cellwid),
                        new Size(_cellwid, _cellwid)));
                    gr.FillRectangle(new SolidBrush(Color.Red),                                    //а финиш красным
                        new Rectangle(new Point(lab.finish.X * _cellwid, lab.finish.Y * _cellwid),
                        new Size(_cellwid, _cellwid)));
                }
                picMaze.Image = bm;
                inBitmap = bm;//отображаем картинку
            }
        }
    }
}
                
    


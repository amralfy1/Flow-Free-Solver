using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flow_Solver
{
    public partial class MainMenu : Form
    {
        
        public MainMenu()
        {
            
            InitializeComponent();
            
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            
            
    }

        public static int grid_size;
        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public struct point
        {
            public int x, y, index;
            public char c;
            public Color color;

        };
        public static int lines;
        public static float xspace, yspace;
        public void draw()
        {
            Graphics gr = panel4.CreateGraphics();
            Pen mypn = new Pen(Brushes.White, 5);
            lines = grid_size;
            float x = 0f;
            float y = 0f;
            xspace = panel4.Width / lines;
            yspace = panel4.Height / lines;


            for (int i = 0; i <= lines; i++)
            {
                gr.DrawLine(mypn, x, y, x, panel4.Height);
                x += xspace;
            }

            x = 0f;
            for (int i = 0; i <= lines; i++)
            {
                gr.DrawLine(mypn, x, y, panel4.Width, y);
                y += yspace;
            }
        }
        

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            grid_size = 5;
            panel3.Visible = true;
            panel4.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            draw();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            grid_size = 6;
            panel3.Visible = true;
            panel4.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            draw();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            grid_size = 7;
            panel3.Visible = true;
            panel4.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            draw();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            grid_size = 8;
            panel3.Visible = true;
            panel4.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            draw();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            grid_size = 9;
            panel3.Visible = true;
            panel4.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = false;
            panel1.Visible = true;
            panel2.Visible = false;
            panel4.Invalidate();
        }
        point[,] arr;
        point[,] arr1;
        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            arr = new point[lines, lines];
            arr1 = new point[lines, lines];
            for(int i=0;i<lines;i++)
            {
                for(int j=0;j<lines;j++)
                {
                    arr[i, j].c = '.';
                }
            }
            float new_x = xspace / 2;
            float new_y = yspace / 2;

            float c_xspace = 0;
            float c_yspace = 0;

            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < lines; j++)
                {
                    arr[i, j].x = (int)(new_x + c_xspace);
                    arr1[i, j].x = (int)(new_x + c_xspace) + 415;
                    arr[i, j].y = (int)(new_y + c_yspace);
                    arr1[i, j].y = (int)(new_y + c_yspace) + 40;

                    c_xspace += xspace;
                }
                c_xspace = 0;
                c_yspace += yspace;
            }
            Color r = Color.Red;
            dic.Add(r, 'R');
            Color b = Color.Blue;
            dic.Add(b, 'B');
            Color g = Color.Green;
            dic.Add(g, 'G');
            Color y = Color.Yellow;
            dic.Add(y, 'Y');
            Color v = Color.Violet;
            dic.Add(v, 'V');
            Color lg = Color.LightGreen;
            dic.Add(lg, 'L');
            Color c = Color.Cyan;
            dic.Add(c, 'C');
            Color w = Color.Brown;
            dic.Add(w, 'W');
            Color t = Color.White;
            dic.Add(t, 'T');
            Color p = Color.Pink;
            dic.Add(p, 'P');


            dic1.Add('R', r);
            
            dic1.Add('B',b);
            
            dic1.Add('G', g);

            dic1.Add('Y', y);

            dic1.Add('V', v);

            dic1.Add('L', lg);

            dic1.Add('C', c);

            dic1.Add('W', w);

            dic1.Add('T', t);

            dic1.Add('P', p);
        }

        Dictionary<Color, char> dic = new Dictionary<Color, char>();
        Dictionary<char, Color> dic1 = new Dictionary<char, Color>();
        Color cl = new Color();
        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;

            for (int i = 0; i < lines; i++) 
            {
                for (int j = 0; j < lines; j++)
                {

                    if (X >= (int)arr1[i,j].x-xspace/2&& X <= (int)arr1[i, j].x + xspace/2)
                    {
                        if (Y >= (int)arr1[i, j].y - yspace/2 && Y <= (int)arr1[i, j].y + yspace/2)
                        {
                            arr[i, j].c = dic[cl];
                            arr[i, j].color = cl;
                            Graphics g = panel4.CreateGraphics();
                            Pen p = new Pen(Color.Black);
                            SolidBrush sb = new SolidBrush(arr[i, j].color);
                            g.DrawEllipse(p, arr[i, j].x-xspace/2+10, arr[i, j].y - yspace / 2+10, xspace-20, yspace-20);
                            g.FillEllipse(sb, arr[i, j].x - xspace / 2+10, arr[i, j].y - yspace / 2+10, xspace-20, yspace-20);

                            break;
                        }
                    }
                }
            }
        }

        
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            cl = Color.Red;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            cl = Color.Blue;
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            cl = Color.Brown;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            cl = Color.Cyan;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            cl = Color.Green;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            cl = Color.LightGreen;
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            cl = Color.Pink;
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            cl = Color.White;
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            cl = Color.Yellow;
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            cl = Color.Violet;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;

            Bitmap FirstImage;
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\Libraries\\Pictures";
            openFileDialog1.Filter = "*.BMP;*.PPM;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            string fname = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        string ext = Path.GetExtension(openFileDialog1.FileName);
                        fname = openFileDialog1.FileName;
                        using (myStream)
                        {
                            int factor = 5; // number of rows 
                            lines = 5;
                            FirstImage = new Bitmap(myStream);
                            Bitmap bit = FirstImage;
                            int x = bit.Width / factor;
                            int y = bit.Height / factor;
                            int a = x / 2;
                            int b = y / 2;
                            Dictionary<Tuple<Tuple<int, int>, int>, char> dict = new Dictionary<Tuple<Tuple<int, int>, int>, char>();
                            char temp = 'A';
                            char[,] arr = new char[factor, factor];
                            Dictionary<char, Color> diction = new Dictionary<char, Color>();
                            for (int j = 0; j < factor; j++)
                            {
                                for (int i = 0; i < factor; i++)
                                {
                                    int r = bit.GetPixel(a + i * x, b + j * y).R;
                                    int G1 = bit.GetPixel(a + i * x, b + j * y).G;
                                    int c = bit.GetPixel(a + i * x, b + j * y).B;
                                    Tuple<Tuple<int, int>, int> n = new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(r, G1), c);
                                    if (r < 30 && G1 < 30 && c < 30)
                                    {
                                        arr[j, i] = '.';
                                    }
                                    else
                                    {
                                        if (dict.ContainsKey(n))
                                        {
                                            arr[j, i] = dict[n];
                                        }
                                        else
                                        {
                                            arr[j, i] = temp;
                                            diction.Add(arr[j, i], bit.GetPixel(a + i * x, b + j * y));
                                            dict.Add(n, temp);
                                            int q = Convert.ToInt32(temp);
                                            q++;
                                            temp = Convert.ToChar(q);
                                        }
                                    }
                                }
                            }
                            FileStream fs = new FileStream("ali.txt", FileMode.Create);
                            StreamWriter sw = new StreamWriter(fs);
                            FreeFlowSolver temp10 = new FreeFlowSolver(arr, factor);
                            node[,] solved = temp10.solved;
                            for (int i = 1; i <= factor; i++)
                            {
                                for (int j = 1; j <= factor; j++)
                                {
                                    solved[i, j].color = diction[solved[i, j].c];
                                }
                            }
                            for(int i=0;i<lines;i++)
                            {
                                for(int j = 0; j < lines; j++)
                                {
                                    this.arr[i, j].c = solved[i+1, j+1].c;
                                    this.arr[i, j].color = solved[i + 1, j + 1].color;
                                    this.arr[i, j].index = solved[i + 1, j + 1].step;
                                }
                            }
                            panel3.Visible = true;
                            panel4.Visible = true;
                            Graphics g = panel4.CreateGraphics();

                            float x1, x2, y1, y2;


                            for (int i = 0; i < lines; i++)
                            {
                                for (int j = 0; j < lines; j++)
                                {
                                    if (this.arr[i, j].c == '-')
                                    {
                                        continue;
                                    }
                                    if (this.arr[i, j].index == 1)
                                    {
                                        Color c = this.arr[i, j].color;
                                        SolidBrush brush = new SolidBrush(c);
                                        Pen p = new Pen(brush, xspace/2-10);

                                        x1 = this.arr[i, j].x; x2 = this.arr[i, j].x; y1 = this.arr[i, j].y; y2 = this.arr[i, j].y;
                                        int temp_i = i, temp_j = j;
                                        char hold = this.arr[temp_i, temp_j].c;
                                        int counter = 2;
                                        while (temp_i < lines && temp_j < lines)
                                        {
                                            if (temp_j + 1 < lines && this.arr[temp_i, temp_j + 1].c == hold && this.arr[temp_i, temp_j + 1].index == counter)
                                            {
                                                int loop = this.arr[temp_i, temp_j + 1].x - this.arr[temp_i, temp_j].x;
                                                for (int k = 0; k < loop; k++)
                                                {
                                                    x2++;
                                                    g.DrawLine(p, x1 - xspace/5, y1, x2, y2);
                                                    Thread.Sleep(5);
                                                }

                                                counter++;
                                                this.arr[temp_i, temp_j].c = '-';
                                                this.arr[temp_i, temp_j + 1].c = '-';
                                                temp_j++;

                                                x1 = x2;
                                                y1 = y2;

                                            }

                                            else if (temp_i + 1 < lines && this.arr[temp_i + 1, temp_j].c == hold && this.arr[temp_i + 1, temp_j].index == counter)
                                            {
                                                int loop = this.arr[temp_i + 1, temp_j].y - this.arr[temp_i, temp_j].y;
                                                for (int k = 0; k < loop; k++)
                                                {
                                                    y2++;
                                                    g.DrawLine(p, x1, y1 - xspace/5, x2, y2);
                                                    Thread.Sleep(5);
                                                }
                                                counter++;
                                                this.arr[temp_i, temp_j].c = '-';
                                                this.arr[temp_i + 1, temp_j].c = '-';
                                                temp_i++;
                                                x1 = x2;
                                                y1 = y2;

                                            }

                                            else if (temp_j - 1 >= 0 && this.arr[temp_i, temp_j - 1].c == hold && this.arr[temp_i, temp_j - 1].index == counter)
                                            {
                                                int loop = this.arr[temp_i, temp_j].x - this.arr[temp_i, temp_j - 1].x;
                                                for (int k = 0; k < loop; k++)
                                                {
                                                    x2--;
                                                    g.DrawLine(p, x1 + xspace/5, y1, x2, y2);
                                                    Thread.Sleep(5);
                                                }
                                                counter++;
                                                this.arr[temp_i, temp_j].c = '-';
                                                this.arr[temp_i, temp_j - 1].c = '-';
                                                temp_j--;
                                                x1 = x2;
                                                y1 = y2;

                                            }

                                            else if (temp_i - 1 >= 0 && this.arr[temp_i - 1, temp_j].c == hold && this.arr[temp_i - 1, temp_j].index == counter)
                                            {
                                                int loop = this.arr[temp_i, temp_j].y - this.arr[temp_i - 1, temp_j].y;
                                                for (int k = 0; k < loop; k++)
                                                {
                                                    y2--;
                                                    g.DrawLine(p, x1, y1 + xspace/5, x2, y2);
                                                    Thread.Sleep(5);
                                                }
                                                counter++;
                                                this.arr[temp_i, temp_j].c = '-';
                                                this.arr[temp_i - 1, temp_j].c = '-';
                                                temp_i--;
                                                x1 = x2;
                                                y1 = y2;

                                            }
                                            else
                                            {
                                                break;
                                            }
                                            continue;

                                        }
                                    }

                                }
                            }
                            sw.Close();
                            fs.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            //for (int i = 0; i < lines; i++)
            //{
            //    string s = "";
            //    for (int j = 0; j < lines; j++)
            //    {
            //        s+= arr[i, j].c+" ";
                    
            //    }
            //    //MessageBox.Show(s);
            //}

         
            
            

            //arr[0, 4].c = 'R';
            //arr[0, 4].index = 1;
            //arr[0, 4].color = Color.Red;
            //arr[0, 3].c = 'R';
            //arr[0, 3].index = 2;
            //arr[0, 3].color = Color.Red;
            //arr[0, 2].c = 'R';
            //arr[0, 2].index = 3;
            //arr[0, 2].color = Color.Red;
            //arr[1, 2].c = 'R';
            //arr[1, 2].index = 4;
            //arr[1, 2].color = Color.Red;
            //arr[2, 2].c = 'R';
            //arr[2, 2].index = 5;
            //arr[2, 2].color = Color.Red;
            //arr[3, 2].c = 'R';
            //arr[3, 2].index = 6;
            //arr[3, 2].color = Color.Red;
            //arr[4, 2].c = 'R';
            //arr[4, 2].index = 7;
            //arr[4, 2].color = Color.Red;


            //arr[1, 0].c = 'L';
            //arr[1, 0].index = 1;
            //arr[1, 0].color = Color.Blue;
            //arr[2, 0].c = 'L';
            //arr[2, 0].index = 2;
            //arr[2, 0].color = Color.Blue;
            //arr[3, 0].c = 'L';
            //arr[3, 0].index = 3;
            //arr[3, 0].color = Color.Blue;
            //arr[4, 0].c = 'L';
            //arr[4, 0].index = 4;
            //arr[4, 0].color = Color.Blue;


            //arr[1, 1].c = 'F';
            //arr[1, 1].index = 1;
            //arr[1, 1].color = Color.Green;
            //arr[2, 1].c = 'F';
            //arr[2, 1].index = 2;
            //arr[2, 1].color = Color.Green;
            //arr[3, 1].c = 'F';
            //arr[3, 1].index = 3;
            //arr[3, 1].color = Color.Green;
            //arr[4, 1].c = 'F';
            //arr[4, 1].index = 4;
            //arr[4, 1].color = Color.Green;

            //arr[0, 0].c = 'H';
            //arr[0, 0].index = 1;
            //arr[0, 0].color = Color.Sienna;
            //arr[0, 1].c = 'H';
            //arr[0, 1].index = 2;
            //arr[0, 1].color = Color.Sienna;


            //arr[1, 3].c = 'S';
            //arr[1, 3].index = 1;
            //arr[1, 3].color = Color.Olive;
            //arr[2, 3].c = 'S';
            //arr[2, 3].index = 2;
            //arr[2, 3].color = Color.Olive;
            //arr[3, 3].c = 'S';
            //arr[3, 3].index = 3;
            //arr[3, 3].color = Color.Olive;
            //arr[4, 3].c = 'S';
            //arr[4, 3].index = 4;
            //arr[4, 3].color = Color.Olive;
            //arr[4, 4].c = 'S';
            //arr[4, 4].index = 5;
            //arr[4, 4].color = Color.Olive;


            //arr[1, 4].c = 'D';
            //arr[1, 4].index = 1;
            //arr[1, 4].color = Color.Salmon;
            //arr[2, 4].c = 'D';
            //arr[2, 4].index = 2;
            //arr[2, 4].color = Color.Salmon;
            //arr[3, 4].c = 'D';
            //arr[3, 4].index = 3;
            //arr[3, 4].color = Color.Salmon;
            char[,] arr2 = new char[lines, lines];
            for(int i=0;i<lines;i++)
            {
                for(int j = 0; j < lines; j++)
                {
                    arr2[i, j] = arr[i, j].c;
                }
            }
            FreeFlowSolver temp = new FreeFlowSolver(arr2, lines);
            for(int i=0;i<lines;i++)
            {
                for(int j=0;j<lines;j++)
                {
                    arr[i, j].c = temp.solved[i+1, j+1].c;
                    arr[i, j].color = dic1[temp.solved[i+1, j+1].c];
                    arr[i, j].index = temp.solved[i+1, j+1].step;
                    //MessageBox.Show(arr[i, j].index.ToString());
                }
            }
            Graphics g = panel4.CreateGraphics();

            float x1, x2, y1, y2;

            
            for (int i = 0; i < lines; i++)
            {
                for (int j = 0; j < lines; j++)
                {
                    if (arr[i, j].c == '-')
                    {
                        continue;
                    }
                    if (arr[i, j].index == 1)
                    {
                        Color c = arr[i, j].color;
                        SolidBrush brush = new SolidBrush(c);
                        Pen p = new Pen(brush, xspace/3f);

                        x1 = arr[i, j].x; x2 = arr[i, j].x; y1 = arr[i, j].y; y2 = arr[i, j].y;
                        int temp_i = i, temp_j = j;
                        char hold = arr[temp_i, temp_j].c;
                        int counter = 2;
                        while (temp_i < lines && temp_j < lines)
                        {
                            if (temp_j + 1 < lines && arr[temp_i, temp_j + 1].c == hold && arr[temp_i, temp_j + 1].index == counter)
                            {
                                int loop = arr[temp_i, temp_j + 1].x - arr[temp_i, temp_j].x;
                                for (int k = 0; k < loop; k++)
                                {
                                    x2++;
                                    g.DrawLine(p, x1 - xspace/5, y1, x2, y2);
                                    Thread.Sleep(2);
                                }

                                counter++;
                                arr[temp_i, temp_j].c = '-';
                                arr[temp_i, temp_j + 1].c = '-';
                                temp_j++;

                                x1 = x2;
                                y1 = y2;

                            }

                            else if (temp_i + 1 < lines && arr[temp_i + 1, temp_j].c == hold && arr[temp_i + 1, temp_j].index == counter)
                            {
                                int loop = arr[temp_i + 1, temp_j].y - arr[temp_i, temp_j].y;
                                for (int k = 0; k < loop; k++)
                                {
                                    y2++;
                                    g.DrawLine(p, x1, y1 - xspace/5, x2, y2);
                                    Thread.Sleep(2);
                                }
                                counter++;
                                arr[temp_i, temp_j].c = '-';
                                arr[temp_i + 1, temp_j].c = '-';
                                temp_i++;
                                x1 = x2;
                                y1 = y2;

                            }

                            else if (temp_j - 1 >= 0 && arr[temp_i, temp_j - 1].c == hold && arr[temp_i, temp_j - 1].index == counter)
                            {
                                int loop = arr[temp_i, temp_j].x - arr[temp_i, temp_j - 1].x;
                                for (int k = 0; k < loop; k++)
                                {
                                    x2--;
                                    g.DrawLine(p, x1 + xspace/5, y1, x2, y2);
                                    Thread.Sleep(2);
                                }
                                counter++;
                                arr[temp_i, temp_j].c = '-';
                                arr[temp_i, temp_j - 1].c = '-';
                                temp_j--;
                                x1 = x2;
                                y1 = y2;

                            }

                            else if (temp_i - 1 >= 0 && arr[temp_i - 1, temp_j].c == hold && arr[temp_i - 1, temp_j].index == counter)
                            {
                                int loop = arr[temp_i, temp_j].y - arr[temp_i - 1, temp_j].y;
                                for (int k = 0; k < loop; k++)
                                {
                                    y2--;
                                    g.DrawLine(p, x1, y1 + xspace/5, x2, y2);
                                    Thread.Sleep(2);
                                }
                                counter++;
                                arr[temp_i, temp_j].c = '-';
                                arr[temp_i - 1, temp_j].c = '-';
                                temp_i--;
                                x1 = x2;
                                y1 = y2;

                            }
                            else
                            {
                                break;
                            }
                            continue;

                        }
                    }

                }
            }
        }
    }
}

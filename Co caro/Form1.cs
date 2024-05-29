using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_caro
{
    public partial class Form1 : Form
    {

        #region Properties
        Manager DrawBoard;
        #endregion

        public Form1()
        {
            InitializeComponent();

            DrawBoard = new Manager(ChessBoard, TBName, pictureBox1);

            Newgame();
        }

        #region
        void Newgame()
        {
            DrawBoard.DrawBoard(); 
        }

        void Quit()
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            /*WindowState = FormWindowState.Maximized;//Hien thi fullscreen khi chay ung dung
            TopMost = true;*/
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Newgame();
        }

        private void quitGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
               e.Cancel = true;
        }

        #endregion

    }
}

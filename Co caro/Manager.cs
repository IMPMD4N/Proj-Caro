using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_caro
{
    public class Manager
    {

        #region Properties
        private Panel chessboard;//Tao panel de luu tru

        public Panel Chessboard
        {
            get { return chessboard; }
            set { chessboard = value; }
        }

        private List<Player> player;//Tao list player de co nhieu nguoi choi

        public List<Player> Player 
        {
            get { return player; }
            set { player = value; }
        }

        private int currentPlayer;//Danh dau nguoi choi hien tai

        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        private TextBox playerName;

        public TextBox PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        private PictureBox playermark;

        public PictureBox PlayerMark
        {
            get { return playermark; }
            set { playermark = value; }
        }


        private List<List<Button>> matrix;
        public List<List<Button>> Matrix 
        {
            get { return matrix; }
            set { matrix = value; }
        }


        #endregion

        #region Initialize
        public Manager(Panel chessboard, TextBox playerName, PictureBox mark)//Tao ham dung
        {
            this.Chessboard = chessboard;
            this.PlayerName = playerName;
            this.PlayerMark = mark;
            this.Player = new List<Player>()
                { 
                    new Player ("PMD4N", Image.FromFile(Application.StartupPath + "\\Resources\\x.png")),
                    new Player ("ATU", Image.FromFile(Application.StartupPath + "\\Resources\\o.png"))
                };
            CurrentPlayer = 0;
            ChangePlayer();
        }


        #endregion

        #region Methods
        public void DrawBoard()//Ve bang; Ve cac o lan luot theo tung hang
        {
            Chessboard.Enabled = true;
            Chessboard.Controls.Clear();

            Matrix = new List<List<Button>>();
            
            Button oldButton = new Button()
            {
                Width = 0,
                Location = new Point(0, 0)
            };//Khoi tao oldButton de luu lai nut cu
            for (int i = 0; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.CHESS_BOARD_HEIGHT; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,//Kich thuoc tung nut
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + Cons.CHESS_WIDTH, oldButton.Location.Y), //Toa do nut moi(theo hang) = (X + chieu ngang nut cu, Y)
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };// Tao thanh 1 hang nut

                    btn.Click += btn_Click;//Tao event khi an vao nut

                    Chessboard.Controls.Add(btn);//Them cac nut vao panel Chessboard

                    Matrix[i].Add(btn); //Luu nut vao trong mang Matrix

                    oldButton = btn; // Cap nhat nut cu sau khi ve xong hang
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);// Toa do nut moi(theo cot) = (X, Y + chieu doc nut cu)
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null) //Tranh bam de vao nut da duoc bam
                return;

            Mark(btn);

            ChangePlayer();

            if (IsEndgame(btn))
            {
                Endgame();
            }
        }

        private void Endgame()
        {
            MessageBox.Show(Player[CurrentPlayer].Name + " Thắng!");
        }
        private bool IsEndgame(Button btn)
        {
            return IsEndHorizontal(btn) || IsEndVertical(btn) || IsEndPrimary(btn) || IsEndSub(btn);
        }
        private Point GetChessPoint(Button btn)// Lấy toạ độ của nút
        {

            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);

            Point point = new Point(horizontal, vertical);


            return point;
        }
        private bool IsEndHorizontal(Button btn)// Thắng theo hàng ngang
        {
            Point point = GetChessPoint(btn);

            int Countleft = 0;
            for (int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    Countleft++;
                }
                else
                    break;
            }

            int Countright = 0;
            for (int i = point.X + 1; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    Countright++;
                }
                else
                    break;
            }

            return Countleft + Countright == 5;

        }

        private bool IsEndVertical(Button btn)// Thắng theo hàng dọc
        {
            Point point = GetChessPoint(btn);

            int Counttop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    Counttop++;
                }
                else
                    break;
            }

            int Countbottom = 0;
            for (int i = point.Y+1; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    Countbottom++;
                }
                else
                    break;
            }
            
            return Counttop + Countbottom == 5;
            
        }

        private bool IsEndPrimary(Button btn)// Thắng theo đường chéo chính
        {
            Point point = GetChessPoint(btn);

            int Counttop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                    break;

                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    Counttop++;
                }
                else
                    break;
            }

            int Countbottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X + i >= Cons.CHESS_BOARD_WIDTH)
                    break;

                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    Countbottom++;
                }
                else
                    break;
            }

            return Counttop + Countbottom == 5;

        }
        private bool IsEndSub(Button btn)// Thắng theo đường chéo phụ
        {
            Point point = GetChessPoint(btn);

            int Counttop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > Cons.CHESS_BOARD_WIDTH || point.Y - i < 0)
                    break;

                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    Counttop++;
                }
                else
                    break;
            }

            int Countbottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_HEIGHT - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X - i < 0)
                    break;

                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    Countbottom++;
                }
                else
                    break;
            }

            return Counttop + Countbottom == 5;
        }
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;

            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
        }

        private void ChangePlayer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name;

            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }
        #endregion

        
    }
}

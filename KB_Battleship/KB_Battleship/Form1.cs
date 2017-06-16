using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KB_Battleship
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        #region declarations
        //mouse graphics for selected avatar - player 1
        bool slct_A1_P1 = false;
        bool slct_A2_P1 = false;
        bool slct_A3_P1 = false;
        //mouse graphics for selected avatar - player 2
        bool slct_A1_P2 = false;
        bool slct_A2_P2 = false;
        bool slct_A3_P2 = false;

        //placing ships - current boat selected
        int boatclick_P1 = 0;
        int boatclick_P2 = 0;
        
        //P1 ship declaration
        Ships PB  = new Ships("PatrolBoat", 2, 0, 0, 0, false, false, 1);
        Ships SUB = new Ships("Submarine", 3, 0, 0, 0, false, false, 2);
        Ships DES = new Ships("Destroyer", 3, 0, 0, 0, false, false, 3);
        Ships BAT = new Ships("Battleship", 4, 0, 0, 0, false, false, 4);
        Ships AIR = new Ships("Aircraft", 5, 0, 0, 0, false, false, 5);

        //P2 ship declaration
        Ships PB2  = new Ships("PatrolBoat", 2, 0, 0, 0, false, false, 1);
        Ships SUB2 = new Ships("Submarine", 3, 0, 0, 0, false, false, 2);
        Ships DES2 = new Ships("Destroyer", 3, 0, 0, 0, false, false, 3);
        Ships BAT2 = new Ships("Battleship", 4, 0, 0, 0, false, false, 4);
        Ships AIR2 = new Ships("Aircraft", 5, 0, 0, 0, false, false, 5);

        Player P1 = new Player();
        Player P2 = new Player();
        #endregion
        
        private void Form1_Load(object sender, EventArgs e)
        {
            pg1_Instructions.Visible = false;
            pg2_Avatar_P1.Visible    = false;
            pg3_SetBoard_P1.Visible  = false;
            pg4_PlayerChoice.Visible = false;
            pg5_Avatar_P2.Visible    = false;
            pg6_SetBoard_P2.Visible  = false;
            pg7_GameTime_COM.Visible = false;
            //pg8_GameTime_P1.Visible  = false;
            //pg9_GameTime_P2.Visible  = false;
            //pg10_GameOver.Visible    = false;
        }

        public class Ships
        {
            protected string Name;
            protected int Size;
            protected int HitCount;
            protected int Index; //Ship Type
            protected Point topLeft = new Point();
            protected bool DirectionVertical;
            protected bool Placed;      //is it placed when placing all ships
            protected bool DeathMessage = false; //did we give a message yet or not
            

            public Ships()
            {
                Name = "Default";
                Size = 1;
                HitCount = 0;
                topLeft.X = 0;
                topLeft.Y = 0;
                DirectionVertical = true;
                Placed = false;
                DeathMessage = false;
            }
            public Ships(string N, int S, int HC, int Px, int Py, bool R, bool P, int I)
            {
                Name = N;
                Size = S;
                HitCount = HC;
                topLeft.X = Px;
                topLeft.Y = Py;
                DirectionVertical = R;
                Placed = P;
                Index = I;
            }
            public string GetName()
            {
                return Name;

            }
            public void SetName(string n)
            {
                Name = n;
            }

            public int GetIndex(int i)
            {
                return Index;
            }

            public int GetSize()
            {
                return Size;
            }
            public void SetSize(int s)
            {
                Size = s;
            }

            public int GetHitCount()
            {
                return HitCount;
            }
            public void SetHitCount(int h)
            {
                if (HitCount <= Size)
                {
                    HitCount = h;
                }
                else
                {
                    throw (new IndexOutOfRangeException());
                }

            }

            public string GetPosition()
            {
                // return Position as a letter and number i.e A1
                int x = topLeft.X;
                int y = topLeft.Y;
                char[] Alphabet = new char[9] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' };

                return Alphabet[x] + y.ToString();

            }
            public void SetTopLeft(int x, int y)
            {
                topLeft.X = x;
                topLeft.Y = y;
            }
            public bool GetDirection()
            {
                //true is vertical, false is horizontal
                return DirectionVertical;
            }
            public void SetDirection(bool D)
            {
                DirectionVertical = D;
            }
            public bool GetPlaced()
            {
                return Placed;
            }
            public void SetPlaced(bool P)
            {
                Placed = P;
            }
            public bool IsDead(Player P)
            {
                if ((HitCount == Size) && (DeathMessage == false))
                {
                    MessageBox.Show("Your" + Name + "is dead.");
                    DeathMessage = true;
                    P.ResetHuntMode();
                    return true;
                }
                else if ((HitCount == Size) && (DeathMessage == true))
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }
            public void Hit()
            {
                HitCount = HitCount + 1;
            }

            public void Rotate()
            {
                //toggles direction vector

                if (DirectionVertical == true)
                    DirectionVertical = false;
                else
                    DirectionVertical = true;
            }
            public void Randomize(Random rnd, int[,] PlayerArray)
            {
                int i, x, y, Orientation, EmptyCounter;

                Orientation = rnd.Next(10);
                if (Orientation <= 4) //horizontal
                {
                    bool AllEmpty = false;

                    while (AllEmpty == false)
                    {
                        i = 0;
                        EmptyCounter = 0;
                        x = rnd.Next(0, 9 - Size);
                        y = rnd.Next(0, 9);
                        if (x - 1 + Size - 1 < 10)
                        {
                            while (i < Size)
                            {
                                //checks if spots empty or not
                                int EmptyChecker;
                                EmptyChecker = PlayerArray[x + i, y];
                                if (EmptyChecker == 0)
                                {
                                    EmptyCounter++;
                                }
                                else
                                {

                                }
                                i++;

                            }
                        }
                        else
                        {

                        }
                        if (EmptyCounter == Size)
                        {
                            AllEmpty = true;
                        }
                        else
                        {

                        }

                        if (AllEmpty == true)
                        {
                            i = 0;
                            while (i < Size)
                            {
                                PlayerArray[x + i, y] = Index;
                                i++;
                            }
                        }
                        else
                        {

                        }
                    }

                }

                else //vertical
                {
                    bool AllEmpty = false;

                    while (AllEmpty == false)
                    {
                        i = 0;
                        EmptyCounter = 0;
                        x = rnd.Next(0, 9);
                        y = rnd.Next(0, 9 - Size);
                        if (x - 1 + Size - 1 < 10)
                        {
                            while (i < Size)
                            {
                                int EmptyChecker;
                                EmptyChecker = PlayerArray[x, y + i];
                                if (EmptyChecker == 0)
                                {
                                    EmptyCounter++;
                                }
                                else
                                {

                                }
                                i++;

                            }
                        }
                        if (EmptyCounter == Size)
                        {
                            AllEmpty = true;
                        }

                        if (AllEmpty == true)
                        {
                            i = 0;
                            while (i < Size)
                            {
                                PlayerArray[x, y + i] = Index;
                                i++;
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
            
            //public void HuntMode(Player P1, Player P2)
            //{
            //    if (getTargetModeHR() == true)
            //    {
                    

            //    }
            //    else if (getTargetModeHL() == true)
            //    {
                    
            //    }
            //    else if (getTargetModeVU() == true)
            //    {
                    
            //    }
            //    else if (getTargetModeVD() == true)
            //    {
                    
            //    }
            //    IsDead();
            //}
        }

        public class Player
        {
            protected string strName;
            protected int iScore;
            protected int iAvatar;
            protected int iHitCount;
            //stores player ship information 
            protected int[,] PlayerArray = new int[10, 10];
            protected bool TargetModeHR;
            protected bool TargetModeHL;
            protected bool TargetModeVU;
            protected bool TargetModeVD;
            protected bool Hunt;
            public void ResetHuntMode()
            {
                setTargetModeHL(false);
                setTargetModeHR(false);
                setTargetModeVD(false);
                setTargetModeVU(false);
                setHunt(false);
            }

            protected Point FirstHit;
            protected Point LastHit;
            protected bool Turn;
            public void SwitchTurn(Player other)
            {
                if (this.getTurn() == false && other.getTurn() == true)
                {
                    this.setTurn(true);
                    other.setTurn(false);
                }
                else
                {
                    this.setTurn(false);
                    other.setTurn(true);
                }
            }
            //list of points for COM to hit
            protected List<Point> ToHit = new List<Point>(4);
            public void AddToHit(int x, int y)
            {
                ToHit.Add(new Point { X = x, Y = y });
            }
            public int getToHitLength()
            {
                return ToHit.Count();
            }
            public Point getToHitPoint()
            {
                return ToHit[0];
            }
            public void RemoveToHitPoint()
            {
                ToHit.RemoveAt(0);
            }
            public void ClearToHitPoints()
            {
                ToHit.Clear();
            }


            public void setFirstHit(int x, int y)
            {
                FirstHit.X = x;
                FirstHit.Y = y;
            }
            public Point getFirstHit()
            {
                return FirstHit;
            }
            public void setLastHit(int x, int y)
            {
                LastHit.X = x;
                LastHit.Y = y;
            }
            public Point getLastHit()
            {
                return LastHit;
            }

            public void setHunt(bool b)
            {
                Hunt = b;
            }
            public bool getHunt()
            {
                return Hunt;
            }

            public Player()
            {
                strName = ("Butthead");
                iScore = 0;
                iAvatar = 1;
                iHitCount = 0;
            }


            public void setTurn(bool b)
            {
                Turn = b;
            }
            public bool getTurn()
            {
                return Turn;
            }
            public void setName(string name)
            {
                strName = name;

            }
            public string getName()
            {
                return strName;
            }

            public void setScore(int score)
            {
                iScore = score;
            }
            public int getScore()
            {
                return iScore;
            }

            public void setAvatar(int avatar)
            {
                iAvatar = avatar;
            }
            public int getAvatar()
            {
                return iAvatar;
            }

            public void setHitCount(int hit)
            {
                iHitCount = hit;
            }
            public int getHitCount()
            {
                return iHitCount;
            }

            public bool isWinner(Player P1, Ships S1, Ships S2, Ships S3, Ships S4, Ships S5)
            {
                //needs win condition 
                if (S1.IsDead(P1) == true && S2.IsDead(P1) == true && S3.IsDead(P1) == true && S4.IsDead(P1) == true && S5.IsDead(P1) == true)
                //I THINK ITS 17????
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            public bool isWinner()
            {
                if (getHitCount() == 17)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public int getPlayerArray(int x, int y)
            {
                return PlayerArray[x, y];
            }
            public int getPlayerArray(Point p)
            {
                int x, y;
                x = p.X;
                y = p.Y;
                return PlayerArray[x, y];
            }
            public void setPlayerArray(int x, int y, int i)
            {
                PlayerArray[x, y] = i;
            }
            public void setPlayerArray(Point p, int i)
            {
                int x, y;
                x = p.X;
                y = p.Y;
                PlayerArray[x, y] = i;
            }
            public int[,] getPlayerArrayAll()
            {
                return PlayerArray;
            }

            public void setTargetModeHR(bool b)
            {
                TargetModeHR = b;
            }
            public bool getTargetModeHR()
            {
                return TargetModeHR;
            }
            public void setTargetModeHL(bool b)
            {
                TargetModeHL = b;
            }
            public bool getTargetModeHL()
            {
                return TargetModeHL;
            }
            public void setTargetModeVU(bool b)
            {
                TargetModeVU = b;
            }
            public bool getTargetModeVU()
            {
                return TargetModeVU;
            }
            public void setTargetModeVD(bool b)
            {
                TargetModeVU = b;
            }
            public bool getTargetModeVD()
            {
                return TargetModeVD;
            }
        }

        public void DrawShipPlacement(int x, int y, Ships S)
        {
            //curent counter for size
            int i = 0;
            if (S.GetPlaced() == false)    //if already placed can't
            {
                //if it is horizontal or vertical
                if (S.GetDirection() == false) //horizontal
                {
                    bool AllEmpty = true;

                    if (x - 1 + S.GetSize() - 1 < 10)
                    {
                        while (i < S.GetSize())
                        {
                            int EmptyChecker;
                            //EmptyChecker = P1Array[x - 1 + i, y - 1];
                            EmptyChecker = P1.getPlayerArray(x - 1 + i, y - 1);
                            if (EmptyChecker != 0)
                            {
                                AllEmpty = false;
                                MessageBox.Show("Temporary: YOU CAN'T DO THAT. THE SHIP IS NOT IN A VALID SPOT.");
                                pb_PlaceShips_P1.Invalidate();
                                return;
                            }
                            i++;

                        }
                    }
                    else
                    {
                        AllEmpty = false;
                        MessageBox.Show("Temporary: YOU CAN'T DO THAT. THE SHIP IS NOT IN A VALID SPOT.");
                        pb_PlaceShips_P1.Invalidate();
                        return;
                    }


                    if (AllEmpty == true)
                    {
                        i = 0;
                        while (i < S.GetSize())
                        {
                            P1.setPlayerArray(x - 1 + i, y - 1, boatclick_P1);
                            //P1Array[x - 1 + i, y - 1] = boatclick_P1;
                            i++;
                        }
                        S.SetPlaced(true);
                        S.SetTopLeft(x - 1, y - 1);
                        pb_PlaceShips_P1.Invalidate();
                    }


                }


                else //vertical
                {
                    bool AllEmpty = true;

                    if (y - 1 + S.GetSize() - 1 < 10)
                    {
                        while (i < S.GetSize())
                        {
                            int EmptyChecker;
                            //EmptyChecker = P1Array[x - 1, y - 1 + i];
                            EmptyChecker = P1.getPlayerArray(x - 1, y - 1 + i);
                            if (EmptyChecker != 0)
                            {
                                AllEmpty = false;
                                MessageBox.Show("Temporary: YOU CAN'T DO THAT. THE SHIP IS NOT IN A VALID SPOT.");
                                pb_PlaceShips_P1.Invalidate();
                                return;
                            }
                            i++;

                        }
                    }
                    else
                    {
                        AllEmpty = false;
                        MessageBox.Show("Temporary: YOU CAN'T DO THAT. THE SHIP IS NOT IN A VALID SPOT.");
                        pb_PlaceShips_P1.Invalidate();
                        return;
                    }


                    if (AllEmpty == true)
                    {
                        i = 0;
                        while (i < S.GetSize())
                        {
                            P1.setPlayerArray(x - 1, y - 1 + i, boatclick_P1);
                            //P1Array[x - 1, y - 1 + i] = boatclick_P1;
                            i++;
                        }
                        S.SetPlaced(true);
                        S.SetTopLeft(x - 1, y - 1);
                        pb_PlaceShips_P1.Invalidate();
                    }
                }
            }
            else
            {
                MessageBox.Show(" You have already placed this ship.");
                return;
            }


        }
        public void UndoShipPlacement(int ship, Player P)
        {

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (P.getPlayerArray(i, j) == ship)
                        P.setPlayerArray(i, j, 0);
                    //if (P1Array[i, j] == ship)
                    //    P1Array[i, j] = 0;

                }
            }
            pb_PlaceShips_P1.Invalidate();
        }
        public void randomizeGrid()
        {
            Random rnd = new Random();
            //puts the random number into r

            PB2.Randomize (rnd, P2.getPlayerArrayAll());
            SUB2.Randomize(rnd, P2.getPlayerArrayAll());
            DES2.Randomize(rnd, P2.getPlayerArrayAll());
            BAT2.Randomize(rnd, P2.getPlayerArrayAll());
            AIR2.Randomize(rnd, P2.getPlayerArrayAll());
        }

        #region btnStart graphics
        private void btnStart_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnStart.ClientRectangle,
            Color.OrangeRed, 5, ButtonBorderStyle.Outset,
            Color.OrangeRed, 5, ButtonBorderStyle.Outset,
            Color.OrangeRed, 5, ButtonBorderStyle.Outset,
            Color.OrangeRed, 5, ButtonBorderStyle.Outset);
        }

        private void btnStart_MouseEnter(object sender, EventArgs e)
        {
            btnStart.BackColor = Color.Red;
            btnStart.ForeColor = Color.White;
            btnStart.Invalidate();
        }

        private void btnStart_MouseLeave(object sender, EventArgs e)
        {
            btnStart.BackColor = SystemColors.ControlLight;
            btnStart.ForeColor = Color.Black;
            btnStart.Invalidate();
        }
        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            pg1_Instructions.Visible = true;
        }

        #region btnNext_1 graphics
        private void btnNext_1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnNext_1.ClientRectangle,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset);
        }

        private void btnNext_1_MouseEnter(object sender, EventArgs e)
        {
            btnNext_1.UseVisualStyleBackColor = false;
            btnNext_1.BackColor = Color.Navy;
            btnNext_1.ForeColor = Color.White;
            btnNext_1.Invalidate();
        }

        private void btnNext_1_MouseLeave(object sender, EventArgs e)
        {
            btnNext_1.ForeColor = Color.Black;
            btnNext_1.BackColor = Color.RoyalBlue;
            btnNext_1.Invalidate();
        }
#endregion

        private void btnNext_1_Click(object sender, EventArgs e)
        {
            pg2_Avatar_P1.Visible = true;

            bkgd_A1_P1.Visible = false;
            bkgd_A2_P1.Visible = false;
            bkgd_A3_P1.Visible = false;
            slct_A1_P1 = false;
            slct_A2_P1 = false;
            slct_A3_P1 = false;
        }

        #region P1_Avatar graphics
        #region pb_A1_P1 graphics
        private void pb_A1_P1_MouseEnter(object sender, EventArgs e)
        {
            bkgd_A1_P1.Visible = true;
        }

        private void pb_A1_P1_MouseLeave(object sender, EventArgs e)
        {
            if (slct_A1_P1 == false)
            {
                bkgd_A1_P1.Visible = false;
            }
        }

        private void pb_A1_P1_Click(object sender, EventArgs e)
        {
            bkgd_A1_P1.Visible = true;
            bkgd_A2_P1.Visible = false;
            bkgd_A3_P1.Visible = false;

            slct_A1_P1 = true;
            slct_A2_P1 = false;
            slct_A3_P1 = false;
        }
        #endregion

        #region pb_A2_P1 graphics
        private void pb_A2_P1_MouseEnter(object sender, EventArgs e)
        {
            bkgd_A2_P1.Visible = true;
        }

        private void pb_A2_P1_MouseLeave(object sender, EventArgs e)
        {
            if (slct_A2_P1 == false)
            {
                bkgd_A2_P1.Visible = false;
            }
        }

        private void pb_A2_P1_Click(object sender, EventArgs e)
        {
            bkgd_A1_P1.Visible = false;
            bkgd_A2_P1.Visible = true;
            bkgd_A3_P1.Visible = false;

            slct_A1_P1 = false;
            slct_A2_P1 = true;
            slct_A3_P1 = false;
        }
        #endregion

        #region pb_A3_P1 graphics
        private void pb_A3_P1_MouseEnter(object sender, EventArgs e)
        {
            bkgd_A3_P1.Visible = true;
        }

        private void pb_A3_P1_MouseLeave(object sender, EventArgs e)
        {
            if (slct_A3_P1 == false)
            {
                bkgd_A3_P1.Visible = false;
            }
        }

        private void pb_A3_P1_Click(object sender, EventArgs e)
        {
            bkgd_A1_P1.Visible = false;
            bkgd_A2_P1.Visible = false;
            bkgd_A3_P1.Visible = true;

            slct_A1_P1 = false;
            slct_A2_P1 = false;
            slct_A3_P1 = true;
        }
        #endregion
        #endregion

        #region btnBack_2 graphics
        private void btnBack_2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnBack_2.ClientRectangle,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset);
        }

        private void btnBack_2_MouseEnter(object sender, EventArgs e)
        {
            btnBack_2.UseVisualStyleBackColor = false;
            btnBack_2.BackColor = Color.Navy;
            btnBack_2.ForeColor = Color.White;
            btnBack_2.Invalidate();
        }

        private void btnBack_2_MouseLeave(object sender, EventArgs e)
        {
            btnBack_2.ForeColor = Color.Black;
            btnBack_2.BackColor = Color.RoyalBlue;
            btnBack_2.Invalidate();
        }
        #endregion

        private void btnBack_2_Click(object sender, EventArgs e)
        {
            pg2_Avatar_P1.Visible = false;
        }

        #region btnNext_2 graphics
        private void btnNext_2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnNext_2.ClientRectangle,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset);
        }

        private void btnNext_2_MouseEnter(object sender, EventArgs e)
        {
            btnNext_2.UseVisualStyleBackColor = false;
            btnNext_2.BackColor = Color.Navy;
            btnNext_2.ForeColor = Color.White;
            btnNext_2.Invalidate();
        }

        private void btnNext_2_MouseLeave(object sender, EventArgs e)
        {
            btnNext_2.ForeColor = Color.Black;
            btnNext_2.BackColor = Color.RoyalBlue;
            btnNext_2.Invalidate();
        }
        #endregion

        private void btnNext_2_Click(object sender, EventArgs e)
        {
            bkgd_A1_P1.Visible = false;
            bkgd_A2_P1.Visible = false;
            bkgd_A3_P1.Visible = false;

            if (slct_A1_P1 == true)
            {
                P1.setAvatar(1);
                pg3_SetBoard_P1.Visible = true;
            }
            else if (slct_A2_P1 == true)
            {
                P1.setAvatar(2);
                pg3_SetBoard_P1.Visible = true;
            }
            else if (slct_A3_P1 == true)
            {
                P1.setAvatar(3);
                pg3_SetBoard_P1.Visible = true;
            }
            else
            {
                MessageBox.Show("You must select an avatar to continue.");  
            }
        }

        private void pb_PlaceShips_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            for (int i = 0; i < 12; i++)
            {
                g.DrawLine(myPen, 0, 30 * i, 300, 30 * i);
                g.DrawLine(myPen, 30 * i, 0, 30 * i, 300);
            }


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (P1.getPlayerArray(i, j) != 0)
                    {
                        SolidBrush myBrush = new SolidBrush(Color.RoyalBlue);
                        g.FillRectangle(myBrush, i * 30, j * 30, 30, 30);
                    }
                }
            }
        }

        #region PB_P1 click+paint
        private void lb_PB_P1_Click(object sender, EventArgs e)
        {
            lb_PB_P1.BorderStyle = BorderStyle.Fixed3D;
            lb_SUB_P1.BorderStyle = BorderStyle.None;
            lb_DES_P1.BorderStyle = BorderStyle.None;
            lb_BAT_P1.BorderStyle = BorderStyle.None;
            lb_AIR_P1.BorderStyle = BorderStyle.None;
            boatclick_P1 = 1;
        }

        private void pb_PB_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (PB.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawRectangle(myPen, 0, 0, 39, 19);

            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawRectangle(myPen, 0, 0, 19, 39);

            }
        }

        private void pb_PB_P1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_PB_P1.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            //if false it changes to width/height to if it were true, and then changes rotate to true
            if (PB.GetDirection() == false)
            {
                pb_PB_P1.Width = 20;
                pb_PB_P1.Height = 40;
                PB.Rotate();
            }
            else
            {
                pb_PB_P1.Width = 40;
                pb_PB_P1.Height = 20;
                PB.Rotate();
            }

            pb_PB_P1.Invalidate();
        }
        #endregion

        #region SUB_P1 click+paint
        private void lb_SUB_P1_Click(object sender, EventArgs e)
        {
            lb_PB_P1.BorderStyle = BorderStyle.None;
            lb_SUB_P1.BorderStyle = BorderStyle.Fixed3D;
            lb_DES_P1.BorderStyle = BorderStyle.None;
            lb_BAT_P1.BorderStyle = BorderStyle.None;
            lb_AIR_P1.BorderStyle = BorderStyle.None;
            boatclick_P1 = 2;
        }
        
        private void pb_SUB_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (SUB.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawLine(myPen, 40, 0, 40, 20);
                g.DrawRectangle(myPen, 0, 0, 59, 19);
            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawLine(myPen, 0, 40, 20, 40);
                g.DrawRectangle(myPen, 0, 0, 19, 59);
            }
        }

        private void pb_SUB_P1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_SUB_P1.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            if (SUB.GetDirection() == false)
            {
                pb_SUB_P1.Width = 20;
                pb_SUB_P1.Height = 60;
                SUB.Rotate();
            }
            else
            {
                pb_SUB_P1.Width = 60;
                pb_SUB_P1.Height = 20;
                SUB.Rotate();
            }
            pb_SUB_P1.Invalidate();
        }
        #endregion

        #region DES_P1 click+paint
        private void lb_DES_P1_Click(object sender, EventArgs e)
        {
            lb_PB_P1.BorderStyle = BorderStyle.None;
            lb_SUB_P1.BorderStyle = BorderStyle.None;
            lb_DES_P1.BorderStyle = BorderStyle.Fixed3D;
            lb_BAT_P1.BorderStyle = BorderStyle.None;
            lb_AIR_P1.BorderStyle = BorderStyle.None;
            boatclick_P1 = 3;
        }

        private void pb_DES_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (DES.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawLine(myPen, 40, 0, 40, 20);
                g.DrawRectangle(myPen, 0, 0, 59, 19);
            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawLine(myPen, 0, 40, 20, 40);
                g.DrawRectangle(myPen, 0, 0, 19, 59);
            }
        }

        private void pb_DES_P1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_DES_P1.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            if (DES.GetDirection() == false)
            {
                pb_DES_P1.Width = 20;
                pb_DES_P1.Height = 60;
                DES.Rotate();
            }
            else
            {
                pb_DES_P1.Width = 60;
                pb_DES_P1.Height = 20;
                DES.Rotate();
            }

            pb_DES_P1.Invalidate();
        }
        #endregion

        #region BAT_P1 click+paint
        private void lb_BAT_P1_Click(object sender, EventArgs e)
        {
            lb_PB_P1.BorderStyle = BorderStyle.None;
            lb_SUB_P1.BorderStyle = BorderStyle.None;
            lb_DES_P1.BorderStyle = BorderStyle.None;
            lb_BAT_P1.BorderStyle = BorderStyle.Fixed3D;
            lb_AIR_P1.BorderStyle = BorderStyle.None;
            boatclick_P1 = 4;
        }

        private void pb_BAT_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (BAT.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawLine(myPen, 40, 0, 40, 20);
                g.DrawLine(myPen, 60, 0, 60, 20);
                g.DrawRectangle(myPen, 0, 0, 79, 19);
            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawLine(myPen, 0, 40, 20, 40);
                g.DrawLine(myPen, 0, 60, 20, 60);
                g.DrawRectangle(myPen, 0, 0, 19, 79);
            }
        }

        private void pb_BAT_P1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_BAT_P1.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            
            if (BAT.GetDirection() == false)
            {
                pb_BAT_P1.Width = 20;
                pb_BAT_P1.Height = 80;
                BAT.Rotate();
            }
            else
            {
                pb_BAT_P1.Width = 80;
                pb_BAT_P1.Height = 20;
                BAT.Rotate();
            }

            pb_BAT_P1.Invalidate();
        }
        #endregion

        #region AIR_P1 click+paint
        private void lb_AIR_P1_Click(object sender, EventArgs e)
        {
            lb_PB_P1.BorderStyle = BorderStyle.None;
            lb_SUB_P1.BorderStyle = BorderStyle.None;
            lb_DES_P1.BorderStyle = BorderStyle.None;
            lb_BAT_P1.BorderStyle = BorderStyle.None;
            lb_AIR_P1.BorderStyle = BorderStyle.Fixed3D;
            boatclick_P1 = 5;
        }

        private void pb_AIR_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (AIR.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawLine(myPen, 40, 0, 40, 20);
                g.DrawLine(myPen, 60, 0, 60, 20);
                g.DrawLine(myPen, 80, 0, 80, 20);
                g.DrawRectangle(myPen, 0, 0, 99, 19);
            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawLine(myPen, 0, 40, 20, 40);
                g.DrawLine(myPen, 0, 60, 20, 60);
                g.DrawLine(myPen, 0, 80, 20, 80);
                g.DrawRectangle(myPen, 0, 0, 19, 99);
            }
        }

        private void pb_AIR_P1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_AIR_P1.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            
            if (AIR.GetDirection() == false)
            {
                pb_AIR_P1.Width = 20;
                pb_AIR_P1.Height = 100;
                AIR.Rotate();
            }
            else
            {
                pb_AIR_P1.Width = 100;
                pb_AIR_P1.Height = 20;
                AIR.Rotate();
            }
            pb_AIR_P1.Invalidate();
        }
#endregion

        //place ships undo btn
        private void btnUndo_3_Click(object sender, EventArgs e)
        {
            UndoShipPlacement(boatclick_P1, P1);

            switch (boatclick_P1)
            {
                case 1:
                    PB.SetPlaced(false);
                    break;
                case 2:
                    SUB.SetPlaced(false);
                    break;
                case 3:
                    DES.SetPlaced(false);
                    break;
                case 4:
                    BAT.SetPlaced(false);
                    break;
                case 5:
                    AIR.SetPlaced(false);
                    break;
                default:
                    break;

            }
        }
        
        //commented out next page visible
        private void btnSubmit_3_Click(object sender, EventArgs e)
        {
            //when all ships placed, can submit and move onto game
            if ((PB.GetPlaced() == true) && (SUB.GetPlaced() == true) && (DES.GetPlaced() == true) && (BAT.GetPlaced() == true) && (AIR.GetPlaced() == true))
            {
                MessageBox.Show("TEMPORARY: SUBMITTED");
                pg4_PlayerChoice.Visible = true;
                P1.setTurn(true);
                P2.setTurn(false);
            }

            else
            {
                MessageBox.Show("You must place all of the ships first.");
                return;
            }
        }

        private void btnBack_3_Click(object sender, EventArgs e)
        {
            pg3_SetBoard_P1.Visible = false;
        }

        private void pb_PlaceShips_P1_MouseClick(object sender, MouseEventArgs e)
        {
            //boatclick_P1
            //the number that reps the boat
            //mouse location / 30 + 1 gives array location - -1 = array index
            int x = Convert.ToInt32(Math.Floor(e.X / 30.0)) + 1;
            int y = Convert.ToInt32(Math.Floor(e.Y / 30.0)) + 1;
            
            //for the Patrol Boat
            if (boatclick_P1 == 1)
                DrawShipPlacement(x, y, PB);

            //do it in a method that passes the Boat name
            else if (boatclick_P1 == 2)
                DrawShipPlacement(x, y, SUB);
            else if (boatclick_P1 == 3)
                DrawShipPlacement(x, y, DES);
            else if (boatclick_P1 == 4)
                DrawShipPlacement(x, y, BAT);
            else //boatclick_P1 == 5
                DrawShipPlacement(x, y, AIR);

        }

        #region btnComputer graphics
        private void btnComputer_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnComputer.ClientRectangle,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset);
        }

        private void btnComputer_MouseEnter(object sender, EventArgs e)
        {
            btnComputer.UseVisualStyleBackColor = false;
            btnComputer.ForeColor = Color.White;
            btnComputer.BackColor = Color.Navy;
            btnComputer.Invalidate();
        }

        private void btnComputer_MouseLeave(object sender, EventArgs e)
        {
            btnComputer.ForeColor = Color.Black;
            btnComputer.BackColor = Color.RoyalBlue;
            btnComputer.Invalidate();
        }
        #endregion

        #region btn2Player graphics
        private void btn2Player_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btn2Player.ClientRectangle,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset);
        }

        private void btn2Player_MouseEnter(object sender, EventArgs e)
        {
            btn2Player.UseVisualStyleBackColor = false;
            btn2Player.ForeColor = Color.White;
            btn2Player.BackColor = Color.Navy;
            btn2Player.Invalidate();
        }

        private void btn2Player_MouseLeave(object sender, EventArgs e)
        {
            btn2Player.ForeColor = Color.Black;
            btn2Player.BackColor = Color.RoyalBlue;
            btn2Player.Invalidate();
        }
        #endregion

        private void btnComputer_Click(object sender, EventArgs e)
        {
            pg5_Avatar_P2.Visible = true;
            pg6_SetBoard_P2.Visible = true;
            pg7_GameTime_COM.Visible = true;
            randomizeGrid();
        }

        private void btn2Player_Click(object sender, EventArgs e)
        {
            pg5_Avatar_P2.Visible = true;

            bkgd_A1_P2.Visible = false;
            bkgd_A2_P2.Visible = false;
            bkgd_A3_P2.Visible = false;
            slct_A1_P2 = false;
            slct_A2_P2 = false;
            slct_A3_P2 = false;
        }

        #region P2_Avatar graphics
        #region pb_A1_P2 graphics
        private void pb_A1_P2_MouseEnter(object sender, EventArgs e)
        {
            bkgd_A1_P2.Visible = true;
        }

        private void pb_A1_P2_MouseLeave(object sender, EventArgs e)
        {
            if (slct_A1_P2 == false)
            {
                bkgd_A1_P2.Visible = false;
            }
        }

        private void pb_A1_P2_Click(object sender, EventArgs e)
        {
            bkgd_A1_P2.Visible = true;
            bkgd_A2_P2.Visible = false;
            bkgd_A3_P2.Visible = false;
                     
            slct_A1_P2 = true;
            slct_A2_P2 = false;
            slct_A3_P2 = false;
        }
#endregion

        #region pb_A2_P2 graphics
        private void pb_A2_P2_MouseEnter(object sender, EventArgs e)
        {
            bkgd_A2_P2.Visible = true;
        }

        private void pb_A2_P2_MouseLeave(object sender, EventArgs e)
        {
            if (slct_A2_P2 == false)
            {
                bkgd_A2_P2.Visible = false;
            }
        }

        private void pb_A2_P2_Click(object sender, EventArgs e)
        {
            bkgd_A1_P2.Visible = false;
            bkgd_A2_P2.Visible = true;
            bkgd_A3_P2.Visible = false;

            slct_A1_P2 = false;
            slct_A2_P2 = true;
            slct_A3_P2 = false;
        }
#endregion

        #region pb_A3_P2 graphics
        private void pb_A3_P2_MouseEnter(object sender, EventArgs e)
        {
            bkgd_A3_P2.Visible = true;
        }

        private void pb_A3_P2_MouseLeave(object sender, EventArgs e)
        {
            if (slct_A3_P2 == false)
            {
                bkgd_A3_P2.Visible = false;
            }
        }

        private void pb_A3_P2_Click(object sender, EventArgs e)
        {
            bkgd_A1_P2.Visible = false;
            bkgd_A2_P2.Visible = false;
            bkgd_A3_P2.Visible = true;

            slct_A1_P2 = false;
            slct_A2_P2 = false;
            slct_A3_P2 = true;
        }
        #endregion

        #endregion

        #region btnBack_5 graphics
        private void btnBack_5_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnBack_5.ClientRectangle,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset);
        }

        private void btnBack_5_MouseEnter(object sender, EventArgs e)
        {
            btnBack_5.UseVisualStyleBackColor = false;
            btnBack_5.BackColor = Color.Navy;
            btnBack_5.ForeColor = Color.White;
            btnBack_5.Invalidate();
        }

        private void btnBack_5_MouseLeave(object sender, EventArgs e)
        {
            btnBack_5.ForeColor = Color.Black;
            btnBack_5.BackColor = Color.RoyalBlue;
            btnBack_5.Invalidate();
        }
#endregion

        private void btnBack_5_Click(object sender, EventArgs e)
        {
            pg5_Avatar_P2.Visible = false;
        }

        #region btnNext_5 graphics
        private void btnNext_5_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnNext_5.ClientRectangle,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset);
        }

        private void btnNext_5_MouseEnter(object sender, EventArgs e)
        {
            btnNext_5.UseVisualStyleBackColor = false;
            btnNext_5.BackColor = Color.Navy;
            btnNext_5.ForeColor = Color.White;
            btnNext_5.Invalidate();
        }

        private void btnNext_5_MouseLeave(object sender, EventArgs e)
        {
            btnNext_5.ForeColor = Color.Black;
            btnNext_5.BackColor = Color.RoyalBlue;
            btnNext_5.Invalidate();
        }
#endregion

        private void btnNext_5_Click(object sender, EventArgs e)
        {
            bkgd_A1_P2.Visible = false;
            bkgd_A2_P2.Visible = false;
            bkgd_A3_P2.Visible = false;

            if (slct_A1_P2 == true)
            {
                P2.setAvatar(1);
                pg6_SetBoard_P2.Visible = true;
            }
            else if (slct_A2_P2 == true)
            {
                P2.setAvatar(2);
                pg6_SetBoard_P2.Visible = true;
            }
            else if (slct_A3_P2 == true)
            {
                P2.setAvatar(3);
                pg6_SetBoard_P2.Visible = true;
            }
            else
            {
                MessageBox.Show("You must select an avatar to continue.");
            }
        }

        private void pb_PlaceShips_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            for (int i = 0; i < 12; i++)
            {
                g.DrawLine(myPen, 0, 30 * i, 300, 30 * i);
                g.DrawLine(myPen, 30 * i, 0, 30 * i, 300);
            }


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (P2.getPlayerArray(i, j) != 0)
                    {
                        SolidBrush myBrush = new SolidBrush(Color.RoyalBlue);
                        g.FillRectangle(myBrush, i * 30, j * 30, 30, 30);
                    }
                }
            }
        }

        #region PB_P2 click+paint
        private void lb_PB_P2_Click(object sender, EventArgs e)
        {
            lb_PB_P2.BorderStyle = BorderStyle.Fixed3D;
            lb_SUB_P2.BorderStyle = BorderStyle.None;
            lb_DES_P2.BorderStyle = BorderStyle.None;
            lb_BAT_P2.BorderStyle = BorderStyle.None;
            lb_AIR_P2.BorderStyle = BorderStyle.None;
            boatclick_P2 = 1;
        }

        private void pb_PB_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (PB2.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawRectangle(myPen, 0, 0, 39, 19);

            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawRectangle(myPen, 0, 0, 19, 39);

            }
        }

        private void pb_PB_P2_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_PB_P2.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            //if false it changes to width/height to if it were true, and then changes rotate to true
            if (PB2.GetDirection() == false)
            {
                pb_PB_P2.Width = 20;
                pb_PB_P2.Height = 40;
                PB2.Rotate();
            }
            else
            {
                pb_PB_P2.Width = 40;
                pb_PB_P2.Height = 20;
                PB2.Rotate();
            }

            pb_PB_P2.Invalidate();
        }
        #endregion

        #region SUB_P2 click+paint
        private void lb_SUB_P2_Click(object sender, EventArgs e)
        {
            lb_PB_P2.BorderStyle = BorderStyle.None;
            lb_SUB_P2.BorderStyle = BorderStyle.Fixed3D;
            lb_DES_P2.BorderStyle = BorderStyle.None;
            lb_BAT_P2.BorderStyle = BorderStyle.None;
            lb_AIR_P2.BorderStyle = BorderStyle.None;
            boatclick_P2 = 2;
        }

        private void pb_SUB_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (SUB2.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawLine(myPen, 40, 0, 40, 20);
                g.DrawRectangle(myPen, 0, 0, 59, 19);
            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawLine(myPen, 0, 40, 20, 40);
                g.DrawRectangle(myPen, 0, 0, 19, 59);
            }
        }

        private void pb_SUB_P2_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_SUB_P2.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            if (SUB2.GetDirection() == false)
            {
                pb_SUB_P2.Width = 20;
                pb_SUB_P2.Height = 60;
                SUB2.Rotate();
            }
            else
            {
                pb_SUB_P2.Width = 60;
                pb_SUB_P2.Height = 20;
                SUB2.Rotate();
            }
            pb_SUB_P2.Invalidate();
        }

        #endregion

        #region DES_P2 click+paint
        private void lb_DES_P2_Click(object sender, EventArgs e)
        {
            lb_PB_P2.BorderStyle = BorderStyle.None;
            lb_SUB_P2.BorderStyle = BorderStyle.None;
            lb_DES_P2.BorderStyle = BorderStyle.Fixed3D;
            lb_BAT_P2.BorderStyle = BorderStyle.None;
            lb_AIR_P2.BorderStyle = BorderStyle.None;
            boatclick_P2 = 3;
        }

        private void pb_DES_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (DES2.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawLine(myPen, 40, 0, 40, 20);
                g.DrawRectangle(myPen, 0, 0, 59, 19);
            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawLine(myPen, 0, 40, 20, 40);
                g.DrawRectangle(myPen, 0, 0, 19, 59);
            }
        }

        private void pb_DES_P2_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_DES_P2.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            if (DES2.GetDirection() == false)
            {
                pb_DES_P2.Width = 20;
                pb_DES_P2.Height = 60;
                DES2.Rotate();
            }
            else
            {
                pb_DES_P2.Width = 60;
                pb_DES_P2.Height = 20;
                DES2.Rotate();
            }

            pb_DES_P2.Invalidate();
        }
        #endregion

        #region BAT_P2 click+paint
        private void lb_BAT_P2_Click(object sender, EventArgs e)
        {
            lb_PB_P2.BorderStyle = BorderStyle.None;
            lb_SUB_P2.BorderStyle = BorderStyle.None;
            lb_DES_P2.BorderStyle = BorderStyle.None;
            lb_BAT_P2.BorderStyle = BorderStyle.Fixed3D;
            lb_AIR_P2.BorderStyle = BorderStyle.None;
            boatclick_P2 = 4;
        }

        private void pb_BAT_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (BAT2.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawLine(myPen, 40, 0, 40, 20);
                g.DrawLine(myPen, 60, 0, 60, 20);
                g.DrawRectangle(myPen, 0, 0, 79, 19);
            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawLine(myPen, 0, 40, 20, 40);
                g.DrawLine(myPen, 0, 60, 20, 60);
                g.DrawRectangle(myPen, 0, 0, 19, 79);
            }
        }

        private void pb_BAT_P2_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_BAT_P2.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            if (BAT2.GetDirection() == false)
            {
                pb_BAT_P2.Width = 20;
                pb_BAT_P2.Height = 80;
                BAT2.Rotate();
            }
            else
            {
                pb_BAT_P2.Width = 80;
                pb_BAT_P2.Height = 20;
                BAT2.Rotate();
            }

            pb_BAT_P2.Invalidate();
        }
        #endregion

        #region AIR_P2 click+paint
        private void lb_AIR_P2_Click(object sender, EventArgs e)
        {
            lb_PB_P2.BorderStyle = BorderStyle.None;
            lb_SUB_P2.BorderStyle = BorderStyle.None;
            lb_DES_P2.BorderStyle = BorderStyle.None;
            lb_BAT_P2.BorderStyle = BorderStyle.None;
            lb_AIR_P2.BorderStyle = BorderStyle.Fixed3D;
            boatclick_P2 = 5;
        }

        private void pb_AIR_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (AIR2.GetDirection() == false)
            {
                g.DrawLine(myPen, 20, 0, 20, 20);
                g.DrawLine(myPen, 40, 0, 40, 20);
                g.DrawLine(myPen, 60, 0, 60, 20);
                g.DrawLine(myPen, 80, 0, 80, 20);
                g.DrawRectangle(myPen, 0, 0, 99, 19);
            }
            else
            {
                g.DrawLine(myPen, 0, 20, 20, 20);
                g.DrawLine(myPen, 0, 40, 20, 40);
                g.DrawLine(myPen, 0, 60, 20, 60);
                g.DrawLine(myPen, 0, 80, 20, 80);
                g.DrawRectangle(myPen, 0, 0, 19, 99);
            }
        }

        private void pb_AIR_P2_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(pb_AIR_P2.Handle);
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            if (AIR2.GetDirection() == false)
            {
                pb_AIR_P2.Width = 20;
                pb_AIR_P2.Height = 100;
                AIR2.Rotate();
            }
            else
            {
                pb_AIR_P2.Width = 100;
                pb_AIR_P2.Height = 20;
                AIR2.Rotate();
            }
            pb_AIR_P2.Invalidate();
        }
        #endregion

        private void btnUndo_6_Click(object sender, EventArgs e)
        {
            UndoShipPlacement(boatclick_P2, P2);

            switch (boatclick_P2)
            {
                case 1:
                    PB2.SetPlaced(false);
                    break;
                case 2:
                    SUB2.SetPlaced(false);
                    break;
                case 3:
                    DES2.SetPlaced(false);
                    break;
                case 4:
                    BAT2.SetPlaced(false);
                    break;
                case 5:
                    AIR2.SetPlaced(false);
                    break;
                default:
                    break;

            }
        }

        //UNCOMMENT visible when page created
        private void btnSubmit_6_Click(object sender, EventArgs e)
        {
            //when all ships placed, can submit and move onto game
            if ((PB2.GetPlaced() == true) && (SUB2.GetPlaced() == true) && (DES2.GetPlaced() == true) && (BAT2.GetPlaced() == true) && (AIR2.GetPlaced() == true))
            {
                MessageBox.Show("TEMPORARY: SUBMITTED");
                //pg7_GameTime_P1.Visible = true;
                P1.setTurn(true);
                P2.setTurn(false);
            }

            else
            {
                MessageBox.Show("You must place all of the ships first.");
                return;
            }
        }

        private void btnBack_6_Click(object sender, EventArgs e)
        {
            pg6_SetBoard_P2.Visible = false;
        }

        private void pb_PlaceShips_P2_MouseClick(object sender, MouseEventArgs e)
        {
            //boatclick_P2
            //the number that reps the boat
            //mouse location / 30 + 1 gives array location - -1 = array index
            int x = Convert.ToInt32(Math.Floor(e.X / 30.0)) + 1;
            int y = Convert.ToInt32(Math.Floor(e.Y / 30.0)) + 1;

            //for the Patrol Boat
            if (boatclick_P2 == 1)
                DrawShipPlacement(x, y, PB2);

            //do it in a method that passes the Boat name
            else if (boatclick_P2 == 2)
                DrawShipPlacement(x, y, SUB2);
            else if (boatclick_P2 == 3)
                DrawShipPlacement(x, y, DES2);
            else if (boatclick_P2 == 4)
                DrawShipPlacement(x, y, BAT2);
            else //boatclick_P2 == 5
                DrawShipPlacement(x, y, AIR2);
        }

        private void pb_COM_Grid_MouseClick(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(Math.Floor(e.X / 30.0)) + 1;
            int y = Convert.ToInt32(Math.Floor(e.Y / 30.0)) + 1;

            if (P1.getTurn() == true)
            {
                if (P2.getPlayerArray(x - 1, y - 1) > 0)
                {
                    P2.setPlayerArray(x - 1, y - 1, -1); //-1 = hit
                    P1.setHitCount(P1.getHitCount() + 1);
                }
                else if (P2.getPlayerArray(x - 1, y - 1) == 0)
                {
                    P2.setPlayerArray(x - 1, y - 1, -2); //-2 = miss
                }
                else
                {
                    return;
                }

                pb_COM_Grid.Invalidate();
                P1.SwitchTurn(P2);
            }
            else
            {

            }

            if (P1.isWinner() == true)
            {
                pb_P1_Grid.Enabled = false;
                pb_COM_Grid.Enabled = false;
                //do win stuff
                P1.setScore(P1.getScore() + 1);
            }
            else
            {
                TurnCOM();
            }
        }

        private void ShipHitCounter(int index)
        {
            if (index == 1)
            {
                PB.SetHitCount(PB.GetHitCount() + 1);
            }
            else if (index == 2)
            {
                SUB.SetHitCount(SUB.GetHitCount() + 1);
            }
            else if (index == 3)
            {
                DES.SetHitCount(DES.GetHitCount() + 1);
            }
            else if (index == 4)
            {
                BAT.SetHitCount(BAT.GetHitCount() + 1);
            }
            else if (index == 5)
            {
                AIR.SetHitCount(AIR.GetHitCount() + 1);
            }
            else
            {

            }
            P2.setHitCount(P2.getHitCount() + 1);
        }

        private void TurnCOM()
        {
            while (P2.getTurn() == true)
            {
                //COM targeting direction 
                if (P2.getHunt() == true)
                {
                    if (P2.getTargetModeHR() == true)
                    {
                        if (P2.getLastHit().X + 1 <= 9)
                        {
                            if (P1.getPlayerArray(P2.getLastHit().X + 1, P2.getLastHit().Y) > 0)
                            {
                                ShipHitCounter(P1.getPlayerArray(P2.getLastHit().X + 1, P2.getLastHit().Y));
                                P1.setPlayerArray(P2.getLastHit().X + 1, P2.getLastHit().Y, -1);
                                P2.setLastHit(P2.getLastHit().X + 1, P2.getLastHit().Y);
                                P1.SwitchTurn(P2);
                            }
                            //miss 
                            else if (P1.getPlayerArray(P2.getLastHit().X + 1, P2.getLastHit().Y) == 0)
                            {
                                P1.setPlayerArray(P2.getLastHit().X + 1, P2.getLastHit().Y, -2);
                                //reverse direction to hit from first hit point 
                                P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                                P2.setTargetModeHR(false);
                                P2.setTargetModeHL(true);
                                P1.SwitchTurn(P2);
                            }
                            //if spot has already been hit
                            else
                            {
                                P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                                //reverse direction to hit from firsthit point
                                P2.setTargetModeHR(false);
                                P2.setTargetModeHL(true);
                            }
                        }
                        else //if hit will be out of bounds 
                        {
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            P2.setTargetModeHR(false);
                            P2.setTargetModeHL(true);
                        }
                    }
                    else if (P2.getTargetModeHL() == true)
                    {
                        if (P2.getLastHit().X - 1 >= 0)
                        {
                            if (P1.getPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y) > 0)
                            {
                                ShipHitCounter(P1.getPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y));
                                P1.setPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y, -1);
                                P2.setLastHit(P2.getLastHit().X - 1, P2.getLastHit().Y);
                                P1.SwitchTurn(P2);
                            }
                            else if (P1.getPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y) == 0)
                            {
                                P1.setPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y, -2);
                                //reverse direction to hit from first point 
                                P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                                P2.setTargetModeHR(true);
                                P2.setTargetModeHL(false);
                                P1.SwitchTurn(P2);
                            }
                            else
                            {
                                //reverse direction to hit from first point 
                                P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                                P2.setTargetModeHR(true);
                                P2.setTargetModeHL(false);
                            }
                        }
                        else
                        {
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            P2.setTargetModeHR(true);
                            P2.setTargetModeHL(false);
                        }
                    }
                    else if (P2.getTargetModeVU() == true)
                    {
                        if (P2.getLastHit().Y - 1 >= 0)
                        {
                            if (P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y - 1) > 0)
                            {

                                ShipHitCounter(P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y - 1));
                                P1.setPlayerArray(P2.getLastHit().X, P2.getLastHit().Y - 1, -1);
                                P2.setLastHit(P2.getLastHit().X, P2.getLastHit().Y - 1);
                                P1.SwitchTurn(P2);
                            }
                            else if (P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y - 1) == 0)
                            {
                                P1.setPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y, -2);
                                //reverse direction to hit from first point 
                                P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                                P2.setTargetModeVU(false);
                                P2.setTargetModeVD(true);
                                P1.SwitchTurn(P2);
                            }
                            else
                            {
                                //reverse direction to hit from first point 
                                P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                                P2.setTargetModeVU(false);
                                P2.setTargetModeVD(true);
                            }
                        }
                        else
                        {
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            P2.setTargetModeVU(false);
                            P2.setTargetModeVD(true);
                        }
                    }
                    else if (P2.getTargetModeVD() == true)
                    {
                        if (P2.getLastHit().Y + 1 <= 9)
                        {
                            if (P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y + 1) > 0)
                            {
                                ShipHitCounter(P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y + 1));
                                P1.setPlayerArray(P2.getLastHit().X, P2.getLastHit().Y + 1, -1);
                                P2.setLastHit(P2.getLastHit().X, P2.getLastHit().Y + 1);
                                P1.SwitchTurn(P2);
                            }
                            else if (P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y + 1) == 0)
                            {
                                P1.setPlayerArray(P2.getLastHit().X, P2.getLastHit().Y + 1, -2);
                                //reverse direction to hit from first point 
                                P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                                P2.setTargetModeVU(true);
                                P2.setTargetModeVD(false);
                                P1.SwitchTurn(P2);
                            }
                            else
                            {
                                //reverse direction to hit from first point 
                                P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                                P2.setTargetModeVU(true);
                                P2.setTargetModeVD(false);
                            }
                        }
                        else
                        {
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            P2.setTargetModeVU(true);
                            P2.setTargetModeVD(false);
                        }
                    }
                    
                }
                else
                {
                    //if no points in to hit list
                    if (P2.getToHitLength() == 0)
                    {
                        Random rnd = new Random();
                        int x, y;
                        x = rnd.Next(0, 10);
                        y = rnd.Next(0, 10);
                        //if random hits ship 
                        if (P1.getPlayerArray(x, y) > 0)
                        {
                            ShipHitCounter(P1.getPlayerArray(x, y));
                            //store first point hit of a ship
                            P2.setFirstHit(x, y);
                            P1.setPlayerArray(x, y, -1);
                            P2.setHitCount(P2.getHitCount() + 1);
                            P1.SwitchTurn(P2);
                            //if surrounding points have not been hit, add to list of points to hit
                            //Left point
                            if (x - 1 >= 0 && P1.getPlayerArray(x - 1, y) >= 0)
                            {
                                P2.AddToHit(x - 1, y);
                            }
                            //Right point
                            if (x + 1 <= 9 && P1.getPlayerArray(x + 1, y) >= 0)
                            {
                                P2.AddToHit(x + 1, y);
                            }
                            //Up point
                            if (y - 1 >= 0 && P1.getPlayerArray(x, y - 1) >= 0)
                            {
                                P2.AddToHit(x, y - 1);
                            }
                            //Down point
                            if (y + 1 <= 9 && P1.getPlayerArray(x, y + 1) >= 0)
                            {
                                P2.AddToHit(x, y + 1);
                            }
                            
                        }
                        //COM random hit misses
                        else if (P1.getPlayerArray(x, y) == 0)
                        {
                            P1.setPlayerArray(x, y, -2);
                            P1.SwitchTurn(P2);
                        }
                        //if spot has already been hit
                        else
                        {

                        }
                    }
                    else
                    {
                        if (P2.getFirstHit().X + 1 == P2.getToHitPoint().X)
                        {
                            //Future hits go right
                            P2.setTargetModeHR(true);
                            P2.setLastHit(P2.getToHitPoint().X, P2.getToHitPoint().Y);
                            ShipHitCounter(P1.getPlayerArray(P2.getToHitPoint()));
                            P1.setPlayerArray(P2.getToHitPoint(), -1);
                            P2.ClearToHitPoints();

                        }
                        else if (P2.getFirstHit().X - 1 == P2.getToHitPoint().X)
                        {
                            //Future hits go left
                            P2.setTargetModeHL(true);
                            P2.setLastHit(P2.getToHitPoint().X, P2.getToHitPoint().Y);
                            ShipHitCounter(P1.getPlayerArray(P2.getToHitPoint()));
                            P1.setPlayerArray(P2.getToHitPoint(), -1);
                            P2.ClearToHitPoints();
                        }
                        else if (P2.getFirstHit().Y + 1 == P2.getToHitPoint().Y)
                        {
                            //Future hits go down
                            P2.setTargetModeVD(true);
                            P2.setLastHit(P2.getToHitPoint().X, P2.getToHitPoint().Y);
                            ShipHitCounter(P1.getPlayerArray(P2.getToHitPoint()));
                            P1.setPlayerArray(P2.getToHitPoint(), -1);
                            P2.ClearToHitPoints();
                        }
                        else if (P2.getFirstHit().Y - 1 == P2.getToHitPoint().Y)
                        {
                            //Future hits go up
                            P2.setTargetModeVU(true);
                            P2.setLastHit(P2.getToHitPoint().X, P2.getToHitPoint().Y);
                            ShipHitCounter(P1.getPlayerArray(P2.getToHitPoint()));
                            P1.setPlayerArray(P2.getToHitPoint(), -1);
                            P2.ClearToHitPoints();
                        }
                        P2.setHunt(true);
                        P1.SwitchTurn(P2);
                    } 
                    
                    
                }
                PB.IsDead(P2);
                SUB.IsDead(P2);
                DES.IsDead(P2);
                BAT.IsDead(P2);
                AIR.IsDead(P2);
                
            }

            pb_P1_Grid.Invalidate();

            if (P2.isWinner() == true)
            {
                pb_P1_Grid.Enabled = false;
                pb_COM_Grid.Enabled = false;
                P2.setScore(P2.getScore() + 1);
            }
            else
            {

            }
        }

        private void pb_P1_Grid_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            for (int i = 0; i < 12; i++)
            {
                g.DrawLine(myPen, 0, 30 * i, 300, 30 * i);
                g.DrawLine(myPen, 30 * i, 0, 30 * i, 300);
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (P1.getPlayerArray(i, j) == -1) //if ship is hit 
                    {
                        SolidBrush myBrush = new SolidBrush(Color.Crimson);
                        g.FillRectangle(myBrush, i * 30, j * 30, 30, 30);
                    }
                    else if (P1.getPlayerArray(i, j) == -2)
                    {
                        SolidBrush myBrush = new SolidBrush(Color.Black);
                        g.FillRectangle(myBrush, i * 30, j * 30, 30, 30);
                    }
                }
            }
        }

        private void pb_COM_Grid_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            for (int i = 0; i < 12; i++)
            {
                g.DrawLine(myPen, 0, 30 * i, 300, 30 * i);
                g.DrawLine(myPen, 30 * i, 0, 30 * i, 300);
            }


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (P2.getPlayerArray(i, j) == -1) //if ship is hit 
                    {
                        SolidBrush myBrush = new SolidBrush(Color.Crimson);
                        g.FillRectangle(myBrush, i * 30, j * 30, 30, 30);
                    }
                    else if (P2.getPlayerArray(i, j) == -2)
                    {
                        SolidBrush myBrush = new SolidBrush(Color.Black);
                        g.FillRectangle(myBrush, i * 30, j * 30, 30, 30);
                    }
                }
            }
        }
    }
}

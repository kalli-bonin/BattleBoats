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
            pg7_GameTime_P1.Visible  = false;
            //pg8_GameTime_P2.Visible  = false;
            //pg9_GameOver.Visible    = false;
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
            protected bool TargetModeHR;
            protected bool TargetModeHL;
            protected bool TargetModeVU;
            protected bool TargetModeVD;
            protected bool Hunt;

            public Ships()
            {
                Name = "Default";
                Size = 1;
                HitCount = 0;
                topLeft.X = 0;
                topLeft.Y = 0;
                DirectionVertical = true;
                Placed = false;     //is the ship placed in the starting screen
                DeathMessage = false;   //only one death message should be shown
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
            public void IncreaseHitCount(int i)
            {
                if (Index == i)
                {
                    SetHitCount(GetHitCount() + 1);
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
            public bool IsDead()
            {
                if ((HitCount == Size) && (DeathMessage == false))
                {
                    MessageBox.Show("Your" + Name + "is dead.");
                    ResetHuntMode();
                    return true;
                }
                else if ((HitCount == Size) && (DeathMessage == true))
                {
                    ResetHuntMode();
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
            public void Randomize(Random rnd, int[,] PlayerArray
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
            public void setHunt(bool b)
            {
                Hunt = b;
            }
            public bool getHunt()
            {
                return Hunt;
            }
            public void ResetHuntMode()
            {
                setTargetModeHR(false);
                setTargetModeHL(false);
                setTargetModeVU(false);
                setTargetModeVD(false);
                setHunt(false);
            }

            public void HuntMode(Player P1, Player P2)
            {
                if (getTargetModeHR() == true)
                {
                    if (P2.getLastHit().X + 1 <= 9)
                    {
                        if (P1.getPlayerArray(P2.getLastHit().X + 1, P2.getLastHit().Y) > 0)
                        {
                            P1.setPlayerArray(P2.getLastHit().X + 1, P2.getLastHit().Y, -1);
                            P2.setLastHit(P2.getLastHit().X + 1, P2.getLastHit().Y);
                            SetHitCount(GetHitCount() + 1);
                            P1.SwitchTurn(P2);
                        }
                        else if (P1.getPlayerArray(P2.getLastHit().X + 1, P2.getLastHit().Y) == 0)
                        {
                            P1.setPlayerArray(P2.getLastHit().X + 1, P2.getLastHit().Y, -2);
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            setTargetModeHR(false);
                            setTargetModeHL(true);
                            P1.SwitchTurn(P2);
                        }
                        else //if 
                        {
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            setTargetModeHR(false);
                            setTargetModeHL(true);
                        }
                    }
                    else //if hit will be out of bounds 
                    {
                        P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                        setTargetModeHR(false);
                        setTargetModeHL(true);
                    }

                }
                else if (getTargetModeHL() == true)
                {
                    if (P2.getLastHit().X - 1 >= 0)
                    {
                        if (P1.getPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y) > 0)
                        {
                            P1.setPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y, -1);
                            P2.setLastHit(P2.getLastHit().X - 1, P2.getLastHit().Y);
                            SetHitCount(GetHitCount() + 1);
                            P1.SwitchTurn(P2);
                        }
                        else if (P1.getPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y) == 0)
                        {
                            P1.setPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y, -2);
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            setTargetModeHR(true);
                            setTargetModeHL(false);
                            P1.SwitchTurn(P2);
                        }
                        else
                        {
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            setTargetModeHR(true);
                            setTargetModeHL(false);
                        }
                    }
                    else
                    {
                        //reverse direction to hit from first point 
                        P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                        setTargetModeHR(true);
                        setTargetModeHL(false);
                    }
                }
                else if (getTargetModeVU() == true)
                {
                    if (P2.getLastHit().Y - 1 >= 0)
                    {
                        if (P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y - 1) > 0)
                        {
                            P1.setPlayerArray(P2.getLastHit().X, P2.getLastHit().Y - 1, -1);
                            P2.setLastHit(P2.getLastHit().X, P2.getLastHit().Y - 1);
                            SetHitCount(GetHitCount() + 1);
                            P1.SwitchTurn(P2);
                        }
                        else if (P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y - 1) == 0)
                        {
                            P1.setPlayerArray(P2.getLastHit().X - 1, P2.getLastHit().Y, -2);
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            setTargetModeVU(false);
                            setTargetModeVD(true);
                            P1.SwitchTurn(P2);
                        }
                        else
                        {
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            setTargetModeVU(false);
                            setTargetModeVD(true);
                        }
                    }
                    else
                    {
                        //reverse direction to hit from first point 
                        P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                        setTargetModeVU(false);
                        setTargetModeVD(true);
                    }
                }
                else if (getTargetModeVD() == true)
                {
                    if (P2.getLastHit().Y + 1 <= 9)
                    {
                        if (P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y + 1) > 0)
                        {
                            P1.setPlayerArray(P2.getLastHit().X, P2.getLastHit().Y + 1, -1);
                            P2.setLastHit(P2.getLastHit().X, P2.getLastHit().Y + 1);
                            SetHitCount(GetHitCount() + 1);
                            P1.SwitchTurn(P2);
                        }
                        else if (P1.getPlayerArray(P2.getLastHit().X, P2.getLastHit().Y + 1) == 0)
                        {
                            P1.setPlayerArray(P2.getLastHit().X, P2.getLastHit().Y + 1, -2);
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            setTargetModeVU(true);
                            setTargetModeVD(false);
                            P1.SwitchTurn(P2);
                        }
                        else
                        {
                            //reverse direction to hit from first point 
                            P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                            setTargetModeVU(true);
                            setTargetModeVD(false);
                        }
                    }
                    else
                    {
                        //reverse direction to hit from first point 
                        P2.setLastHit(P2.getFirstHit().X, P2.getFirstHit().Y);
                        setTargetModeVU(true);
                        setTargetModeVD(false);
                    }
                }
                IsDead();
            }
        }

        public class Player
        {
            public Player()
            {
                iScore = 0;
                iAvatar = 1;
                iHitCount = 0;
            }

            public Ships PB = new Ships("PatrolBoat", 2, 0, 0, 0, false, false, 1);
            public Ships SUB = new Ships("Submarine", 3, 0, 0, 0, false, false, 2);
            public Ships DES = new Ships("Destroyer", 3, 0, 0, 0, false, false, 3);
            public Ships BAT = new Ships("Battleship", 4, 0, 0, 0, false, false, 4);
            public Ships AIR = new Ships("Aircraft", 5, 0, 0, 0, false, false, 5);

            protected int iScore;
            protected int iAvatar;
            protected int iHitCount;

            //stores player ship information///////////////
            protected int[,] PlayerArray = new int[10, 10];
            //--------------------------------------------//
            

            //confirming placing all ships and which ships are placed
            public bool allPlaced = false;
            public int boatclick = 0;
            //-----------------------------------------------------//

            protected Point FirstHit;
            protected Point LastHit;

            //P1 turn or P2 turn?///////////////////
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
            public void setTurn(bool b)
            {
                Turn = b;
            }
            public bool getTurn()
            {
                return Turn;
            }
            //------------------------------------//

            //list of points for COM to hit//////////////////
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
            //---------------------------------------------//

            //setters and getters for //////////////////////
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
            
            //score
            public void setScore(int score)
            {
                iScore = score;
            }
            public int getScore()
            {
                return iScore;
            }

            //avatar
            public void setAvatar(int avatar)
            {
                iAvatar = avatar;
            }
            public int getAvatar()
            {
                return iAvatar;
            }

            //hitcount
            public void setHitCount(int hit)
            {
                iHitCount = hit;
            }
            public int getHitCount()
            {
                return iHitCount;
            }

            //playerArray
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
            //-------------------------------------------------//
            public bool isWinner()
            {
                //needs win condition 
                if (iHitCount == 17)
                //I THINK ITS 17????
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void checkHit(Player other, int x, int y)
            {
                if (other.getPlayerArray(x - 1, y - 1) > 0)
                {
                    other.setPlayerArray(x - 1, y - 1, -1); //-1 = hit
                    other.setHitCount(this.getHitCount() + 1);
                }
                else if (other.getPlayerArray(x - 1, y - 1) == 0)
                {
                    other.setPlayerArray(x - 1, y - 1, -2); //-2 = miss
                }
                else
                {
                    return;
                }
            }
            
        }

        public void DrawShipPlacement(int x, int y, Ships S, Player P, Control PIC)
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
                            EmptyChecker = P.getPlayerArray(x - 1 + i, y - 1);
                            if (EmptyChecker != 0)
                            {
                                AllEmpty = false;
                                MessageBox.Show("Temporary: YOU CAN'T DO THAT. THE SHIP IS NOT IN A VALID SPOT.");
                                PIC.Invalidate();
                                return;
                            }
                            i++;

                        }
                    }
                    else
                    {
                        AllEmpty = false;
                        MessageBox.Show("Temporary: YOU CAN'T DO THAT. THE SHIP IS NOT IN A VALID SPOT.");
                        PIC.Invalidate();
                        return;
                    }


                    if (AllEmpty == true)
                    {
                        i = 0;
                        while (i < S.GetSize())
                        {
                            P.setPlayerArray(x - 1 + i, y - 1, P.boatclick);
                            //P1Array[x - 1 + i, y - 1] = boatclick_P1;
                            i++;
                        }
                        S.SetPlaced(true);
                        S.SetTopLeft(x - 1, y - 1);
                        PIC.Invalidate();
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
                            EmptyChecker = P.getPlayerArray(x - 1, y - 1 + i);
                            if (EmptyChecker != 0)
                            {
                                AllEmpty = false;
                                MessageBox.Show("Temporary: YOU CAN'T DO THAT. THE SHIP IS NOT IN A VALID SPOT.");
                                PIC.Invalidate();
                                return;
                            }
                            i++;

                        }
                    }
                    else
                    {
                        AllEmpty = false;
                        MessageBox.Show("Temporary: YOU CAN'T DO THAT. THE SHIP IS NOT IN A VALID SPOT.");
                        PIC.Invalidate();
                        return;
                    }


                    if (AllEmpty == true)
                    {
                        i = 0;
                        while (i < S.GetSize())
                        {
                            P.setPlayerArray(x - 1, y - 1 + i, P.boatclick);
                            //P1Array[x - 1, y - 1 + i] = boatclick_P1;
                            i++;
                        }
                        S.SetPlaced(true);
                        S.SetTopLeft(x - 1, y - 1);
                        PIC.Invalidate();
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

            P2.PB.Randomize (rnd, P2.getPlayerArrayAll());
            P2.SUB.Randomize(rnd, P2.getPlayerArrayAll());
            P2.DES.Randomize(rnd, P2.getPlayerArrayAll());
            P2.BAT.Randomize(rnd, P2.getPlayerArrayAll());
            P2.AIR.Randomize(rnd, P2.getPlayerArrayAll());
        }
        public void PaintGrid(Player P, Graphics g, Control pb)
        {
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //draw grid
            for (int i = 0; i < 12; i++)
            {
                g.DrawLine(myPen, 0, 30 * i, 300, 30 * i);
                g.DrawLine(myPen, 30 * i, 0, 30 * i, 300);
            }
            //fill in boxes
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (P.getPlayerArray(i, j) == -1) //if ship is hit 
                    {
                        SolidBrush myBrush = new SolidBrush(Color.Crimson);
                        g.FillRectangle(myBrush, i * 30, j * 30, 30, 30);
                    }
                    else if (P.getPlayerArray(i, j) == -2)
                    {
                        SolidBrush myBrush = new SolidBrush(Color.Black);
                        g.FillRectangle(myBrush, i * 30, j * 30, 30, 30);
                    }
                    //placing ships
                    else if (P.getPlayerArray(i, j) > 0 && P.allPlaced != true)
                    {
                        SolidBrush myBrush = new SolidBrush(Color.RoyalBlue);
                        g.FillRectangle(myBrush, i * 30, j * 30, 30, 30);
                    }
                }
            }
            //pb.Invalidate();
        }

        #region mouse graphics
        public void PaintButton(Control btn, Graphics e)
        {
            ControlPaint.DrawBorder(e, btn.ClientRectangle,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset,
            Color.DodgerBlue, 5, ButtonBorderStyle.Outset);
        }
        public void ButtonEnter(Control btn)
        {
            //btn.UseVisualStyleBackColor = false;
            btn.BackColor = Color.Navy;
            btn.ForeColor = Color.White;
            btn.Invalidate();
        }
        public void ButtonLeave(Control btn)
        {
            btn.ForeColor = Color.Black;
            btn.BackColor = Color.RoyalBlue;
            btn.Invalidate();
        }
        #endregion

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
            PaintButton(btnNext_1, e.Graphics);
        }

        private void btnNext_1_MouseEnter(object sender, EventArgs e)
        {
            ButtonEnter(btnNext_1);
        }

        private void btnNext_1_MouseLeave(object sender, EventArgs e)
        {
            ButtonLeave(btnNext_1);
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
            PaintButton(btnBack_2, e.Graphics);
        }

        private void btnBack_2_MouseEnter(object sender, EventArgs e)
        {
            ButtonEnter(btnBack_2);
        }

        private void btnBack_2_MouseLeave(object sender, EventArgs e)
        {
            ButtonLeave(btnBack_2);
        }
        #endregion

        private void btnBack_2_Click(object sender, EventArgs e)
        {
            pg2_Avatar_P1.Visible = false;
        }

        #region btnNext_2 graphics
        private void btnNext_2_Paint(object sender, PaintEventArgs e)
        {
            PaintButton(btnNext_2, e.Graphics);
        }

        private void btnNext_2_MouseEnter(object sender, EventArgs e)
        {
            ButtonEnter(btnNext_2);
        }

        private void btnNext_2_MouseLeave(object sender, EventArgs e)
        {
            ButtonLeave(btnNext_2);
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
            PaintGrid(P1, e.Graphics, pb_PlaceShips_P1);
        }

        #region PB_P1 click+paint
        private void lb_PB_P1_Click(object sender, EventArgs e)
        {
            lb_PB_P1.BorderStyle = BorderStyle.Fixed3D;
            lb_SUB_P1.BorderStyle = BorderStyle.None;
            lb_DES_P1.BorderStyle = BorderStyle.None;
            lb_BAT_P1.BorderStyle = BorderStyle.None;
            lb_AIR_P1.BorderStyle = BorderStyle.None;
            P1.boatclick = 1;
        }

        private void pb_PB_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P1.PB.GetDirection() == false)
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
            if (P1.PB.GetDirection() == false)
            {
                pb_PB_P1.Width = 20;
                pb_PB_P1.Height = 40;
                P1.PB.Rotate();
            }
            else
            {
                pb_PB_P1.Width = 40;
                pb_PB_P1.Height = 20;
                P1.PB.Rotate();
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
            P1.boatclick = 2;
        }
        
        private void pb_SUB_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P1.SUB.GetDirection() == false)
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

            if (P1.SUB.GetDirection() == false)
            {
                pb_SUB_P1.Width = 20;
                pb_SUB_P1.Height = 60;
                P1.SUB.Rotate();
            }
            else
            {
                pb_SUB_P1.Width = 60;
                pb_SUB_P1.Height = 20;
                P1.SUB.Rotate();
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
            P1.boatclick = 3;
        }

        private void pb_DES_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P1.DES.GetDirection() == false)
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

            if (P1.DES.GetDirection() == false)
            {
                pb_DES_P1.Width = 20;
                pb_DES_P1.Height = 60;
                P1.DES.Rotate();
            }
            else
            {
                pb_DES_P1.Width = 60;
                pb_DES_P1.Height = 20;
                P1.DES.Rotate();
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
            P1.boatclick = 4;
        }

        private void pb_BAT_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P1.BAT.GetDirection() == false)
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
            
            if (P1.BAT.GetDirection() == false)
            {
                pb_BAT_P1.Width = 20;
                pb_BAT_P1.Height = 80;
                P1.BAT.Rotate();
            }
            else
            {
                pb_BAT_P1.Width = 80;
                pb_BAT_P1.Height = 20;
                P1.BAT.Rotate();
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
            P1.boatclick = 5;
        }

        private void pb_AIR_P1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P1.AIR.GetDirection() == false)
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
            
            if (P1.AIR.GetDirection() == false)
            {
                pb_AIR_P1.Width = 20;
                pb_AIR_P1.Height = 100;
                P1.AIR.Rotate();
            }
            else
            {
                pb_AIR_P1.Width = 100;
                pb_AIR_P1.Height = 20;
                P1.AIR.Rotate();
            }
            pb_AIR_P1.Invalidate();
        }
#endregion

        //place ships undo btn
        private void btnUndo_3_Click(object sender, EventArgs e)
        {
            UndoShipPlacement(P1.boatclick, P1);

            switch (P1.boatclick)
            {
                case 1:
                    P1.PB.SetPlaced(false);
                    break;
                case 2:
                    P1.SUB.SetPlaced(false);
                    break;
                case 3:
                    P1.DES.SetPlaced(false);
                    break;
                case 4:
                    P1.BAT.SetPlaced(false);
                    break;
                case 5:
                    P1.AIR.SetPlaced(false);
                    break;
                default:
                    break;

            }
        }
        
        private void btnSubmit_3_Click(object sender, EventArgs e)
        {
            //when all ships placed, can submit and move onto game
            if ((P1.PB.GetPlaced() == true) && (P1.SUB.GetPlaced() == true) && (P1.DES.GetPlaced() == true) && (P1.BAT.GetPlaced() == true) && (P1.AIR.GetPlaced() == true))
            {
                MessageBox.Show("TEMPORARY: SUBMITTED");
                pg4_PlayerChoice.Visible = true;
                P1.setTurn(true);
                P2.setTurn(false);
                P1.allPlaced = true;
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
            if (P1.boatclick == 1)
                DrawShipPlacement(x, y, P1.PB, P1, pb_PlaceShips_P1);

            //do it in a method that passes the Boat name
            else if (P1.boatclick == 2)
                DrawShipPlacement(x, y, P1.SUB, P1, pb_PlaceShips_P1);
            else if (P1.boatclick == 3)
                DrawShipPlacement(x, y, P1.DES, P1, pb_PlaceShips_P1);
            else if (P1.boatclick == 4)
                DrawShipPlacement(x, y, P1.BAT, P1, pb_PlaceShips_P1);
            else //P1.boatclick == 5
                DrawShipPlacement(x, y, P1.AIR, P1, pb_PlaceShips_P1);

        }

        #region btnComputer graphics
        private void btnComputer_Paint(object sender, PaintEventArgs e)
        {
            PaintButton(btnComputer, e.Graphics);
        }

        private void btnComputer_MouseEnter(object sender, EventArgs e)
        {
            ButtonEnter(btnComputer);
        }

        private void btnComputer_MouseLeave(object sender, EventArgs e)
        {
            ButtonLeave(btnComputer);
        }
        #endregion

        #region btn2Player graphics
        private void btn2Player_Paint(object sender, PaintEventArgs e)
        {
            PaintButton(btn2Player, e.Graphics);
        }

        private void btn2Player_MouseEnter(object sender, EventArgs e)
        {
            ButtonEnter(btn2Player);
        }

        private void btn2Player_MouseLeave(object sender, EventArgs e)
        {
            ButtonLeave(btn2Player);
        }
        #endregion

        private void btnComputer_Click(object sender, EventArgs e)
        {
            //skips two pages
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
            PaintButton(btnBack_5, e.Graphics);
        }

        private void btnBack_5_MouseEnter(object sender, EventArgs e)
        {
            ButtonEnter(btnBack_5);
        }

        private void btnBack_5_MouseLeave(object sender, EventArgs e)
        {
            ButtonLeave(btnBack_5);
        }
#endregion

        private void btnBack_5_Click(object sender, EventArgs e)
        {
            pg5_Avatar_P2.Visible = false;
        }

        #region btnNext_5 graphics
        private void btnNext_5_Paint(object sender, PaintEventArgs e)
        {
            PaintButton(btnNext_5, e.Graphics);
        }

        private void btnNext_5_MouseEnter(object sender, EventArgs e)
        {
            ButtonEnter(btnNext_5);
        }

        private void btnNext_5_MouseLeave(object sender, EventArgs e)
        {
            ButtonLeave(btnNext_5);
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
            PaintGrid(P2, e.Graphics, pb_PlaceShips_P2);
        }

        #region PB_P2 click+paint
        private void lb_PB_P2_Click(object sender, EventArgs e)
        {
            lb_PB_P2.BorderStyle = BorderStyle.Fixed3D;
            lb_SUB_P2.BorderStyle = BorderStyle.None;
            lb_DES_P2.BorderStyle = BorderStyle.None;
            lb_BAT_P2.BorderStyle = BorderStyle.None;
            lb_AIR_P2.BorderStyle = BorderStyle.None;
            P2.boatclick = 1;
        }

        private void pb_PB_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P2.PB.GetDirection() == false)
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
            if (P2.PB.GetDirection() == false)
            {
                pb_PB_P2.Width = 20;
                pb_PB_P2.Height = 40;
                P2.PB.Rotate();
            }
            else
            {
                pb_PB_P2.Width = 40;
                pb_PB_P2.Height = 20;
                P2.PB.Rotate();
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
            P2.boatclick = 2;
        }

        private void pb_SUB_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P2.SUB.GetDirection() == false)
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

            if (P2.SUB.GetDirection() == false)
            {
                pb_SUB_P2.Width = 20;
                pb_SUB_P2.Height = 60;
                P2.SUB.Rotate();
            }
            else
            {
                pb_SUB_P2.Width = 60;
                pb_SUB_P2.Height = 20;
                P2.SUB.Rotate();
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
            P2.boatclick = 3;
        }

        private void pb_DES_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P2.DES.GetDirection() == false)
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

            if (P2.DES.GetDirection() == false)
            {
                pb_DES_P2.Width = 20;
                pb_DES_P2.Height = 60;
                P2.DES.Rotate();
            }
            else
            {
                pb_DES_P2.Width = 60;
                pb_DES_P2.Height = 20;
                P2.DES.Rotate();
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
            P2.boatclick = 4;
        }

        private void pb_BAT_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P2.BAT.GetDirection() == false)
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

            if (P2.BAT.GetDirection() == false)
            {
                pb_BAT_P2.Width = 20;
                pb_BAT_P2.Height = 80;
                P2.BAT.Rotate();
            }
            else
            {
                pb_BAT_P2.Width = 80;
                pb_BAT_P2.Height = 20;
                P2.BAT.Rotate();
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
            P2.boatclick = 5;
        }

        private void pb_AIR_P2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen myPen = new Pen(Color.Black, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            if (P2.AIR.GetDirection() == false)
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

            if (P2.AIR.GetDirection() == false)
            {
                pb_AIR_P2.Width = 20;
                pb_AIR_P2.Height = 100;
                P2.AIR.Rotate();
            }
            else
            {
                pb_AIR_P2.Width = 100;
                pb_AIR_P2.Height = 20;
                P2.AIR.Rotate();
            }
            pb_AIR_P2.Invalidate();
        }
        #endregion

        private void btnUndo_6_Click(object sender, EventArgs e)
        {
            UndoShipPlacement(P2.boatclick, P2);

            switch (P2.boatclick)
            {
                case 1:
                    P2.PB.SetPlaced(false);
                    break;
                case 2:
                    P2.SUB.SetPlaced(false);
                    break;
                case 3:
                    P2.DES.SetPlaced(false);
                    break;
                case 4:
                    P2.BAT.SetPlaced(false);
                    break;
                case 5:
                    P2.AIR.SetPlaced(false);
                    break;
                default:
                    break;

            }
        }

        private void btnSubmit_6_Click(object sender, EventArgs e)
        {
            //when all ships placed, can submit and move onto game
            if ((P2.PB.GetPlaced() == true) && (P2.SUB.GetPlaced() == true) && (P2.DES.GetPlaced() == true) && (P2.BAT.GetPlaced() == true) && (P2.AIR.GetPlaced() == true))
            {
                MessageBox.Show("TEMPORARY: SUBMITTED");
                //skips COM page
                pg7_GameTime_COM.Visible = true;
                pg7_GameTime_P1.Visible = true;

                int avatar = P1.getAvatar();
                int avatar2 = P2.getAvatar();
                switch (avatar)
                {
                    case 1:
                        pb_MA1_P1.Visible = true;
                        break;
                    case 2:
                        pb_MA2_P1.Visible = true;
                        break;
                    case 3:
                        pb_MA3_P1.Visible = true;
                        break;
                    default:
                        break;
                }
                switch (avatar2)
                {
                    case 4:
                        pb_MA4_P2.Visible = true;
                        break;
                    case 5:
                        pb_MA4_P2.Visible = true;
                        break;
                    case 6:
                        pb_MA6_P2.Visible = true;
                        break;
                    default:
                        break;
                }
                
                P1.setTurn(true);
                P2.setTurn(false);
                P2.allPlaced = true;
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
            if (P2.boatclick == 1)
                DrawShipPlacement(x, y, P2.PB, P2, pb_PlaceShips_P2);

            //do it in a method that passes the Boat name
            else if (P2.boatclick == 2)
                DrawShipPlacement(x, y, P2.SUB, P2, pb_PlaceShips_P2);
            else if (P2.boatclick == 3)
                DrawShipPlacement(x, y, P2.DES, P2, pb_PlaceShips_P2);
            else if (P2.boatclick == 4)
                DrawShipPlacement(x, y, P2.BAT, P2, pb_PlaceShips_P2);
            else //P2.boatclick == 5
                DrawShipPlacement(x, y, P2.AIR, P2, pb_PlaceShips_P2);
        }

        #region vs Computer
        private void pb_COM_Grid_MouseClick(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(Math.Floor(e.X / 30.0)) + 1;
            int y = Convert.ToInt32(Math.Floor(e.Y / 30.0)) + 1;

            if (P1.getTurn() == true)
            {
                P1.checkHit(P2, x, y);

                pb_COM_Grid.Invalidate();
                P1.SwitchTurn(P2);
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

        private void TurnCOM()
        {
            while (P2.getTurn() == true)
            {
                //Check if currently hunting specific ship
                if (P1.PB.getHunt() == true)
                {
                    P1.PB.HuntMode(P1, P2);
                }
                else if (P1.SUB.getHunt() == true)
                {
                    P1.SUB.HuntMode(P1, P2);
                }
                else if (P1.DES.getHunt() == true)
                {
                    P1.DES.HuntMode(P1, P2);
                }
                else if (P1.BAT.getHunt() == true)
                {
                    P1.BAT.HuntMode(P1, P2);
                }
                else if (P1.AIR.getHunt() == true)
                {
                    P1.AIR.HuntMode(P1, P2);
                }
                //if points to hit in list 
                else if (P2.getToHitLength() == 0)
                {
                    Random rnd = new Random();
                    int x, y;
                    x = rnd.Next(0, 10);
                    y = rnd.Next(0, 10);
                    if (P1.getPlayerArray(x, y) > 0)
                    {
                        P2.setFirstHit(x, y);
                        P1.setPlayerArray(x, y, -1);
                        P2.setHitCount(P2.getHitCount() + 1);
                        //if surrounding points have not been hit, add to list of points to hit
                        if (x - 1 >= 0 && P1.getPlayerArray(x - 1, y) >= 0)
                        {
                            P2.AddToHit(x - 1, y);
                        }
                        if (x + 1 <= 9 && P1.getPlayerArray(x + 1, y) >= 0)
                        {
                            P2.AddToHit(x + 1, y);
                        }
                        if (y - 1 >= 0 && P1.getPlayerArray(x, y - 1) >= 0)
                        {
                            P2.AddToHit(x, y - 1);
                        }
                        if (y + 1 <= 9 && P1.getPlayerArray(x, y + 1) >= 0)
                        {
                            P2.AddToHit(x, y + 1);
                        }
                        P1.SwitchTurn(P2);
                    }
                    else if (P1.getPlayerArray(x, y) == 0)
                    {
                        P1.setPlayerArray(x, y, -2);
                        P1.SwitchTurn(P2);
                    }
                    else
                    {

                    }
                }
                else
                {
                    //if hit spot contains a ship 
                    if (P1.getPlayerArray(P2.getToHitPoint()) > 0)
                    {
                        //sees which type of ship is being hit and enables corresponding direction 
                        if (P1.getPlayerArray(P2.getToHitPoint()) == 1)
                        {
                            P1.PB.setHunt(true);
                            if (P2.getFirstHit().X + 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X + 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.PB.setTargetModeHR(true);
                            }
                            else if (P2.getFirstHit().X - 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X - 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.PB.setTargetModeHL(true);
                            }
                            else if (P2.getFirstHit().Y + 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y + 1) > -1)
                            {
                                P1.PB.setTargetModeVD(true);
                            }
                            else if (P2.getFirstHit().Y - 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y - 1) > -1)
                            {
                                P1.PB.setTargetModeVU(true);
                            }
                        }
                        else if (P1.getPlayerArray(P2.getToHitPoint()) == 2)
                        {
                            P1.SUB.setHunt(true);
                            if (P2.getFirstHit().X + 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X + 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.SUB.setTargetModeHR(true);
                            }
                            else if (P2.getFirstHit().X - 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X - 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.SUB.setTargetModeHL(true);
                            }
                            else if (P2.getFirstHit().Y + 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y + 1) > -1)
                            {
                                P1.SUB.setTargetModeVD(true);
                            }
                            else if (P2.getFirstHit().Y - 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y - 1) > -1)
                            {
                                P1.SUB.setTargetModeVU(true);
                            }
                        }
                        else if (P1.getPlayerArray(P2.getToHitPoint()) == 3)
                        {
                            P1.DES.setHunt(true);
                            if (P2.getFirstHit().X + 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X + 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.DES.setTargetModeHR(true);
                            }
                            else if (P2.getFirstHit().X - 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X - 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.DES.setTargetModeHL(true);
                            }
                            else if (P2.getFirstHit().Y + 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y + 1) > -1)
                            {
                                P1.DES.setTargetModeVD(true);
                            }
                            else if (P2.getFirstHit().Y - 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y - 1) > -1)
                            {
                                P1.DES.setTargetModeVU(true);
                            }
                        }
                        else if (P1.getPlayerArray(P2.getToHitPoint()) == 4)
                        {
                            P1.BAT.setHunt(true);
                            if (P2.getFirstHit().X + 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X + 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.BAT.setTargetModeHR(true);
                            }
                            else if (P2.getFirstHit().X - 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X - 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.BAT.setTargetModeHL(true);
                            }
                            else if (P2.getFirstHit().Y + 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y + 1) > -1)
                            {
                                P1.BAT.setTargetModeVD(true);
                            }
                            else if (P2.getFirstHit().Y - 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y - 1) > -1)
                            {
                                P1.BAT.setTargetModeVU(true);
                            }
                        }
                        else if (P1.getPlayerArray(P2.getToHitPoint()) == 5)
                        {
                            P1.AIR.setHunt(true);
                            if (P2.getFirstHit().X + 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X + 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.AIR.setTargetModeHR(true);
                            }
                            else if (P2.getFirstHit().X - 1 == P2.getToHitPoint().X && P1.getPlayerArray(P2.getToHitPoint().X - 1, P2.getToHitPoint().Y) > -1)
                            {
                                P1.AIR.setTargetModeHL(true);
                            }
                            else if (P2.getFirstHit().Y + 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y + 1) > -1)
                            {
                                P1.AIR.setTargetModeVD(true);
                            }
                            else if (P2.getFirstHit().Y - 1 == P2.getToHitPoint().Y && P1.getPlayerArray(P2.getToHitPoint().X, P2.getToHitPoint().Y - 1) > -1)
                            {
                                P1.AIR.setTargetModeVU(true);
                            }

                        }
                        P1.setPlayerArray(P2.getToHitPoint(), -1);
                        P2.ClearToHitPoints();

                        //Check orientation and direction for next hits


                        //if surrounding points have not been hit, add to list of points to hit
                        //if (x - 1 >= 0 && P1.getPlayerArray(x - 1, y) >= 0)
                        //{
                        //    P2.AddToHit(x - 1, y);
                        //}
                        //if (x + 1 <= 9 && P1.getPlayerArray(x + 1, y) >= 0)
                        //{
                        //    P2.AddToHit(x + 1, y);
                        //}
                        //if (y - 1 >= 0 && P1.getPlayerArray(x, y - 1) >= 0)
                        //{
                        //    P2.AddToHit(x, y - 1);
                        //}
                        //if (y + 1 <= 9 && P1.getPlayerArray(x, y + 1) >= 0)
                        //{
                        //    P2.AddToHit(x, y + 1);
                        //}
                    }
                    else
                    {
                        P1.setPlayerArray(P2.getToHitPoint(), -2);
                        P2.RemoveToHitPoint();
                    }
                    P1.SwitchTurn(P2);
                }
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
            PaintGrid(P1, e.Graphics, pb_P1_Grid);
        }

        private void pb_COM_Grid_Paint(object sender, PaintEventArgs e)
        {
            PaintGrid(P2, e.Graphics, pb_COM_Grid);
        }
        #endregion

        #region 2Player
        private void pb_P1onP1_Paint(object sender, PaintEventArgs e)
        {
            PaintGrid(P1, e.Graphics, pb_P1onP1);
        }

        private void pb_P2onP1_Paint(object sender, PaintEventArgs e)
        {
            PaintGrid(P2, e.Graphics, pb_P2onP1);
        }

        private void pb_P2onP1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(Math.Floor(e.X / 30.0)) + 1;
            int y = Convert.ToInt32(Math.Floor(e.Y / 30.0)) + 1;

            if (P1.getTurn() == true)
            {
                P1.checkHit(P2, x, y);

                pb_P2onP1.Invalidate();
                P1.SwitchTurn(P2);
            }

            if (P1.isWinner() == true)
            {
                pb_P1onP1.Enabled = false;
                pb_P2onP1.Enabled = false;
                //do win stuff
                P1.setScore(P1.getScore() + 1);
            }
            else
            {
                P1.SwitchTurn(P2);
            }
        }

        private void btnNext_7_Click(object sender, EventArgs e)
        {
            if (P1.getTurn() == true)
                MessageBox.Show("You have not completed your turn yet.");
            else
                pg8_GameTime_P2.Visible = true;
            
        }
        
        private void pb_P2onP2_Paint(object sender, PaintEventArgs e)
        {
            PaintGrid(P2, e.Graphics, pb_P2onP2);
        }

        private void pb_P1onP2_Paint(object sender, PaintEventArgs e)
        {
            PaintGrid(P1, e.Graphics, pb_P1onP2);
        }

        private void pb_P1onP2_MouseClick(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(Math.Floor(e.X / 30.0)) + 1;
            int y = Convert.ToInt32(Math.Floor(e.Y / 30.0)) + 1;

            if (P2.getTurn() == true)
            {
                P2.checkHit(P1, x, y);

                pb_P1onP2.Invalidate();
                P2.SwitchTurn(P1);
            }

            if (P2.isWinner() == true)
            {
                pb_P2onP2.Enabled = false;
                pb_P1onP2.Enabled = false;
                //do win stuff
                P2.setScore(P2.getScore() + 1);
            }
            else
            {
                P2.SwitchTurn(P1);
            }
        }

        private void btnNext_8_Click(object sender, EventArgs e)
        {
            if (P2.getTurn() == true)
                MessageBox.Show("You have not completed your turn yet.");
            else
                pg8_GameTime_P2.Visible = false;
        }
#endregion

    }
}

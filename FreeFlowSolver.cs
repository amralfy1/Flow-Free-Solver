using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flow_Solver
{
    struct node
    {
        public char c;
        public int step;
        public Color color;
    };
    class FreeFlowSolver
    {
        //To Store Current Grid
        char[,] Grid = new char[15, 15]; //θ(1)
        int[,] Step = new int[15, 15];
        bool[,] input = new bool[15, 15];
        int[] Weight = new int[200];
        int[] Have = new int[100];
        int[] Parent = new int[200];
        bool[,] CanMove = new bool[100, 100];
        bool[,] Visit = new bool[15, 15];
        int[] appear = new int[200];
        bool[,] VisCol = new bool[15, 15];
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };
        public node[,] solved;
        bool FoundSolution = false;
        int HaveTwo = 1;
        int n;
        bool ValidPos(int x, int y)
        {
            return (x >= 1) && (x <= n) && (y >= 1) && (y <= n);//θ(1)
        }
        int findparent(int u)//θ(1) //Log*
        {
            if (u == Parent[u]) return u;//θ(1)
            return Parent[u] = findparent(Parent[u]);//θ(1) //Log*
        }
        bool isconnected(int u, int v)//θ(1)
        {
            return findparent(u) == findparent(v);//θ(1)
        }
        void connect(int a, int b)//θ(1)
        {
            a = findparent(a); b = findparent(b); //Log* = //θ(1)
            if (a == b) return;//θ(1)
            if (Weight[b] > Weight[a])//θ(1)
            {
                int tmp = a;//θ(1)
                a = b;//θ(1)
                b = tmp;//θ(1)
            }
            Parent[b] = a;//θ(1)
            Weight[a] += Weight[b];//θ(1)
        }
        void connectall()//θ(N^2)
        {
            for (int x = 1; x <= n; x++)//θ(N^2)
            {
                for (int y = 1; y <= n; y++)//θ(N)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int newx = x + dx[i], newy = y + dy[i];
                        if (ValidPos(x + dx[i], y + dy[i]) && ((Grid[x + dx[i], y + dy[i]] == '.') || (Grid[x, y] == Grid[x + dx[i], y + dy[i]])) && CanMove[x + dx[i], y + dy[i]])
                            connect(x * n + y, newx * n + newy);//Log* = //θ(1)
                    }
                }
            }
        }
        void InitDsu()//θ(N^2)
        {
            for (int i = 1; i <= n; i++) //θ(N^2)
                for (int j = 1; j <= n; j++) ///θ(N)
                {
                    Parent[i * n + j] = i * n + j;//θ(1)
                    Weight[i * n + j] = 1;//θ(1)
                }
            connectall();//θ(N^2)
        }
        bool checkconnection()
        {
            for (int i = 0; i < 200; i++) appear[i] = -1;//θ(1)
            for (int i = 1; i <= n; i++)//θ(N^2)
            {
                for (int j = 1; j <= n; j++)//θ(N)
                {
                    if (CanMove[i, j] && Grid[i, j] != '.')//θ(1)
                    {
                        if (appear[Grid[i, j] - 'A'] == -1)//θ(1)
                        {
                            appear[Grid[i, j] - 'A'] = i * n + j;//θ(1)
                        }
                        else
                        {
                            if (!isconnected(i * n + j, appear[Grid[i, j] - 'A'])) return true;//θ(1)
                        }
                    }
                }
            }
            return false;//θ(1)
        }
        int CountMoves(int x, int y)
        {
            int ret = 0;//θ(1)
            if ((!CanMove[x, y]) || Grid[x, y] == '.') return 0;//θ(1)
            for (int i = 0; i < 4; i++) if (ValidPos(x + dx[i], y + dy[i]) && ((Grid[x + dx[i], y + dy[i]] == '.') || (Grid[x, y] == Grid[x + dx[i], y + dy[i]])) && CanMove[x + dx[i], y + dy[i]]) ret++;//θ(1)
            return ret;//θ(1)
        }
        bool deadCell()
        {
            for (int x = 1; x <= n; x++)//θ(N^2)
            {
                for (int y = 1; y <= n; y++)//θ(N)
                {
                    if (Grid[x, y] != '.') continue;//θ(1)
                    int cnt = 0;//θ(1)
                    for (int i = 0; i < 4; i++)//θ(1)
                    {
                        if (!ValidPos(x + dx[i], y + dy[i])) continue;//θ(1)
                        if (CanMove[x + dx[i], y + dy[i]]) cnt++;//θ(1)
                    }
                    if (cnt <= 1) return true;//θ(1)
                }
            }
            return false;//θ(1)
        }
        bool invalidGrid()
        {
            int[] flag = new int[100]; //θ(1)
            int opens = 0, cells = 0;
            bool[] flag2 = new bool[100];
            for (int i = 1; i <= n; i++)
            {
                cells = 0;
                for (int j = 0; j <= n; j++) flag2[j] = false;
                for (int j = 1; j <= n; j++)
                {
                    if (Grid[i, j] == '.') cells++;
                    else if (CanMove[i, j])
                    {
                        if (flag[Grid[i, j] - 'A'] == 0)
                        {
                            
                            flag2[Grid[i, j] - 'A'] = true;
                            flag[Grid[i, j] - 'A'] = 1;
                        }
                        else
                        {
                            if (flag2[Grid[i, j] - 'A'])
                            {
                                flag2[Grid[i, j] - 'A'] = false;
                            }
                            else
                            {
                                opens--;
                            }
                            flag[Grid[i, j] - 'A'] = 0;
                        }
                    }
                }
                if (opens > cells) return true;
                for (int j = 0; j <= n; j++)
                {
                    if (flag2[j]) opens++;
                }
            }
            opens = 0;
            cells = 0;

            for (int j = 1; j <= n; j++)
            {
                cells = 0;
                for (int i = 0; i <= n; i++) flag2[i] = false;
                for (int i = 1; i <= n; i++)
                {
                    if (Grid[i, j] == '.') cells++;
                    else if (CanMove[i, j])
                    {
                        if (flag[Grid[i, j] - 'A'] == 0)
                        {
                            flag2[Grid[i, j] - 'A'] = true;
                            flag[Grid[i, j] - 'A'] = 1;
                        }
                        else
                        {
                            if (flag2[Grid[i, j] - 'A'])
                            {
                                flag2[Grid[i, j] - 'A'] = false;
                            }
                            else
                            {
                                opens--;
                            }
                            flag[Grid[i, j] - 'A'] = 0;
                        }
                    }
                }
                if (opens > cells) return true;
                for (int i = 0; i <= n; i++)
                {
                    if (flag2[i]) opens++;
                }
            }
            return false;
        }
        Tuple<int, int> chooseLow()
        {
            int mn = 2000000000;//θ(1)
            int retx = 0, rety = 0;//θ(1)
            for (int i = 1; i <= n; i++)//θ(N^2)
            {
                for (int j = 1; j <= n; j++)//θ(N)
                {
                    int cnt = CountMoves(i, j);//θ(1)
                    if (CanMove[i, j] && Grid[i, j] != '.' && cnt >= 1 && cnt < mn)//θ(1)
                    {
                        mn = cnt;//θ(1)
                        retx = i;//θ(1)
                        rety = j;//θ(1)
                    }
                }
            }
            return new Tuple<int, int>(retx, rety);//θ(1)
        }
        bool forced()
        {
            for (int x = 1; x <= n; x++)//θ(N^2)
            {
                for (int y = 1; y <= n; y++)//θ(N)
                {
                    if (CountMoves(x, y) == 1 && Grid[x, y] != '.') //θ(1)
                    {
                        for (int i = 0; i < 4; i++)//θ(1)
                        {
                            int NewX = x + dx[i], NewY = y + dy[i];//θ(1)
                            if (ValidPos(x + dx[i], y + dy[i]) && ((Grid[x + dx[i], y + dy[i]] == '.') || (Grid[x, y] == Grid[x + dx[i], y + dy[i]])) && CanMove[x + dx[i], y + dy[i]])//θ(1)
                            {
                                CanMove[x, y] = false;//θ(1)
                                if (Grid[NewX, NewY] == Grid[x, y]) CanMove[NewX, NewY] = false;//θ(1)
                                Grid[NewX, NewY] = Grid[x, y];//θ(1)
                                Step[NewX, NewY] = Step[x, y] + 1;
                                return true;//θ(1)
                            }
                        }

                    }
                }
            }
            for (int x = 1; x <= n; x++)//θ(N^2)
            {
                for (int y = 1; y <= n; y++)//θ(N)
                {
                    if (Grid[x, y] != '.') continue;//θ(1)
                    int CntColor = 0, CntDots = 0;//θ(1)
                    int Xcolor = -1, Ycolor = -1;//θ(1)
                    for (int i = 0; i < 4; i++)//θ(1)
                    {
                        int NewX = x + dx[i], NewY = y + dy[i];//θ(1)
                        if (ValidPos(NewX, NewY) && Grid[NewX, NewY] != '.' && CanMove[NewX, NewY])//θ(1)
                        {
                            CntColor++;//θ(1)
                            Xcolor = NewX;//θ(1)
                            Ycolor = NewY;//θ(1)
                        }
                        if (ValidPos(NewX, NewY) && Grid[NewX, NewY] == '.')//θ(1)
                        {
                            CntDots++;//θ(1)
                        }
                    }
                    if (CntColor == 1 && CntDots == 1)
                    {
                        CanMove[Xcolor, Ycolor] = false;//θ(1)
                        Grid[x, y] = Grid[Xcolor, Ycolor];//θ(1)
                        Step[x, y] = Step[Xcolor, Ycolor] + 1;
                        return true;//θ(1)
                    }
                }
            }
            return false;//θ(1)
        }
        void dfs(int x, int y)
        {
            if (CanMove[x, y] && Grid[x, y] != '.')//θ(1)
            {
                Have[Grid[x, y] - 'A']++;//θ(1)
                if (Have[Grid[x, y] - 'A'] == 2) HaveTwo = 1;//θ(1)
                return;//θ(1)
            }
            Visit[x, y] = true;//θ(1)
            for (int i = 0; i < 4; i++)//θ(1)
            {
                if (ValidPos(x + dx[i], y + dy[i]) && CanMove[x + dx[i], y + dy[i]] && !Visit[x + dx[i], y + dy[i]])//θ(1)
                {
                    dfs(x + dx[i], y + dy[i]);
                }
            }
        }
        bool component()
        {
            for (int x = 0; x < 15; x++)//θ(1)
            {
                for (int y = 0; y < 15; y++)//θ(1)
                {
                    Visit[x, y] = false;//θ(1)
                }
            }
            for (int x = 1; x <= n; x++)//θ(N^2)
                for (int y = 1; y <= n; y++)//θ(N)
                {
                    if (CanMove[x, y] && !Visit[x, y] && Grid[x, y] == '.')//θ(1)
                    {
                        HaveTwo = 0;//θ(1)
                        for (int ii = 0; ii < 15; ii++)//θ(1)
                        {
                            Have[ii] = 0;//θ(1)
                        }
                        dfs(x, y);//Total θ(N^2)
                        if (HaveTwo != 1)//θ(1)
                        {
                            return true;//θ(1)
                        }
                    }
                }
            return false;//θ(1)
        }
        void solve()
        {
            if (FoundSolution == true) return;//θ(1)
            while (forced()) { }// O(N^4)
            Tuple<int, int> S = chooseLow();//θ(N^2)
            int X = S.Item1, Y = S.Item2;
            if (deadCell()) //θ(N^2)
            {
                return;//θ(1)
            }
            if (invalidGrid())//θ(N^2)
            {
                return;//θ(1)
            }
            InitDsu();//θ(N^2)
            if (checkconnection())//θ(N^2)
            {
                return;//θ(1)
            }
            if (component())//θ(N^2)
            {
                return;//θ(1)
            }
            bool Solution = true;
            for (int i = 1; i <= n; i++)//θ(N^2)
            {
                for (int j = 1; j <= n; j++)//θ(N)
                {
                    if (CanMove[i, j]) Solution = false;//θ(1)
                }
            }
            if (Solution == true)//θ(1)
            {
                FoundSolution = true;//θ(1)
                return;//θ(1)
            }
            for (int i = 0; i < 4; i++)//θ(N^2)
            {
                int NewX = X + dx[i], NewY = Y + dy[i]; // θ(1)
                char[,] CopyGrid = new char[15, 15]; // θ(1)
                int[,] CopyStep = new int[15, 15];
                bool[,] CopyCanMove = new bool[15, 15]; // θ(1)
                for (int a = 1; a <= n; a++)  // θ(N^2)
                    for (int b = 1; b <= n; b++) // θ(N)
                    {
                        CopyGrid[a, b] = Grid[a, b]; // θ(1)
                        CopyStep[a, b] = Step[a, b];
                        CopyCanMove[a, b] = CanMove[a, b]; // θ(1)
                    }
                CanMove[X, Y] = false;  // θ(1)
                if (ValidPos(NewX, NewY) && CanMove[NewX, NewY] && Grid[NewX, NewY] == '.') // θ(1)
                {
                    Grid[NewX, NewY] = Grid[X, Y]; // θ(1)
                    Step[NewX, NewY] = Step[X, Y] + 1;
                    solve();
                    if (FoundSolution == true) return; // θ(1)
                }
                else if (ValidPos(NewX, NewY) && CanMove[NewX, NewY] && Grid[NewX, NewY] == Grid[X, Y]) // θ(1)
                {
                    CanMove[NewX, NewY] = false; // θ(1)
                    solve();
                    if (FoundSolution == true) return; // θ(1)
                }
                for (int a = 1; a <= n; a++)  // θ(N^2)
                    for (int b = 1; b <= n; b++) // θ(N)
                    {
                        Grid[a, b] = CopyGrid[a, b]; // θ(1)
                        Step[a, b] = CopyStep[a, b];
                        CanMove[a, b] = CopyCanMove[a, b]; // θ(1)
                    }
            }
        }

        Tuple<int, int> GetNext(int x, int y)
        {
            int retx = 0, rety = 0, mx = -1;
            for (int i = 0; i < 4; i++)
            {
                int Newx = x + dx[i], Newy = y + dy[i];
                if (Step[Newx, Newy] > mx && !VisCol[Newx, Newy] && Grid[x, y] == Grid[Newx, Newy] && ValidPos(Newx, Newy) == true)
                {
                    mx = Step[Newx, Newy];
                    retx = Newx;
                    rety = Newy;
                }
            }
            return new Tuple<int, int>(retx, rety);
        }
        void GetPath(int x, int y, int cnt)
        {

            VisCol[x, y] = true;
            Step[x, y] = cnt;
            if (input[x, y] == true && cnt != 1) return;
            Tuple<int, int> GN = GetNext(x, y);
            GetPath(GN.Item1, GN.Item2, cnt + 1);
        }
        public void ConstructStep()
        {
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (!VisCol[i, j] && input[i, j] == true && Grid[i, j] != '.')
                    {

                        GetPath(i, j, 1);
                    }
                }
            }
        }
        public FreeFlowSolver(char[,] g, int num)
        {
           
            n = num;
            solved = new node[n + 1, n + 1];
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    this.Grid[i, j] = g[i - 1, j - 1];
                    this.CanMove[i, j] = true;
                    if (Grid[i, j] != '.') input[i, j] = true;
                }
            }
            Stopwatch start = Stopwatch.StartNew();
            solve();
            start.Stop();
            ConstructStep();
            TimeSpan u = start.Elapsed;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {

                    solved[i, j].c = this.Grid[i, j];
                    solved[i, j].step = this.Step[i, j];

                }
            }
            for (int i = 1; i <= n; i++)
            {
                string s = "";
                for (int j = 1; j <= n; j++)
                {

                    solved[i, j].c = this.Grid[i, j];
                    solved[i, j].step = this.Step[i, j];
                  //  s += solved[i, j].step.ToString()+" ";
                    
                }
                //MessageBox.Show(s);
            }
            // system("pause");
        }
    }
}

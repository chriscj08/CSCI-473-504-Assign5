using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Chris_Parker_Assignment5
{
    public enum Difficulty { Easy, Medium, Hard };


    public partial class Form1 : Form
    {
        private static List<Puzzle> easyPuzzles;
        private static List<Puzzle> medPuzzles;
        private static List<Puzzle> hardPuzzles;
        private static bool easyDone;
        private static bool medDone;
        private static bool hardDone;
        private static TextBox[,] easyMode;
        private static TextBox[,] medMode;
        private static TextBox[,] hardMode;
        private static int index;        
        private Stopwatch stopwatch;
        private TimeSpan elapsedTime = new TimeSpan();
        private static bool switcher;
        private static string diffSetting;
        private static int sum;        


        public Form1()
        {
            InitializeComponent();
            easyPuzzles = new List<Puzzle>();
            medPuzzles = new List<Puzzle>();
            hardPuzzles = new List<Puzzle>();
            easyMode = new TextBox[5, 5];
            medMode = new TextBox[7, 7];
            hardMode = new TextBox[9, 9];
            switcher = false;            
            index = 0;
            stopwatch = new Stopwatch();   
            diffSetting = "Easy";
            puzzleDiff.DataSource = Enum.GetValues(typeof(Difficulty));            
            BuildEasyMode();
            BuildMedMode();
            BuildHardMode();
            easyDone = false;
            medDone = false;
            hardDone = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
        }
        //Still a work in progress 
        public void ReadPuzzles()
        {
            using (StreamReader inFile = new StreamReader("directory.txt"))
            {
                string fileName = inFile.ReadLine(); //Read the first puzzle file

                while (fileName != null)
                {
                    using (StreamReader inFile2 = new StreamReader(fileName)) //Now we can open the puzzle file
                    {
                        string input = inFile2.ReadLine(); //Read the first line of the puzzle

                        while (input != null)
                        {
                            int i = 0;
                            int j = 0;
                            int sum = 0;

                            if (input.Length == 3) //Easy puzzle
                            {
                                int[,] tempArray = new int[5, 5];
                                int[,] tempArray2 = new int[5, 5];

                                //Create Initial Puzzle 2D Array
                                for (i = 0; i < 3; i++)
                                {
                                    sum = 0; //Reset for a new row

                                    //Populate what will be initialPuzzle
                                    for (j = 0; j < 3; j++)
                                    {
                                        tempArray[i, j] = (int)Char.GetNumericValue(input[j]);
                                        sum += tempArray[i, j];
                                    }
                                    tempArray[i, 3] = sum; //Set the 4th column sums

                                    input = inFile2.ReadLine();
                                }

                                //Get initial sums for columns 1 through 3
                                for (j = 0; j < 3; j++)
                                {
                                    sum = 0;

                                    for (i = 0; i < 3; i++)
                                    {
                                        sum += tempArray[i, j];
                                    }
                                    tempArray[3, j] = sum;
                                }
                                for (i = 0; i < 3; i++) //Get diagonal sum
                                    tempArray[3, 3] += tempArray[i, i];

                                input = inFile2.ReadLine();

                                //Create Solution Puzzle 2D Array
                                for (i = 0; i < 3; i++)
                                {
                                    sum = 0; //Reset for a new row

                                    //Populate what will be solutionPuzzle
                                    for (j = 0; j < 3; j++)
                                    {
                                        tempArray2[i, j] = (int)Char.GetNumericValue(input[j]);
                                        sum += tempArray2[i, j];
                                    }
                                    tempArray2[i, 3] = sum; //Set the 4th column sums
                                    tempArray2[i, 4] = sum;

                                    input = inFile2.ReadLine();
                                }

                                //Get initial sums for columns 1 through 3
                                for (j = 0; j < 3; j++)
                                {
                                    sum = 0;

                                    for (i = 0; i < 3; i++)
                                    {
                                        sum += tempArray2[i, j];
                                    }
                                    tempArray2[3, j] = sum;
                                    tempArray2[4, j] = sum;
                                }
                                for (i = 0; i < 3; i++) //Get diagonal sum
                                    tempArray2[3, 3] += tempArray2[i, i];
                                tempArray2[4, 4] = tempArray2[3, 3];

                                //Set initial puzzle correct sums equal to solution puzzle correct sums
                                for (j = 0; j < 5; j++)
                                    tempArray[4, j] = tempArray2[4, j];
                                for (i = 0; i < 4; i++)
                                    tempArray[i, 4] = tempArray2[i, 4];

                                Puzzle tempPuzzle = new Puzzle(tempArray, tempArray, tempArray2, 0);
                                easyPuzzles.Add(tempPuzzle);

                            }
                            else if (input.Length == 5)//Medium puzzle
                            {
                                int[,] tempArray = new int[7, 7];
                                int[,] tempArray2 = new int[7, 7];

                                //Create Initial Puzzle 2D Array
                                for (i = 0; i < 5; i++)
                                {
                                    sum = 0; //Reset for a new row

                                    //Populate what will be initialPuzzle
                                    for (j = 0; j < 5; j++)
                                    {
                                        tempArray[i, j] = (int)Char.GetNumericValue(input[j]);
                                        sum += tempArray[i, j];
                                    }
                                    tempArray[i, 5] = sum; //Set the 6th column sums

                                    input = inFile2.ReadLine();
                                }

                                //Get initial sums for columns 1 through 5
                                for (j = 0; j < 5; j++)
                                {
                                    sum = 0;

                                    for (i = 0; i < 5; i++)
                                    {
                                        sum += tempArray[i, j];
                                    }
                                    tempArray[5, j] = sum;
                                }
                                for (i = 0; i < 5; i++) //Get diagonal sum
                                    tempArray[5, 5] += tempArray[i, i];

                                input = inFile2.ReadLine();

                                //Create Solution Puzzle 2D Array
                                for (i = 0; i < 5; i++)
                                {
                                    sum = 0; //Reset for a new row

                                    //Populate what will be solutionPuzzle
                                    for (j = 0; j < 5; j++)
                                    {
                                        tempArray2[i, j] = (int)Char.GetNumericValue(input[j]);
                                        sum += tempArray2[i, j];
                                    }
                                    tempArray2[i, 5] = sum; //Set the 6th column sums
                                    tempArray2[i, 6] = sum;

                                    input = inFile2.ReadLine();
                                }

                                //Get initial sums for columns 1 through 5
                                for (j = 0; j < 5; j++)
                                {
                                    sum = 0;

                                    for (i = 0; i < 5; i++)
                                    {
                                        sum += tempArray2[i, j];
                                    }
                                    tempArray2[5, j] = sum;
                                    tempArray2[6, j] = sum;
                                }
                                for (i = 0; i < 5; i++) //Get diagonal sum
                                    tempArray2[5, 5] += tempArray2[i, i];
                                tempArray2[6, 6] = tempArray2[5, 5];
                                tempArray[6, 6] = tempArray2[6, 6];

                                //Set initial puzzle correct sums equal to solution puzzle correct sums
                                for (j = 0; j < 6; j++)
                                    tempArray[6, j] = tempArray2[6, j];
                                for (i = 0; i < 5; i++)
                                    tempArray[i, 6] = tempArray2[i, 6];

                                Puzzle tempPuzzle = new Puzzle(tempArray, tempArray, tempArray2, (Difficulty)1);
                                medPuzzles.Add(tempPuzzle);
                            }
                            else if (input.Length == 7)//Hard puzzle
                            {
                                int[,] tempArray = new int[9, 9];
                                int[,] tempArray2 = new int[9, 9];

                                //Create Initial Puzzle 2D Array
                                for (i = 0; i < 7; i++)
                                {
                                    sum = 0; //Reset for a new row

                                    //Populate what will be initialPuzzle
                                    for (j = 0; j < 7; j++)
                                    {
                                        tempArray[i, j] = (int)Char.GetNumericValue(input[j]);
                                        sum += tempArray[i, j];
                                    }
                                    tempArray[i, 7] = sum; //Set the 4th column sums

                                    input = inFile2.ReadLine();
                                }

                                //Get initial sums for columns 1 through 3
                                for (j = 0; j < 7; j++)
                                {
                                    sum = 0;

                                    for (i = 0; i < 7; i++)
                                    {
                                        sum += tempArray[i, j];
                                    }
                                    tempArray[7, j] = sum;
                                }
                                for (i = 0; i < 7; i++) //Get diagonal sum
                                    tempArray[7, 7] += tempArray[i, i];

                                input = inFile2.ReadLine();

                                //Create Solution Puzzle 2D Array
                                for (i = 0; i < 7; i++)
                                {
                                    sum = 0; //Reset for a new row

                                    //Populate what will be solutionPuzzle
                                    for (j = 0; j < 7; j++)
                                    {
                                        tempArray2[i, j] = (int)Char.GetNumericValue(input[j]);
                                        sum += tempArray2[i, j];
                                    }
                                    tempArray2[i, 7] = sum; //Set the 4th column sums
                                    tempArray2[i, 8] = sum;

                                    input = inFile2.ReadLine();
                                }

                                //Get initial sums for columns 1 through 3
                                for (j = 0; j < 7; j++)
                                {
                                    sum = 0;

                                    for (i = 0; i < 7; i++)
                                    {
                                        sum += tempArray2[i, j];
                                    }
                                    tempArray2[7, j] = sum;
                                    tempArray2[8, j] = sum;
                                }
                                for (i = 0; i < 7; i++) //Get diagonal sum
                                    tempArray2[7, 7] += tempArray2[i, i];
                                tempArray2[8, 8] = tempArray2[7, 7];

                                //Set initial puzzle correct sums equal to solution puzzle correct sums
                                for (j = 0; j < 9; j++)
                                    tempArray[8, j] = tempArray2[8, j];
                                for (i = 0; i < 8; i++)
                                    tempArray[i, 8] = tempArray2[i, 8];

                                Puzzle tempPuzzle = new Puzzle(tempArray, tempArray, tempArray2, (Difficulty)2);
                                hardPuzzles.Add(tempPuzzle);
                            }

                            sum = 0;
                            input = inFile2.ReadLine();
                        }
                    }

                    fileName = inFile.ReadLine();
                }
            }
        }//End of ReadPuzzles

        public void BuildEasyMode()
        {
            easyMode[0, 0] = c3; easyMode[0, 1] = c4; easyMode[0, 2] = c5; easyMode[0, 3] = c6; easyMode[0, 4] = c7;
            easyMode[1, 0] = d3; easyMode[1, 1] = d4; easyMode[1, 2] = d5; easyMode[1, 3] = d6; easyMode[1, 4] = d7;
            easyMode[2, 0] = e3; easyMode[2, 1] = e4; easyMode[2, 2] = e5; easyMode[2, 3] = e6; easyMode[2, 4] = e7;
            easyMode[3, 0] = f3; easyMode[3, 1] = f4; easyMode[3, 2] = f5; easyMode[3, 3] = f6; easyMode[3, 4] = f7;
            easyMode[4, 0] = g3; easyMode[4, 1] = g4; easyMode[4, 2] = g5; easyMode[4, 3] = g6; easyMode[4, 4] = g7;

        }


        public void BuildMedMode()
        {
            medMode[0, 0] = b2; medMode[0, 1] = b3; medMode[0, 2] = b4; medMode[0, 3] = b5; medMode[0, 4] = b6; medMode[0, 5] = b7; medMode[0, 6] = b8;
            medMode[1, 0] = c2; medMode[1, 1] = c3; medMode[1, 2] = c4; medMode[1, 3] = c5; medMode[1, 4] = c6; medMode[1, 5] = c7; medMode[1, 6] = c8;
            medMode[2, 0] = d2; medMode[2, 1] = d3; medMode[2, 2] = d4; medMode[2, 3] = d5; medMode[2, 4] = d6; medMode[2, 5] = d7; medMode[2, 6] = d8;
            medMode[3, 0] = e2; medMode[3, 1] = e3; medMode[3, 2] = e4; medMode[3, 3] = e5; medMode[3, 4] = e6; medMode[3, 5] = e7; medMode[3, 6] = e8;
            medMode[4, 0] = f2; medMode[4, 1] = f3; medMode[4, 2] = f4; medMode[4, 3] = f5; medMode[4, 4] = f6; medMode[4, 5] = f7; medMode[4, 6] = f8;
            medMode[5, 0] = g2; medMode[5, 1] = g3; medMode[5, 2] = g4; medMode[5, 3] = g5; medMode[5, 4] = g6; medMode[5, 5] = g7; medMode[5, 6] = g8;
            medMode[6, 0] = h2; medMode[6, 1] = h3; medMode[6, 2] = h4; medMode[6, 3] = h5; medMode[6, 4] = h6; medMode[6, 5] = h7; medMode[6, 6] = h8;
        }

        public void BuildHardMode()
        {
            hardMode[0, 0] = a1; hardMode[0, 1] = a2; hardMode[0, 2] = a3; hardMode[0, 3] = a4; hardMode[0, 4] = a5; hardMode[0, 5] = a6; hardMode[0, 6] = a7; hardMode[0, 7] = a8; hardMode[0, 8] = a9;
            hardMode[1, 0] = b1; hardMode[1, 1] = b2; hardMode[1, 2] = b3; hardMode[1, 3] = b4; hardMode[1, 4] = b5; hardMode[1, 5] = b6; hardMode[1, 6] = b7; hardMode[1, 7] = b8; hardMode[1, 8] = b9;
            hardMode[2, 0] = c1; hardMode[2, 1] = c2; hardMode[2, 2] = c3; hardMode[2, 3] = c4; hardMode[2, 4] = c5; hardMode[2, 5] = c6; hardMode[2, 6] = c7; hardMode[2, 7] = c8; hardMode[2, 8] = c9;
            hardMode[3, 0] = d1; hardMode[3, 1] = d2; hardMode[3, 2] = d3; hardMode[3, 3] = d4; hardMode[3, 4] = d5; hardMode[3, 5] = d6; hardMode[3, 6] = d7; hardMode[3, 7] = d8; hardMode[3, 8] = d9;
            hardMode[4, 0] = e1; hardMode[4, 1] = e2; hardMode[4, 2] = e3; hardMode[4, 3] = e4; hardMode[4, 4] = e5; hardMode[4, 5] = e6; hardMode[4, 6] = e7; hardMode[4, 7] = e8; hardMode[4, 8] = e9;
            hardMode[5, 0] = f1; hardMode[5, 1] = f2; hardMode[5, 2] = f3; hardMode[5, 3] = f4; hardMode[5, 4] = f5; hardMode[5, 5] = f6; hardMode[5, 6] = f7; hardMode[5, 7] = f8; hardMode[5, 8] = f9;
            hardMode[6, 0] = g1; hardMode[6, 1] = g2; hardMode[6, 2] = g3; hardMode[6, 3] = g4; hardMode[6, 4] = g5; hardMode[6, 5] = g6; hardMode[6, 6] = g7; hardMode[6, 7] = g8; hardMode[6, 8] = g9;
            hardMode[7, 0] = h1; hardMode[7, 1] = h2; hardMode[7, 2] = h3; hardMode[7, 3] = h4; hardMode[7, 4] = h5; hardMode[7, 5] = h6; hardMode[7, 6] = h7; hardMode[7, 7] = h8; hardMode[7, 8] = h9;
            hardMode[8, 0] = i1; hardMode[8, 1] = i2; hardMode[8, 2] = i3; hardMode[8, 3] = i4; hardMode[8, 4] = i5; hardMode[8, 5] = i6; hardMode[8, 6] = i7; hardMode[8, 7] = i8; hardMode[8, 8] = i9;
        }

        /* Method disableTextBoxes
         * Purpose: Disables all textBoxes on the playing field
         * The reasoning is so when generate puzzle is called.
         * It can enable only the textboxes needed for that puzzle
         * 
         * */
        public void disableTextBoxes()
        {
            foreach (TextBox singleItem in hardMode)
            {
                singleItem.Enabled = false;
                singleItem.ForeColor = Color.White;
                singleItem.Text = "";
            }
            MsgBox.Text = "The values are hidden until you start the timer";
        }

        /* Method enableTextBoxes()
         * Purpose: enable the textboxes that will be used for the program
         * So the user can't click a box that is "Hidden" 
         * */
        public void enableTextBoxes()
        {
            //Gets the current difficulty setting
            diffSetting = puzzleDiff.SelectedItem.ToString();
            if (diffSetting == "Easy")
            {
                foreach (TextBox singleItem in easyMode)
                {
                    singleItem.Enabled = true;
                }
            }
            else if (diffSetting == "Medium")
            {
                foreach (TextBox singleItem in medMode)
                {
                    singleItem.Enabled = true;
                }
            }
            else
            {
                foreach (TextBox singleItem in hardMode)
                {
                    singleItem.Enabled = true;
                }
            }
        }
        //This method looks at the puzzle being played and allocates 
        //textboxes with the appropriate values
        public void CreatePlayingField(List<Puzzle> puzzles, int index2, string diffSetting2)
        {

            if (diffSetting2 == "Easy")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        easyMode[i, j].Text = puzzles[index2][i, j].ToString();
                    }
                }
            }
            else if (diffSetting2 == "Medium")
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        medMode[i, j].Text = puzzles[index2][i, j].ToString();
                    }
                }
            }
            if (diffSetting2 == "Hard")
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (hardMode[i, j].Text != puzzles[index2][i, j].ToString() || hardMode[i, j].Text != "")
                        {
                            hardMode[i, j].Text = puzzles[index2][i, j].ToString();
                        }
                    }
                }              

            }

        }
        
        public void CalcNewEasySums(TextBox omega, int index2)
        {
            //Change the puzzle in progress array based on user input
            for (int i = 0; i < 3; i++)
            {
                sum = 0;

                for (int j = 0; j < 3; j++)
                {
                    if (easyMode[i, j].Name == omega.Name)
                    {
                        easyPuzzles[index2][i, j] = (int)Char.GetNumericValue(omega.Text[0]);
                        if (easyPuzzles[index].IsValueInSolution((int)Char.GetNumericValue(omega.Text[0]), i, j))
                        {
                            omega.BackColor = Color.Green;
                            omega.Enabled = false;
                        }
                        else
                        {
                            omega.BackColor = Color.Red;
                        }
                        if (easyPuzzles[index2].IsItCorrect())//********** CHECK FOR CORRECT SOLUTION HERE*********/////
                        {
                            easyDone = true;
                            MsgBox.Clear();
                            MsgBox.Text = "YOU DID IT\r\n";                            
                            elapsedTime = stopwatch.Elapsed;
                            timer1.Stop();
                            stopwatch.Stop();
                            //easyTimes.Add(elapsedTime);
                            MsgBox.Text += String.Format("{0:00}:{1:00}", elapsedTime.Minutes, elapsedTime.Seconds);
                        }
                    }

                    sum += easyPuzzles[index2][i, j]; //calculate new sums for the rows

                }

                easyPuzzles[index2][i, 3] = sum;
            }

            //Calculates new sums for the columns
            for (int j = 0; j < 3; j++)
            {
                sum = 0;

                for (int i = 0; i < 3; i++)
                {
                    sum += easyPuzzles[index2][i, j];
                }
                easyPuzzles[index2][3, j] = sum;
            }

            easyPuzzles[index2][3, 3] = 0;
            for (int i = 0; i < 3; i++) //Get new diagonal sum
                easyPuzzles[index2][3, 3] += easyPuzzles[index2][i, i];

            CreatePlayingField(easyPuzzles, index2, diffSetting);
        }

        public void CalcNewMedSums(TextBox omega, int index2)
        {
            //Change the puzzle in progress array based on user input
            for (int i = 0; i < 5; i++)
            {
                sum = 0;

                for (int j = 0; j < 5; j++)
                {
                    if (medMode[i, j].Name == omega.Name)
                    {                       
                        medPuzzles[index2][i, j] = (int)Char.GetNumericValue(omega.Text[0]);
                        if (medPuzzles[index].IsValueInSolution((int)Char.GetNumericValue(omega.Text[0]), i, j))
                        {
                            omega.BackColor = Color.Green;
                            omega.Enabled = false;
                        }
                        else
                        {
                            omega.BackColor = Color.Red;
                        }
                        if (medPuzzles[index2].IsItCorrect())//********** CHECK FOR CORRECT SOLUTION HERE*********/////
                        {
                            medDone = true;
                        }
                    }
                    sum += medPuzzles[index2][i, j]; //calculate new sums for the rows

                }

                medPuzzles[index2][i, 5] = sum;
            }

            //Calculates new sums for the columns
            for (int j = 0; j < 5; j++)
            {
                sum = 0;

                for (int i = 0; i < 5; i++)
                {
                    sum += medPuzzles[index2][i, j];
                }
                medPuzzles[index2][5, j] = sum;
            }

            medPuzzles[index2][5, 5] = 0;
            for (int i = 0; i < 5; i++) //Get new diagonal sum
            {
                medPuzzles[index2][5, 5] += medPuzzles[index2][i, i];
                
            }

            CreatePlayingField(medPuzzles, index2, diffSetting);
        }

        public void CalcNewHardSums(TextBox omega, int index2)
        {
           
            //Change the puzzle in progress array based on user input
            for (int i = 0; i < 7; i++)
            {
                sum = 0;

                for (int j = 0; j < 7; j++)
                {
                    if (hardMode[i, j].Name == omega.Name)
                    {
                        hardPuzzles[index2][i, j] = (int)Char.GetNumericValue(omega.Text[0]);
                        if (hardPuzzles[index].IsValueInSolution((int)Char.GetNumericValue(omega.Text[0]), i, j))
                        {
                            omega.BackColor = Color.Green;
                            omega.Enabled = false;
                        }
                        else
                        {
                            omega.BackColor = Color.Red;
                        }
                        
                        if (hardPuzzles[index2].IsItCorrect())//********** CHECK FOR CORRECT SOLUTION HERE*********/////
                        {
                            hardDone = true;

                        }
                    }
                    sum += hardPuzzles[index2][i, j]; //calculate new sums for the rows

                }

                hardPuzzles[index2][i, 7] = sum;
            }

            //Calculates new sums for the columns
            for (int j = 0; j < 7; j++)
            {
                sum = 0;

                for (int i = 0; i < 7; i++)
                {
                    sum += hardPuzzles[index2][i, j];
                }
                hardPuzzles[index2][7, j] = sum;
            }

            hardPuzzles[index2][7, 7] = 0;
            for (int i = 0; i < 7; i++) //Get new diagonal sum
                hardPuzzles[index2][7, 7] += hardPuzzles[index2][i, i];
            
            
            CreatePlayingField(hardPuzzles, index2, diffSetting);
            

        }

        private void Generate_Puzzle(object sender, EventArgs e)
        {
            ReadPuzzles();
            CreatePlayingField(easyPuzzles, 0, "Easy");
            disableTextBoxes();
            enableTextBoxes();
            index = (int) puzzleNum.Value - 1;
            
            diffSetting = puzzleDiff.SelectedItem.ToString();

            if (diffSetting == "Easy")
            {
                foreach (TextBox singleItem in hardMode)
                {
                    singleItem.BackColor = Color.Black;
                }
                foreach (TextBox singleItem in easyMode)
                {
                    singleItem.BackColor = Color.White;
                }

                CreatePlayingField(easyPuzzles, index, diffSetting);
            }
            else if (diffSetting == "Medium")
            {
                foreach (TextBox singleItem in hardMode)
                {
                    singleItem.BackColor = Color.Black;
                }
                foreach (TextBox singleItem in medMode)
                {
                    singleItem.BackColor = Color.White;
                }

                CreatePlayingField(medPuzzles, index, diffSetting);
            }
            else if (diffSetting == "Hard")
            {
                foreach (TextBox singleItem in hardMode)
                {
                    singleItem.BackColor = Color.Black;
                }
                foreach (TextBox singleItem in hardMode)
                {
                    singleItem.BackColor = Color.White;
                }

                switcher = false;
                CreatePlayingField(hardPuzzles, index, diffSetting);
            }

        }

        private void timerStartStopClick(object sender, EventArgs e)
        {

            if (timerStartStop.Text == "Start")
            {
                timer1.Start();
                stopwatch.Start();
                foreach (TextBox singleItem in hardMode)
                {
                    singleItem.ForeColor = Color.Black;
                }
                timerStartStop.Text = "Stop";
                MsgBox.Clear();

            }
            else if (timerStartStop.Text == "Stop")
            {
                timer1.Stop();
                stopwatch.Stop();
                foreach (TextBox singleItem in hardMode)
                {
                    singleItem.ForeColor = Color.White;
                }
                timerStartStop.Text = "Start";
                MsgBox.Text = "The values are hidden until you start the timer";
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            elapsedTime = stopwatch.Elapsed;
            timerBox.Text = String.Format("{0:00}:{1:00}", elapsedTime.Minutes, elapsedTime.Seconds);
            
        }

        private void fieldKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox omega = sender as TextBox;
            omega.MaxLength = 1;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        
        private void fieldTextChanged(object sender, EventArgs e)
        {
            
            TextBox omega = sender as TextBox;
                        
            if (System.Text.RegularExpressions.Regex.IsMatch(omega.Text, "  ^ [1-9]"))
            {
                omega.Text = "";
            }
            
            if (omega.Text == "0")
            {
                omega.Text = "";
            }
            
            if (omega.Text != "" && diffSetting == "Easy")
            {
                CalcNewEasySums(omega, index);   
            }

            if (omega.Text != "" && diffSetting == "Medium")
            {
                CalcNewMedSums(omega, index);
            }
            //MessageBox.Show(k.ToString());
            if (omega.Text != "" && diffSetting == "Hard")
            {
                
                CalcNewHardSums(omega, index);
               
            }


        }

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        //Method purpose of changing the backColor the text box that has focus
        private void hasFocus(object sender, EventArgs e)
        {
            TextBox omega = sender as TextBox;
            omega.BackColor = Color.Cyan;
            HideCaret(this.Handle);
        }
        
        //Method purpose of changing the backColor the text box that nolonger has focus
        private void lostFocus(object sender, EventArgs e)
        {
            TextBox omega = sender as TextBox;
            if (omega.BackColor != Color.Green)
            {
                omega.BackColor = Color.White;
            }
        }

        private void resetBoard(object sender, EventArgs e)
        {
            timer1.Stop();            
            stopwatch.Reset();            
            timerStartStop.Text = "Start";
            timerBox.Text = "00:00:000";
            MsgBox.Text = "Board reset\r\n";
            MsgBox.Text += "The values are hidden until you start the timer";
            diffSetting = puzzleDiff.SelectedItem.ToString();
            index = (int)puzzleNum.Value - 1;
            foreach (TextBox singleItem in hardMode)
            {
                singleItem.ForeColor = Color.White;
            }
            if (diffSetting == "Easy")
            {
                easyPuzzles[index].resetPuzzle();
                CreatePlayingField(easyPuzzles, index, diffSetting);
            }
            else if (diffSetting == "Medium")
            {
                medPuzzles[index].resetPuzzle();
                CreatePlayingField(medPuzzles, index, diffSetting);
            }
            else
            {
                hardPuzzles[index].resetPuzzle();
                CreatePlayingField(hardPuzzles, index, diffSetting);
            }

        }  

        private void checkSolutionButton(object sender, EventArgs e)
        {
            
            MsgBox.Clear();
            MsgBox.Text = "Checking solution\r\n";

            diffSetting = puzzleDiff.SelectedItem.ToString();

            if (easyDone)
            {
                MsgBox.Clear();
                MsgBox.Text = "YOU DID IT\r\n";
                elapsedTime = stopwatch.Elapsed;
                timer1.Stop();
                stopwatch.Stop();
                //easyTimes.Add(elapsedTime);
                MsgBox.Text += String.Format("{0:00}:{1:00}", elapsedTime.Minutes, elapsedTime.Seconds);
            }
            else if (medDone)
            {
                MsgBox.Clear();
                MsgBox.Text = "YOU DID IT\r\n";
                elapsedTime = stopwatch.Elapsed;
                timer1.Stop();
                stopwatch.Stop();
                //medTimes.Add(elapsedTime);
                MsgBox.Text += String.Format("{0:00}:{1:00}", elapsedTime.Minutes, elapsedTime.Seconds);

            }
            else if (hardDone)
            {
                MsgBox.Clear();
                MsgBox.Text = "YOU DID IT\r\n";
                elapsedTime = stopwatch.Elapsed;
                timer1.Stop();
                stopwatch.Stop();
                //hardTimes.Add(elapsedTime);
                MsgBox.Text += String.Format("{0:00}:{1:00}", elapsedTime.Minutes, elapsedTime.Seconds);
            }
            else MsgBox.Text += "Solution is not correct. Keep trying";

        }

        private void saveProgress(object sender, EventArgs e)
        {
            MsgBox.Clear();
            MsgBox.Text = "Progress Saved";
        }

        //calls get cheats
        private void cheatButtonClick(object sender, EventArgs e)
        {
            MsgBox.Clear();
            timer1.Stop();
            stopwatch.Reset();
            timerStartStop.Text = "Start";
            timerBox.Text = "00:00:000";
            MsgBox.Text = "Cheating Enabled: Your time is now voided";
            diffSetting = puzzleDiff.SelectedItem.ToString();
            index = (int)puzzleNum.Value - 1;
            if (diffSetting == "Easy")
            {
                easyPuzzles[index].getCheats();
                CreatePlayingField(easyPuzzles, index, diffSetting);
            }
            else if (diffSetting == "Medium")
            {
                medPuzzles[index].getCheats();
                CreatePlayingField(medPuzzles, index, diffSetting);
            }
            else
            {
                hardPuzzles[index].getCheats();
                CreatePlayingField(hardPuzzles, index, diffSetting);
            }
        }
    } //End of Form1

    /* Class: Puzzle
     * 
     * Use:
     *  This class describes all the relevant information
     * for each puzzle we'll be letting the user try and solve.
     * It tracks each puzzles initial state, in-progress state,
     * solution state, fastest time to complete a puzzle, the
     * average time to complete a puzzle, difficulty, and 
     * possibly other things yet to be determined.
     * 
     * 
     */
    public class Puzzle 
    {
        //Data members. Not sure yet if we'll need to keep all of these as we progress through the assignment.
        private readonly int[,] puzzleInitial; //The initial state of the puzzle
        private int[,] puzzleInProgress; //The saved state of the puzzle if user moves onto another
        private readonly int[,] puzzleSolution; //The solution of the puzzle
        private Difficulty puzzleDifficulty; //The difficulty of the puzzle (Easy, Medium, or Hard)
        private bool started; //Has the user started a puzzle?
        private int savedTime; //Save the time elapsed if user moves onto a new puzzle
        private int bestTime; //Save the fastest time to complete
        private double avgTime; //Get the average time to complete
        private bool invalidAttempt; //If the user uses the cheat button, invalidate this attempt for purposes of best time and average time

        //Default constructor
        public Puzzle()
        {
            this.puzzleInitial = new int[0, 0];
            this.puzzleInProgress = new int[0, 0];
            this.puzzleSolution = new int[0, 0];
            this.puzzleDifficulty = 0;
            this.started = false;
            this.savedTime = 0;
            this.bestTime = 0;
            this.avgTime = 0;
            this.invalidAttempt = false;
        }

        //Overloaded consructor
        public Puzzle(int[,] puzzleInitial, int[,] puzzleInProgress, int[,] puzzleSolution, Difficulty puzzleDifficulty)
        {
            this.puzzleInitial = puzzleInitial;
            this.puzzleInProgress = puzzleInProgress;
            this.puzzleSolution = puzzleSolution;
            this.puzzleDifficulty = puzzleDifficulty;
            this.started = false;
            this.savedTime = 0;
            this.bestTime = 0;
            this.avgTime = 0;
            this.invalidAttempt = false;
        }

        public int this[int index, int index2]
        {
            get { return puzzleInProgress[index, index2]; }
            set { puzzleInProgress[index, index2] = value; }
        }

        //This method will tell us if the user won or not
        public bool IsItCorrect()
        {
            if (this.puzzleDifficulty == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (puzzleInProgress[i, j] != puzzleSolution[i, j])
                            return false;
                    }
                }
            }
            else if (this.puzzleDifficulty == (Difficulty)1)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (puzzleInProgress[i, j] != puzzleSolution[i, j])
                            return false;
                    }
                }
            }
            else if (this.puzzleDifficulty == (Difficulty)2)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (puzzleInProgress[i, j] != puzzleSolution[i, j])
                            return false;
                    }
                }
            }

            return true;
        }

        //This will check if an aswer entered by the user is in the puzzle or not
        //You must provide a row and column number so the function knows where the value
        //should be.
        public bool IsValueInSolution(int value, int i, int j)
        {
            if (value == puzzleSolution[i, j])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Resets the playing field
        //If the inprogess puzzle doesn't match solution. It gets set to zero
        public void resetPuzzle()
        {
            if (this.puzzleDifficulty == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if(puzzleInProgress[i, j] != puzzleSolution[i, j])
                             puzzleInProgress[i, j] = 0;
                    }
                }
            }
            else if (this.puzzleDifficulty == (Difficulty)1)
            {
                
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (puzzleInProgress[i, j] != puzzleSolution[i, j])
                            puzzleInProgress[i, j] = 0;
                    }
                }
            }
            else if (this.puzzleDifficulty == (Difficulty)2)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (puzzleInProgress[i, j] != puzzleSolution[i, j])
                            puzzleInProgress[i, j] = 0;
                    }
                }
            }
        }

        //gives the user cheats
        //if the inprogress != solution. It fills it in for them
        //does this a fixed number of times
        public void getCheats()
        {
            int valuesGiven = 0;
            int max = 0;
            if (this.puzzleDifficulty == 0)
            {
                max = 3;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if(valuesGiven<max )
                        {
                            if(puzzleInProgress[i, j] != puzzleSolution[i, j])
                            {
                                puzzleInProgress[i, j] = puzzleSolution[i, j];
                                valuesGiven++;
                            }
                            
                        }
                    }
                }
               
                
            }
            else if (this.puzzleDifficulty == (Difficulty)1)
            {
                max = 6;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (valuesGiven < max)
                        {
                            if (puzzleInProgress[i, j] != puzzleSolution[i, j])
                            {
                                puzzleInProgress[i, j] = puzzleSolution[i, j];
                                valuesGiven++;
                            }

                        }
                    }
                }

            }
            else if (this.puzzleDifficulty == (Difficulty)2)
            {
                max = 12;
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (valuesGiven < max)
                        {
                            if (puzzleInProgress[i, j] != puzzleSolution[i, j])
                            {
                                puzzleInProgress[i, j] = puzzleSolution[i, j];
                                valuesGiven++;
                            }

                        }
                    }
                }
            }
        }

    } //End of Puzzle 
}

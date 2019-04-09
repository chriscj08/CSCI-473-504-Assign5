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

namespace Chris_Parker_Assignment5
{
    public enum Difficulty { Easy, Medium, Hard };

    public partial class Form1 : Form
    {
        private static List<Puzzle> puzzles;
        private static int[,] gameBoard; //Not sure yet if I'll need this...

        public Form1()
        {
            InitializeComponent();
            puzzles = new List<Puzzle>();
            //ReadPuzzles();
        }
       
        //Still a work in progress 
        public void ReadPuzzles ()
        {
            using (StreamReader inFile = new StreamReader("C:/Users/Chrips/source/repos/CSCI-473-504-Assign5/Chris_Parker_Assignment5/Chris_Parker_Assignment5/a5/directory.txt"))
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
                                        tempArray[i, j] = (int) Char.GetNumericValue(input[j]);
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
                                puzzles.Add(tempPuzzle);

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

                                Puzzle tempPuzzle = new Puzzle(tempArray, tempArray, tempArray2, (Difficulty) 1);
                                puzzles.Add(tempPuzzle);
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

                                Puzzle tempPuzzle = new Puzzle(tempArray, tempArray, tempArray2, (Difficulty) 2);
                                puzzles.Add(tempPuzzle);
                            }

                            sum = 0;
                            input = inFile2.ReadLine();
                        }
                    }

                    fileName = inFile.ReadLine();
                }             
            }
        }//End of ReadPuzzles
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Generate_Puzzle(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                MessageBox.Show(puzzles[i].ToString());
            }
        }

        private void timerStartStopClick(object sender, EventArgs e)
        {

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
    public class Puzzle //: IComparable
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

        public override string ToString()
        {
            string theString = String.Empty;

            if(this.puzzleDifficulty == (Difficulty) 0)
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    theString += String.Format("{0,5}", puzzleSolution[i, j]); 
                }

                theString += "\n";
            }
            if (this.puzzleDifficulty == (Difficulty)1)
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        theString += String.Format("{0,5}", puzzleSolution[i, j]);
                    }

                    theString += "\n";
                }
            if (this.puzzleDifficulty == (Difficulty)2)
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        theString += String.Format("{0,5}", puzzleSolution[i, j]);
                    }

                    theString += "\n";
                }
            return theString;
        }
        /*****Needs work still*****Need to determine what order to sort the puzzle objects...
        //Our CompareTo method which allows us to sort our Puzzle objects
        public int CompareTo(Object puzzle)
        {
            if (puzzle == null)
            { return 1; }

            Puzzle comparer = puzzle as Puzzle;

            int result = 

        }
        */

    } //End of Puzzle 

} //End of Assignment 5

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
            ReadPuzzles();
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

                            
                            if (input.Length == 3) //Easy puzzle
                            {
                                int[,] tempArray = new int[5, 5];

                                //Create Initial Puzzle 2D Array
                                for (i = 0; i < 5; i++)
                                {
                                    for (j = 0; j < 5; j++)
                                    {
                                        tempArray[i, j] = Convert.ToInt32(input[j]);
                                    }
                                }


                                   
                            }
                            else if (input.Length == 5)//Medium puzzle
                            {

                            }
                            else if (input.Length == 7)//Hard puzzle
                            {

                            }
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

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
        public Puzzle(int[,] puzzleInitial, int[,] puzzleInProgress, int[,] puzzleSolution, Difficulty puzzleDifficulty, 
                      bool started, int savedTime, int bestTime, double avgTime, bool invalidAttempt)
        {
            this.puzzleInitial = puzzleInitial;
            this.puzzleInProgress = puzzleInProgress;
            this.puzzleSolution = puzzleSolution;
            this.puzzleDifficulty = puzzleDifficulty;
            this.started = started;
            this.savedTime = savedTime;
            this.bestTime = bestTime;
            this.avgTime = avgTime;
            this.invalidAttempt = invalidAttempt;
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

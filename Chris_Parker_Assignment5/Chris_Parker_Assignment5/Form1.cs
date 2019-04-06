using System;
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
        }

        private void ReadPuzzles ()
        {

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

        /*****Needs work still*****
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yahtzee
{
    public partial class Yahtzee : Form
    {

        Random rnd = new Random();

        Die[] Dice = new Die[] { new Die(), new Die(), new Die(), new Die(), new Die() };

        System.IO.StreamReader file = new System.IO.StreamReader("scores.txt");

        int rolls = 3;
        int scoreCount = 0; //for calculating points
        //int currentFace = 0; //for ones-sixes
        int Sum = 0;
        int Total = 0;
        bool gameStarted = false;

        int highest = 0;
        int secondHighest = 0;

        int[] Numbers = new int[] { 0, 0, 0, 0, 0, 0 }; //1-6, for calculating three/four of a kind, full house

        string[] highScores = new string[5]; //holds the five high scores
        public Yahtzee()
        {
            InitializeComponent();
        }

        private void scoreThrees_Click(object sender, EventArgs e)
        {

        }


        private void scoreBox_Enter(object sender, EventArgs e)
        {

        }

        private void keepBtn1_Click(object sender, EventArgs e)
        {
            if (Die1.Text != "-")
            {
                Dice[0].setKeep();

                if (Dice[0].getKeep() == true)
                {
                    keepBtn1.Text = "X";
                    keepBtn1.BackColor = Color.Gray;
                }
                else
                {
                    keepBtn1.Text = "";
                    keepBtn1.BackColor = Color.White;
                }
            }
        }

        private void keepBtn2_Click(object sender, EventArgs e)
        {
            if (Die2.Text != "-")
            {
                Dice[1].setKeep();

                if (Dice[1].getKeep() == true)
                {
                    keepBtn2.Text = "X";
                    keepBtn2.BackColor = Color.Gray;
                }
                else
                {
                    keepBtn2.Text = "";
                    keepBtn2.BackColor = Color.White;
                }
            }
        }

        private void keepBtn3_Click(object sender, EventArgs e)
        {
            if (Die3.Text != "-")
            {
                Dice[2].setKeep();

                if (Dice[2].getKeep() == true)
                {
                    keepBtn3.Text = "X";
                    keepBtn3.BackColor = Color.Gray;
                }
                else
                {
                    keepBtn3.Text = "";
                    keepBtn3.BackColor = Color.White;
                }
            }
        }

        private void keepBtn4_Click(object sender, EventArgs e)
        {
            if (Die4.Text != "-")
            {
                Dice[3].setKeep();

                if (Dice[3].getKeep() == true)
                {
                    keepBtn4.Text = "X";
                    keepBtn4.BackColor = Color.Gray;
                }
                else
                {
                    keepBtn4.Text = "";
                    keepBtn4.BackColor = Color.White;
                }
            }
        }

        private void keepBtn5_Click(object sender, EventArgs e)
        {
            if (Die5.Text != "-")
            {
                Dice[4].setKeep();

                if (Dice[4].getKeep() == true)
                {
                    keepBtn5.Text = "X";
                    keepBtn5.BackColor = Color.Gray;
                }
                else
                {
                    keepBtn5.Text = "";
                    keepBtn5.BackColor = Color.White;
                }
            }
        }

        private void rollBTN_Click(object sender, EventArgs e)
        {
            if (rolls > 0)
            {
                if (Dice[0].getKeep() == false)
                    Dice[0].Roll(rnd.Next(1, 7));
                if (Dice[1].getKeep() == false)
                    Dice[1].Roll(rnd.Next(1, 7));
                if (Dice[2].getKeep() == false)
                    Dice[2].Roll(rnd.Next(1, 7));
                if (Dice[3].getKeep() == false)
                    Dice[3].Roll(rnd.Next(1, 7));
                if (Dice[4].getKeep() == false)
                    Dice[4].Roll(rnd.Next(1, 7));

                Die1.Text = Dice[0].getFace().ToString();
                Die2.Text = Dice[1].getFace().ToString();
                Die3.Text = Dice[2].getFace().ToString();
                Die4.Text = Dice[3].getFace().ToString();
                Die5.Text = Dice[4].getFace().ToString();

                calculate();

                rolls--;

                rollCountLBL.Text = rolls.ToString();

                highest = 0;
                secondHighest = 0;

                for (int i = 0; i < 6; i++)
                    Numbers[i] = 0;


                if (gameStarted == false)
                    gameStarted = true;
            }
        }

        private void calculate() //after every roll, calculates the scores for each selection
        {

            for (int i = 1; i <= 6; i++) //checks for ones-sixes
            {
                scoreCount = 0; //reset the score

                foreach (Die d in Dice)
                {
                    if (d.getFace() == i)
                        scoreCount = scoreCount + i;
                }

                switch (i)
                {
                    case 1:
                        if (OnesBTN.BackColor != Color.Green)
                        {
                            OnesBTN.Text = scoreCount.ToString();
                        }
                        break;
                    case 2:
                        if (TwosBTN.BackColor != Color.Green)
                        {
                            TwosBTN.Text = scoreCount.ToString();

                        }
                        break;
                    case 3:
                        if (ThreesBTN.BackColor != Color.Green)
                        {
                            ThreesBTN.Text = scoreCount.ToString();

                        }
                        break;
                    case 4:
                        if (FoursBTN.BackColor != Color.Green)
                        { 
                            FoursBTN.Text = scoreCount.ToString();

                        }
                        break;
                    case 5:
                        if (FivesBTN.BackColor != Color.Green)
                        { 
                            FivesBTN.Text = scoreCount.ToString();

                        }
                        break;
                    case 6:
                        if (SixesBTN.BackColor != Color.Green)
                        { 
                            SixesBTN.Text = scoreCount.ToString();
  
                        }
                        break;
                    default:
                        break;
                }

                scoreCount = 0;

                
            }

            scoreCount = 0;

            if (ChanceBTN.BackColor != Color.Green) //calculating chance
            {
                foreach (Die d in Dice)
                {
                    scoreCount = scoreCount + d.getFace();
                }

                ChanceBTN.Text = scoreCount.ToString();
            }

            scoreCount = 0;

            foreach (Die d in Dice) //finds out which numbers were rolled
            {
                int h = 0;
                h = d.getFace() - 1;

                Numbers[h]++;
            }

            foreach (int n in Numbers) //finds the first and second highest
            {
                if (n > highest)
                {
                    secondHighest = highest;
                    highest = n;
                    //MessageBox.Show("array: " + Numbers[0] + "  " + Numbers[1] + "  " + Numbers[2] + "  " + Numbers[3] + "  " + Numbers[4] + "  " + Numbers[5]); testing
                }
                else if (n > secondHighest)
                    secondHighest = n; //if 1 is the highest, then second highest might not get updated, which could screw up full house with 3 ones, this should fix
            }


            if (ThreeofakindBTN.BackColor != Color.Green && highest >= 3) //calculating Yahtzee
            {

                foreach (Die d in Dice)
                {
                    scoreCount = scoreCount + d.getFace();
                }

                ThreeofakindBTN.Text = scoreCount.ToString();
            }
            else if (ThreeofakindBTN.BackColor != Color.Green)
                ThreeofakindBTN.Text = "0";

            scoreCount = 0;

            if (FourofakindBTN.BackColor != Color.Green && highest >= 4) //calculating Yahtzee
            {
                foreach (Die d in Dice)
                {
                    scoreCount = scoreCount + d.getFace();
                }

                FourofakindBTN.Text = scoreCount.ToString();
            }
            else if (FourofakindBTN.BackColor != Color.Green)
                FourofakindBTN.Text = "0";

            scoreCount = 0;

            if (YahtzeeBTN.BackColor != Color.Green && highest == 5) //calculating Yahtzee
            {
                scoreCount = 50;
                YahtzeeBTN.Text = scoreCount.ToString();
            }
            else if (YahtzeeBTN.BackColor != Color.Green)
                YahtzeeBTN.Text = "0";

            scoreCount = 0;

            if (SmallstraightBTN.BackColor != Color.Green && ((Numbers[0] > 0 && Numbers[1] > 0 && Numbers[2] > 0 && Numbers[3] > 0) ||
                (Numbers[1] > 0 && Numbers[2] > 0 && Numbers[3] > 0 && Numbers[4] > 0) ||
                (Numbers[2] > 0 && Numbers[3] > 0 && Numbers[4] > 0 && Numbers[5] > 0)) )
            {
                scoreCount = 30;
                SmallstraightBTN.Text = scoreCount.ToString();
            }
            else if (SmallstraightBTN.BackColor != Color.Green)
                SmallstraightBTN.Text = "0";

            scoreCount = 0;

            if (LargestraightBTN.BackColor != Color.Green && ((Numbers[0] > 0 && Numbers[1] > 0 && Numbers[2] > 0 && Numbers[3] > 0 && Numbers[4] > 0) ||
                (Numbers[1] > 0 && Numbers[2] > 0 && Numbers[3] > 0 && Numbers[4] > 0 && Numbers[5] > 0)))
            {
                scoreCount = 40;
                LargestraightBTN.Text = scoreCount.ToString();
            }
            else if (LargestraightBTN.BackColor != Color.Green)
                LargestraightBTN.Text = "0";

            scoreCount = 0;

            if (FullhouseBTN.BackColor != Color.Green && highest == 3 && secondHighest == 2) //calculating Full House
            {
                scoreCount = 25;
                FullhouseBTN.Text = scoreCount.ToString();
            }
            else if (FullhouseBTN.BackColor != Color.Green)
                FullhouseBTN.Text = "0";

            scoreCount = 0;
            //updates Sum, checks for bonus
            //SumBTN.Text = Sum.ToString();


            if (Sum >= 63)
                bonusScoreLBL.Text = "35";
            else
                bonusScoreLBL.Text = "0";
            


        }


        //                 CLICKING BUTTONS
        private void OnesBTN_Click(object sender, EventArgs e)
        {
            if (OnesBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                OnesBTN.BackColor = Color.Green;
                if (OnesBTN.Text == "")
                    OnesBTN.Text = "0";

                Sum = Sum + Int32.Parse(OnesBTN.Text);
                Total = Total + Int32.Parse(OnesBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void TwosBTN_Click_1(object sender, EventArgs e)
        {
            if (TwosBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                TwosBTN.BackColor = Color.Green;
                if (TwosBTN.Text == "")
                    TwosBTN.Text = "0";

                Sum = Sum + Int32.Parse(TwosBTN.Text);
                Total = Total + Int32.Parse(TwosBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void ThreesBTN_Click(object sender, EventArgs e)
        {
            if (ThreesBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                ThreesBTN.BackColor = Color.Green;
                if (ThreesBTN.Text == "")
                    ThreesBTN.Text = "0";

                Sum = Sum + Int32.Parse(ThreesBTN.Text);
                Total = Total + Int32.Parse(ThreesBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void FoursBTN_Click(object sender, EventArgs e)
        {
            if (FoursBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                FoursBTN.BackColor = Color.Green;
                if (FoursBTN.Text == "")
                    FoursBTN.Text = "0";

                Sum = Sum + Int32.Parse(FoursBTN.Text);
                Total = Total + Int32.Parse(FoursBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void FivesBTN_Click(object sender, EventArgs e)
        {
            if (FivesBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                FivesBTN.BackColor = Color.Green;
                if (FivesBTN.Text == "")
                    FivesBTN.Text = "0";

                Sum = Sum + Int32.Parse(FivesBTN.Text);
                Total = Total + Int32.Parse(FivesBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void SixesBTN_Click(object sender, EventArgs e)
        {
            if (SixesBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                SixesBTN.BackColor = Color.Green;
                if (SixesBTN.Text == "")
                    SixesBTN.Text = "0";

                Sum = Sum + Int32.Parse(SixesBTN.Text);
                Total = Total + Int32.Parse(SixesBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void uncheck()
        {
            foreach (Die d in Dice) //uncheck the dice
            {
                if (d.getKeep() == true)
                    d.setKeep();
            }

            keepBtn1.Text = "";
            keepBtn1.BackColor = Color.White;
            keepBtn2.Text = "";
            keepBtn2.BackColor = Color.White;
            keepBtn3.Text = "";
            keepBtn3.BackColor = Color.White;
            keepBtn4.Text = "";
            keepBtn4.BackColor = Color.White;
            keepBtn5.Text = "";
            keepBtn5.BackColor = Color.White;


            sumScoreLBL.Text = Sum.ToString();

            Die1.Text = "-";
            Die2.Text = "-";
            Die3.Text = "-";
            Die4.Text = "-";
            Die5.Text = "-";


            //PUTTING THE END GAME HERE, BECAUSE UNCHECK SHOULD HAPPEN AFTER EVERY BUTTON
            if (OnesBTN.BackColor == Color.Green && TwosBTN.BackColor == Color.Green && ThreesBTN.BackColor == Color.Green &&
                FoursBTN.BackColor == Color.Green && FivesBTN.BackColor == Color.Green && SixesBTN.BackColor == Color.Green &&
                ThreeofakindBTN.BackColor == Color.Green && FourofakindBTN.BackColor == Color.Green && FullhouseBTN.BackColor == Color.Green &&
                SmallstraightBTN.BackColor == Color.Green && LargestraightBTN.BackColor == Color.Green && ChanceBTN.BackColor == Color.Green &&
                YahtzeeBTN.BackColor == Color.Green)
            {
                Total = Total + Int32.Parse(bonusScoreLBL.Text);
                TotalscoreLBL.Text = Total.ToString();
                rolls = 0;
                gameStarted = false;
                MessageBox.Show("         GAME OVER " + Environment.NewLine + "" + Environment.NewLine + "       Total Score: " + Total);
            }



            TotalscoreLBL.Text = Total.ToString();



        }

        private void Die1_Click(object sender, EventArgs e)
        {

        }

        private void gAMEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file.Close();
            Application.Exit();
        }

        private void nEWGAMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //clear everything
            rolls = 3;
            rollCountLBL.Text = "3";
            scoreCount = 0; //for calculating points
            //currentFace = 0; //for ones-sixes
            Sum = 0;
            Total = 0;
            gameStarted = false;
            highest = 0;
            secondHighest = 0;

            Die1.Text = "-";
            Die2.Text = "-";
            Die3.Text = "-";
            Die4.Text = "-";
            Die5.Text = "-";

            OnesBTN.Text = "";
            OnesBTN.BackColor = SystemColors.Control;
            TwosBTN.Text = "";
            TwosBTN.BackColor = SystemColors.Control;
            ThreesBTN.Text = "";
            ThreesBTN.BackColor = SystemColors.Control;
            FoursBTN.Text = "";
            FoursBTN.BackColor = SystemColors.Control;
            FivesBTN.Text = "";
            FivesBTN.BackColor = SystemColors.Control;
            SixesBTN.Text = "";
            SixesBTN.BackColor = SystemColors.Control;

            ThreeofakindBTN.Text = "";
            ThreeofakindBTN.BackColor = SystemColors.Control;
            FourofakindBTN.Text = "";
            FourofakindBTN.BackColor = SystemColors.Control;
            SmallstraightBTN.Text = "";
            SmallstraightBTN.BackColor = SystemColors.Control;
            LargestraightBTN.Text = "";
            LargestraightBTN.BackColor = SystemColors.Control;
            FullhouseBTN.Text = "";
            FullhouseBTN.BackColor = SystemColors.Control;
            ChanceBTN.Text = "";
            ChanceBTN.BackColor = SystemColors.Control;
            YahtzeeBTN.Text = "";
            YahtzeeBTN.BackColor = SystemColors.Control;

            for (int i = 0; i < 6; i++)
                Numbers[i] = 0;

            sumScoreLBL.Text = "0";
            bonusScoreLBL.Text = "0";

            uncheck();

        }

        private void ChanceBTN_Click(object sender, EventArgs e)
        {
            if (ChanceBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                ChanceBTN.BackColor = Color.Green;
                if (ChanceBTN.Text == "")
                    ChanceBTN.Text = "0";

                Total = Total + Int32.Parse(ChanceBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void rollCountLBL_Click(object sender, EventArgs e)
        {

        }

        private void ThreeofakindBTN_Click(object sender, EventArgs e)
        {
            if (ThreeofakindBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                ThreeofakindBTN.BackColor = Color.Green;
                if (ThreeofakindBTN.Text == "")
                    ThreeofakindBTN.Text = "0";

                Total = Total + Int32.Parse(ThreeofakindBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void FourofakindBTN_Click(object sender, EventArgs e)
        {
            if (FourofakindBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                FourofakindBTN.BackColor = Color.Green;
                if (FourofakindBTN.Text == "")
                    FourofakindBTN.Text = "0";

                Total = Total + Int32.Parse(FourofakindBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void FullhouseBTN_Click(object sender, EventArgs e)
        {
            if (FullhouseBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                FullhouseBTN.BackColor = Color.Green;
                if (FullhouseBTN.Text == "")
                    FullhouseBTN.Text = "0";

                Total = Total + Int32.Parse(FullhouseBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void SmallstraightBTN_Click(object sender, EventArgs e)
        {
            if (SmallstraightBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                SmallstraightBTN.BackColor = Color.Green;
                if (SmallstraightBTN.Text == "")
                    SmallstraightBTN.Text = "0";

                Total = Total + Int32.Parse(SmallstraightBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void LargestraightBTN_Click(object sender, EventArgs e)
        {
            if (LargestraightBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                LargestraightBTN.BackColor = Color.Green;
                if (LargestraightBTN.Text == "")
                    LargestraightBTN.Text = "0";

                Total = Total + Int32.Parse(LargestraightBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void YahtzeeBTN_Click(object sender, EventArgs e)
        {
            if (YahtzeeBTN.BackColor == SystemColors.Control && gameStarted == true && rolls != 3) //if the button hasn't been pressed
            {
                YahtzeeBTN.BackColor = Color.Green;
                if (YahtzeeBTN.Text == "")
                    YahtzeeBTN.Text = "0";

                Total = Total + Int32.Parse(YahtzeeBTN.Text);

                rolls = 3;
                rollCountLBL.Text = rolls.ToString();

                uncheck(); //clears the keeps
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created 2015" + Environment.NewLine + "By: Geoff Miller");
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Roll the dice up to three times, then select a catagory on the right." + Environment.NewLine +
                            "Score 63 or higher in the single number catagories for a 35 point bonus.");
        }

        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //File.WriteAllText(scores.txt, "Score: 100");
            if (highScores[0] == null) //if the scores are empty
            {
                for (int i = 0; i < 5; i++)
                {
                    highScores[i] = file.ReadLine();
                }
            }

            MessageBox.Show("High Scores" + Environment.NewLine + "================" + Environment.NewLine
                + highScores[0] + Environment.NewLine
                + highScores[1] + Environment.NewLine
                + highScores[2] + Environment.NewLine
                + highScores[3] + Environment.NewLine
                + highScores[4] + Environment.NewLine);
        }




    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeypadSolver
{
   
    public partial class frmMain : Form
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZZ";
        List<KeyPadBlock> keypadBlocks = new List<KeyPadBlock>();

        //Stage Direction Indices
        const int UR = 0;
        const int UC = 1;
        const int UL = 2;
        const int SR = 3;
        const int SC = 4;
        const int SL = 5;
        const int DR = 6;
        const int DC = 7;
        const int DL = 8;

        public frmMain()
        {
            InitializeComponent();

            BuildLetterGroups();
            BuildStageArray();
        }

        /// <summary>
        /// Build 
        /// </summary>
        private void BuildLetterGroups()
        {
            int cntr = 0;

            for (int i = 0; i < 9; i++)
            {
                keypadBlocks.Add(new KeyPadBlock
                {
                    LetterGroup = alphabet.Substring(cntr, 3)
                    
                });

                cntr += 3;
            }
        }

        private void BuildStageArray()
        {
            keypadBlocks[0].KeyPadLockLabel = lblUpstageRight;
            keypadBlocks[0].DisplayText = lblUpstageRight.Text;

            keypadBlocks[1].KeyPadLockLabel = lblUpstageCenter;
            keypadBlocks[1].DisplayText = lblUpstageCenter.Text;

            keypadBlocks[2].KeyPadLockLabel = lblUpstageLeft;
            keypadBlocks[2].DisplayText = lblUpstageLeft.Text;

            keypadBlocks[3].KeyPadLockLabel = lblStageRight;
            keypadBlocks[3].DisplayText = lblStageRight.Text;

            keypadBlocks[4].KeyPadLockLabel = lblStageCenter;
            keypadBlocks[4].DisplayText = lblStageCenter.Text;

            keypadBlocks[5].KeyPadLockLabel = lblStageLeft;
            keypadBlocks[5].DisplayText = lblStageLeft.Text;

            keypadBlocks[6].KeyPadLockLabel = lblDownStageRight;
            keypadBlocks[6].DisplayText = lblDownStageRight.Text;

            keypadBlocks[7].KeyPadLockLabel = lblDownstageCenter;
            keypadBlocks[7].DisplayText = lblDownstageCenter.Text;

            keypadBlocks[8].KeyPadLockLabel = lblDownstageLeft;
            keypadBlocks[8].DisplayText = lblDownstageLeft.Text;

            for (int i = 0; i < keypadBlocks.Count; i++)
            {
                keypadBlocks[i].KeyPadLockLabel.Tag = keypadBlocks[i].LetterGroup;
                keypadBlocks[i].KeyPadLockLabel.Text = keypadBlocks[i].LetterGroup;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblOutput.Text = String.Empty;
        }

        private void BtnMoveLetters_Click(object sender, EventArgs e)
        {
            string temp = "";

            temp = keypadBlocks[0].LetterGroup;

            for (int i = 0; i < keypadBlocks.Count - 1; i++)
            {
                keypadBlocks[i].LetterGroup = keypadBlocks[i + 1].LetterGroup;
                keypadBlocks[i].Tag = String.Empty;
                keypadBlocks[i].DisplayText = Regex.Replace(keypadBlocks[i].DisplayText, @"\(.*?\)", "");
                keypadBlocks[i].DisplayText = Regex.Replace(keypadBlocks[i].DisplayText, @"\s+", " ");
                keypadBlocks[i].DisplayText = Regex.Replace(keypadBlocks[i].DisplayText, @"\s+", " ");
            }

            keypadBlocks[keypadBlocks.Count - 1].LetterGroup = temp;

            BuildStageArray();
        }

        /// <summary>
        /// Given a stage direction and a letter in the trio of letters, return decoded character
        /// </summary>
        /// <param name="stageDirection">Stage Direction</param>
        /// <param name="characterNumber">Character Index Number</param>
        /// <returns></returns>
        private char FindCharacter(string stageDirection, int characterNumber)
        {
            switch (stageDirection)
            {
                case "UR":
                    return keypadBlocks[UR].LetterGroup[characterNumber];
                case "UC":
                    return keypadBlocks[UC].LetterGroup[characterNumber];
                case "UL":
                    return keypadBlocks[UL].LetterGroup[characterNumber];
                case "SR":
                    return keypadBlocks[SR].LetterGroup[characterNumber];
                case "SC":
                    return keypadBlocks[SC].LetterGroup[characterNumber];
                case "SL":
                    return keypadBlocks[SL].LetterGroup[characterNumber];
                case "DR":
                    return keypadBlocks[DR].LetterGroup[characterNumber];
                case "DC":
                    return keypadBlocks[DC].LetterGroup[characterNumber];
                case "DL":
                    return keypadBlocks[DL].LetterGroup[characterNumber];
                default:
                    
                    break;
            }

            return ' ';
        }

        private void BtnDecode_Click(object sender, EventArgs e)
        {
            List<string> output = new List<string>();

            for (int i = 0; i < txtInstructions.Lines.Length; i++)
            {
                if (txtInstructions.Lines[i] == "")
                {
                    continue;
                }
                string[] setsToDecode = txtInstructions.Lines[i].Split(new char[] { ' ' });
                string decodedString = "";

                for (int x = 0; x < setsToDecode.Length; x++)
                {
                    int character_number = Convert.ToInt32(setsToDecode[x][2].ToString());
                    character_number -= 1;

                    char decodedCharacter = FindCharacter(setsToDecode[x][0].ToString() + setsToDecode[x][1].ToString(), character_number);
                    decodedString += decodedCharacter;
                }

                output.Add(decodedString);
            }

            string finalOutput = String.Join(System.Environment.NewLine, output);

            lblOutput.Text = finalOutput;


        }
    }

    /// <summary>
    /// Class representing one block on the 3X3 Grid
    /// </summary>
    internal class KeyPadBlock
    {
        public string LetterGroup;
        public string Tag;
        public string DisplayText;
        public Label KeyPadLockLabel;

    }
}

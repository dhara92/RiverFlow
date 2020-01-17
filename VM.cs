/* Final Assignment
 * Group: 3
 * Members:
 * Dhara Narola
 * Kirti
 * Laren
 * Laxen Sapani
 * Mitesh Ghevariya
 * */
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace RiverFlow
{
    class VM : INotifyPropertyChanged
    {
        public string userContent;
        public string longRiver;
        public string[] words;
        public string lineWidth;
        public BindingList<string> inputValue = new BindingList<string>();
        public BindingList<string> output = new BindingList<string>();

        public string UserContent
        {
            get { return userContent; }
            set { userContent = value; NotifyPropertyChanged(); }
        }
        public string LongRiver
        {
            get { return longRiver; }
            set { longRiver = value; NotifyPropertyChanged(); }
        }
        public string LineWidth
        {
            get { return lineWidth; }
            set { lineWidth = value; NotifyPropertyChanged(); }
        }
        public BindingList<string> SpaceIndex
        {
            get;
            private set;
        }
        public BindingList<string> InputValue
        {
            get { return inputValue; }
            set { inputValue = value; NotifyPropertyChanged(); }
        }
        public BindingList<string> Output
        {
            get { return output; }
            set { output = value; NotifyPropertyChanged(); }
        }

        //This function is used to open the file from computer
        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                UserContent = File.ReadAllText(openFileDialog.FileName);
            words = File.ReadAllText(openFileDialog.FileName).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
         }
        // method to count long river
        public void CountRiverLength()
        {
            int longestRiver = 0;
            int longestLineWidth = 0;
            int longestWord = 0;

            //to Find longet word
            for (int i = 0; i < words.Length; ++i)
            {
                longestWord = Math.Max(longestWord, words[i].Length);
            }

            for (int maxLineWidth = longestWord; maxLineWidth < UserContent.Length; ++maxLineWidth)
            {
                int riverLength = CalcRiverLength(maxLineWidth);
                if (riverLength > longestRiver)
                {
                    longestRiver = riverLength;//bind this  
                    longestLineWidth = maxLineWidth;//bind this
                }
            }
            LongRiver = longestRiver.ToString();
            LineWidth = longestLineWidth.ToString();
        }
        public int CalcRiverLength(int maxWidth)
        {
            List<List<int>> sIndex = new List<List<int>>();
            sIndex.Add(new List<int>());
            int line = 0;
            string currentLine = words[1];

            for (int i = 2; i < words.Length; ++i)
            {
                if (currentLine.Length + words[i].Length + 1 <= maxWidth)
                {
                    sIndex[line].Add(currentLine.Length);
                    currentLine += " " + words[i];
                }
                else
                {
                    currentLine = words[i];
                    sIndex.Add(new List<int>());
                    ++line;
                }
            }
            
            List<River> rivers = new List<River>();

            //sIndex will have all the integer numbers of the spaces
            for (int row = 0; row < sIndex.Count; ++row)
            {
                for (int j = 0; j < sIndex[row].Count; ++j)
                {
                    bool makeNewRiver = true;
                    for (int r = 0; r < rivers.Count; ++r)
                    {
                        if (Math.Abs(sIndex[row][j] - rivers[r].lastSpaceIndex) <= 1 &&
                            rivers[r].lastRowIndex == row - 1)
                        {
                            rivers[r].AddSpaceIndex(sIndex[row][j], row);//sIndex[i][j] is my current sIndex and i is the current row
                            makeNewRiver = false;
                        }
                    }
                    if (makeNewRiver)
                    {
                        rivers.Add(new River(sIndex[row][j], row));
                    }
                }
            }
            int longestRiver = 0;
            for (int i = 0; i < rivers.Count; ++i)
            {
                longestRiver = Math.Max(longestRiver, rivers[i].length);
            }
            return longestRiver;
        }

        class River
        {
            public River(int spaceIndex, int rowIndex)
            {
                length = 1;
                lastSpaceIndex = spaceIndex;// lastspaceindex is where the river was
                lastRowIndex = rowIndex;
            }
            public void AddSpaceIndex(int spaceIndex, int rowIndex)
            {
                ++length;
                lastSpaceIndex = spaceIndex;
                lastRowIndex = rowIndex;
            }
            public int length;
            public int lastSpaceIndex;
            public int lastRowIndex;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
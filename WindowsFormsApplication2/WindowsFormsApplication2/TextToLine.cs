using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    public class TextToLine
    {
        private string str;
        private string[] lines, endTimes, startTimes, texts;

        public TextToLine(string str)
        {
            Str = str;
            int i = intialize();
            lines = new string[i];
            getLine();
            endTimes = new string[countTimeLine(lines)];
            startTimes = new string[countTimeLine(lines)];
            texts = new string[countTextLine(lines)];
            setStartTimeLines();
            setEndTimeLines();
            setTextLines();
        }

        public string Str { set { str = value;} get { return str;} }

    //    public string[] Lines(int j) { set { lines[j] = value; } get { return lines; } }

    //    public string[] EndTimes { set { endTimes = value; } get { return endTimes; } }

    //    public string[] StartTimes { set { startTimes = value; } get { return startTimes; } }

    //    public string[] Texts { set { texts = value; } get { return texts; } }

        private void getLine()
        {
            string ch="";
            int i = 0, j = 0;
            foreach (char cha in Str)
            {
                if (cha == '\n')
                {
                    
                    lines[j] = ch;
                    ch = "";
                    j++;
                }
                else
                {
                    if (cha != '\r')
                    {
                        ch += cha.ToString();
                        i++;
                    }
                }
            }
        }

        public bool hasTimeLine(string s)
        {
            if (s != null)
                if (s.Contains("-->"))
                    return true;
            return false;
        }

        public bool hasRowLine(string s)
        {
            float output;
            return float.TryParse(s, out output);

        }

        public bool hasBrokeLine(string s)
        {
            if (s == "")
                return true;
            else return false;
        }

        public void setStartTimeLines()
        {
            string startTime = "";
            int i = 0;
            bool flag = true;
            foreach (string line in lines)
            {
                if (hasTimeLine(line))
                {
                    foreach (char ch in line)
                    {
                        if (ch == '-')
                            flag = false;
                        if (ch != ' ' && flag)
                            startTime += ch;
                    }
                    startTimes[i] = startTime;
                    startTime = "";
                    flag = true;
                    i++;
                }
            }
        }

        public void setEndTimeLines()
        {
            string endTime = "";
            int i = 0;
            bool flag = false;
            foreach (string line in lines)
            {
                if (hasTimeLine(line))
                {
                    foreach (char ch in line)
                    {
                        if (flag)
                            if (ch != ' ')
                                endTime += ch.ToString();
                        if (ch == '>')
                            flag = true;
                    }
                    endTimes[i] = endTime;
                    endTime = "";
                    flag = false;
                    i++;
                }
            }
        }

        public void setTextLines()
        {
            string textline = "";
            int i = 0;
            bool flag = false;
            foreach (string line in lines)
            {
                if (hasBrokeLine(line))
                {
                    texts[i] = textline.Remove(textline.Length-1);
                    textline = null;
                    flag = false;
                    i++;
                }
                if (flag)
                    textline += line + "\r\n";
                if (hasTimeLine(line))
                    flag = true;
            }
        }

        public List<string> getStartTimeLines()
        {
            List<string> startTimeList = new List<string>();
            foreach (string line in startTimes)
                startTimeList.Add(line);
            return startTimeList;
        }

        public List<string> getEndTimeLines()
        {
            List<string> endTimeList = new List<string>();
            foreach (string line in endTimes)
                endTimeList.Add(line);
            return endTimeList;
        }

        public List<string> getTextLines()
        {
            List<string> textList = new List<string>();
            foreach (string line in texts)
                textList.Add(line);
            return textList;
        }

        private int intialize()
        {
            int i = 0;
            foreach (char ch in str)
            {
                if (ch == '\n')
                    i++;
            }
            return i+1;
        }

        public int countTimeLine(string[] lines)
        {
            int i = 0;
            foreach (string line in lines)
                if (hasTimeLine(line))
                   i++;
            return i;
        }

        public int countRowLine(string[] lines)
        {
            int i = 0;
            foreach (string line in lines)
                if (hasRowLine(line))
                    i++;
            return i;
        }

        public int countBrokeLine(string[] lines)
        {
            int i = 0;
            foreach (string line in lines)
                if (hasBrokeLine(line))
                    i++;
            return i;
        }

        public int countTextLine(string[] lines)
        {
            int i = 0;
            bool flag = false;
            foreach (string line in lines)
            {
                if (hasBrokeLine(line))
                    flag = false;
                if (flag)
                    i++;
                if (hasTimeLine(line))
                    flag = true;
            }
            return i;
        }

        //should add function for get time and text of class


    }
}

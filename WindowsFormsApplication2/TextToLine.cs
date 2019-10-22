using System;

namespace WindowsFormsApplication2
{

    public class TextToLines
    {
        
        private string str, s;
        private string[] lines, endTimes, startTimes, texts;

        public TextToLines(string str)
        {
            this.str = Str;
            getLine();
        }

        public string Str { set; get; }

        public string[] Lines { set; get; }

        public string[] EndTimes { set; get; }

        public string[] StartTimes { set; get; }

        public string[] Texts { set; get; }

        private void getLine()
        {
            char[] ch;
            int i = 0, j = 0, l;
            foreach (char ch in Str)
            {
                if (ch == '\n')
                {
                    l = 0;
                    while (ch[l] != null)
                    {
                        s += ch[l];
                        ch[l] = null;
                        l++;
                    }
                    lines[j] = s;
                    s = null;
                    j++;
                }
                else
                {
                    ch[i] = ch;
                    i++;
                }
            }
        }

        public bool hasTimeLine(string s)
        {
            bool flag1, flag2;
            foreach (char ch in s)
            {
                if ((ch == '-') && (!flag1))
                {
                    flag1 = true;
                    broke();
                }
                if ((ch == '-') && (!flag2))
                {
                    flag2 = true;
                    broke();
                }
                if ((ch == '>') && flag1 && flag2)
                    return true;
                flag1 = false;
                flag2 = false;
            }
            return false;
        }

        public bool hasRowLine(string s)
        {
            float output;
            return float.TryParse(s, out output);

        }

        public bool hasBrokeLine(string s)
        {
            if (s == null)
                return true;
            else return false;
        }

        public void setStartTimeLines()
        {
            string startTime;
            int i = 0;
            foreach (string line in lines)
            {
                if (hasTimeLine(line))
                {
                    foreach (char ch in line)
                    {
                        if (ch == '-')
                            broke();
                        if (ch != ' ')
                            startTime += ch;
                    }
                    startTimes[i] = startTime;
                    i++;
                }
            }
        }

        public void setEndTimeLines()
        {
            string endTime;
            int i = 0;
            bool flag = false;
            foreach (string line in lines)
            {
                if (hasTimeLine(line))
                {
                    foreach (char ch in line)
                    {
                        if (ch == '>')
                            flag = true;
                        if (flag)
                            if (ch != ' ')
                                endTime += ch;
                    }
                    endTimes[i] = endTime;
                    i++;
                }
            }
        }

        public void setTextLines()
        {
            string textline;
            int i = 0;
            bool flag = false;
            foreach (string line in lines)
            {
                if (hasBrokeLine(line))
                {
                    texts[i] = textline;
                    textline = null;
                    flag = false;
                    i++;
                }
                if (flag)
                    textline += line;
                if (hasTimeLine(line))
                    flage = true;
            }
        }

        public string[] getStartTimeLines()
        {
            setStartTimeLines();
            return StartTimes;
        }

        public string[] getEndTimeLines()
        {
            setEndTimeLines();
            return EndTimes;
        }

        public string[] getTextLines()
        {
            setTextLines();
            return Texts;
        }

        //should add function for get time and text of class
    }
}


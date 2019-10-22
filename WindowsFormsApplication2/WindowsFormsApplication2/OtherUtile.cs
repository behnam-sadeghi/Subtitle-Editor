using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    public class OtherUtile
    {
        public OtherUtile() {}

        public string getMovieName(string name)
        {
            int index = 0, i = 0;
            foreach (char ch in name)
            {
                if (ch == '.')
                    index = i;
                i++;
            }
            name = name.Remove(index);
            index = 0;
            i = 0;
            foreach (char ch in name)
            {
                if (ch == '\\')
                    index = i;
                i++;
            }
            index = (name.Length - index) - 1;
            name = reverce(name);
            name = name.Remove(index);
            name = reverce(name);
            return name;
        }

        public string reverce(string str)
        {
            string temp = "";
            for (int i = str.Length; i > 0; i--)
                temp += str[(i - 1)].ToString();
            return temp;
        }

        public string changeDecToHex(int R, int G, int B)
        {
            string output = "#", outputR = "", outputG = "", outputB = "";
            //red
            while (R > 0)
            {
                switch (R % 16)
                {
                    case 10: outputR += "A"; break;
                    case 11: outputR += "B"; break;
                    case 12: outputR += "C"; break;
                    case 13: outputR += "D"; break;
                    case 14: outputR += "E"; break;
                    case 15: outputR += "F"; break;
                    default: outputR += R % 16; break;
                }
                R = R / 16;
            }
            while (outputR.Length < 2)
                outputR += "0";
            outputR = reverce(outputR);

            //green
            while (G > 0)
            {
                switch (G % 16)
                {
                    case 10: outputG += "A"; break;
                    case 11: outputG += "B"; break;
                    case 12: outputG += "C"; break;
                    case 13: outputG += "D"; break;
                    case 14: outputG += "E"; break;
                    case 15: outputG += "F"; break;
                    default: outputG += G % 16; break;
                }
                G = G / 16;
            }
            while (outputG.Length < 2)
                outputG += "0";
            outputG = reverce(outputG);

            //blue
            while (B > 0)
            {
                switch (B % 16)
                {
                    case 10: outputB += "A"; break;
                    case 11: outputB += "B"; break;
                    case 12: outputB += "C"; break;
                    case 13: outputB += "D"; break;
                    case 14: outputB += "E"; break;
                    case 15: outputB += "F"; break;
                    default: outputB += B % 16; break;
                }
                B = B / 16;
            }
            while (outputB.Length < 2)
                outputB += "0";
            outputB = reverce(outputB);

            output += outputR + outputG + outputB;
            return output;
        }

        public string subtractionTime(string firstTime, string secondTime)
        {
            DetectList detectList = new DetectList();
            return detectList.getTime((detectList.getTime(secondTime) - detectList.getTime(firstTime)));
        }

        public string addTime(string firstTime, string secondTime)
        {
            DetectList detectList = new DetectList();
            return detectList.getTime((double)(detectList.getTime(firstTime) + detectList.getTime(secondTime)));
        }
    }
}

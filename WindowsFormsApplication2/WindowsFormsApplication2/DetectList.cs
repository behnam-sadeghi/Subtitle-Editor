using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    public class DetectList
    {
        private List<string> startTimes, textsSubtitle, endTimes;
        private string stringCurrentTime = "";
        private int indexSubtitleOnEdit = -1;

        public DetectList(List<string> startTimes, List<string> textsSubtitle, List<string> endTimes)
        {
           // StartTimes = new List<string> [startTimes.Count];   // >> that is wrong
            StartTimes = startTimes;
         //   TextsSubtitle = textsSubtitle;
            TextsSubtitle = textsSubtitle;
          //  EndTimes = endTimes;
            EndTimes = endTimes;
        }

        public DetectList()
        { }

        public List<string> StartTimes { set { startTimes = value; } get { return startTimes; } }

        public List<string> TextsSubtitle { set { textsSubtitle = value; } get { return textsSubtitle; } }

        public List<string> EndTimes { set { endTimes = value; } get { return endTimes; } }

        public string StringCurrentTime { set { stringCurrentTime = value; } get { return stringCurrentTime; } }

        public int IndexSubtitileOnEdit { set { indexSubtitleOnEdit = value; } get { return indexSubtitleOnEdit; } }

       // public double CurrentTime { set { currentTime = value; } get { return currentTime; } }

        public string GetStartTime(double currentTime)
        {
            return StartTimes[getIndexOfEndTime(currentTime)];
        }

        public string GetEndTime(double currentTime)
        {
            return EndTimes[getIndexOfEndTime(currentTime)];
        }

        public string GetDurationTime(double currentTime)
        {
            double end = changeStringToDouble(EndTimes[getIndexOfEndTime(currentTime)]);
            double start = changeStringToDouble(StartTimes[getIndexOfEndTime(currentTime)]);
            return changeDoubleToString(end - start);
        }

        public string GetTextSubtitle(double currentTime)
        {
            return TextsSubtitle[getIndexOfEndTime(currentTime)];
        }

        public void SetStartTime(int index, string text)
        {
            StartTimes[index] = text;
        }

        public void SetEndTime(int index, string text)
        {
            EndTimes[index] = text;
        }

        public void SetTextSubtitle(int index, string text)
        {
            TextsSubtitle[index] = text;
        }

        public int GetIndexSubtitle(double currentTime)
        {
            /// <summary>
            /// this function return index subtitle that is playing or will play.
            /// </summary>
            
            return getIndexOfEndTime(currentTime);
        }

        public int howManySubtitleNow(double currentTime)
        {
            int counter = 0, index = getIndexOfEndTime(currentTime);
            while (changeStringToDouble(StartTimes[index]) < currentTime)
            {
                counter++;
                index++;
            }
            return counter;
        }

        public string getSubtitleTextByIndex(int index)
        {
            return TextsSubtitle[index];
        }

        public bool hasSubtitle(double currentTime)
        {
            int index = getIndexOfEndTime(currentTime);
            if (index == getIndexOfStartTime(currentTime))
                if (index == EndTimes.Count)
                    return false;
                else return true;
            else if (index == EndTimes.Count - 1)
                return true; 
            return false;
        }
        
        public bool hasNextSubtitle(double currentTime)
        {
            if (getIndexOfStartTime(currentTime) < StartTimes.Count)
                return true;
            else return false;
        }

        public double getRemainingTime(double currentTime)
        {
            double endTime = changeStringToDouble(EndTimes[getIndexOfEndTime(currentTime)]);
            return (endTime - currentTime);
        }

        public double getRemainingTimeToNext(double currentTime)
        {
            double startTime = changeStringToDouble(StartTimes[getIndexOfEndTime(currentTime)]);
            return (startTime - currentTime);
        }

        private int getIndexOfStartTime(double currentTime)
        {
            for (int index = 0; index < StartTimes.Count ; index++)
            {
                if (changeStringToDouble(StartTimes[index]) > currentTime)
                    return index -1;
            }
            return StartTimes.Count;
        }

        // for get index subtitle line use this function(getIndexOfEndTime())
        private int getIndexOfEndTime(double currentTime)
        {
            for (int index = 0; index < EndTimes.Count; index++)
            {
                if (changeStringToDouble(EndTimes[index]) > currentTime)
                    return index;
            }
            return EndTimes.Count;
        }

        public string getTime(double number)
        {
            return changeDoubleToString(number);
        }

        public double getTime(string number)
        {
            return changeStringToDouble(number);
        }

        private double changeStringToDouble(string str)
        {
            char[] time = new char[12];
            int i = 0;
            foreach (char character in str)
            {
                time[i] = character;
                i++;
            }
            double number;
            number = double.Parse(time[0].ToString()) * 36000;
            number += double.Parse(time[1].ToString()) * 3600;
            number += double.Parse(time[3].ToString()) * 600;
            number += double.Parse(time[4].ToString()) * 60;
            number += double.Parse(time[6].ToString()) * 10;
            number += double.Parse(time[7].ToString()) * 1;
            number += double.Parse(time[9].ToString()) * 0.1;
            number += double.Parse(time[10].ToString()) * 0.01;
            number += double.Parse(time[11].ToString()) * 0.001;
            return number;
        }

        private string changeDoubleToString(double number)
        {
            string str = "";
            str = getHour(number)+":"+getMinute(number)+":"+getSecond(number)+"."+getmilisecond(number);
            return str;
        }

        private string getHour(double number)
        {
            int hour = ((int)number/3600);
            if (hour < 10)
                return "0" + hour;
            else return hour + "";
        }

        private string getMinute(double number)
        {
            int minute = (((int)number % 3600 )/ 60);
            if (minute < 10)
                return "0" + minute;
            else return minute+ "";
        }

        private string getSecond(double number)
        {
            int second = (((int)number) % 60);
            if (second < 10)
                return "0" + second;
            else return second + "";
        }

        private string getmilisecond(double number)
        {
            int milisecond = (int)((number-(double)((int)number))*1000);
            if (milisecond != 0)
                return milisecond + "";
            else return "000";
        }

    }
}

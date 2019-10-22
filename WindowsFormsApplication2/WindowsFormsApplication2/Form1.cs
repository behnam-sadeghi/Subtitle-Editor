using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private bool canBeChangeTextBoxFlag = true,
            subtitleLoadFlag = false,
            canChangeSubtitleWithMovie = true;
        private string movieName = "";
        DetectList detectList;
        OtherUtile otherUtile = new OtherUtile();
        DataSet dataset = new DataSet();
        private List<Subtitle> subList;

        public Form1()
        {
            InitializeComponent();
            textBoxSubtitleText.TextAlign = HorizontalAlignment.Left;
            creatingGrid();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showToolStripMenuItem.Checked = ! showToolStripMenuItem.Checked;
            if (showToolStripMenuItem.Checked)
            {
                if (upToolStripMenuItem.Checked)
                    label2.Visible = true;
                else
                    label1.Visible = true;
                backgroundToolStripMenuItem.Enabled = true;
                fontToolStripMenuItem.Enabled = true;
                colorToolStripMenuItem.Enabled = true;
                stateToolStripMenuItem.Enabled = true;
            }
            else {
                label1.Visible = false;
                label2.Visible = false;
                backgroundToolStripMenuItem.Enabled = false;
                fontToolStripMenuItem.Enabled = false;
                colorToolStripMenuItem.Enabled = false;
                stateToolStripMenuItem.Enabled = false;
            }
        }

        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundToolStripMenuItem.Checked = ! backgroundToolStripMenuItem.Checked;
            if (backgroundToolStripMenuItem.Checked)
                backgroundVisiblityToolStripMenuItem.Enabled = true;
            else
                backgroundVisiblityToolStripMenuItem.Enabled = false;
        }

        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (upToolStripMenuItem.Checked)
            {
                downToolStripMenuItem.Checked = !downToolStripMenuItem.Checked;
                upToolStripMenuItem.Checked = !upToolStripMenuItem.Checked;
                label1.Visible = true;
                label2.Visible = false;
                label1.BringToFront();
            }
        }

        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (downToolStripMenuItem.Checked)
            {
                downToolStripMenuItem.Checked = !downToolStripMenuItem.Checked;
                upToolStripMenuItem.Checked = !upToolStripMenuItem.Checked;
                label2.Visible = true;
                label1.Visible = false;
                label2.BringToFront();
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowDialog();
            //todo: edit text font change
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            //todo: edit text color change
        }

        private void backgroundVisiblityToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            label1.BackColor = Color.Blue;
            //todo : should change alpha background label1 and label2s
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            OpenFileDialog a = new OpenFileDialog();
            a.ShowDialog();
            //MessageBox.Show(a.FileName.ToString());
            try
            {
                
                if (!File.Exists("subtitle.srt"))
                    File.CreateText("subtitle.srt");
            }
            catch (Exception)
            {
                MessageBox.Show("error in create file");
            }

            try
            {
                StreamReader sr = new StreamReader(a.FileName.ToString());
                string s = sr.ReadToEnd();
                //listBox1.Text = s;
                MessageBox.Show(s);
                sr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("error in reading file");
            }

            */

        }

        private void movieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                axWindowsMediaPlayer1.URL = openFileDialog.FileName;
                movieName = openFileDialog.FileName;
                OtherUtile otherUtile = new OtherUtile();
                movieName = otherUtile.getMovieName(movieName);
            }
            axWindowsMediaPlayer1.Ctlcontrols.play();
            Timer timer1 = new Timer()
            {
                Interval = 100
            };
            timer1.Enabled = true;
            timer1.Tick += new System.EventHandler(OnTimerEvent);
            

        }

        private void subtitleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            try
            {
                subtitleLoadFlag = true;
                Subtitle subtitle = new Subtitle();
                //    >>  should be change to list  <<
                
                List<string> textlines = new List<string>();
                List<string> showlines = new List<string>();
                List<string> hidelines = new List<string>();

                StreamReader sr = new StreamReader(openFileDialog.FileName.ToString());
                TextToLine textToLine = new TextToLine(sr.ReadToEnd().ToString());

                textlines = textToLine.getTextLines();
                showlines = textToLine.getStartTimeLines();
                hidelines = textToLine.getEndTimeLines();

                detectList = new DetectList(showlines, textlines, hidelines);
                dataGridView1.AutoResizeColumns();

                for (int index = 0; index < showlines.Count; index++)
                {
                    subtitle.rowId = index + 1;
                    subtitle.showTime = showlines[index];
                    subtitle.hideTime = hidelines[index];
                    subtitle.duration = detectList.getTime(detectList.getTime(hidelines[index])- detectList.getTime(showlines[index]));
                    subtitle.subtitleText = textlines[index];
                   // subList.Add(subtitle);
                    dataGridView1.Rows.Add(subtitle.rowId, subtitle.showTime, subtitle.hideTime, subtitle.duration, subtitle.subtitleText);
                }
                dataGridView1.AutoResizeColumns();
            }
            catch (Exception)
            {
                MessageBox.Show("error in reading file");
            }
        }

        

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            
        }

        private void OnTimerEvent(object sender, EventArgs arg)
        {
            if (subtitleLoadFlag)
            {
                //here is for algoritm for showing subtitle
                // do thing that be do in 100 milisecond

                if (axWindowsMediaPlayer1.status.Equals("Paused"))
                {  // part pause

                    if (detectList.hasSubtitle(axWindowsMediaPlayer1.Ctlcontrols.currentPosition))
                    {

                    }
                    else
                    {

                    }
                }
                else if (axWindowsMediaPlayer1.status.Equals("Stopped"))
                {  // part stop
                    if (detectList.hasSubtitle(axWindowsMediaPlayer1.Ctlcontrols.currentPosition))
                    {

                    }
                    else 
                    {
 
                    }
                        if (detectList.IndexSubtitileOnEdit != -1)
                        detectList.IndexSubtitileOnEdit = -1;
                }
                else if (axWindowsMediaPlayer1.status.Remove(7,(axWindowsMediaPlayer1.status.Length-7)).Equals("Playing"))
                {  // part play
                    double currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                    if (detectList.hasSubtitle(currentPosition))
                    {
                        if (canChangeSubtitleWithMovie)
                        {
                            if (detectList.IndexSubtitileOnEdit != detectList.GetIndexSubtitle(currentPosition))
                            {
                                if (detectList.IndexSubtitileOnEdit >= 0)
                                {
                                    // replace changed subtitle
                                    detectList.SetStartTime(detectList.IndexSubtitileOnEdit, textBoxShowTime.Text);
                                    detectList.SetTextSubtitle(detectList.IndexSubtitileOnEdit, textBoxSubtitleText.Text);
                                    detectList.SetEndTime(detectList.IndexSubtitileOnEdit, textBoxHideTime.Text);
                                    dataGridView1.Rows[detectList.IndexSubtitileOnEdit].InheritedStyle.ForeColor = Color.Red;
                                    dataGridView1.Rows[detectList.IndexSubtitileOnEdit].Cells[1].Value = textBoxShowTime.Text;
                                    dataGridView1.Rows[detectList.IndexSubtitileOnEdit].Cells[2].Value = textBoxHideTime.Text;
                                    dataGridView1.Rows[detectList.IndexSubtitileOnEdit].Cells[3].Value = textBoxDuration.Text;
                                    dataGridView1.Rows[detectList.IndexSubtitileOnEdit].Cells[4].Value = textBoxSubtitleText.Text;
                                    
                                }

                                // show new subtitle
                                canBeChangeTextBoxFlag = false;
                                detectList.IndexSubtitileOnEdit = detectList.GetIndexSubtitle(currentPosition);
                                textBoxShowTime.Text = detectList.GetStartTime(currentPosition);
                                textBoxSubtitleText.Text = detectList.GetTextSubtitle(currentPosition);
                                textBoxHideTime.Text = detectList.GetEndTime(currentPosition);
                                textBoxDuration.Text = detectList.GetDurationTime(currentPosition);
                                canBeChangeTextBoxFlag = true;
                            }
                        }
                    }
                    else
                    {

                    }
                    
                }
              //  MessageBox.Show(axWindowsMediaPlayer1.status.Remove(7,axWindowsMediaPlayer1.status.Length-7)+"-");
                //  DetectList detectList = new DetectList();
                //  if ( axWindowsMediaPlayer1.Ctlcontrols.currentPosition.ToString();
            }
        }

        private void setStartTime_Click(object sender, EventArgs e)
        {
         //--   textBox1.Text = detectList.getTime(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
        }

        private void setEndTime_Click(object sender, EventArgs e)
        {
        //--    textBox3.Text = detectList.getTime(axWindowsMediaPlayer1.Ctlcontrols.currentPosition);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // this part for back or forward little second movie
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = (axWindowsMediaPlayer1.Ctlcontrols.currentPosition + ((double)trackBar1.Value/10));
           // MessageBox.Show(((trackBar1.Value - 50) / 10)+"");
         //   trackBar1.Value = 50;
           

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonBold_Click(object sender, EventArgs e)
        {
            textBoxSubtitleText.Text = "<b>" + textBoxSubtitleText.Text + "</b>";
           
        }

        private void textBoxSubtitleText_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonItalic_Click(object sender, EventArgs e)
        {
            textBoxSubtitleText.Text = "<i>" + textBoxSubtitleText.Text + "</i>";
        }

        private void buttonUnderLine_Click(object sender, EventArgs e)
        {
            textBoxSubtitleText.Text = "<u>" + textBoxSubtitleText.Text + "</u>";
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            textBoxSubtitleText.Text = "<font color=" + 
                otherUtile.changeDecToHex(colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B) +
                ">" + textBoxSubtitleText.Text + "</font>";
        }

        private void TextBoxShowTime_TextChanged(object sender, EventArgs e)
        {
            if (canBeChangeTextBoxFlag)
            {
                double currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

                detectList.SetStartTime(detectList.IndexSubtitileOnEdit, textBoxShowTime.Text);
                textBoxHideTime.Text = otherUtile.addTime(textBoxShowTime.Text, textBoxDuration.Text);
                detectList.SetEndTime(detectList.IndexSubtitileOnEdit, textBoxHideTime.Text);
            }
        }

        private void TextBoxHideTime_TextChanged(object sender, EventArgs e)
        {
            if (canBeChangeTextBoxFlag)
            {
                double currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

                detectList.SetEndTime(detectList.IndexSubtitileOnEdit, textBoxHideTime.Text);
                textBoxDuration.Text = otherUtile.subtractionTime(textBoxShowTime.Text, textBoxHideTime.Text);
                detectList.SetStartTime(detectList.IndexSubtitileOnEdit, textBoxShowTime.Text);
            }
        }

        private void TextBoxDuration_TextChanged(object sender, EventArgs e)
        {
            if (canBeChangeTextBoxFlag)
            {
                double currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                //MessageBox.Show("dur:" + textBoxDuration.Text + " sho:" + textBoxShowTime.Text);
                textBoxHideTime.Text = otherUtile.addTime(textBoxShowTime.Text, textBoxDuration.Text);
                detectList.SetEndTime(detectList.IndexSubtitileOnEdit, textBoxHideTime.Text);
            }
        }

        private void NumericUpDownShowTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void NumericUpDownHideTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ChangeSubtitleWithMovieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (changeSubtitleWithMovieToolStripMenuItem.Checked)
                canChangeSubtitleWithMovie = true;
            else canChangeSubtitleWithMovie = false;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dataGridView1.Rows[detectList.IndexSubtitileOnEdit].Cells[4].Value+"");
        }

        private void NumericUpDownDuration_ValueChanged(object sender, EventArgs e)
        {
            detectList = new DetectList();
            double number = 10;
            number += (double)numericUpDownDuration.Value;
            // numericUpDownDuration.Value = (decimal)detectList.getTime(textBoxDuration.Text);
            textBoxDuration.Text = detectList.getTime(detectList.getTime(textBoxDuration.Text) + (number - 10) * 0.1);
        }

        private void buttonFont_Click(object sender, EventArgs e)
        {
            textBoxSubtitleText.Text = "<font>" + textBoxSubtitleText.Text + "</font>";
        }

       

        private void creatingGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.DataSource = subList;

            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "Row";
            column1.HeaderText = "Row";
            column1.DataPropertyName = "Row";
            dataGridView1.Columns.Add(column1);
            
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "showTime";
            column2.HeaderText = "ShowTime";
            column2.DataPropertyName = "showTime";
            dataGridView1.Columns.Add(column2);

            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "hideTime";
            column3.HeaderText = "HideTime";
            column3.DataPropertyName = "hideTime";
            dataGridView1.Columns.Add(column3);

            DataGridViewTextBoxColumn column4 = new DataGridViewTextBoxColumn();
            column4.Name = "duration";
            column4.HeaderText = "Duration";
            column4.DataPropertyName = "duration";
            dataGridView1.Columns.Add(column4);

            DataGridViewTextBoxColumn column5 = new DataGridViewTextBoxColumn();
            column5.Name = "text";
            column5.HeaderText = "Text";
            column5.DataPropertyName = "text";
            dataGridView1.Columns.Add(column5);
        }

    }

    struct Subtitle
    {
        public int rowId { set; get; }
        public string subtitleText { get; set; }
        public string showTime { get; set; }
        public string hideTime { get; set; }
        public string duration { get; set; }

    }
}

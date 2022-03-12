using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FFMpegBatchConvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            inputExt.Text = ".mp4";
            outputExt.Text = ".mp4";
            vidscale.Text = "720";


        }
        public string ffmpegPath = "";
        public string outputPath = "";
        public string inputPath = "";
        public string inext = ".mp4";
        public string outext = ".mp4";
        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.ShowDialog();
            ffmpegPath = folderBrowserDialog1.SelectedPath;
            label1.Text = ffmpegPath;
            // MessageBox.Show(ffmpegPath);
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Select input folder
            // MessageBox.Show(ffmpegPath);

            folderBrowserDialog2.ShowNewFolderButton = true;
            folderBrowserDialog2.ShowDialog();
            inputPath = folderBrowserDialog2.SelectedPath;
            label2.Text = folderBrowserDialog2.SelectedPath;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Convert
            if(ffmpegPath == "" || outputPath == "" || inputPath == "" || inext == "" || outext == "") {

                MessageBox.Show("You did not pick FFMpeg path, input, or output!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if(vidscale.Text != "" && comboBox1.Text != "")
            {
                //check if scale and bitrate have been selected
                MessageBox.Show(vidscale.Text);
                MessageBox.Show(outputPath);

                // string strCmdText;


                string cmd = "for /F \"tokens=1 delims=.\" %%a in ('dir /B \"" + inputPath + "\\*" + inext + "\"') do \"" + ffmpegPath + "\\ffmpeg.exe\"  -y -i \"" + inputPath + "\\%%a" + inext + "\" -vf \"scale=" + vidscale.Text + ":-2\" -b:v " + comboBox1.Text + " -b:a " + comboBox1.Text + "  \"" + outputPath + "\\%%a" + outext + "\""; ;
                // cmd = "echo Test!";
                string[] batch =
                {
                    "@echo off", "title Converting...", cmd, "pause", "exit"
                };
                File.WriteAllLines("gen.bat", batch);
                // ExecuteCommandSync("gen.bat");
                if(MessageBox.Show("This process will replace any existing file\n\nAre you sure you want to proceed?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("cmd.exe", "/C gen.bat");
                } else
                {
                    MessageBox.Show("Conversion canceled", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                string q = "";
            } else
            {
                MessageBox.Show("You did not select scale and bitrate!");
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        public void ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/K " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = false;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();
                // Display the command output.
                MessageBox.Show(result);
            }
            catch (Exception objException)
            {
                // Log the exception
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog3.ShowNewFolderButton = true;
            folderBrowserDialog3.ShowDialog();
            outputPath = folderBrowserDialog3.SelectedPath;
            label7.Text = outputPath;
        }

        private void inputExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            inext = inputExt.Text;
        }

        private void outputExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            outext = outputExt.Text;
        }

        private void vidscale_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PythonConnection;

namespace App
{
    /// <summary>
    /// DOS Classification Program
    /// </summary>
    public partial class Classification : Form
    {
        //DataSet path
        private const string pathData = @"../../../Scripts/data.csv"; 
        //JOBLIB path
        private const string pathJoblib = @"../../../Scripts/model.joblib";
        //Python script path
        private const string filePythonNamePath = @"../../../Scripts/main.py";
        //Python (.exe) path
        private string pathPy = @"";

        /// <summary>
        /// Centered form
        /// </summary>
        public Classification()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        /// <summary>
        /// Loading preparation message
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Arguments</param>
        private void Classification_Load(object sender, EventArgs e)
        {
            StartMessage();
        }

        /// <summary>
        /// This Button starts dataset checking
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Arguments</param>
        private void dataButton_Click(object sender, EventArgs e)
        {
            try
            {
                CheckData(pathData);
                MessageBox.Show("Your dataset is OK");
            }
            catch (Exception)
            {
                MessageBox.Show("Problems with DataSet.");
                return;
            }
        }

        /// <summary>
        /// Checking DataSet
        /// </summary>
        /// <param name="path">DataSet path</param>
        private void CheckData(string path)
        {
            using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                //No.,"Time","Source","Destination","Protocol","Length","Info"
                string[] head = sr.ReadLine().Split(',');
                if (head[0] != "\"No.\"" || head[1] != "\"Time\"" ||
                    head[2] != "\"Source\"" || head[3] != "\"Destination\"" ||
                    head[4] != "\"Protocol\"" || head[5] != "\"Length\"" || head[6] != "\"Info\"")
                {
                    throw new ArgumentException("Sth wrong with dataset.");
                }
            }
        }

        /// <summary>
        /// This Button checks JOBLIB's availability
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Arguments</param>
        private void joblibButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(pathJoblib))
                {
                    MessageBox.Show("JOBLIB is OK.");
                    return;
                }
                throw new ArgumentException();
            }
            catch(Exception)
            {
                MessageBox.Show("Problems with JOBLIB.");
            }
        }

        /// <summary>
        /// This button starts predication after component checks.
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Arguments</param>
        private void predictButton_Click(object sender, EventArgs e)
        {
            this.Text = "Process could take about 1 minute, please wait...";
            //Checking the selected server
            this.Enabled = false;
            if (serversBox.SelectedIndex == -1)
            {
                MessageBox.Show("You should choose server in comboBox.");
                this.Enabled = true;
                return;
            }

            //Checking the availability of the model
            try
            {
                if (!File.Exists(pathJoblib))
                {
                    MessageBox.Show("Problems with JOBLIB.");
                    this.Enabled = true;
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problems with access to JOBLIB");
                this.Enabled = true;
                return;
            }

            //Checking Data
            try
            {
                CheckData(pathData);
                this.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Problems with DataSet");
                this.Enabled = true;
                return;
            }

            //Checking python path
            if (String.IsNullOrEmpty(pathPy))
            {
                MessageBox.Show("Python path is empty");
                this.Enabled = true;
                return;
            }

            //Start predication
            StartProcess(pathPy, filePythonNamePath, serversBox.SelectedItem.ToString());
            this.Text = "Dos Attack Classification Program";
            this.Enabled = true;
        }

        /// <summary>
        /// This button starts choosing the python path direction
        /// </summary>
        /// <param name="sender">Form</param>
        /// <param name="e">Arguments</param>
        private void pythonButton_Click(object sender, EventArgs e)
        {
            try
            {
                string pathPython = ReturnPath(); //choosing process
                if (String.IsNullOrEmpty(pathPython))
                {
                    throw new ArgumentException("Path is empty");
                }
                pathPy = pathPython;
            }
            catch (Exception)
            {
                MessageBox.Show("Sth wrong with your python directory");
            }
        }

        /// <summary>
        /// choosing the python path direction
        /// </summary>
        /// <returns>String path</returns>
        private string ReturnPath()
        {
            string filePath = "";
            
            //some lemitation to make user to do right choice
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.FileName = "python*";
                openFileDialog.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (!(openFileDialog.ShowDialog() == DialogResult.OK))
                {
                    throw new ArgumentException();
                }
                filePath = openFileDialog.FileName;
            }
            return filePath;
        }

        /// <summary>
        /// Running python script
        /// </summary>
        /// <param name="pathPy"> Python (.exe) path </param>
        /// <param name="pathFilePy"> Python script path </param>
        /// <param name="server"> string server's IP </param>
        private void StartProcess(string pathPy, string pathFilePy, string server)
        {
            string outputText, standardError;

            // Create execution object            
            PythonCsConnector mlSharpPython = new PythonCsConnector(pathPy);

            // Create comand line instruction
            string fileNameParameter = $"{pathFilePy} {$"{server}"}";
            // Running python script
            outputText = mlSharpPython.RunPythonScript(fileNameParameter, out standardError);
            if (string.IsNullOrEmpty(standardError))
            {
                //if there aren't some execptions in python and c#
                PythonMessage(outputText);
                ShowImage();
            }
            else
            {
                //if there are some execptions in python and c#
                PythonMessage(standardError);
            }
        }

        private void ShowImage()
        {
            try
            {
                string graphPath = "graph.png";
                using (var file = new FileStream(graphPath, FileMode.Open,
                    FileAccess.Read, FileShare.Inheritable))
                {
                    Image image = Image.FromStream(file);
                    graphBox.SizeMode = (PictureBoxSizeMode)1;
                    graphBox.Image = image;
                }
                File.Delete(graphPath);
                //Image image = Image.FromFile("graph.png");
            }
            catch (Exception)
            {
                MessageBox.Show("Sth wrong with access to the picture.");
            }
        }

        private void StartMessage()
        {
            string message = "Before you start working , make sure that:\n" +
                            "\t1) You have your (.joblib) file in ../Scripts\n" +
                            "\t2) You have your (.csv) file in ../Scripts\n" +
                            "\t3) You have installed the following packages for your python:\n" +
                            "\t   pandas, numpy, joblib, matplotlib, argparse.";
            string caption = "Preparation!";
            MessageBox.Show(message, caption);
        }

        private void PythonMessage(string message)
        {
            string caption = "Python message";
            MessageBox.Show(message, caption);
        }
    }
}

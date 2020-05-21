using System;
using System.Diagnostics;

namespace PythonConnection
{
    /// <summary>
    /// Connection between C# and Python 
    /// </summary>
    public class PythonCsConnector
    {
        public readonly string filePythonExePath;

        /// <summary>
        /// Connection constructor
        /// </summary>
        /// <param name="exePythonPath">Python (.exe) path</param>
        public PythonCsConnector(string exePythonPath)
        {
            filePythonExePath = exePythonPath;
        }

        /// <summary>
        /// Run Python script
        /// </summary>
        /// <param name="filePythonScript">Python script path + arguments</param>
        /// <param name="standardError">Output error/exceptions</param>
        /// <returns>Output python answers</returns>
        public string RunPythonScript(string filePythonScript, out string standardError)
        {
            string outputText = string.Empty;
            standardError = string.Empty;
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo(filePythonExePath)
                    {
                        Arguments = filePythonScript,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };
                    process.Start();
                    outputText = process.StandardOutput.ReadToEnd();
                    outputText = outputText.Replace(Environment.NewLine, string.Empty);
                    standardError = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                standardError = ex.Message;
                return "";
            }
            return outputText;
        }
    }
}

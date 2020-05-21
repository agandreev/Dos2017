using System;
using PythonConnection;
namespace RunPythonScript
{
    public class Program
    {
        // Get config settings
        private static string filePythonExePath = @"C:\Users\Lenovo X1 Extreme\Anaconda3\python.exe";
        //private static string folderImagePath = Properties.Settings.Default.FolderImagePath;
        private static string filePythonNamePath = @"../../../Scripts/main.py";
        //private static string filePythonParameterName = Properties.Settings.Default.FilePythonParameterName;

        static void Main()
        {
            string outputText, standardError;

            // Instantiate Machine Learning C# - Python class object            
            IMLSharpPython mlSharpPython = new MLSharpPython(filePythonExePath);

            // Define Python script file and input parameter name
            string fileNameParameter = $"{filePythonNamePath} {$"sth"}";
            // Execute the python script file 
            outputText = mlSharpPython.ExecutePythonScript(fileNameParameter, out standardError);
            if (string.IsNullOrEmpty(standardError))
            {
                //if there aren't some execptions in python and c#
                Console.WriteLine(outputText);
            }
            else
            {
                //if there are some execption in python and c#
                Console.WriteLine(standardError);
            }
            Console.ReadKey();
        }
    }
}

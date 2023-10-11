using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LovelyBytes.AssetVariables
{
    internal struct FileMapping
    {
        internal string SourcePath;
        internal string FileName;
    }
    
    internal abstract class BaseGenerator<TGenerator> : EditorWindow
        where TGenerator : BaseGenerator<TGenerator>
    {
        protected static string ParentDirectory
            => GeneratorUtils.GetParentDirectory(nameof(BaseGenerator<TGenerator>));
        
        protected static string PathToVariableTemplate 
            => ParentDirectory + "/Templates/Variable.txt";
        protected static string PathToListenerTemplate 
            => ParentDirectory + "/Templates/Listener.txt";
        
        private static string TargetDirectory = "";
        
        protected static void ShowWindow()
        {
            TargetDirectory = GeneratorUtils.GetSelectionDirectory();
            var window = GetWindow(typeof(TGenerator)) as TGenerator;
            
            if (window)
                window.Show();
        }

        protected virtual void OnGUI()
        {
            if (!HasUserInput()) 
                return;
            
            if (!GUILayout.Button("Generate"))
                return;

            if (!IsInputValid())
                return;
            
            GenerateSourceFiles();
        }
        
        private static void DisplayFailureDialog(in string failureMessage)
        {
            EditorUtility.DisplayDialog("Failed to create source Files", failureMessage, "Sad");
        }

        protected virtual void GenerateSourceFiles()
        {
            var keywords = GetKeywordValues();
                
            try
            {
                WriteAllFiles();
                DisplaySuccessDialog();
                AssetDatabase.Refresh();
            }
            catch (FileWriter.Exception e)
            {
                DisplayFailureDialog(e.Message);
            }
        }

        /// <summary>
        /// Use this method to check for user input.
        /// The return value should indicate whether the minimum amount of input has been provided.
        /// </summary>
        protected virtual bool HasUserInput()
        {
            GeneratorUtils.SelectDirectoryGUI(ref TargetDirectory);
            return true;
        }

        /// <summary>
        /// Use this method to check the user input for correctness.
        /// The return value should indicate whether valid input has been provided.
        /// </summary>
        protected virtual bool IsInputValid()
        {
            if (Directory.Exists(TargetDirectory)) 
                return true;
            
            EditorUtility.DisplayDialog(
                $"Directory does not exist: {TargetDirectory}",
                "Make sure to pick an existing directory next time",
                "Alrighty then");
            return false;

        }
        /// <summary>
        /// This method should return an array of mappings
        /// that indicates which keyword should be replaced with which value.
        /// Unused keywords don't need to be mapped.
        /// </summary>
        protected abstract KeyValuePair<string, string>[] GetKeywordValues();
        /// <summary>
        /// Each entry in this array should indicate the path to a script template,
        /// as well as the folder where the processed template should be stored as a script
        /// </summary>
        protected abstract FileMapping[] GetFileMappings();
        
        private void WriteAllFiles()
        {
            var keywordValues = GetKeywordValues();
            var fileMappings = GetFileMappings();
            
            FileWriter fileWriter = new();

            foreach (FileMapping mapping in fileMappings)
            {
                fileWriter.LoadFile(mapping.SourcePath);
                
                foreach(var pair in keywordValues)
                    fileWriter.SetKeyword(pair.Key, pair.Value);

                string nameSpace = GeneratorUtils.GetRootNamespace(TargetDirectory);
                fileWriter.SetNameSpace(nameSpace);
                
                fileWriter.WriteFile($"{TargetDirectory}/{mapping.FileName}");
            }
        }
        
        private void DisplaySuccessDialog()
        {
            string message = GetFileMappings().Aggregate(
                "Created the following scripts:\n\n", 
                (current, mapping) => current + $"{mapping.FileName}\n\n"
            );

            EditorUtility.DisplayDialog(
                "Success",
                message,
                "Nicenstein"
            );
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LovelyBytesGaming.AssetVariables
{
    internal struct FileMapping
    {
        internal string SourcePath;
        internal string DestinationPath;
    }
    
    internal abstract class BaseGenerator<TGenerator> : EditorWindow
        where TGenerator : BaseGenerator<TGenerator>
    {
        protected static string ParentDirectory
            => Utils.GetParentDirectory(nameof(BaseGenerator<TGenerator>));
        
        protected static void ShowWindow()
        {
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

            try
            {
                InitializePluginFolder();
            }
            catch (FileWriter.Exception e)
            {
                DisplayFailureDialog(e.Message);
                return;
            }
            
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
        protected abstract bool HasUserInput();
        /// <summary>
        /// Use this method to check the user input for correctness.
        /// The return value should indicate whether valid input has been provided.
        /// </summary>
        protected abstract bool IsInputValid();
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
        
        private static void InitializePluginFolder()
        {
            if (FileWriter.DirectoryExists(Constants.TargetDirectoryRuntime)) 
                return;
            
            FileWriter fileWriter = new();
            
            fileWriter.SetContent(Constants.RuntimeAsmRef);
            fileWriter.WriteFile(Constants.TargetDirectoryRuntime + "Runtime.asmref");
                        
            fileWriter.SetContent(Constants.EditorAsmRef);
            fileWriter.WriteFile(Constants.TargetDirectoryEditor + "Editor.asmref");
        }
        
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
                
                fileWriter.WriteFile(mapping.DestinationPath);
            }
        }
        
        private void DisplaySuccessDialog()
        {
            string message = GetFileMappings().Aggregate(
                "Created the following scripts:\n\n", 
                (current, mapping) => current + $"{mapping.DestinationPath}\n\n"
            );

            EditorUtility.DisplayDialog(
                "Success",
                message,
                "Nicenstein"
            );
        }
    }
}

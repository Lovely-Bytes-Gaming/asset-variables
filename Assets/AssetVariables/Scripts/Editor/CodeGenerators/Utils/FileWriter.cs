using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine.PlayerLoop;

namespace LovelyBytes.AssetVariables
{
    internal class FileWriter
    {
        internal class Exception : System.Exception
        {
            public Exception(string message) : base(message) {}            
        }
        
        private string _fileContent;

        internal void SetContent(string content)
        {
            _fileContent = content;
        }
        
        internal void LoadFile(string path)
        {
            if (!File.Exists(path))
                throw new Exception($"The template file at path {path} does not exist.");
            
            _fileContent = File.ReadAllText(path);
        }

        internal void SetKeyword(string keyword, string content)
        {
            if (string.IsNullOrEmpty(_fileContent))
                throw new Exception($"Trying to set keyword {keyword} to {content}, but no source file was loaded!");

            _fileContent = _fileContent.Replace(keyword, content);
        }

        internal void SetNameSpace(string nameSpace)
        {
            if (string.IsNullOrEmpty(nameSpace))
            {
                SetKeyword(EditorConstants.NamespaceBeginKeyword, "");
                SetKeyword(EditorConstants.NamespaceEndKeyword, "");
                return;
            }

            int index = _fileContent.IndexOf(EditorConstants.NamespaceBeginKeyword, 
                StringComparison.Ordinal);
            
            index = _fileContent.IndexOf('\n', index) + 1;

            while (index > 0)
            {
                int endIndex = _fileContent.IndexOf(EditorConstants.NamespaceEndKeyword, StringComparison.Ordinal);
                
                if (index >= endIndex)
                    break;
                
                _fileContent = _fileContent.Insert(index, "\t");
                index = _fileContent.IndexOf('\n', index) + 1;
            }
            
            SetKeyword(EditorConstants.NamespaceBeginKeyword, $"namespace {nameSpace}\n{{\n");
            SetKeyword(EditorConstants.NamespaceEndKeyword, "}");
        } 

        internal void WriteFile(string destPath)
        {
            if (string.IsNullOrEmpty(_fileContent))
                throw new Exception($"Trying to write file to {destPath}, but no file was loaded!");

            var file = new FileInfo(destPath);
            
            if (file.Directory == null)
                throw new Exception($"Cannot resolve parent directory for {destPath}!");
            
            file.Directory.Create();
            File.WriteAllText(file.FullName, _fileContent);
        }

        internal static bool DirectoryExists(string path)
        {
            var file = new FileInfo(path);
            return file.Directory is { Exists: true };
        }
    }
}

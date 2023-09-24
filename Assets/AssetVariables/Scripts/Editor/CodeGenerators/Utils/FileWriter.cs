using System.IO;

namespace LovelyBytesGaming.AssetVariables
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

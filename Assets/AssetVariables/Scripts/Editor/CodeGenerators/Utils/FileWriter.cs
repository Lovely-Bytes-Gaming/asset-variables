using System.IO;

namespace LovelyBytesGaming.AssetVariables
{
    internal class FileWriter
    {
        internal struct Result
        {
            internal bool Success;
            internal string Message;
        }
        
        private string _fileContent;

        internal Result LoadFile(string path)
        {
            if (!File.Exists(_fileContent))
            {
                return new Result
                {
                    Success = false,
                    Message = $"The template file at path {path} does not exist."
                };
            }

            _fileContent = File.ReadAllText(path);
            return new Result { Success = true };
        }

        internal Result SetKeyword(string keyword, string content)
        {
            if (string.IsNullOrEmpty(_fileContent))
            {
                return new Result
                {
                    Success = false,
                    Message = $"Trying to set keyword {keyword} to {content}, but no source file was loaded!"
                };
            }

            _fileContent = _fileContent.Replace(keyword, content);
            return new Result { Success = true };
        }

        internal Result WriteFile(string destPath)
        {
            if (string.IsNullOrEmpty(_fileContent))
            {
                return new Result
                {
                    Success = false,
                    Message = $"Trying to write file to {destPath}, but no file was loaded!"
                };
            }
            
            var file = new FileInfo(destPath);
            
            if (file.Directory == null)
            {
                return new Result
                {
                    Success = false,
                    Message = $"Cannot resolve parent directory for {destPath}!"
                };
            }
            
            file.Directory.Create();
            File.WriteAllText(file.FullName, _fileContent);

            return new Result { Success = true };
        }

        internal static bool DirectoryExists(string path)
        {
            var file = new FileInfo(path);
            return file.Directory is { Exists: true };
        }
    }
}

#if UNITY_EDITOR 

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

internal sealed class ScriptKeywordProcessor : AssetModificationProcessor
{
    private static readonly char[] delimiters = new char[] { '/', '\\', '.' };
    private static readonly List<string> excludeFromNamespace = new() 
    { 
        "Scripts", 
        "Editor", 
        "Runtime", 
        "Experimental" 
    };

    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        
        if(!path.EndsWith(".cs"))
            return;

        string namespaceString = NamespaceFromPath(path);
        string assetMenuString = namespaceString.Replace('.', '/');

        int assetFolderIndex = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, assetFolderIndex) + path;
        
        if (!System.IO.File.Exists(path))
            return;

        string fileContent = System.IO.File.ReadAllText(path);
        fileContent = fileContent.Replace("#NAMESPACE#", namespaceString);
        fileContent = fileContent.Replace("#ASSETMENU#", assetMenuString);

        System.IO.File.WriteAllText(path, fileContent);
        AssetDatabase.Refresh();
    }

    public static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath) 
    {
        if(sourcePath.EndsWith(".cs")) 
            OnWillMoveScriptAsset(sourcePath, destinationPath);

        return AssetMoveResult.DidNotMove;
    }

    private static void OnWillMoveScriptAsset(string sourcePath, string destinationPath) 
    {
        string fileContent = System.IO.File.ReadAllText(sourcePath);
        fileContent = MatchNamespaceWithDirectory(fileContent, sourcePath, destinationPath);

        System.IO.File.WriteAllText(sourcePath, fileContent);
    }

    private static string MatchNamespaceWithDirectory(string scriptContent, string sourcePath, string destinationPath) 
    {
        string oldNamespace = NamespaceFromPath(sourcePath);
        string newNamespace = NamespaceFromPath(destinationPath);

        // only change the namespace if the file's namespace already replicated the projects folder structure before it was moved
        return scriptContent.Replace(oldNamespace, newNamespace);
    }

    private static string NamespaceFromPath(string path) 
    {
        List<string> namespaces = path.Split(delimiters).ToList();
        namespaces = namespaces.GetRange(1, namespaces.Count - 3);
        namespaces = namespaces.Except(excludeFromNamespace).ToList();

        string namespaceString = "";
        for (int i = 0; i < namespaces.Count; i++)
        {
            namespaceString += namespaces[i];

            if(i + 1 < namespaces.Count)
                namespaceString += ".";
        }

        return namespaceString;
    }
}

#endif
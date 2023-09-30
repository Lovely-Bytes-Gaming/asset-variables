using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace LovelyBytesGaming.AssetVariables
{
    internal class TypeWrapperGenerator : BaseGenerator<TypeWrapperGenerator>
    {
        private static string _nameConstraint;
        private static int _selectedAssemblyIndex = 0;
        private static int _selectedTypeIndex = 0;
        private static Type _selectedType = null;
        
        [MenuItem(Constants.AssetMenuBasePath + "Create Wrapper for Type")]
        public static void CreateNewTypeWrapper()
        {
            ShowWindow();
        }
        
        protected override bool HasUserInput()
        {
            _nameConstraint = EditorGUILayout.TextField("Assembly Name Constraint", _nameConstraint);
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (!string.IsNullOrEmpty(_nameConstraint))
                assemblies = assemblies.Where(asm => asm.FullName.Contains(_nameConstraint)).ToArray();
            
            List<string> options = new();

            for (int i = 0; i < assemblies.Length; ++i)
            {
                string assemblyName = assemblies[i]
                    .GetName()
                    .Name;

                options.Add(assemblyName);
            }

            _selectedAssemblyIndex = EditorGUILayout.Popup("Assemblies", _selectedAssemblyIndex, options.ToArray());

            Assembly selectedAssembly = assemblies[_selectedAssemblyIndex];
            
            options.Clear();

            foreach (Type type in selectedAssembly.GetTypes())
            {
                // currently only supports structs and enums
                if (!type.IsValueType)
                    continue;

                string typeName = type.FullName;

                if (typeName == null)
                    continue;
                
                if (typeName.Contains("<PrivateImplementationDetails>"))
                    continue;

                if (typeName.Contains("UnitySourceGeneratedAssemblyMonoScriptTypes"))
                    continue;
                
                options.Add(typeName);
            }

            _selectedTypeIndex = EditorGUILayout.Popup("Types", _selectedTypeIndex, options.ToArray());
            return false;
        }

        protected override bool IsInputValid()
        {
            return false;
        }

        protected override KeyValuePair<string, string>[] GetKeywordValues()
        {
            return null;
        }

        protected override FileMapping[] GetFileMappings()
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

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
            {
                assemblies = assemblies
                    .Where(asm => asm.FullName.Contains(_nameConstraint))
                    .ToArray();

                if (assemblies.Length < 1)
                    return false;
            }

            List<string> options = new();

            foreach (Assembly assembly in assemblies)
            {
                string assemblyName = assembly
                    .GetName()
                    .Name;

                options.Add(assemblyName);
            }

            _selectedAssemblyIndex = EditorGUILayout.Popup("Assemblies", _selectedAssemblyIndex, options.ToArray());

            Assembly selectedAssembly = assemblies[_selectedAssemblyIndex];
            
            options.Clear();

            var types = selectedAssembly
                .GetExportedTypes()
                .Where(IsValidType)
                .ToArray();

            if (types.Length < 1)
                return false;
            
            foreach (Type type in types)
            {
                string typeName = type.FullName;

                if (typeName == null)
                    continue;
                
                options.Add(typeName);
            }

            _selectedTypeIndex = EditorGUILayout.Popup("Types", _selectedTypeIndex, options.ToArray());
            _selectedType = types[_selectedTypeIndex];
            return true;
        }

        protected override bool IsInputValid()
        {
            return true;
        }

        protected override KeyValuePair<string, string>[] GetKeywordValues()
        {
            return null;
        }

        protected override FileMapping[] GetFileMappings()
        {
            return null;
        }

        private static bool IsValidType(Type type)
        {
            if (typeof(Component).IsAssignableFrom(type))
                return false;

            if (typeof(GameObject).IsAssignableFrom(type))
                return false;
            
            if (type.IsEnum)
                return true;

            // type must provide a parameterless constructor in order to be valid.
            // structs always have an implicit one that cannot be overriden
            if (type.IsClass && type.GetConstructor(Type.EmptyTypes) == null)
                return false;
            
            // type should be serializable, so that its value can be saved along the asset
            return type.GetCustomAttribute(typeof(SerializableAttribute)) != null;
        }
    }
}

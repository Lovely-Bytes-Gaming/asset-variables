// 0: Name
// 1: Editor Fields

using UnityEngine;
using UnityEditor;

namespace LovelyBytesGaming.AssetVariables
{
    [CustomEditor(typeof(FooVariable))]
    public class FooVariableEditor : VariableEditor<Foo>
    {
        protected override Foo GenericEditorField(string description, Foo value)
        {
            value.One = EditorGUILayout.Toggle("One", value.One);
		    value.Two = EditorGUILayout.IntField("Two", value.Two);
            
            return value;
        }
    }
}
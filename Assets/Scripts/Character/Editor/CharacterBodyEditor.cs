using Cattac.Character;
using UnityEditor;
using UnityEngine;

namespace Character.Editor
{
    
    [CustomEditor(typeof(CharacterBody))]
    public class CharacterBodyEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            CharacterBody data = (CharacterBody)target;

            base.OnInspectorGUI();

            EditorGUILayout.Space(25);

            if (GUILayout.Button("Set Data"))
                data.SetBodyData();
        }
    }
}
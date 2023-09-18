using UnityEngine;
using UnityEngine.UI;
using UnityEditorInternal;
using UnityEditor.AnimatedValues;

namespace UnityEditor.UI
{

    [CustomEditor(typeof(AdvancedGridLayout), true)]
    [CanEditMultipleObjects]
    /// <summary>
    /// Custom Editor for the AdvancedGridLayout Component.
    /// Extend this class to write a custom editor for a component derived from GridLayoutGroup.
    /// </summary>
    public class AdvancedGridLayoutEditor : GridLayoutGroupEditor
    {
        SerializedProperty m_autoSizeVertical;
        SerializedProperty m_autoSizeHorizontal;
        SerializedProperty m_isSquare;
        SerializedProperty m_useMaxCellSize;
        SerializedProperty m_maxCellSize;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_autoSizeVertical = serializedObject.FindProperty("_autoSizeVertical");
            m_autoSizeHorizontal = serializedObject.FindProperty("_autoSizeHorizontal");
            m_isSquare = serializedObject.FindProperty("_isSquare");
            m_useMaxCellSize = serializedObject.FindProperty("_useMaxCellSize");
            m_maxCellSize = serializedObject.FindProperty("_maxCellSize");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(m_autoSizeVertical, true);
            EditorGUILayout.PropertyField(m_autoSizeHorizontal, true);
            EditorGUILayout.PropertyField(m_isSquare, true);
            EditorGUILayout.PropertyField(m_useMaxCellSize, true);

            if (m_useMaxCellSize.boolValue == true)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(m_maxCellSize, true);
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}


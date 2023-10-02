//================================================================================
//
//  RenameFieldAttribute
//
//  Unity�C���X�y�N�^�[��Ńt�B�[���h�̖��O��ύX����ׂ̑���
//
//================================================================================

using UnityEditor;
using UnityEngine;

class RenameFieldAttribute : PropertyAttribute
{

    /// <summary>
    /// �t�B�[���h��
    /// </summary>
    public string fieldName
    {
        get;
        private set;
    }

    /// <summary>
    /// �t�B�[���h�A�g���r���[�g�̖��O�ύX
    /// </summary>
    /// <param name="name">�ύX��̖��O</param>
    public RenameFieldAttribute(string name)
    {
        fieldName = name;
    }

    /// <summary>
    /// �G�f�B�^�[GUI��ł̏���
    /// </summary>
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RenameFieldAttribute))]
    class FieldNameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string[] path = property.propertyPath.Split('.');
            bool isArray = 1 < path.Length && path[1] == "Array";

            if (!isArray && attribute is RenameFieldAttribute fieldName)
            {
                label.text = fieldName.fieldName;
            }

            EditorGUI.PropertyField(position, property, label, true);
        }
    }
#endif

}

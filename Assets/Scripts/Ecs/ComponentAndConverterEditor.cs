using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using NaughtyAttributes;

#if UNITY_EDITOR
public class ComponentAndConverterEditor : EditorWindow
{
    [SerializeField] private string _componentName1;
    private string _componentName = "Component name";

    [Button] 
    public void CreateComponentAndConverter()
    {
        EcsScriptsCreation.CreateComponentAndConverter(_componentName);
    }

    private void OnGUI()
    {
        _componentName = EditorGUILayout.TextField(_componentName);

        if(GUILayout.Button("Create"))
        {
            CreateComponentAndConverter();
        }
    }
}
#endif
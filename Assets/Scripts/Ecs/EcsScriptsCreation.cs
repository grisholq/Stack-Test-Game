using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
public class EcsScriptsCreation
{
    public const string PATH_TO_SYSTEM_SCRIPT_TEMPLATE = @"C:\Unity Scripts Templates\NewSystem.txt";
    public const string PATH_TO_COMPONENT_SCRIPT_TEMPLATE = @"C:\Unity Scripts Templates\NewComponent.txt";
    public const string PATH_TO_COMPONENT_CONVERTER_SCRIPT_TEMPLATE = @"C:\Unity Scripts Templates\NewConverter.txt";
    public const string PATH_TO_TAG_SCRIPT_TEMPLATE = @"C:\Unity Scripts Templates\NewTag.txt";
    public const string PATH_TO_EXTENTION_SCRIPT_TEMPLATE = @"C:\Unity Scripts Templates\SystemsExtention.txt";

    [MenuItem("Ecs/System")]
    public static void CreateSystem()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(PATH_TO_SYSTEM_SCRIPT_TEMPLATE, "NewSystem.cs");
    }

    [MenuItem("Ecs/Component")]
    public static void CreateComponent(MenuCommand cmd)
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(PATH_TO_COMPONENT_SCRIPT_TEMPLATE, "NewComponent.cs");
        
    }

    public static void CreateComponentAndConverter(string componentName)
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(PATH_TO_COMPONENT_SCRIPT_TEMPLATE, componentName + ".cs");
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(PATH_TO_COMPONENT_CONVERTER_SCRIPT_TEMPLATE, componentName + "Converter.cs");
    }

    [MenuItem("Ecs/ConverterMenu")]
    public static void OpenConverterMenu(MenuCommand cmd)
    {
        EditorWindow wnd = EditorWindow.GetWindow<ComponentAndConverterEditor>();
        wnd.titleContent = new GUIContent("Converter and component menu");
        wnd.Show();
    }

    [MenuItem("Ecs/Tag")]
    public static void CreateTag(MenuCommand cmd)
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(PATH_TO_TAG_SCRIPT_TEMPLATE, "NewTag.cs");
    }

    [MenuItem("Ecs/SystemsExtention")]
    public static void CreateSystemsExtention(MenuCommand cmd)
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(PATH_TO_EXTENTION_SCRIPT_TEMPLATE, "SystemsExtention.cs");
    }
}
#endif
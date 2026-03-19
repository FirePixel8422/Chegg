using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BaseUnitTypeSO))]
public sealed class BaseUnitTypeSOEditor : Editor
{
    private Type[] cachedUnitTypes;

    private void OnEnable()
    {
        CacheTypes();
    }

    private void CacheTypes()
    {
        cachedUnitTypes = TypeCache.GetTypesDerivedFrom<UnitTypeBase>()
            .Where(t => !t.IsAbstract && !t.IsGenericType && t.IsClass)
            .OrderBy(t => t.Name)
            .ToArray();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Set UnitType"))
        {
            ShowMenu();
        }

        if (cachedUnitTypes.Length == 0)
        {
            EditorGUILayout.HelpBox("No UnitTypeBase implementations found.", MessageType.Warning);

            if (GUILayout.Button("Refresh Types"))
            {
                CacheTypes();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowMenu()
    {
        GenericMenu menu = new GenericMenu();

        for (int i = 0; i < cachedUnitTypes.Length; i++)
        {
            Type type = cachedUnitTypes[i];
            menu.AddItem(new GUIContent(type.Name), false, OnTypeSelected, type);
        }

        menu.ShowAsContext();
    }

    private void OnTypeSelected(object typeObj)
    {
        Type type = (Type)typeObj;
        BaseUnitTypeSO so = (BaseUnitTypeSO)target;

        Undo.RecordObject(so, "Set UnitType");

        so.Value = CreateInstance(type);

        EditorUtility.SetDirty(so);
        serializedObject.Update();

        Repaint();
    }

    private new UnitTypeBase CreateInstance(Type type)
    {
        return (UnitTypeBase)Activator.CreateInstance(type);
    }
}
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class CustomEditorWindow : EditorWindow
{
    protected Dictionary<String, VisualElement> fields = new Dictionary<String, VisualElement>();

    //Field category list offered by Unity: https://docs.unity3d.com/6000.1/Documentation/Manual/UIE-ElementRef.html
    //Some of these have been implemented below, but Unity has others we can add if needed

    /// <summary>
    /// Displays the given text in the window.
    /// </summary>
    /// <param name="root">Root Visual Element</param>
    /// <param name="label">Text to display.</param>
    protected void CreateLabel(VisualElement root, string label)
    {
        Label element = new Label(label);
        root.Add(element);
    }

    protected void AddSpace(VisualElement root)
    {
        CreateLabel(root, "***");
    }

    private void AddField(VisualElement root, string key, VisualElement field)
    {
        root.Add(field);
        fields.Add(key, field);
    }

    /// <summary>
    /// Creates a Unity object field in the window to take in user input.
    /// </summary>
    /// <param name="label">Label that will be displayed next to the field in the editor window.</param>
    /// <param name="key">Key that can be used to access this field from the Dictionary.</param>
    /// <param name="type">Type the user can input in the editor (can be any Unity object).</param>
    /// <param name="root">Root Visual Element</param>
    protected void CreateField(string label, string key, Type type, VisualElement root)
    {
        ObjectField field = new ObjectField(label);
        field.objectType = type;
        AddField(root, key, field);
    }

    /// <summary>
    /// Creates a string field in the window to take in user input.
    /// </summary>
    /// <param name="label">Label that will be displayed next to the field in the editor window.</param>
    /// <param name="key">Key that can be used to access this field from the Dictionary.</param>
    /// <param name="root">Root Visual Element</param>
    /// <param name="defaultValue">Optional Default Value</param>
    protected void CreateStringField(string label, string key, VisualElement root, string defaultValue = "")
    {
        TextField field = new TextField();
        field.label = label;
        field.textEdition.placeholder = defaultValue;
        AddField(root, key, field);
    }

    /// <summary>
    /// Creates a float field in the window to take in user input.
    /// </summary>
    /// <param name="label">Label that will be displayed next to the field in the editor window.</param>
    /// <param name="key">Key that can be used to access this field from the Dictionary.</param>
    /// <param name="root">Root Visual Element</param>
    /// <param name="defaultValue">Optional Default Value</param>
    protected void CreateFloatField(string label, string key, VisualElement root, float defaultValue = 0f)
    {
        FloatField field = new FloatField(label);
        field.value = defaultValue;
        AddField(root, key, field);
    }

    /// <summary>
    /// Creates a integer field in the window to take in user input.
    /// </summary>
    /// <param name="label">Label that will be displayed next to the field in the editor window.</param>
    /// <param name="key">Key that can be used to access this field from the Dictionary.</param>
    /// <param name="root">Root Visual Element</param>
    /// <param name="defaultValue">Optional Default Value</param>
    protected void CreateIntegerField(string label, string key, VisualElement root, int defaultValue = 0)
    {
        IntegerField field = new IntegerField(label);
        field.value = defaultValue;
        AddField(root, key, field);
    }

    /// <summary>
    /// Creates a toggle in the window to take in user input.
    /// </summary>
    /// <param name="label">Label that will be displayed next to the toggle in the editor window.</param>
    /// <param name="key">Key that can be used to access this field from the Dictionary.</param>
    /// <param name="root">Root Visual Element</param>
    /// <param name="defaultValue">Optional Default Value</param>
    protected void CreateBooleanField(string label, string key, VisualElement root, bool defaultValue = false)
    {
        Toggle field = new Toggle(label);
        field.value = defaultValue;
        AddField(root, key, field);
    }

    /// <summary>
    /// Creates a dropdown menu in the window to take in user input.
    /// </summary>
    /// <param name="root">Root Visual Element</param>
    /// <param name="label">Label that will be displayed next to the dropdown in the editor window.</param>
    /// <param name="key">Key that can be used to access this field from the Dictionary.</param>
    /// <param name="options">Options for the dropdown.</param>
    protected void CreateDropdown(VisualElement root, string label, string key, params string[] options)
    {
        DropdownField field = new DropdownField(label);

        foreach (string option in options)
        {
            field.choices.Add(option);
        }

        AddField(root, key, field);
    }
}

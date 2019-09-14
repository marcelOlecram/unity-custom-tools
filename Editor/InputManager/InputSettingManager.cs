using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class InputSettingManager : EditorWindow
{
    #region variables
    
    // to map values 
    private InputAxis _inputAxis;
    // input element data
    public string _name;
    public string descriptiveName;
    public string descriptiveNegativeName;
    public string negativeButton;
    public string positiveButton;
    public string altNegativeButton;
    public string altPositiveButton;
    public float gravity;
    public float dead;
    public float sensitivity;
    public bool snap = false;
    public bool invert = false;
    public int type;
    public int axis;
    public int joyNum;
    // types of input  
    private string[] typeValues = new string[]
    {
        "Key or Mouse Movement", "Mouse Movement", "Joystick Axis"
    };
    // axis values
    private string[] axisValues = new string[]
    {
        "X axis", "Y axis", "3rd axis (Joysticks and Scrollwheel)",
        "4th axis (Joysticks)",
        "5th axis (Joysticks)",
        "6th axis (Joysticks)",
        "7th axis (Joysticks)",
        "8th axis (Joysticks)",
        "9th axis (Joysticks)",
        "10th axis (Joysticks)",
        "11th axis (Joysticks)",
        "12th axis (Joysticks)",
        "13th axis (Joysticks)",
        "14th axis (Joysticks)",
        "15th axis (Joysticks)",
        "16th axis (Joysticks)",
        "17th axis (Joysticks)",
        "18th axis (Joysticks)",
        "19th axis (Joysticks)",
        "20th axis (Joysticks)",
        "21th axis (Joysticks)",
        "22th axis (Joysticks)",
        "23th axis (Joysticks)",
        "24th axis (Joysticks)",
        "25th axis (Joysticks)",
        "26th axis (Joysticks)",
        "27th axis (Joysticks)",
        "28th axis (Joysticks)"
    };
    // joystick values
    private string[] joystickValues = new string[]
    {
        "Get Motion from all Joysticks",
        "Joystick 1",
        "Joystick 2",
        "Joystick 3",
        "Joystick 4",
        "Joystick 5",
        "Joystick 6",
        "Joystick 7",
        "Joystick 8",
        "Joystick 9",
        "Joystick 10",
        "Joystick 11",
        "Joystick 12",
        "Joystick 13",
        "Joystick 14",
        "Joystick 15",
        "Joystick 16",
    };


    #endregion

    #region Unity Methods

    private void Awake()
    {
        ClearValues();
        _inputAxis = new InputAxis();
    }
    
    private void OnGUI()
    {        
        // button to clear all input fields
        if (GUILayout.Button("CLEAR ALL"))
        {            
            _inputAxis = new InputAxis();
            ClearValues();
            Debug.Log("CLEARED");
        }
        // button to add 4 joystick by default
        /*
        if (GUILayout.Button("Load 4 joysticks"))
        {
            Debug.Log("making axis");
            AddJoystick(1);
            AddJoystick(2);
            AddJoystick(3);
            AddJoystick(4);
        }
        */
        // Get Values to add as new input element
        GUILayout.Label("New Input Axis", EditorStyles.boldLabel);
        _name = EditorGUILayout.TextField("Name", _name);
        descriptiveName = EditorGUILayout.TextField("DescriptiveName", descriptiveName);
        descriptiveNegativeName = EditorGUILayout.TextField("DescriptiveNegativeName", descriptiveNegativeName);
        negativeButton = EditorGUILayout.TextField("NegativeButton", negativeButton);
        positiveButton = EditorGUILayout.TextField("PositiveButton", positiveButton);
        altNegativeButton = EditorGUILayout.TextField("AltNegativeButton", altNegativeButton);
        altPositiveButton = EditorGUILayout.TextField("AltPositiveButton", altPositiveButton);
        gravity = EditorGUILayout.FloatField("Gravity", gravity);
        dead = EditorGUILayout.FloatField("Dead", dead);
        sensitivity = EditorGUILayout.FloatField("Sensitivity", sensitivity);
        snap = EditorGUILayout.Toggle("Snap", snap);
        invert = EditorGUILayout.Toggle("Invert", invert);

        type = EditorGUILayout.Popup("Type", type, typeValues);
        axis = EditorGUILayout.Popup("Axis", axis, axisValues);
        joyNum = EditorGUILayout.Popup("JoyNum", joyNum, joystickValues);       

        // Button to add new Input
        if (GUILayout.Button("Add Input Axis"))
        {
            _inputAxis = new InputAxis();

            _inputAxis.name = _name;
            _inputAxis.descriptiveName = descriptiveName;
            _inputAxis.descriptiveNegativeName = descriptiveNegativeName;
            _inputAxis.negativeButton = negativeButton;
            _inputAxis.positiveButton = positiveButton;
            _inputAxis.altNegativeButton = altNegativeButton;
            _inputAxis.altPositiveButton = altPositiveButton;
            _inputAxis.gravity = gravity;
            _inputAxis.dead = dead;
            _inputAxis.sensitivity = sensitivity;
            _inputAxis.snap = snap;
            _inputAxis.invert = invert;
            _inputAxis.type = type;
            _inputAxis.axis = axis;
            _inputAxis.joyNum = joyNum;
            AddAxis(_inputAxis);
            ClearValues();
        }
    }

    #endregion

    #region My Methods

    [MenuItem("Window/Custom Input Manager")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<InputSettingManager>("Custom Input Manager");
    }

    // adds joystick as input element
    private void AddJoystick(int player)
    {
        var playerPrefix = $"P{player}";
        var inputAxis = new InputAxis();
        var newAxisName = "";
        foreach(var field in typeof(ControllerButtonNames).GetFields())
        {
            Debug.Log(field.GetValue(newAxisName).ToString());
            newAxisName = $"{playerPrefix}_{field.GetValue(newAxisName).ToString()}";

            inputAxis.name = newAxisName;
            inputAxis.descriptiveName = descriptiveName;
            inputAxis.descriptiveNegativeName = descriptiveNegativeName;
            inputAxis.negativeButton = negativeButton;
            inputAxis.altNegativeButton = altNegativeButton;

            var temp = field.GetValue(newAxisName).ToString();
            if (field.Name.Contains("Button") || field.Name.Contains("Bumper"))
            {
                inputAxis.positiveButton = ControllerButtonNames.GetMapping(temp).Replace("joystick", $"joystick {player}");//*
                inputAxis.altPositiveButton = ControllerButtonNames.GetMapping(temp).Replace("joystick", $"joystick {player}");//*    

                inputAxis.gravity = 1000f;
                inputAxis.dead = 0.001f;
                inputAxis.sensitivity = 1000;
            } else {
                inputAxis.gravity = 0f;
                inputAxis.dead = 0.19f;
                inputAxis.sensitivity = 1f;

                inputAxis.type = 2;
                inputAxis.axis = int.Parse(ControllerButtonNames.GetMapping(temp));
                inputAxis.joyNum = player;
            }
            inputAxis.snap = snap;
            inputAxis.invert = invert;
            AddAxis(inputAxis);
            ClearInputAxis(ref inputAxis);
        }
    }

    private void ClearValues()
    {
        _name = string.Empty;
        descriptiveName = string.Empty;
        descriptiveNegativeName = string.Empty;
        negativeButton = string.Empty;
        positiveButton = string.Empty;
        altNegativeButton = string.Empty;
        altPositiveButton = string.Empty;
        gravity = 0f;
        dead = 0f;
        sensitivity = 0f;
        snap = false;
        invert = false;        
        type = 0;
        axis = 0;
        joyNum = 0;
}

    private void ClearInputAxis(ref InputAxis inputAxis)
    {
        inputAxis.name = string.Empty;
        inputAxis.descriptiveName = string.Empty;
        inputAxis.descriptiveNegativeName = string.Empty;
        inputAxis.negativeButton = string.Empty;
        inputAxis.positiveButton = string.Empty;
        inputAxis.altNegativeButton = string.Empty;
        inputAxis.altPositiveButton = string.Empty;
        inputAxis.gravity = 0f;
        inputAxis.dead = 0f;
        inputAxis.sensitivity = 0f;
        inputAxis.snap = false;
        inputAxis.invert = false;
        inputAxis.type = 0;
        inputAxis.axis = 0;
        inputAxis.joyNum = 0;
    }

    private static void AddAxis(InputAxis axis)
    {
        if (AxisDefined(axis.name))
        {
            return;
        }
        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.arraySize++;
        serializedObject.ApplyModifiedProperties();

        SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

        GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
        GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
        GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
        GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
        GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
        GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
        GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
        GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
        GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
        GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
        GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
        GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
        GetChildProperty(axisProperty, "type").intValue = axis.type;
        GetChildProperty(axisProperty, "axis").intValue = axis.axis;
        GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

        serializedObject.ApplyModifiedProperties();        
    }

    private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            if (child.name == name)
            {
                return child;
            }
        } while (child.Next(false));
        return null;
    }

    private static bool AxisDefined(string axisName)
    {
        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.Next(true);
        axesProperty.Next(true);
        while (axesProperty.Next(false))
        {
            SerializedProperty axis = axesProperty.Copy();
            axis.Next(true);
            if (axis.stringValue == axisName) return true;
        }
        return false;
    }
    #endregion
}

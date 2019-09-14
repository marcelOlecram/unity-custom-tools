using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(Transform))]
[CanEditMultipleObjects]
public class CustomTransform : Editor
{
    #region variables
    // TODO re-arrange variables for order
    // Store selected transform;
    private Transform _transform;
    // Foldouts
    private static bool quaternionFoldout = false;  
    private static bool positionFoldout = false;
    private static bool rotationFoldout = false;
    private static bool scaleFoldout = false;
    // Button properties
    private GUILayoutOption[] buttonOptions = new GUILayoutOption[2] { GUILayout.Width(200f), GUILayout.MinWidth(100f) };
    // Used variables
    private Vector3 minPos, maxPos;
    private bool posConstraintX, posConstraintY, posConstraintZ;
    private bool rotConstraintX, rotConstraintY, rotConstraintZ;
    private float minScale = 1f, maxScale = 2f;

    #endregion

    #region Unity Methods    

    public override void OnInspectorGUI()
    {
        _transform = (Transform)target;
        RealWorldPosition();
        EditorGUILayout.Space();
        StandardTransformInspector();
        ResetTransformDataInspector();
        QuaternionInspector();
        PositionInspector();
        RotationInspector();
        ScaleInpector();
        //base.OnInspectorGUI();
    }
    #endregion

    #region My Methods

    #region inspectors
    /// <summary>
    /// Shows buttons to reset trasnform properties
    /// </summary>
    private void ResetTransformDataInspector()
    {
        bool resetPos, resetRot, resetSca, resetAll;
        EditorGUILayout.BeginHorizontal();
        resetPos = Button("Reset Position");
        resetRot = Button("Reset Rotation");
        resetSca = Button("Reset Scale");
        resetAll = Button("Reset All");
        EditorGUILayout.EndHorizontal();
        Transform[] selectedTransforms = Selection.transforms;
        if (selectedTransforms.Length >= 1)
        {
            foreach (var item in selectedTransforms)
            {
                if (resetPos)
                {
                    item.localPosition = Vector3.zero;
                    continue;
                }
                if (resetRot)
                {
                    item.localEulerAngles = Vector3.zero;
                    continue;
                }
                if (resetAll)
                {
                    item.localScale = Vector3.one;
                    continue;
                }
                if (resetAll)
                {
                    item.localPosition = Vector3.zero;
                    item.localEulerAngles = Vector3.zero;
                    item.localScale = Vector3.one;
                    continue;
                }
            }
        }
    }

    /// <summary>
    /// Shows the real world position
    /// </summary>
    private void RealWorldPosition()
    {
        bool didPositionChange = false;
        Vector3 initialWorldPosition = _transform.localPosition;
        EditorGUI.BeginChangeCheck();
        Vector3 worldPosition = EditorGUILayout.Vector3Field("World Pos", _transform.position);
        if (EditorGUI.EndChangeCheck())
        {
            didPositionChange = true;
            
        }
        if (didPositionChange)
        {
            Undo.RecordObject(_transform, _transform.name);
            _transform.position = worldPosition;
        }
    }

    /// <summary>
    /// Recreates the usual transform inspector
    /// </summary>
    private void StandardTransformInspector()
    {
        bool didPositionChange = false;
        bool didRotationChange = false;
        bool didScaleChange = false;

        // keep current transfor values
        Vector3 initialLocalPosition = _transform.localPosition;
        Vector3 initialLocalEuler = _transform.localEulerAngles;
        Vector3 initialLocalScale = _transform.localScale;

        EditorGUI.BeginChangeCheck();
        Vector3 localPosition = EditorGUILayout.Vector3Field("Position", _transform.localPosition);
        if (EditorGUI.EndChangeCheck())
            didPositionChange = true;

        EditorGUI.BeginChangeCheck();
        Vector3 localEulerAngles = EditorGUILayout.Vector3Field("Euler Rotation", _transform.localEulerAngles);
        if (EditorGUI.EndChangeCheck())
            didRotationChange = true;

        EditorGUI.BeginChangeCheck();
        Vector3 localScale = EditorGUILayout.Vector3Field("Scale", _transform.localScale);
        if (EditorGUI.EndChangeCheck())
            didScaleChange = true;

        // apply changes to selected object
        if (didPositionChange || didRotationChange || didScaleChange)
        {
            Undo.RecordObject(_transform, _transform.name);

            if (didPositionChange)
                _transform.localPosition = localPosition;

            if (didRotationChange)
                _transform.localEulerAngles = localEulerAngles;

            if (didScaleChange)
                _transform.localScale = localScale;

        }

        // apply changes to selected objects        
        Transform[] selectedTransforms = Selection.transforms;
        if (selectedTransforms.Length > 1)
        {
            foreach (var item in selectedTransforms)
            {
                if (didPositionChange || didRotationChange || didScaleChange)
                    Undo.RecordObject(item, item.name);

                if (didPositionChange)
                {
                    item.localPosition = ApplyChangesOnly(
                        item.localPosition, initialLocalPosition, _transform.localPosition);
                }

                if (didRotationChange)
                {
                    item.localEulerAngles = ApplyChangesOnly(
                        item.localEulerAngles, initialLocalEuler, _transform.localEulerAngles);
                }

                if (didScaleChange)
                {
                    item.localScale = ApplyChangesOnly(
                        item.localScale, initialLocalScale, _transform.localScale);
                }

            }
        }
    }

    /// <summary>
    /// Shows Quaternion inspector with possibily to edit directly
    /// </summary>
    private void QuaternionInspector()
    {
        quaternionFoldout = EditorGUILayout.Foldout(quaternionFoldout, "Quaternion Rotation:  " + _transform.localRotation.ToString("F3"));
        if (quaternionFoldout)
        {
            EditorGUI.BeginChangeCheck();
            Vector4 qRotation = EditorGUILayout.Vector4Field("Be Careful", QuaternionToVector4(_transform.localRotation));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_transform, "modify quaternion rotation on " + _transform.name);
                _transform.localRotation = ConvertToQuaternion(qRotation);
            }
        }
    }

    /// <summary>
    /// Shows position tools to randomize position
    /// </summary>
    private void PositionInspector()
    {
        positionFoldout = EditorGUILayout.Foldout(positionFoldout, "Position");
        if (positionFoldout)
        {
            EditorGUILayout.LabelField("Random Position", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Position contraints");
            minPos = EditorGUILayout.Vector3Field("min", minPos, GUILayout.MaxWidth(600f));
            maxPos = EditorGUILayout.Vector3Field("max", maxPos, GUILayout.MaxWidth(600f));
            posConstraintX = EditorGUILayout.ToggleLeft("X", posConstraintX, GUILayout.Width(100));
            posConstraintY = EditorGUILayout.ToggleLeft("Y", posConstraintY, GUILayout.Width(100));
            posConstraintZ = EditorGUILayout.ToggleLeft("Z", posConstraintZ, GUILayout.Width(100));
            Vector3 newPosition;
            if (Button("Random Position"))
            {
                for (int i = 0; i < Selection.transforms.Length; i++)
                {
                    Transform t = Selection.transforms[i];
                    newPosition = t.position;
                    if (!posConstraintX)
                    {
                        newPosition.x = Random.Range(minPos.x, maxPos.x);
                    }
                    if (!posConstraintY)
                    {
                        newPosition.y = Random.Range(minPos.y, maxPos.y);
                    }
                    if (!posConstraintZ)
                    {
                        newPosition.z = Random.Range(minPos.z, maxPos.z);
                    }
                    if (!posConstraintX || !posConstraintY || !posConstraintZ)
                    {
                        Undo.RecordObject(t, "Random position " + t.name);
                        t.position = newPosition;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Shows rotation tools to randomize rotation
    /// </summary>
    private void RotationInspector()
    {
        rotationFoldout = EditorGUILayout.Foldout(rotationFoldout, "Rotation");
        if (rotationFoldout)
        {
            EditorGUILayout.LabelField("Random Rotation", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Axis contraints");
            rotConstraintX = EditorGUILayout.ToggleLeft("X", rotConstraintX, GUILayout.Width(100));
            rotConstraintY = EditorGUILayout.ToggleLeft("Y", rotConstraintY, GUILayout.Width(100));
            rotConstraintZ = EditorGUILayout.ToggleLeft("Z", rotConstraintZ, GUILayout.Width(100));
            Vector3 newRotation;
            if (Button("Random Rotation"))
            {
                for (int i = 0; i < Selection.transforms.Length; i++)
                {
                    Transform t = Selection.transforms[i];
                    newRotation = t.localEulerAngles;
                    if (!rotConstraintX)
                    {
                        newRotation.x = Random.Range(0f, 360f);
                    }
                    if (!rotConstraintY)
                    {
                        newRotation.y = Random.Range(0f, 360f);
                    }
                    if (!rotConstraintZ)
                    {
                        newRotation.z = Random.Range(0f, 360f);
                    }
                    if (!rotConstraintX || !rotConstraintY || !rotConstraintZ)
                    {
                        Undo.RecordObject(t, "Random rotation " + t.name);
                        t.localEulerAngles = newRotation;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Shows scale tools to randomize scale in a uniform way
    /// </summary>
    private void ScaleInpector()
    {
        scaleFoldout = EditorGUILayout.Foldout(scaleFoldout, "Scale");
        if (scaleFoldout)
        {
            EditorGUILayout.LabelField("Random Scale", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Scale contraint");
            minScale = EditorGUILayout.FloatField("Min scale:", minScale);
            maxScale = EditorGUILayout.FloatField("Max scale:", maxScale);
            

            Vector3 newScale;
            if (Button("Random Scale"))
            {
                for (int i = 0; i < Selection.transforms.Length; i++)
                {
                    Transform t = Selection.transforms[i];
                    float scale = Random.Range(minScale, maxScale);
                    newScale = new Vector3(scale, scale, scale);
                    Undo.RecordObject(t, "Random scale " + t.name);
                    t.localScale = newScale;
                }
            }
        }        
    }

    #region help methods

    private Vector3 ApplyChangesOnly(Vector3 toApply, Vector3 initial, Vector3 changed)
    {
        if (!Mathf.Approximately(initial.x, changed.x))
            toApply.x = _transform.localPosition.x;

        if (!Mathf.Approximately(initial.y, changed.y))
            toApply.y = _transform.localPosition.y;

        if (!Mathf.Approximately(initial.z, changed.z))
            toApply.z = _transform.localPosition.z;

        return toApply;
    }

    private Quaternion ConvertToQuaternion(Vector4 v4)
    {
        return new Quaternion(v4.x, v4.y, v4.z, v4.w);
    }

    private Vector4 QuaternionToVector4(Quaternion q)
    {
        return new Vector4(q.x, q.y, q.z, q.w);
    }
    

    private bool Button(string label)
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        bool value = GUILayout.Button(label, buttonOptions);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        return value;
    }
    #endregion

    #endregion

    #endregion
}

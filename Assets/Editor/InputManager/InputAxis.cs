using UnityEditor;
using UnityEngine;

[System.Serializable]
public class InputAxis
{
    #region variables

    public string name;
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

    #endregion    
}

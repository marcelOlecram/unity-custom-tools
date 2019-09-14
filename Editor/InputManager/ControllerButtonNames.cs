using System.Collections.Generic;

public class ControllerButtonNames
{
    #region constantes

    public const string ButtonA = "Button_A";// joystick button 0
    public const string ButtonB = "Button_B";// joystick button 1
    public const string ButtonX = "Button_X";// joystick button 2
    public const string ButtonY = "Button_Y";// joystick button 3

    public const string LeftBumper = "Left_Bumper";// joystick button 4
    public const string RightBumper = "Right_Bumper";// joystick button 5

    public const string ButtonBack = "Button_Back";// joystick button 6
    public const string ButtonStart = "Button_Start";// joystick button 7

    public const string LeftTrigger = "Left_Trigger";// 9th Axis
    public const string RightTrigger = "Right_Trigger";// 10th Axis

    public const string LeftStickButton = "LeftStick_Button";// joystick button 8
    public const string LeftStickHorizontal = "LeftStick_H";//X Axis
    public const string LeftStickVertical = "LeftStick_V";//Y Axis

    public const string RightStickButton = "RightStick_Button";// joystick button 9
    public const string RightStickHorizontal = "RightStick_H";//4th Axis
    public const string RightStickVertical = "RightStick_V";//5th Axis

    public const string DpadHorizontal = "DPAD_H";// 6th Axis
    public const string DpadVertical = "DPAD_V";// 7th Axis

    private static readonly Dictionary<string, string> mapping = new Dictionary<string, string> {
        { ButtonA, "joystick button 0" },
        { ButtonB, "joystick button 1" },
        { ButtonX, "joystick button 2" },
        { ButtonY, "joystick button 3" },
        { LeftBumper, "joystick button 4" },
        { RightBumper, "joystick button 5" },
        { ButtonBack, "joystick button 6" },
        { ButtonStart, "joystick button 7" },
        { LeftStickButton, "joystick button 8" },
        { RightStickButton, "joystick button 9" },

        { LeftStickHorizontal, "0" },
        { LeftStickVertical, "1" },
        { RightStickHorizontal, "3" },//4th axis (Joystick)
        { RightStickVertical, "4" },//5th axis (Joystick)
        { DpadHorizontal, "5" },//6th axis (Joystick)
        { DpadVertical, "6" },//7th axis (Joystick)
        { LeftTrigger, "8" },//9th axis (Joystick)
        { RightTrigger, "9" }//10th axis (Joystick)
    };

    public static string GetMapping(string key) {
        return mapping[key];
    }  
        
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIHelp : MonoBehaviour
{
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 150, 100), "W S - Move");
        GUI.Label(new Rect(10, 30, 150, 100), "A D - Rotate");
        GUI.Label(new Rect(10, 50, 150, 100), "LMB RMB - Lift");
    }
}

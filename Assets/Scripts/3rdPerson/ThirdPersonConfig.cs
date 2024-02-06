using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ThirdPersonConfig", fileName = "ThirdPersonConfig", order = 1)]
public class ThirdPersonConfig : ScriptableObject {
    public float distance = 10.0f;
    public float HorizontalSensivity = 4.0f;
    public float Verticalsensivity = 4.0f;
    public Vector3 Shift;
    public bool IsInvertedVertical = true;
}
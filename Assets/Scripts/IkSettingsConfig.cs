using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/IkSettingsConfig", fileName = "IkSettingsConfig", order = 1)]
public class IkSettingsConfig : ScriptableObject {
    public float MoveSpeed = 4;
    public float RotationSpeed = 1.5f;
    public float MaxStepDistance = 1;
    public float RaycastDistanceParam = 1;
    public float MoveFootSpeed = 5;
}
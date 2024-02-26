using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/IkSettingsConfig", fileName = "IkSettingsConfig", order = 1)]
public class IkSettingsConfig : ScriptableObject {

    public float MaxMoveSpeed = 1;
    public float MaxRbVelocity = 1;
    public float StepForce = 1;
    public float MaxAngleBeforeStoppingCoefficient = 45;
    public float StoppingCoefficient = 2;
    
    public float MoveSpeed = 4;
    public float RotationSpeed = 1.5f;
    public float MaxDiagonalStepDistance = 1;
    public float MaxSideStepDistance  = 1;
    public float RaycastDistanceParam = 1;
    public float MoveFootSpeed = 5;
    public float Amplitude = 0.15f;
}
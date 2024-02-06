using UnityEngine;

public class CameraThirdPerson : MonoBehaviour {
    private const float YMin = -50.0f;
    private const float YMax = 50.0f;

    public Transform lookAt;

    public Transform Player;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    [SerializeField]
    private ThirdPersonConfig _thirdPersonConfig;


    void LateUpdate() {
        currentX += Input.GetAxis("Mouse X") * _thirdPersonConfig.HorizontalSensivity * Time.deltaTime;
        float vertDif = Input.GetAxis("Mouse Y") * _thirdPersonConfig.Verticalsensivity * Time.deltaTime;
        vertDif *= _thirdPersonConfig.IsInvertedVertical ? 1 : -1;
        currentY += vertDif;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -_thirdPersonConfig.distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = _thirdPersonConfig.Shift + lookAt.position + rotation * Direction;

        transform.LookAt(_thirdPersonConfig.Shift + lookAt.position);
    }
}
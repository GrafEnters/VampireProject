using UnityEngine;

namespace _3rdPerson {
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
            if (UIFactory.IsInDialog()) {
                return;
            }
            currentX += Input.GetAxis("Mouse X") * _thirdPersonConfig.HorizontalSensivity * Time.deltaTime;
            float vertDif = Input.GetAxis("Mouse Y") * _thirdPersonConfig.Verticalsensivity * Time.deltaTime;
            vertDif *= _thirdPersonConfig.IsInvertedVertical ? 1 : -1;
            currentY += vertDif;

            currentY = Mathf.Clamp(currentY, YMin, YMax);

            Vector3 direction = new Vector3(0, 0, -_thirdPersonConfig.distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            lookAt.transform.localPosition = _thirdPersonConfig.CameraPointPos;
            transform.position = lookAt.position + rotation * (direction + _thirdPersonConfig.Shift);
            transform.LookAt(_thirdPersonConfig.Shift + lookAt.position);
        }
    }
}
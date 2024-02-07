using UnityEngine;

namespace _3rdPerson {
    public class PlayerThirdPerson : MonoBehaviour {
        CharacterController Controller;

        public float Speed;

        public Transform Cam;

        [SerializeField]
        private ThirdPersonConfig _thirdPersonConfig;

        // Start is called before the first frame update
        void Start() {
            Controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update() {
            float Horizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
            float Vertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

            Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            Movement.y = 0f;

            Controller.Move(Movement);

            if (Movement.magnitude != 0f) {
                transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * _thirdPersonConfig.HorizontalSensivity * Time.deltaTime);

                Quaternion CamRotation = Cam.rotation;
                CamRotation.x = 0f;
                CamRotation.z = 0f;

                transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);
            }
        }
    }
}
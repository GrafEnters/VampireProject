using UnityEngine;

namespace _3rdPerson {
    public class PlayerThirdPerson : MonoBehaviour {
        CharacterController Controller;

        public float Speed;

        public Transform Cam;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private ThirdPersonConfig _thirdPersonConfig;

        // Start is called before the first frame update
        void Start() {
            Controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update() {
            float Horizontal = Input.GetAxis("Horizontal") * Speed;
            float Vertical = Input.GetAxis("Vertical") * Speed;

            Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            Movement.y = 0f;
            _animator.SetFloat("movingSpeed", Movement.magnitude);
            
            Movement *= Time.deltaTime;

            Controller.Move(Movement);

            if (Movement.magnitude != 0f) {
                transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * _thirdPersonConfig.HorizontalSensivity * Time.deltaTime);
            }

            Quaternion CamRotation = Cam.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;
            transform.rotation = Quaternion.Lerp(transform.rotation, CamRotation, 0.1f);

          
        }
    }
}
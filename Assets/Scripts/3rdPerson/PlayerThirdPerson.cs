using UnityEngine;

namespace _3rdPerson {
    public class PlayerThirdPerson : MonoBehaviour {
        Rigidbody Controller;

        public float Speed;

        public Transform Cam;

        [SerializeField]
        private Animator _animator;
        
        [SerializeField]
        private float _maxMoveSpeed;

        [SerializeField]
        private ThirdPersonConfig _thirdPersonConfig;

        // Start is called before the first frame update
        void Start() {
            Controller = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate() {
            float Horizontal = Input.GetAxis("Horizontal") * Speed;
            float Vertical = Input.GetAxis("Vertical") * Speed;

            Vector3 Movement = Cam.transform.right * Horizontal + Cam.transform.forward * Vertical;
            Movement.y = 0f;
            if (Movement.magnitude > _maxMoveSpeed) {
                Movement = Movement.normalized * _maxMoveSpeed;
            }
            
            _animator.SetFloat("movingSpeed", Movement.magnitude / _maxMoveSpeed);
            
            Movement *= Time.deltaTime;

            Controller.MovePosition(transform.position +  Movement);

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
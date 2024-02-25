using UnityEngine;

public class FootMover : MonoBehaviour {
    public Vector3 NewTarget { get; set; }

    [SerializeField]
    public Transform _targetPoint;

    [SerializeField]
    private float _distance;

    [SerializeField]
    private float _countLerpPosition = 0.4f;

    [SerializeField]
    private float _countLerpHeight = 0.5f;

    [SerializeField]
    private IkSettingsConfig _ikSettingsConfig;

    private float _currentTime = 1f;

    public float BodyVelocity = 1;

    private float MaxStepDistance => _distance + _ikSettingsConfig.MaxDiagonalStepDistance;
    private float MoveFootSpeed => _ikSettingsConfig.MoveFootSpeed;

    private void Start() {
        NewTarget = _targetPoint.position;
    }

    private void Update() {
        LayerMask msk = LayerMask.GetMask("Ground");
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, msk)) {
            Vector3 directionStep = hit.point - _targetPoint.position;
            float diagonalDist = directionStep.magnitude;
            float sideDist = Vector3.Distance(Vector3.Project(hit.point, transform.right),
                Vector3.Project(_targetPoint.position, transform.right));
            if ((diagonalDist > MaxStepDistance || sideDist > _ikSettingsConfig.MaxSideStepDistance) && _currentTime >= 1) // Проверяем расстояние между попаданием рейкаста и текущим таргетом.
            {
                _currentTime = 0;
                NewTarget = hit.point; // Задаем в свойство NewTarget  координаты новой точки (Это нужно будет нам далее)
            }

            if (_currentTime < 1) {
                // С помощью линейной интерполяции плавненько переходим из текущей точки, в новую. 
                Vector3 position = _targetPoint.position;
                Vector3 footPosition = Vector3.Lerp(position, NewTarget, _countLerpPosition);
                float curAddedAmplitude = Mathf.Cos(_currentTime * Mathf.PI / 2) * _ikSettingsConfig.Amplitude;
                footPosition.y = Mathf.Lerp(position.y, NewTarget.y, _countLerpHeight) + curAddedAmplitude;
                // Меняем позицию таргета на новую. 
                _targetPoint.position = footPosition;
                _currentTime += Time.deltaTime * MoveFootSpeed * BodyVelocity;
            }
        }
    }
}
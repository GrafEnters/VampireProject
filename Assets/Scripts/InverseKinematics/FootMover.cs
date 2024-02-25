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
    private float _amplutide = 0.4f;

    [SerializeField]
    private IkSettingsConfig _ikSettingsConfig;

    private float _currentTime = 1f;

    private float MaxStepDistance => _distance + _ikSettingsConfig.MaxStepDistance;
    private float MoveFootSpeed => _ikSettingsConfig.MoveFootSpeed;

    private void Start() {
        NewTarget = _targetPoint.position;
    }

    private void Update() {
        LayerMask msk = LayerMask.GetMask("Default", "Ground");
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, msk)) {
            float diagonalDist = Vector3.Distance(hit.point, _targetPoint.position);
            if (diagonalDist > MaxStepDistance && _currentTime >= 1) // Проверяем расстояние между попаданием рейкаста и текущим таргетом.
            {
                _currentTime = 0;
                NewTarget = hit.point; // Задаем в свойство NewTarget  координаты новой точки (Это нужно будет нам далее)
            }

            if (_currentTime < 1) {
                // С помощью линейной интерполяции плавненько переходим из текущей точки, в новую. 
                Vector3 position = _targetPoint.position;
                Vector3 footPosition = Vector3.Lerp(position, NewTarget, _countLerpPosition);
                float curAddedAmplitude = Mathf.Cos(_currentTime * Mathf.PI / 2) * _amplutide;
                footPosition.y = Mathf.Lerp(position.y, NewTarget.y, _countLerpHeight) + curAddedAmplitude;
                // Меняем позицию таргета на новую. 
                _targetPoint.position = footPosition;
                _currentTime += Time.deltaTime * MoveFootSpeed;
            }
        }
    }
}
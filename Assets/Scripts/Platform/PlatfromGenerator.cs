using System.Collections;
using UnityEngine;

public class PlatfromGenerator : MonoBehaviour {
    [SerializeField] private bool _isActive;

    [SerializeField] private int _direction;

    [SerializeField] private float _speed;

    [SerializeField] private string _posTag;

    [SerializeField] private Vector2 _generationReloadRange;
    [SerializeField] private Vector2 _speedRange;
 
    [SerializeField] private GameObject _platformPrefab;

    [SerializeField] private Transform _leftPoint, _rightPoint;

    public int direction { get => _direction; set { _direction = value; } }

    private void Start() {
        _speed = Random.Range(_speedRange.x, _speedRange.y);

        foreach(Transform items in gameObject.transform) {
            if (items.tag != _posTag) {
                Destroy(items.gameObject);
            }
        }
       
        CreatePlatform();
    }

    private void CreatePlatform() {
        Transform startPoint = _direction > 0 ? _leftPoint : _rightPoint;

        Platform newPlatform = Instantiate(_platformPrefab, startPoint.position, Quaternion.identity, gameObject.transform).GetComponent<Platform>();
        newPlatform.direction = _direction;
        newPlatform.speed = _speed;
        newPlatform.isActive = true;

        StartCoroutine(nameof(GenerateNewPlatform));
    }

    private IEnumerator GenerateNewPlatform() {
        yield return new WaitForSeconds(Random.Range(_generationReloadRange.x, _generationReloadRange.y));

        CreatePlatform();
    }
}

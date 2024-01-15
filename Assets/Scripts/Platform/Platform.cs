using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Platform : MonoBehaviour {
    [SerializeField] private bool _isActive;

    [SerializeField] private int _direction;
    
    [SerializeField] private float _speed;

    [SerializeField] private Vector2 _xSizeRange;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] private Material[] _materials;

    public bool isActive { set { _isActive = value; } }
    public int direction { set { _direction = value; } }
    public float speed { set { _speed = value;} }

    private void Start() {
        _rb = gameObject.GetComponent<Rigidbody>();
        gameObject.GetComponent<Renderer>().material = GetRandomMateril();

        float randomSize = Random.Range(_xSizeRange.x, _xSizeRange.y);
        gameObject.transform.localScale = new Vector3() { 
            x = randomSize,
            y = gameObject.transform.localScale.y,
            z = gameObject.transform.localScale.z,
        };
    }

    private void FixedUpdate() {
        if (!_isActive) return;

        _rb.velocity = new Vector3(_speed*_direction, 0, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Wall>(out Wall wall)) {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    private Material GetRandomMateril() {
        int randomIndex = Random.Range(0, _materials.Length);

        return _materials[randomIndex];
    }
}

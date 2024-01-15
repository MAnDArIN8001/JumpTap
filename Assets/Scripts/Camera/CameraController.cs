using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraController : MonoBehaviour {
    [SerializeField] private float _speed;

    [SerializeField] private string _playerTag;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _reserveTarget;

    private void Start() {
        _rb = gameObject.GetComponent<Rigidbody>();
        _target.gameObject.GetComponent<PlayerController>().EOnPlayerDead += ResetTarget;
    }

    private void LateUpdate() {
        if (_target == null)
            return;

        Vector3 direction = (_target.position - gameObject.transform.position) + _offset;

        _rb.velocity = direction * _speed;
        gameObject.transform.LookAt(_target);
    }

    private void ResetTarget() {
        _target = _reserveTarget;
    }
}

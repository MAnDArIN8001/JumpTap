using UnityEngine;

public class Wall : MonoBehaviour {
    [SerializeField] private float _speed;

    [SerializeField] private string _playerTag;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _reserveTarget;

    private void Start() {
        _target.GetComponent<PlayerController>().EOnPlayerDead += ResetTarget;
    }

    private void Update() {
        if(!_target) return;

        Vector3 direction = (_target.position - gameObject.transform.position) + _offset;

        gameObject.transform.Translate(new Vector3(0, 0, direction.z) * _speed * Time.deltaTime);
    }

    private void ResetTarget() {
        _target = _reserveTarget;
    }
}

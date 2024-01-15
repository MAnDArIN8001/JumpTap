using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private bool _onGround;

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveForce;
    [SerializeField] private float _checkDistance;

    [SerializeField] private LayerMask _platformLayer;

    private Rigidbody _rb;

    [SerializeField] private GameObject _deathEffect;

    [SerializeField] private Transform _rayStartPosition;

    public delegate void PlayerDead();

    private event PlayerDead _EOnPlayerDead;
    public event PlayerDead EOnPlayerDead { add { _EOnPlayerDead += value; }  remove { _EOnPlayerDead -= value; } }

    private void Start() {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if (Input.GetMouseButton(0) && _onGround) {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform)) {
            _onGround = true;

            if (Physics.Raycast(_rayStartPosition.position, Vector3.down, _checkDistance, _platformLayer)) {
                gameObject.transform.position = new Vector3() {
                    x = gameObject.transform.position.x,
                    y = gameObject.transform.position.y,
                    z = collision.transform.position.z,
                };

                _rb.velocity = new Vector3() { 
                    x = _rb.velocity.x,
                    y = _rb.velocity.y,
                    z = 0,
                };
            }
        }

        _onGround = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<Barrier>(out Barrier barrier)) {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        Instantiate(_deathEffect, gameObject.transform.position, Quaternion.identity);
        _EOnPlayerDead?.Invoke();
    }

    private void Jump() {
        _onGround = false;

        _rb.velocity = new Vector3(0, _jumpForce, _moveForce);
    }

    private void checkForPlatform() {
    }
}

using UnityEngine;

public class PlatformAudio : MonoBehaviour {
    [SerializeField] private AudioSource _platformAudioSource;

    private void Start() {
        _platformAudioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player)) {
            _platformAudioSource.Play();
        }
    }
}

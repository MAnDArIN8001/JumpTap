using System.Collections.Generic;
using UnityEngine;

public class GeneratorRecreator : MonoBehaviour {
    [SerializeField] private bool _isGenerated;

    [SerializeField] private float _distanceBetweenGenerators;

    [SerializeField] private GameObject _platformGenerator;
    [SerializeField] private GameObject _lastGenerator;
    [SerializeField] private GameObject _parent;

    public delegate void LastGeneratorChanged(GameObject newLastGenerator);

    private event LastGeneratorChanged _EOnLastGeneratorChanged;
    public event LastGeneratorChanged EOnLastGeneratorChanged { 
        add { _EOnLastGeneratorChanged += value; }
        remove { _EOnLastGeneratorChanged -= value; }    
    }

    private void Start() {
        List<GameObject> platforms = new List<GameObject>();

        foreach (Transform child in _parent.transform) {
            platforms.Add(child.gameObject);
        }

        _lastGenerator = platforms[platforms.Count - 1];

        foreach (var platform in platforms) {
            platform.GetComponent<GeneratorRecreator>().EOnLastGeneratorChanged += SetLastObject;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<PlayerController>(out PlayerController player) && !_isGenerated) {
            int direction = gameObject.GetComponent<PlatfromGenerator>().direction;

            Vector3 instPosition = new Vector3() {
                x = _lastGenerator.transform.position.x,
                y = _lastGenerator.transform.position.y,
                z = _lastGenerator.transform.position.z + _distanceBetweenGenerators,
            };

            GameObject generator = Instantiate(_lastGenerator, instPosition, Quaternion.identity, _parent.transform);
            generator.GetComponent<PlatfromGenerator>().direction = -direction;

            _lastGenerator = generator;
            _EOnLastGeneratorChanged?.Invoke(generator);

            _isGenerated = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.TryGetComponent<PlayerController>(out PlayerController player)) {
            Destroy(gameObject);
        }
    }

    private void SetLastObject(GameObject newLastGenerator) {
        _lastGenerator = newLastGenerator;
    }
}

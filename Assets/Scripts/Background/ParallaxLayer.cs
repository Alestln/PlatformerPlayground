using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float _parallaxEffectMultiplier = 0.5f;
    [SerializeField] private Transform _cameraTransform;

    private Vector3 _lastCameraPosition;

    private void LateUpdate()
    {
        Vector3 delta = _cameraTransform.position - _lastCameraPosition;
        transform.position += new Vector3(delta.x * _parallaxEffectMultiplier, delta.y * _parallaxEffectMultiplier, 0);
        _lastCameraPosition = _cameraTransform.position;
    }
}

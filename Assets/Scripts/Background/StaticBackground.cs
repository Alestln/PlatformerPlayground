using UnityEngine;

public class StaticBackground : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;

    private void LateUpdate()
    {
        transform.position = new Vector3(_cameraTransform.position.x, _cameraTransform.position.y, transform.position.z);
    }
}

using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[RequireComponent(typeof(SpriteRenderer))]
public class RepeatingBackground : MonoBehaviour
{
    [SerializeField] private float _parralaxFactor = 0.5f;
    [SerializeField] private bool _repeatHorizontally = true;
    [SerializeField] private bool _repeatVertically = false;
    [SerializeField] private Transform _cameraTransform;

    private Vector2 _spriteSize;
    private Vector3 _lastCameraPosition;

    private void Start()
    {
        _lastCameraPosition = _cameraTransform.position;
        _spriteSize = GetComponent<SpriteRenderer>().bounds.size;
    }

    private void LateUpdate()
    {
        Vector3 delta = _cameraTransform.position = _lastCameraPosition;
        transform.position += new Vector3(delta.x * _parralaxFactor, delta.y * _parralaxFactor, transform.position.z);
        _lastCameraPosition = _cameraTransform.position;

        if (_repeatHorizontally)
        {
            
        }

        if (_repeatVertically)
        {

        }
    }
}

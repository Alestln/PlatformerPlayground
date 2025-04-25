using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[RequireComponent(typeof(SpriteRenderer))]
public class RepeatingBackground : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Range(0f, 1f)][SerializeField] private float parallaxFactor = 0.5f;

    private float spriteWidth;
    private Transform leftBg;
    private Transform centerBg;
    private Transform rightBg;

    private bool _leftCrossed, _rightCrossed;

    private Vector3 prevCamPos;
    private Vector3 parallaxOffset;

    private void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        // Центр — это текущий фон
        centerBg = transform;
        leftBg = transform.parent.Find("Left");
        rightBg = transform.parent.Find("Right");

        // Размещение фонов по порядку
        leftBg.position = centerBg.position - new Vector3(spriteWidth, 0f, 0f);
        rightBg.position = centerBg.position + new Vector3(spriteWidth, 0f, 0f);

        prevCamPos = cam.transform.position;
        parallaxOffset = Vector3.zero;
    }

    private void LateUpdate()
    {
        // ==== ПАРАЛЛАКС ====
        Vector3 camDelta = cam.transform.position - prevCamPos;
        parallaxOffset -= new Vector3(camDelta.x * parallaxFactor, camDelta.y * parallaxFactor, 0f);
        prevCamPos = cam.transform.position;

        // Обновляем позицию корневого (центрального) фона + оффсет
        Vector3 basePos = cam.transform.position + parallaxOffset;
        Vector3 newCenterPos = new Vector3(basePos.x, centerBg.position.y, centerBg.position.z);
        centerBg.position = newCenterPos;

        // Обновляем соседей
        leftBg.position = centerBg.position - new Vector3(spriteWidth, 0f, 0f);
        rightBg.position = centerBg.position + new Vector3(spriteWidth, 0f, 0f);

        float camHalfWidth = cam.orthographicSize * cam.aspect;

        float cameraLeftEdge = cam.transform.position.x - camHalfWidth;
        float cameraRightEdge = cam.transform.position.x + camHalfWidth;

        float rightmostBgEdge = centerBg.position.x + spriteWidth / 2f;
        float leftmostBgEdge = centerBg.position.x - spriteWidth / 2f;

        if (cameraLeftEdge > rightmostBgEdge)
        {
            leftBg.position = rightBg.position + Vector3.right * spriteWidth;

            Transform centerTemp = centerBg;
            Transform leftTemp = leftBg;
            Transform rightTemp = rightBg;

            centerBg = leftTemp;
            rightBg = centerTemp;
            leftBg = rightTemp;
        }
        else if (cameraRightEdge < leftmostBgEdge)
        {
            rightBg.position = leftBg.position - Vector3.right * spriteWidth;

            Transform centerTemp = centerBg;
            Transform leftTemp = leftBg;
            Transform rightTemp = rightBg;

            centerBg = rightTemp;
            leftBg = centerTemp;
            rightBg = leftTemp;
        }
    }
}

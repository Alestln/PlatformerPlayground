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
    private Vector3 prevCamPos;
    private float baseParallaxPosition; // Ѕазова€ позици€ дл€ расчета параллакса

    private void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        // ÷ентр Ч это текущий фон
        centerBg = transform;
        leftBg = transform.parent.Find("Left");
        rightBg = transform.parent.Find("Right");

        // –азмещение фонов по пор€дку
        leftBg.position = centerBg.position - new Vector3(spriteWidth, 0f, 0f);
        rightBg.position = centerBg.position + new Vector3(spriteWidth, 0f, 0f);

        prevCamPos = cam.transform.position;
        baseParallaxPosition = centerBg.position.x - cam.transform.position.x; // –азница между начальными позици€ми
    }

    private void LateUpdate()
    {
        // –ассчитываем смещение камеры с прошлого кадра
        Vector3 camDelta = cam.transform.position - prevCamPos;
        prevCamPos = cam.transform.position;

        // ќбновл€ем базовую позицию параллакса
        baseParallaxPosition -= camDelta.x * parallaxFactor;

        // ¬ычисл€ем новую позицию центрального фона с учетом параллакса
        float newCenterX = cam.transform.position.x + baseParallaxPosition;
        centerBg.position = new Vector3(newCenterX, centerBg.position.y, centerBg.position.z);

        // ќбновл€ем соседей
        leftBg.position = centerBg.position - new Vector3(spriteWidth, 0f, 0f);
        rightBg.position = centerBg.position + new Vector3(spriteWidth, 0f, 0f);

        // ѕровер€ем границы камеры
        float camHalfWidth = cam.orthographicSize * cam.aspect;
        float cameraLeftEdge = cam.transform.position.x - camHalfWidth;
        float cameraRightEdge = cam.transform.position.x + camHalfWidth;

        // »спользуем позицию центрального фона дл€ определени€ границ
        float centerBgRightEdge = centerBg.position.x + spriteWidth / 2f;
        float centerBgLeftEdge = centerBg.position.x - spriteWidth / 2f;

        // »спользуем большой запас, чтобы избежать частых переключений
        float safetyMargin = camHalfWidth * 0.1f;

        //  амера полностью справа от центрального фона
        if (cameraLeftEdge > centerBgRightEdge + safetyMargin)
        {
            // —начала физически переместить левый фон вправо
            leftBg.position = rightBg.position + new Vector3(spriteWidth, 0f, 0f);

            // «атем помен€ть ссылки
            (leftBg, centerBg, rightBg) = (centerBg, rightBg, leftBg);
        }
        //  амера полностью слева от центрального фона
        else if (cameraRightEdge < centerBgLeftEdge - safetyMargin)
        {
            // —начала физически переместить правый фон влево
            rightBg.position = leftBg.position - new Vector3(spriteWidth, 0f, 0f);

            // «атем помен€ть ссылки
            (leftBg, centerBg, rightBg) = (rightBg, leftBg, centerBg);
        }
    }
}

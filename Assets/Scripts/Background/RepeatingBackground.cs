using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

[RequireComponent(typeof(SpriteRenderer))]
public class RepeatingBackground : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Range(0f, 0.5f)][SerializeField] private float offsetPercent = 0.05f;
    [Range(0f, 1f)][SerializeField] private float parallaxFactor = 0.5f;

    private float spriteWidth;
    private Transform leftBg;
    private Transform centerBg;
    private Transform rightBg;

    private Vector3 prevCamPos;
    private Vector3 parallaxOffset;

    private void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        // ����� � ��� ������� ���
        centerBg = transform;
        leftBg = transform.parent.Find("Left");
        rightBg = transform.parent.Find("Right");

        // ���������� ����� �� �������
        leftBg.position = centerBg.position - new Vector3(spriteWidth, 0f, 0f);
        rightBg.position = centerBg.position + new Vector3(spriteWidth, 0f, 0f);

        prevCamPos = cam.transform.position;
        parallaxOffset = Vector3.zero;
    }

    private void LateUpdate()
    {
        // ==== ��������� ====
        Vector3 camDelta = cam.transform.position - prevCamPos;
        parallaxOffset -= new Vector3(camDelta.x * parallaxFactor, camDelta.y * parallaxFactor, 0f);
        prevCamPos = cam.transform.position;

        // ��������� ������� ��������� (������������) ���� + ������
        Vector3 basePos = cam.transform.position + parallaxOffset;
        Vector3 newCenterPos = new Vector3(basePos.x, centerBg.position.y, centerBg.position.z);
        centerBg.position = newCenterPos;

        // ��������� �������
        leftBg.position = centerBg.position - new Vector3(spriteWidth, 0f, 0f);
        rightBg.position = centerBg.position + new Vector3(spriteWidth, 0f, 0f);

        float camHalfWidth = cam.orthographicSize * cam.aspect;
        float offset = camHalfWidth * offsetPercent;

        float cameraLeftEdge = cam.transform.position.x - camHalfWidth;
        float cameraRightEdge = cam.transform.position.x + camHalfWidth;

        float rightmostBgEdge = rightBg.position.x + spriteWidth / 2f;
        float leftmostBgEdge = leftBg.position.x - spriteWidth / 2f;

        // ������ ���� ������ � ���������� ����� ��� �� ������
        if (cameraRightEdge + offset > rightmostBgEdge)
        {
            leftBg.position = rightBg.position + Vector3.right * spriteWidth;
            SwapBackgrounds();
        }
        // ������ ���� ����� � ���������� ������ ��� �� �����
        else if (cameraLeftEdge - offset < leftmostBgEdge)
        {
            rightBg.position = leftBg.position - Vector3.right * spriteWidth;
            SwapBackgrounds();
        }
    }

    private void SwapBackgrounds()
    {
        // ������ ������� �����
        Transform temp = leftBg;
        leftBg = centerBg;
        centerBg = rightBg;
        rightBg = temp;
    }
}

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
    private float baseParallaxPosition; // ������� ������� ��� ������� ����������

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
        baseParallaxPosition = centerBg.position.x - cam.transform.position.x; // ������� ����� ���������� ���������
    }

    private void LateUpdate()
    {
        // ������������ �������� ������ � �������� �����
        Vector3 camDelta = cam.transform.position - prevCamPos;
        prevCamPos = cam.transform.position;

        // ��������� ������� ������� ����������
        baseParallaxPosition -= camDelta.x * parallaxFactor;

        // ��������� ����� ������� ������������ ���� � ������ ����������
        float newCenterX = cam.transform.position.x + baseParallaxPosition;
        centerBg.position = new Vector3(newCenterX, centerBg.position.y, centerBg.position.z);

        // ��������� �������
        leftBg.position = centerBg.position - new Vector3(spriteWidth, 0f, 0f);
        rightBg.position = centerBg.position + new Vector3(spriteWidth, 0f, 0f);

        // ��������� ������� ������
        float camHalfWidth = cam.orthographicSize * cam.aspect;
        float cameraLeftEdge = cam.transform.position.x - camHalfWidth;
        float cameraRightEdge = cam.transform.position.x + camHalfWidth;

        // ���������� ������� ������������ ���� ��� ����������� ������
        float centerBgRightEdge = centerBg.position.x + spriteWidth / 2f;
        float centerBgLeftEdge = centerBg.position.x - spriteWidth / 2f;

        // ���������� ������� �����, ����� �������� ������ ������������
        float safetyMargin = camHalfWidth * 0.1f;

        // ������ ��������� ������ �� ������������ ����
        if (cameraLeftEdge > centerBgRightEdge + safetyMargin)
        {
            // ������� ��������� ����������� ����� ��� ������
            leftBg.position = rightBg.position + new Vector3(spriteWidth, 0f, 0f);

            // ����� �������� ������
            (leftBg, centerBg, rightBg) = (centerBg, rightBg, leftBg);
        }
        // ������ ��������� ����� �� ������������ ����
        else if (cameraRightEdge < centerBgLeftEdge - safetyMargin)
        {
            // ������� ��������� ����������� ������ ��� �����
            rightBg.position = leftBg.position - new Vector3(spriteWidth, 0f, 0f);

            // ����� �������� ������
            (leftBg, centerBg, rightBg) = (rightBg, leftBg, centerBg);
        }
    }
}

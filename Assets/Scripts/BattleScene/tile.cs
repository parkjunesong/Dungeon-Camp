using UnityEngine;
using UnityEngine.Tilemaps;

public class tile : MonoBehaviour
{
    public Tilemap groundTilemap; // Inspector���� ����
    //public Transform unit;        // ���� Transform

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��Ŭ��
        {
            // 1. ���콺 ��ǥ�� ���� ��ǥ�� ��ȯ (z�� ���� �ʼ�)
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = -Camera.main.transform.position.z;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            // 2. ���� ��ǥ �� Ÿ�� ��ǥ
            Vector3Int tilePos = groundTilemap.WorldToCell(mouseWorldPos);
            Debug.Log($"Tile Position: {tilePos}");

            // 3. Ÿ�� �߾��� ���� ��ǥ ���
            Vector3 targetPos = groundTilemap.GetCellCenterWorld(tilePos);

            // 4. ���� �̵�
            //unit.position = targetPos;
        }
    }
}
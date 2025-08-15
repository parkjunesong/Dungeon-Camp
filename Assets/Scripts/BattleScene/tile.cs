using UnityEngine;
using UnityEngine.Tilemaps;

public class tile : MonoBehaviour
{
    public Tilemap groundTilemap; // Inspector에서 지정
    //public Transform unit;        // 유닛 Transform

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 좌클릭
        {
            // 1. 마우스 좌표를 월드 좌표로 변환 (z값 보정 필수)
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = -Camera.main.transform.position.z;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            // 2. 월드 좌표 → 타일 좌표
            Vector3Int tilePos = groundTilemap.WorldToCell(mouseWorldPos);
            Debug.Log($"Tile Position: {tilePos}");

            // 3. 타일 중앙의 월드 좌표 계산
            Vector3 targetPos = groundTilemap.GetCellCenterWorld(tilePos);

            // 4. 유닛 이동
            //unit.position = targetPos;
        }
    }
}
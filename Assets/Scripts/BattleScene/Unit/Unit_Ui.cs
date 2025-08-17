using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Ui : MonoBehaviour
{
    private Canvas mainCanvas;
    private Transform Info;
    private Slider HpBar;
    private Slider ShildBar;
    private RectTransform BuffSlot;

    public void Initialize()
    {
        mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Info = Instantiate(Resources.Load<Transform>("Info"), mainCanvas.transform);

        HpBar = Info.GetChild(0).GetComponent<Slider>();
        ShildBar = Info.GetChild(1).GetComponent<Slider>();
        BuffSlot = Info.GetChild(2).GetComponent<RectTransform>();

        UpdateUiPos();
    }
    public void UpdateUiPos()
    {
        Vector3 worldPos = transform.position + new Vector3(0, -0.5f, 0);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mainCanvas.transform as RectTransform, 
            Camera.main.WorldToScreenPoint(worldPos),
            mainCanvas.renderMode == RenderMode.ScreenSpaceCamera ? Camera.main : null,
            out Vector2 localPos
        );
        Info.localPosition = localPos;
    }

    public void UpdateHPBar(int inGame, int inData)
    {
        HpBar.value = (float)inGame / inData;
    }
    public void UpdateShildBar(int inGame, int inData)
    {
        ShildBar.value = (float)inGame / inData;
    }     
    /*
    public void UpdateBuffUI(List<Buff_Base> activeBuffs)
    {
        GameObject template = BuffSlot.GetChild(0).gameObject;
        int buffCount = activeBuffs.Count;
        int iconIndex = 0;

        for (int i = 1; i < BuffSlot.childCount; i++)  // 템플릿을 제외한 모든 버프
        {
            GameObject icon = BuffSlot.GetChild(i).gameObject;

            if (iconIndex < buffCount)
            {
                Buff_Base buff = activeBuffs[iconIndex];
                icon.name = buff.Name;
                icon.GetComponent<Image>().sprite = buff.Icon;
                icon.GetComponentInChildren<Text>().text = "x" + buff.Level;
                icon.SetActive(true);

                // 위치 재조정
                RectTransform rect = icon.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(-100 + iconIndex * 30f, 0);

                iconIndex++;
            }
            else
            {
                icon.SetActive(false); // 더 이상 필요한 아이콘 없음
            }
        }

        // 필요한 아이콘이 부족한 경우 추가 생성
        while (iconIndex < buffCount)
        {
            Buff_Base buff = activeBuffs[iconIndex];
            GameObject icon = Instantiate(template, BuffSlot);
            icon.name = buff.Name;
            icon.GetComponent<Image>().sprite = buff.Icon;
            icon.GetComponentInChildren<Text>().text = "x" + buff.Level;
            icon.SetActive(true);

            RectTransform rect = icon.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(-100 + iconIndex * 30f, 0);

            iconIndex++;
        }
    }
    */ 
}

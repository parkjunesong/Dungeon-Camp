using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Unit_Ui : MonoBehaviour
{
    private Slider HpBar;
    private Slider ShildBar;
    //private RectTransform BuffSlot;
    private Transform unitCanvas;

    void Awake()
    {
        unitCanvas = transform.GetChild(0);

        HpBar = unitCanvas.GetChild(0).GetComponent<Slider>();
        //ShildBar = unitCanvas.GetChild(0).GetChild(1).GetComponent<Slider>();
        //BuffSlot = unitCanvas.GetChild(0).GetChild(2).GetComponent<RectTransform>();       
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
    public void ShowFloatingText(string text, Color color)
    {
        GameObject obj = FloatingTextPool.Instance.Get();
        obj.transform.SetParent(unitCanvas);

        int activeTextCount = unitCanvas.childCount;
        obj.transform.position = transform.position + new Vector3(0, 150 + (30f * activeTextCount), 0);

        Text txt = obj.GetComponentInChildren<Text>();
        txt.text = text;
        txt.color = color;

        StartCoroutine(AnimateText(obj));
    }
    private IEnumerator AnimateText(GameObject obj, float duration = 1f)
    {
        CanvasGroup group = obj.GetComponent<CanvasGroup>();
        RectTransform rect = obj.GetComponent<RectTransform>();

        Vector3 start = rect.anchoredPosition;
        Vector3 end = start + new Vector3(0, 30, 0);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            rect.anchoredPosition = Vector3.Lerp(start, end, t);
            if (group != null)
                group.alpha = 1f - t;

            yield return null;
        }
        FloatingTextPool.Instance.Return(obj);
    }
}

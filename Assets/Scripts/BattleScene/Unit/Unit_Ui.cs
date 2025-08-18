using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Ui : MonoBehaviour
{
    private Canvas mainCanvas;
    public Transform ActionSelector;
    private Transform Info;
    private Slider HpBar;
    private Slider ShildBar;
    private RectTransform BuffSlot;

    public void Initialize()
    {
        mainCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        ActionSelector = Instantiate(Resources.Load<Transform>("ActionSelectorUi"), mainCanvas.transform);
        ActionSelector.GetChild(0).GetComponent<Button>().onClick.AddListener(transform.GetComponent<Unit_Action>().Move);
        ActionSelector.GetChild(1).GetComponent<Button>().onClick.AddListener(transform.GetComponent<Unit_Action>().NormalAttack);
        ActionSelector.GetChild(2).GetComponent<Button>().onClick.AddListener(transform.GetComponent<Unit_Action>().ClassSkill);
        ActionSelector.gameObject.SetActive(false);

        Info = Instantiate(Resources.Load<Transform>("Info"), mainCanvas.transform);
        HpBar = Info.GetChild(0).GetComponent<Slider>();
        ShildBar = Info.GetChild(1).GetComponent<Slider>();
        BuffSlot = Info.GetChild(2).GetComponent<RectTransform>();

        UpdateUiPos();
    }
    public void UpdateUiPos()
    {
        Info.localPosition = localPosChanger(transform.position + new Vector3(0, -0.5f, 0));
        ActionSelector.localPosition = localPosChanger(transform.position + new Vector3(0, 0.25f, 0));
    }
    public Vector3 localPosChanger(Vector3 pos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mainCanvas.transform as RectTransform,
            Camera.main.WorldToScreenPoint(pos),
            mainCanvas.renderMode == RenderMode.ScreenSpaceCamera ? Camera.main : null,
            out Vector2 localPos
        );
        return localPos;
    }

    public void ShowActionSelector(bool isActivate)
    {
        ActionSelector.gameObject.SetActive(isActivate);
    }
    public void ShowInfo(bool isActivate)
    {
        Info.gameObject.SetActive(isActivate);
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

        for (int i = 1; i < BuffSlot.childCount; i++)  // ���ø��� ������ ��� ����
        {
            GameObject icon = BuffSlot.GetChild(i).gameObject;

            if (iconIndex < buffCount)
            {
                Buff_Base buff = activeBuffs[iconIndex];
                icon.name = buff.Name;
                icon.GetComponent<Image>().sprite = buff.Icon;
                icon.GetComponentInChildren<Text>().text = "x" + buff.Level;
                icon.SetActive(true);

                // ��ġ ������
                RectTransform rect = icon.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(-100 + iconIndex * 30f, 0);

                iconIndex++;
            }
            else
            {
                icon.SetActive(false); // �� �̻� �ʿ��� ������ ����
            }
        }

        // �ʿ��� �������� ������ ��� �߰� ����
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

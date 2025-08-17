using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance { get; private set; }

    private int initialPoolSize = 10;
    public GameObject floatingTextPrefab;
    public Canvas mainCanvas;

    private Queue<GameObject> pool = new();

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(floatingTextPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public GameObject Get()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(floatingTextPrefab, transform);
            return obj;
        }
    }
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        pool.Enqueue(obj);
    }

    public void ShowFloatingText(Transform target, string text, Color color)
    {
        GameObject obj = Get();
        obj.transform.SetParent(mainCanvas.transform);

        Vector3 worldPos = target.position + new Vector3(0, 1, 0);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            mainCanvas.transform as RectTransform,
            Camera.main.WorldToScreenPoint(worldPos),
            mainCanvas.renderMode == RenderMode.ScreenSpaceCamera ? Camera.main : null,
            out Vector2 localPos
        );
        obj.transform.localPosition = localPos;

        Text txt = obj.GetComponentInChildren<Text>();
        txt.text = text;
        txt.color = color;

        StartCoroutine(AnimateText(obj));
    }
    private IEnumerator AnimateText(GameObject obj, float duration = 0.5f)
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
        Return(obj);
    }
}
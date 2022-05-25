using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PickSoulPowerUI : MonoBehaviour
{
    public UnityEvent<int> OnSoulSelect;

    [Header("References")]
    [SerializeField]
    private SoulShiftVariantManagerScriptableObject variants;
    [SerializeField]
    private ScrollRect scroll;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private TextMeshProUGUI description;

    [Header("Snap Settings")]
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float snapSpeed = 8f;
    [SerializeField]
    private float edgePos = -58.65f;
    [SerializeField]
    private float itemSize = 35f;

    private int selectedIndex;
    private Coroutine snappingCoroutine;

    private void Awake()
    {
        Populate();
        SnapToClosest();
    }

    private void Populate()
    {
        for (int i = 0; i < variants.Count; i++)
        {
            Instantiate(prefab, content).GetComponent<SoulShiftVariantDisplayUI>().PopulateFields(variants[i]);
        }
    }

    public void SnapToClosest()
    {
        scroll.StopMovement();
        if (snappingCoroutine != null)
        {
            StopCoroutine(snappingCoroutine);
        }
        StartCoroutine(SnapToPosCoroutine(GetClosestPosToSnap(), snapSpeed));
    }

    public void SnapToIndex(int index)
    {
        scroll.StopMovement();
        if (snappingCoroutine != null)
        {
            StopCoroutine(snappingCoroutine);
        }
        selectedIndex = index;
        StartCoroutine(SnapToPosCoroutine(GetPosOfIndex(index), 1f));
    }

    private IEnumerator SnapToPosCoroutine(float posToSnap, float speed)
    {
        float originalPos = content.anchoredPosition.x;

        float time = 1f;
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime * speed;
            content.anchoredPosition = new Vector2(Mathf.Lerp(originalPos, posToSnap, curve.Evaluate(timer / time)), 0f);
            yield return null;
        }

        content.anchoredPosition = new Vector2(posToSnap, 0f);
        OnSoulSelect?.Invoke(selectedIndex);
        description.text = variants[selectedIndex].description;
    }

    private float GetPosOfIndex(int index)
    {
        return -index * itemSize;
    }

    private float GetClosestPosToSnap()
    {
        float posX = edgePos;

        for (int i = 0; i < content.childCount; i++)
        {
            float distA = posX - content.anchoredPosition.x;
            float distB = (posX - itemSize) - content.anchoredPosition.x;
            if (Mathf.Abs(distA) < Mathf.Abs(distB))
            {
                selectedIndex = i;
                return posX;
            }

            posX -= itemSize;
        }

        return posX;
    }
}
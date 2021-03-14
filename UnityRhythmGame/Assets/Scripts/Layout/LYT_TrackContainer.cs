using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class LYT_TrackContainer : MonoBehaviour {
    private RectTransform rectTransform;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();

        ApplyLayout();
    }

#if UNITY_EDITOR
    private void Update() {
        if (!Application.isPlaying) ApplyLayout();
    }
#endif

    private void OnRectTransformDimensionsChange() {
        if (!rectTransform) return;
        ApplyLayout();
    }

    private void ApplyLayout() {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++) {
            Transform child = transform.GetChild(i);
            RectTransform childRectTransform = child.GetComponent<RectTransform>();
            AspectRatioFitter childAspectRatioFitter = child.GetComponent<AspectRatioFitter>();

            float childHeight = rectTransform.rect.height;
            childRectTransform.sizeDelta = new Vector2(childRectTransform.sizeDelta.x, childHeight);

            float contentWidth = childRectTransform.rect.width * childCount;
            float scaleRatio = rectTransform.rect.width / contentWidth;
            childAspectRatioFitter.aspectRatio = Math.Min(childAspectRatioFitter.aspectRatio * scaleRatio, 0.15f);

            float childWidth = childRectTransform.rect.width;
            float childXPosition = (i - childCount / 2) * childWidth;
            childRectTransform.localPosition = new Vector3(childXPosition, 0, 0);
        }
    }
}

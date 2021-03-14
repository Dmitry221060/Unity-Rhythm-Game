using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LYT_bgTrack : MonoBehaviour {
    public float trackHeight { get; private set; }
    private Track parentTrack;
    private RectTransform rectTransform;

    private void Start() {
        parentTrack = transform.GetComponentInParent<Track>();
        rectTransform = GetComponent<RectTransform>();

        ApplyLayout();
    }

#if UNITY_EDITOR
    private void Update() {
        if (!Application.isPlaying) ApplyLayout();
    }
#endif

    private void OnRectTransformDimensionsChange() {
        if (!parentTrack) return;
        ApplyLayout();
    }

    private void ApplyLayout() {
        float parentTrackHeight = parentTrack.GetComponent<RectTransform>().rect.height;
        float controlsHeight = parentTrack.controls.GetComponent<RectTransform>().rect.height;
        trackHeight = parentTrackHeight - controlsHeight;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, trackHeight);
    }
}

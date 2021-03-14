using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
    private Rigidbody2D rb;
    private GameController gc;
    private Track parentTrack;
    private LYT_bgTrack bgTrack;
    private RectTransform rectTransform;
    private float bgTrackHeight;
    private const int noteSizeInBeats = 1;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        parentTrack = transform.GetComponentInParent<Track>();
        bgTrack = parentTrack.bgTrack.GetComponent<LYT_bgTrack>();
        bgTrackHeight = parentTrack.bgTrack.GetComponent<RectTransform>().rect.height;
        rectTransform = GetComponent<RectTransform>();

        Init();
    }

    private IEnumerator OnRectTransformDimensionsChange() {
        if (!rectTransform) yield return null;

        yield return new WaitForEndOfFrame();
        Init();

        float newBgTrackHeight = parentTrack.bgTrack.GetComponent<RectTransform>().rect.height;
        if (newBgTrackHeight != bgTrackHeight) {
            float heightRatio = newBgTrackHeight / bgTrackHeight;
            Vector3 notePosition = rectTransform.localPosition;
            notePosition.y *= heightRatio;
            rectTransform.localPosition = notePosition;
        }
        bgTrackHeight = newBgTrackHeight;
    }

    private void Init() {
        float noteHeight = bgTrack.trackHeight / GameController.beatsInTrack;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, noteHeight);

        UpdateSpeed(gc.BPS);
    }

    private void UpdateSpeed(int BPS) {
        int positionsPerSecond = noteSizeInBeats + BPS;
        float pixelsPerSecond = positionsPerSecond * rectTransform.rect.height;
        Vector2 velocity = new Vector2(0, -1 * pixelsPerSecond);
        rb.velocity = transform.TransformVector(velocity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Button : MonoBehaviour {
    public bool isPressed {
        get { return isKeyPressed || isClicked; }
    }
    public float pressedScaleRatio = 0.8f;

    private Track parentTrack;
    private RectTransform rectTransform;
    private BoxCollider2D boxCollider;
    private string key;
    private KeyCode keyCode;
    private bool isKeyPressed;
    private bool isClicked;

    private void Start() {
        parentTrack = GetComponentInParent<Track>();
        rectTransform = GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider2D>();

        Init();
    }

    private void Init() {
        key = parentTrack.key;
        keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);

        boxCollider.size = rectTransform.rect.size;

        TMP_Text textComponent = GetComponentInChildren<TMP_Text>();
        textComponent.text = key;
    }

#if UNITY_EDITOR
    private void Update() {
        if (Application.isEditor && !Application.isPlaying) Init();
    }
#endif

    private void FixedUpdate() {
        bool isKeyPressed = Input.GetKey(keyCode);

        if (this.isKeyPressed != isKeyPressed) {
            this.isKeyPressed = isKeyPressed;
            DisplayPressure();
        }
    }

    private void DisplayPressure() {
        if (isPressed) {
            transform.localScale = new Vector3(pressedScaleRatio, pressedScaleRatio, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnMouseDown() {
        isClicked = true;
        DisplayPressure();
    }

    private void OnMouseUp() {
        isClicked = false;
        DisplayPressure();
    }
}

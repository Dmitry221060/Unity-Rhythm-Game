using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class Button : MonoBehaviour {
    private Track parentTrack;
    private string key;
    private KeyCode keyCode;
    private Vector3 scale;
    private bool isKeyPressed;
    private bool isClicked;

    public bool isPressed {
        get { return isKeyPressed || isClicked; }
    }
    public float pressedScaleRatio = 0.8f;

    private void Start() {
        parentTrack = GetComponentInParent<Track>();
        key = parentTrack.key;
        keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), key);
        scale = new Vector3(1f, 1f, 1f);

        Init();
    }

    private void Init() {
        Vector3 parentScale = transform.parent.localScale;
        float scaledX = 1 / parentScale.x;
        float scaledY = parentTrack.scale / parentScale.y;

        scale = new Vector3(scaledX, scaledY, 1f);
        transform.localScale = scale;

        TMP_Text textComponent = GetComponentInChildren<TMP_Text>();
        textComponent.text = key;
    }

#if UNITY_EDITOR
    private void Update() {
        if (Application.isEditor && !Application.isPlaying) Start();
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
            transform.localScale = new Vector3(scale.x * pressedScaleRatio, scale.y * pressedScaleRatio, scale.z);
        } else {
            transform.localScale = scale;
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

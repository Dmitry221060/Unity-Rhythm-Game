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

    public bool isPressed { get; private set; }
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
        bool isPressed = Input.GetKey(keyCode);

        if (this.isPressed != isPressed) {
            DisplayPressure(isPressed);
            this.isPressed = isPressed;
        }
    }

    private void DisplayPressure(bool isPressed) {
        if (isPressed) {
            transform.localScale = new Vector3(scale.x * pressedScaleRatio, scale.y * pressedScaleRatio, scale.z);
        } else {
            transform.localScale = scale;
        }
    }
}

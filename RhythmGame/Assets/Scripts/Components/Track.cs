using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Track : MonoBehaviour {
    public const float trackHeight = 8f;
    public string key = "";
    public float scale = 1.5f;

    private void Start() {
        Init();
    }

    private void Init() {
        Vector3 ownScale = transform.localScale;
        transform.localScale = new Vector3(scale, ownScale.y, ownScale.z);
    }

#if UNITY_EDITOR
    private void Update() {
        if (Application.isEditor && !Application.isPlaying) Start();
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
    private Rigidbody2D rb;
    private GameController gc;
    private const int noteSizeInBeats = 1;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        Init();
    }

    private void Init() {
        float noteHeight = Track.trackHeight / GameController.beatsInTrack;
        transform.localScale = new Vector3(1f, noteHeight, 1f);
    }

    private void Update() {
        int positionsPerSecond = noteSizeInBeats + gc.BPS;
        float velocity = positionsPerSecond * transform.localScale.y;
        rb.velocity = new Vector2(0, -1 * velocity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackClass {
    public string key;

    private GameController gameController;
    private Spawner spawner;
    private Button button;
    private Track track;
    private int hitCursorPosition;
    private int maxNotesOnTrack;
    private List<GameObject> notesOnTrack;

    public TrackClass(Transform trackTransform) {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        spawner = trackTransform.GetComponentInChildren<Spawner>();
        button = trackTransform.GetComponentInChildren<Button>();
        track = trackTransform.GetComponent<Track>();
        hitCursorPosition = gameController.lateHitLoyalty;
        maxNotesOnTrack = GameController.beatsInTrack + hitCursorPosition;
        notesOnTrack = new List<GameObject>(new GameObject[maxNotesOnTrack]);

        key = track.key;
    }

    public void Beat(bool spawnNote) {
        GameObject note = null;
        if (spawnNote) note = Spawn();

        notesOnTrack.Add(note);
        if (notesOnTrack.Count > maxNotesOnTrack)
            NoteMovedOutOfTrack();

        CheckForButtonPress();
    }

    public void CheckForButtonPress() {
        if (button.isPressed) {
            int? noteIndex = FindNoteInHitRange();
            if (noteIndex == null) return;

            NoteHitted((int)noteIndex);
        }
    }

    public void DestroyAllNotes() {
        for (int i = 0; i < notesOnTrack.Count; i++) {
            if (notesOnTrack[i] != null) DestroyNote(i);
        }
    }

    private void DestroyNote(int index) {
        Object.Destroy(notesOnTrack[index]);
        notesOnTrack.RemoveAt(index);
        notesOnTrack.Insert(index, null);
    }

    private GameObject Spawn() {
        return spawner.Spawn();
    }

    private int? FindNoteInHitRange() {
        int hitRange = gameController.earlyHitLoyalty + 1 + gameController.lateHitLoyalty;
        for (int i = 0; i < hitRange; i++) {
            if (notesOnTrack[i] != null) return i;
        }

        return null;
    }

    private void NoteMovedOutOfTrack() {
        GameObject note = notesOnTrack[0];
        if (note != null) {
            gameController.onMissNote();
            Object.Destroy(note);
        }
        notesOnTrack.RemoveAt(0);
    }

    private void NoteHitted(int index) {
        DestroyNote(index);
        gameController.onHitNote();
    }
}

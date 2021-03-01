using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManagerClass {
    public AudioClip audioClip;
    public Sprite backgroundImage;

    private List<object> notes;
    private List<TrackClass> tracks = new List<TrackClass>();
    private GameObject trackContainer;
    private GameController gameController;

    public LevelManagerClass(GameController gameController) {
        this.gameController = gameController;
        trackContainer = GameObject.FindGameObjectWithTag("TrackContainer");

        for (int i = 0; i < trackContainer.transform.childCount; i++) {
            TrackClass track = new TrackClass(trackContainer.transform.GetChild(i));
            tracks.Add(track);
        }
    }

    public void LoadLevel(string levelName) {
        string pathToLevel = $"Levels/{levelName}";

        notes = GetNotesFromFile($"{pathToLevel}/{levelName}.level");
        audioClip = Resources.Load<AudioClip>($"{pathToLevel}/{levelName}");
        backgroundImage = Resources.Load<Sprite>($"{pathToLevel}/bg");
    }

    public void DestroyAllNotes() {
        foreach (TrackClass track in tracks) {
            track.DestroyAllNotes();
        }
    }

    public IEnumerator Beat() {
        int beatsBeforeNextNote = 0;
        for (int i = 0; i < notes.Count; i++) {
            while (beatsBeforeNextNote > 0) {
                beatsBeforeNextNote--;
                foreach (TrackClass track in tracks) {
                    track.Beat(false);
                }
                yield return null;
            }

            object note = notes[i];
            if (note is int) {
                beatsBeforeNextNote = (int)note;
                continue;
            }
            string noteKey = (string)note;

            foreach (TrackClass track in tracks) {
                track.Beat(track.key.ToUpper() == noteKey.ToUpper());
            }
            yield return null;
        }

        for (int i = 0; i < GameController.beatsInTrack; i++) {
            foreach (TrackClass track in tracks) {
                track.Beat(false);
            }
            yield return null;
        }

        gameController.onEndOfTrackMap();
    }

    private List<object> GetNotesFromFile(string path) {
        string json = Resources.Load<TextAsset>(path).text;

        LevelDTO level = JsonUtility.FromJson<LevelDTO>(json);
        List<object> notes = new List<object>();
        foreach (string note in level.notes) {
            int beatsBetweenNotes;
            if (int.TryParse(note, out beatsBetweenNotes)) {
                notes.Add(beatsBetweenNotes);
                continue;
            }
            notes.Add(note);
        }
        notes.Reverse();

        return notes;
    }
}

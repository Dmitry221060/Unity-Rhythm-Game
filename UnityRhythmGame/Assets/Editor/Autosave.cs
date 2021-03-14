using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutoSave {
    static AutoSave() {
        EditorApplication.playModeStateChanged += (PlayModeStateChange state) => {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying) {
                Debug.Log("Auto-saving all open scenes... " + state);
                EditorSceneManager.SaveOpenScenes();
                AssetDatabase.SaveAssets();
            }
        };
    }
}

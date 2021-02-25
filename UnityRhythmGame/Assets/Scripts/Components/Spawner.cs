using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject objectToSpawn;

    public GameObject Spawn() {
        GameObject newObject = Instantiate(objectToSpawn, transform.parent);

        newObject.transform.position = transform.position;

        return newObject;
    }
}

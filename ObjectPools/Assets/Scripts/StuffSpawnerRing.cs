﻿using UnityEngine;
using System.Collections;

public class StuffSpawnerRing : MonoBehaviour {
  public int numberOfSpawners;
  public float radius, tiltAngle;
  public StuffSpawner spawnerPrefab;

  void Awake() {
    for (int i = 0; i <= numberOfSpawners; i++) {
      CreateSpawner(i);
    }
  }

  void CreateSpawner(int index) {
    // Create a new rotater object that goes into position on a 360 deg ring based on index 
    Transform rotater = new GameObject("Rotater").transform;
    rotater.SetParent(transform, false);
    rotater.localRotation = Quaternion.Euler(0f, index * 360f / numberOfSpawners, 0f);

    // Create a spawner at the same position
    StuffSpawner spawner = Instantiate<StuffSpawner>(spawnerPrefab);
    spawner.transform.SetParent(rotater, false);
    spawner.transform.localPosition = new Vector3(0f, 0f, radius);
    spawner.transform.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
  }
}

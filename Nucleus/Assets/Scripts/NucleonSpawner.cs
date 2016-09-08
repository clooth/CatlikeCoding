using UnityEngine;
using System.Collections;

public class NucleonSpawner : MonoBehaviour {
  public float timeBetweenSpawns;
  public float spawnDistance;
  public Nucleon[] nucleonPrefabs;

  float timeSinceLastSpawn;

  void FixedUpdate() {
      Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
      Nucleon spawn = Instantiate<Nucleon>(prefab);
      spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
  }
}

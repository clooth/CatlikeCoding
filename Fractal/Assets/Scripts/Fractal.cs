using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {
  // Mesh and material for the fractal
  public Mesh mesh;
  public Material material;

  // Depth settings for the fractal
  public int maximumDepth;
  private int currentDepth;

	// Use this for initialization
	void Start () {
	  gameObject.AddComponent<MeshFilter>().mesh = mesh;
    gameObject.AddComponent<MeshRenderer>().material = material;
    
    if (currentDepth < maximumDepth) {
      new GameObject("Child")
        .AddComponent<Fractal>()
        .Initialize(this);
    }
	}

  private void Initialize(Fractal parent) {
    mesh = parent.mesh;
    material = parent.material;
    maximumDepth = parent.maximumDepth;
    currentDepth = parent.currentDepth + 1;
    transform.parent = parent.transform;
  }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {
  // Mesh and material for the fractal
  public Mesh mesh;
  public Material material;

  // Depth settings for the fractal
  public int maximumDepth;
  private int currentDepth;

  // The child fractal scale factor
  public float childScale;

  // Directions to extend to
  private static Vector3[] childDirections = {
    Vector3.up,
    Vector3.right,
    Vector3.left,
    Vector3.forward,
    Vector3.back
  };

  // Child extension orientations
  private static Quaternion[] childOrientations = {
    Quaternion.identity,
    Quaternion.Euler(0f, 0f, -90f),
    Quaternion.Euler(0f, 0f, 90f),
    Quaternion.Euler(90f, 0f, 0f),
    Quaternion.Euler(-90f, 0f, 0f)
  };

	// Use this for initialization
	void Start () {
	  gameObject.AddComponent<MeshFilter>().mesh = mesh;
    gameObject.AddComponent<MeshRenderer>().material = material;
    
    // Lerp a color depending on the depth of the fractal
    GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.yellow, (float)currentDepth / maximumDepth);

    if (currentDepth < maximumDepth) {
      StartCoroutine(CreateChildren());
    }
	}

  private IEnumerator CreateChildren() {
    for (int i = 0; i < childDirections.Length; i++) {
      yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
      new GameObject("Child")
        .AddComponent<Fractal>()
        .Initialize(this, i);  
    }
  }

  private void Initialize(Fractal parent, int childIndex) {
    mesh = parent.mesh;
    material = parent.material;
    maximumDepth = parent.maximumDepth;
    currentDepth = parent.currentDepth + 1;
    childScale = parent.childScale;
    transform.parent = parent.transform;
    transform.localScale = Vector3.one * childScale;
    transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
    transform.localRotation = childOrientations[childIndex];
  }
	
	// Update is called once per frame
	void Update () {
	
	}
}

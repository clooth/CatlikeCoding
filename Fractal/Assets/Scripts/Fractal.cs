using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {
  // Mesh and material for the fractal
  public Mesh mesh;
  public Material material;
  private Material[,] materials;

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
    Vector3.back,
    Vector3.down
  };

  // Child extension orientations
  private static Quaternion[] childOrientations = {
    Quaternion.identity,
    Quaternion.Euler(0f, 0f, -90f),
    Quaternion.Euler(0f, 0f, 90f),
    Quaternion.Euler(90f, 0f, 0f),
    Quaternion.Euler(-90f, 0f, 0f),
    Quaternion.Euler(0f, 90f, 0f)
  };

  // Rotation speed settings
  public float maximumRotationSpeed;
  private float rotationSpeed;

  // Twisting settings
  public float maximumTwist;

	// Use this for initialization
	void Start () {
    // Initialize materials if needed
    if (materials == null) {
      InitializeMaterials();
    }

    // Initialize mesh renderer
	  gameObject.AddComponent<MeshFilter>().mesh = mesh;
    gameObject.AddComponent<MeshRenderer>().material = materials[currentDepth, Random.Range(0, 2)];

    rotationSpeed = Random.Range(-maximumRotationSpeed, maximumRotationSpeed);
    transform.Rotate(Random.Range(-maximumTwist, maximumTwist), 0f, 0f);

    // Create children if we haven't reached maximum depth yet
    if (currentDepth < maximumDepth) {
      StartCoroutine(CreateChildren());
    }
	}

  private IEnumerator CreateChildren() {
    for (int i = 0; i < childDirections.Length - 1; i++) {
      yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
      new GameObject("Child")
        .AddComponent<Fractal>()
        .Initialize(this, i);
    }
  }

  private void InitializeMaterials() {
    materials = new Material[maximumDepth + 1, 2];
    for (int i = 0; i <= maximumDepth; i++) {
      float t = i / (maximumDepth - 1f);
      t *= t;
      materials[i, 0] = new Material(material);
      materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
      materials[i, 1] = new Material(material);
      materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
    }
    materials[maximumDepth, 0].color = Color.magenta;
    materials[maximumDepth, 1].color = Color.red;
  }

  private void Initialize(Fractal parent, int childIndex) {
    mesh = parent.mesh;
    materials = parent.materials;
    maximumDepth = parent.maximumDepth;
    currentDepth = parent.currentDepth + 1;
    childScale = parent.childScale;
    transform.parent = parent.transform;
    transform.localScale = Vector3.one * childScale;
    transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
    transform.localRotation = childOrientations[childIndex];
    maximumRotationSpeed = parent.maximumRotationSpeed;
    maximumTwist = parent.maximumTwist;
  }
	
	// Update is called once per frame
	void Update () {
    transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);	
	}
}

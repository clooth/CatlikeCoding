using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour {
    // Size of the grid
    public int width, height;

    // Vertices used in the grid
    private Vector3[] vertices;

    // Called when the component is being loaded
    private void Awake() {
        StartCoroutine(Generate());
    }

    // Generate the grid vertices
    private IEnumerator Generate() {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        vertices = new Vector3[(width + 1) * (height + 1)];
        for (int i = 0, y = 0; y <= height; y++) {
            for (int x = 0; x <= width; x++, i++) {
                vertices[i] = new Vector3(x, y);
                yield return wait;
            }
        }
    }

    // Debug gizmos
    private void OnDrawGizmos() {
        // Skip in edit mode
        if (vertices == null) { return; }

        // Draw a black sphere on every vertice
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}

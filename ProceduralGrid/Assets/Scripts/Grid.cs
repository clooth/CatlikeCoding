using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour {
    // Size of the grid
    public int width, height;

    // Vertices used in the grid
    private Vector3[] vertices;

    // The mesh for rendering the grid
    private Mesh mesh;

    // Called when the component is being loaded
    private void Awake() {
        Generate();
    }

    // Generate the grid vertices
    private void Generate() {
        // Create an empty mesh for the grid
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Grid";

        // Generate grid vertices
        vertices = new Vector3[(width + 1) * (height + 1)];
        for (int i = 0, y = 0; y <= height; y++) {
            for (int x = 0; x <= width; x++, i++) {
                vertices[i] = new Vector3(x, y);
            }
        }

        // Set mesh vertices to be the generated ones
        mesh.vertices = vertices;

        // Generate triangles for the grid cells
        int[] triangles = new int[width * height * 6];
        for (int ti = 0, vi = 0, y = 0; y < height; y++, vi++) {
            for (int x = 0; x < width; x++, ti += 6, vi++) {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + width + 1;
                triangles[ti + 5] = vi + width + 2;
            }
        }
        mesh.triangles = triangles;
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

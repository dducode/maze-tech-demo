using UnityEngine;



public class MeshDrawer : MonoBehaviour {

    [SerializeField]
    private MeshFilter target;

    [SerializeField]
    private UserInterface userInterface;

    private Material m_material;


    private void Awake () {
        m_material = new Material(Shader.Find("Standard")) {
            color = Color.black
        };

        userInterface.OnWireframeChanged += value => enabled = value;
    }


    private void OnPostRender () {
        Mesh mesh = target.sharedMesh;

        GL.PushMatrix();
        m_material.SetPass(0);
        GL.Begin(GL.LINES);

        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;

        for (var i = 0; i < triangles.Length - 2; i += 3) {
            GL.Vertex(vertices[triangles[i]]);
            GL.Vertex(vertices[triangles[i + 1]]);

            GL.Vertex(vertices[triangles[i + 1]]);
            GL.Vertex(vertices[triangles[i + 2]]);

            GL.Vertex(vertices[triangles[i + 2]]);
            GL.Vertex(vertices[triangles[i]]);
        }

        GL.End();
        GL.PopMatrix();
    }

}
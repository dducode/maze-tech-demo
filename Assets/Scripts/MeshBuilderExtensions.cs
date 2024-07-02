using UnityEngine;



public static class MeshBuilderExtensions {

    /// <summary>
    /// Adds a XY oriented plane
    /// </summary>
    public static void AddPlane (this MeshBuilder meshBuilder, Vector3 center, Quaternion rotation, Vector2 size) {
        float x = size.x * 0.5f;
        float y = size.y * 0.5f;

        int prevCount = meshBuilder.VerticesCount;

        meshBuilder.AddVertices(
            center + rotation * new Vector3(-x, y, 0),
            center + rotation * new Vector3(x, y, 0),
            center + rotation * new Vector3(-x, -y, 0),
            center + rotation * new Vector3(x, -y, 0)
        );

        meshBuilder.AddIndexes(
            prevCount, prevCount + 1, prevCount + 2, prevCount + 2, prevCount + 1, prevCount + 3
        );
    }


    public static void AddCube (this MeshBuilder meshBuilder, Vector3 center, Vector3 size) {
        meshBuilder.AddCube(center, Quaternion.identity, size);
    }


    /// <summary>
    /// Adds a cube without bottom plane
    /// </summary>
    public static void AddCube (this MeshBuilder meshBuilder, Vector3 center, Quaternion rotation, Vector3 size) {
        float x = size.x * 0.5f;
        float y = size.y * 0.5f;
        float z = size.z * 0.5f;

        int prevCount = meshBuilder.VerticesCount;

        // front plane
        meshBuilder.AddVertices(
            center + rotation * new Vector3(x, y, z),
            center + rotation * new Vector3(-x, y, z),
            center + rotation * new Vector3(x, -y, z),
            center + rotation * new Vector3(-x, -y, z)
        );

        // back plane
        meshBuilder.AddVertices(
            center + rotation * new Vector3(-x, y, -z),
            center + rotation * new Vector3(x, y, -z),
            center + rotation * new Vector3(-x, -y, -z),
            center + rotation * new Vector3(x, -y, -z)
        );

        // right plane
        meshBuilder.AddVertices(
            center + rotation * new Vector3(x, y, -z),
            center + rotation * new Vector3(x, y, z),
            center + rotation * new Vector3(x, -y, -z),
            center + rotation * new Vector3(x, -y, z)
        );

        // left plane
        meshBuilder.AddVertices(
            center + rotation * new Vector3(-x, y, z),
            center + rotation * new Vector3(-x, y, -z),
            center + rotation * new Vector3(-x, -y, z),
            center + rotation * new Vector3(-x, -y, -z)
        );

        // top plane
        meshBuilder.AddVertices(
            center + rotation * new Vector3(-x, y, z),
            center + rotation * new Vector3(x, y, z),
            center + rotation * new Vector3(-x, y, -z),
            center + rotation * new Vector3(x, y, -z)
        );

        for (int i = prevCount; i < meshBuilder.VerticesCount; i += 4)
            meshBuilder.AddIndexes(i, i + 1, i + 2, i + 2, i + 1, i + 3);
    }

}
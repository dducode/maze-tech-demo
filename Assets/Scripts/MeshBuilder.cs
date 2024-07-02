﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;



public class MeshBuilder {

    public int VerticesCount { get; private set; }
    public int IndexesCount { get; private set; }

    private readonly Vector3[] m_vertices;
    private readonly int[] m_indexes;

    private readonly List<SubMeshDescriptor> m_descriptors = new();
    private int m_indexStart;
    private bool m_begunFormation;


    public MeshBuilder (int cubes, int planes, int vertices = 0, int indexes = 0) {
        const int cubeSides = 5; // do not add a bottom plane
        const int planeVertices = 4;
        const int planeIndexes = 6;
        const int cubeVertices = cubeSides * planeVertices;
        const int cubeIndexes = cubeSides * planeIndexes;

        int totalVertices = cubes * cubeVertices + planes * planeVertices + vertices;
        int totalIndexes = cubes * cubeIndexes + planes * planeIndexes + indexes;

        m_vertices = new Vector3[totalVertices];
        m_indexes = new int[totalIndexes];
    }


    public void AddVertices (Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
        m_vertices[VerticesCount++] = v1;
        m_vertices[VerticesCount++] = v2;
        m_vertices[VerticesCount++] = v3;
        m_vertices[VerticesCount++] = v4;
    }


    public void AddIndexes (int t1, int t2, int t3, int t4, int t5, int t6) {
        m_indexes[IndexesCount++] = t1;
        m_indexes[IndexesCount++] = t2;
        m_indexes[IndexesCount++] = t3;
        m_indexes[IndexesCount++] = t4;
        m_indexes[IndexesCount++] = t5;
        m_indexes[IndexesCount++] = t6;
    }


    public void BeginFormSubMesh () {
        if (m_begunFormation) {
            throw new InvalidOperationException(
                $"Impossible to start a new formation. Finish the previous formation by calling the '{nameof(EndFormSubMesh)}' method and try again"
            );
        }

        m_begunFormation = true;
        m_indexStart = IndexesCount;
    }


    public void EndFormSubMesh () {
        if (!m_begunFormation) {
            throw new InvalidOperationException(
                $"Impossible to finish a formation. Begin the formation by calling the '{nameof(BeginFormSubMesh)}' method and try again"
            );
        }

        m_descriptors.Add(new SubMeshDescriptor {
            indexStart = m_indexStart,
            indexCount = IndexesCount - m_indexStart
        });

        m_begunFormation = false;
    }


    public Mesh BuildMesh (string name = null) {
        if (string.IsNullOrEmpty(name))
            name = Guid.NewGuid().ToString();

        var mesh = new Mesh {
            name = name,
            vertices = m_vertices,
            triangles = m_indexes
        };

        if (m_descriptors.Count > 0)
            mesh.SetSubMeshes(m_descriptors, 0, m_descriptors.Count);
        mesh.RecalculateNormals();

        return mesh;
    }

}
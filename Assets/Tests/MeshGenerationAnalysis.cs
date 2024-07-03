using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using Debug = UnityEngine.Debug;



public class MeshGenerationAnalysis {

    private static bool[] m_isOptimized = {true, false};


    [Test]
    public void GenerateMeshes ([ValueSource(nameof(m_isOptimized))] bool isOptimized) {
        var mapGenerator = new MapGenerator();
        var mazeGenerator = new MazeGenerator();
        var size = new Vector2Int(20, 20);

        var watch = new Stopwatch();
        watch.Start();

        for (var i = 0; i < 1000; i++)
            mazeGenerator.GenerateMesh(mapGenerator.GenerateMap(size, i), isOptimized);

        watch.Stop();
        Debug.Log($"Elapsed ms: {watch.ElapsedMilliseconds}");
    }

}
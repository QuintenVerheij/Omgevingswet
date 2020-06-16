using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class MatObject
{
    public Color color;
    public int subMeshStartIndex;
    public int subMeshVertexCount;
    public MatObject(Material material, int subMeshStartIndex, int subMeshVertexCount) {
        Debug.Log("[MatObject init] material shader: " + material.shader.name);
        color = material.color;
        this.subMeshStartIndex = subMeshStartIndex;
        this.subMeshVertexCount = subMeshVertexCount;
    }

    public Material ToMaterial(Material defaultMaterial) { //overwrite defaultMaterial properties with the values of MatObject
        Material mat = new Material(defaultMaterial);
        mat.color = color;
        return mat;
    }

    public static Material[] ToMaterials(MatObject[] matObjects, Material defaultMaterial) {
        Material[] materials = new Material[matObjects.Length];
        for(int i = 0; i < matObjects.Length; i++) {
            materials[i] = matObjects[i].ToMaterial(defaultMaterial);
        }
        return materials;
    }

    public static int[] GetSubMeshStartIndices(MatObject[] matObjects) {
        int[] indices = new int[matObjects.Length];
        for(int i = 0; i < matObjects.Length; i++) {
            indices[i] = matObjects[i].subMeshStartIndex;
        }
        return indices;
    }

    public static int[] GetSubMeshVertexCounts(MatObject[] matObjects) {
        int[] indices = new int[matObjects.Length];
        for (int i = 0; i < matObjects.Length; i++) {
            indices[i] = matObjects[i].subMeshVertexCount;
        }
        return indices;
    }

    public static MatObject[] FromArrays(Material[] materials, int[] subMeshIndices, int[] subMeshVertexCounts) {
        MatObject[] matObjects = new MatObject[materials.Length];
        for (int i = 0; i < materials.Length; i++) {
            matObjects[i] = new MatObject(materials[i], subMeshIndices[i], subMeshVertexCounts[i]);
        }
        return matObjects;
    }
}

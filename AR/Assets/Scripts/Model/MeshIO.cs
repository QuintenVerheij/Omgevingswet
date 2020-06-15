using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public static class MeshIO
{
    [System.Serializable]
    public class MatFileContent {
        public MatObject[] materials;
        public MatFileContent(MatObject[] materials) {
            this.materials = materials;
        }
    }

    public struct MergeOutput {
        public Mesh mesh;
        public Material[] materials;
    }

    //private static FastObjImporter importer = new FastObjImporter();
    private static Dummiesman.OBJLoader importer = new Dummiesman.OBJLoader();

    public static GameObject ImportFromFile(string localPath, Material defaultMaterial) {
        string objPath = Application.persistentDataPath + "/" + localPath + ".obj";
        string matPath = Application.persistentDataPath + "/" + localPath + ".mat.json";
        Debug.Log("Importing obj file from '" + objPath + "'");
        Debug.Log("Importing mat (json) file from '" + matPath + "'");
        GameObject obj = importer.Load(objPath);
        obj.transform.localScale = new Vector3(1,1,-1);

        var materialOutput = ImportMaterials(matPath, defaultMaterial);
        obj.GetComponentInChildren<Renderer>().materials = materialOutput.materials;
        Mesh mesh = obj.GetComponentInChildren<MeshFilter>().mesh;
        mesh.subMeshCount = materialOutput.startIndices.Length;
        //Debug.Log($"[IMPORT] material count: {materialOutput.materials.Length}, startindices count: {materialOutput.startIndices.Length}, submesh count: {mesh.subMeshCount}");
        for(int i = 0; i < materialOutput.startIndices.Length; i++) {
            SubMeshDescriptor submesh = new SubMeshDescriptor();
            submesh.indexStart = materialOutput.startIndices[i];
            submesh.indexCount = materialOutput.vertexCounts[i];
            mesh.SetSubMesh(i, submesh);
        }
        return obj;
    }

    public static void ExportToFile(string localPath, Transform rootObject) {
        string objPath = Application.persistentDataPath + "/" + localPath + ".obj";
        string matPath = Application.persistentDataPath + "/" + localPath + ".mat.json";

        Debug.Log("Exporting obj file from '" + objPath + "'");
        Debug.Log("Exporting mat (json) file from '" + matPath + "'");

        var output = MergeMeshes(rootObject);
        string content = ObjExporterScript.MeshToString(output.mesh, rootObject);
        output.mesh.subMeshCount = output.materials.Length;
        string headerInfo = "o Custom_Mesh\n";
        File.WriteAllText(objPath, headerInfo + content);
        ExportMaterials(matPath, output.materials, output.mesh);
    }

    public struct ImportMaterialOutput {
        public Material[] materials;
        public int[] startIndices;
        public int[] vertexCounts;
    }

    private static ImportMaterialOutput ImportMaterials(string fullPath, Material defaultMaterial) {
        string content = File.ReadAllText(fullPath);
        MatFileContent objContent = JsonUtility.FromJson<MatFileContent>(content);

        return new ImportMaterialOutput {
            materials = MatObject.ToMaterials(objContent.materials, defaultMaterial),
            startIndices = MatObject.GetSubMeshStartIndices(objContent.materials),
            vertexCounts = MatObject.GetSubMeshVertexCounts(objContent.materials)
        };
    }

    private static void ExportMaterials(string fullPath, Material[] materials, Mesh mergedMesh) {
        int[] startIndices = new int[materials.Length];
        int[] vertexCounts = new int[materials.Length];

        for(int i = 0; i < materials.Length; i++) {
            startIndices[i] = mergedMesh.GetSubMesh(i).indexStart;
            vertexCounts[i] = mergedMesh.GetSubMesh(i).indexCount;
        }
        MatFileContent objContent = new MatFileContent(MatObject.FromArrays(materials,startIndices,vertexCounts));
        string content = JsonUtility.ToJson(objContent, true);
        
        File.WriteAllText(fullPath, content);
    }

    private static MergeOutput MergeMeshes(Transform rootObject) {
        Mesh newMesh = new Mesh();

        Renderer[] renderers = rootObject.GetComponentsInChildren<Renderer>();
        CombineInstance[] combineInstances = new CombineInstance[renderers.Length];
        List<Material> materials = new List<Material>();

        for (int i = 0; i < renderers.Length; i++) {
            combineInstances[i] = new CombineInstance();
            Matrix4x4 mat1 = Matrix4x4.TRS(
                renderers[i].transform.position,
                renderers[i].transform.rotation,
                renderers[i].transform.lossyScale
            );
            Matrix4x4 mat2 = rootObject.worldToLocalMatrix;
            combineInstances[i].transform = mat2 * mat1;
            
            //combineInstances[i].transform = Matrix4x4.TRS(renderers[i].transform.localPosition, renderers[i].transform.localRotation, renderers[i].transform.localScale);

            combineInstances[i].mesh = renderers[i].gameObject.GetComponent<MeshFilter>().mesh;
            materials.AddRange(renderers[i].materials);
        }
        newMesh.CombineMeshes(combineInstances, false, true);
        Debug.Log($"mesh vertex count: {newMesh.vertices.Length}");


        return new MergeOutput { mesh = newMesh, materials = materials.ToArray() };
    }

    public static string[] GetListOfObjFileNames() {
        string path = Application.persistentDataPath + "/";
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles();
        List<string> objFiles = new List<string>();
        foreach (var file in fileInfo) {
            if(file.Extension == ".obj") {
                string filename = file.Name.Replace(".obj", "");
                objFiles.Add(filename);
            }
        }

        return objFiles.ToArray();
    }

    public static void DeleteAllCustomModels() {
        string path = Application.persistentDataPath + "/";
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles();
        foreach (var file in fileInfo) {
            if (file.Extension == ".obj" || file.Extension == ".json") {
                File.Delete(file.FullName);
            }
        }
        //ObjectCreationHandler.Instance.LoadAllCustomModels(); //refresh the list of models so it only contains build in models
    }
}

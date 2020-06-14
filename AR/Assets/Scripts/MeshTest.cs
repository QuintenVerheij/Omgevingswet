using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MeshTest : MonoBehaviour {
    public Material defaultMaterial;

    //public GameObject importPrefab;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Clear() {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void Save() {
        if (transform.childCount > 0) {
            string path = $"Custom Model-{System.DateTime.Now.ToString()}";

            Transform child = transform.GetChild(0);
            //Renderer renderer = child.GetComponent<Renderer>();
            //MeshFilter filter = child.GetComponent<MeshFilter>();
            MeshFilter filter = gameObject.GetComponentInChildren<MeshFilter>();
            Renderer renderer = gameObject.GetComponentInChildren<Renderer>();

            MeshIO.ExportToFile(path, child);
        }
    }

    public void Load(string name) {
        Clear();
        string path = name;
        if (path != null && path != "") {
            GameObject obj = MeshIO.ImportFromFile(path, defaultMaterial);
            obj.transform.SetParent(transform);
            //GameObject instance = Instantiate(importPrefab, transform);
            //GameObject instance = Instantiate(obj, transform);
            //instance.GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}
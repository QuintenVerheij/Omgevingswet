using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Model class is used for every object a user can place in the ar scene
public class Model : MonoBehaviour{
	public GameObject highlightPrefab; //prefab that contains the outline shader
	public List<GameObject> highlightInstances = new List<GameObject>(); //instances of highlight prefab. In order to access them they need to be stored in a list

	private void CreateHighlightObject(MeshFilter parentMeshFilter) { 
		GameObject copy = Instantiate(highlightPrefab, parentMeshFilter.transform); //creates an instance of the highlight prefab
		int submeshCount = parentMeshFilter.sharedMesh.subMeshCount;
		copy.GetComponent<MeshFilter>().sharedMesh = parentMeshFilter.sharedMesh;

		MeshRenderer copy_renderer = copy.GetComponent<MeshRenderer>();
		MeshRenderer prefab_renderer = highlightPrefab.GetComponent<MeshRenderer>();

		//A mesh could contain multiple submeshes and each submesh has its own material slot
		//Array size of materials must be equal to the amount of submeshes
		Material highlightMaterial = prefab_renderer.sharedMaterial;
		Material[] materials = new Material[submeshCount];
		for(int i = 0; i < submeshCount; i++) { //set all items in the array to the same highlight material
			materials[i] = highlightMaterial;
		}
		copy_renderer.sharedMaterials = materials;
		copy.SetActive(false); //Make the highlight object invisible
		highlightInstances.Add(copy);
	}
	private void Start() {
		MeshFilter filter = GetComponent<MeshFilter>(); //A MeshFilter is just a component that contains a mesh
		if (filter) {
			CreateHighlightObject(filter);
		}
		MeshFilter[] childFilters = GetComponentsInChildren<MeshFilter>(); //meshes in child objects also needs to highlighted
		foreach(var childFilter in childFilters) {
			CreateHighlightObject(childFilter);
		}
	}

	public void SetHighlight(bool active) { //Turn the outlines on/off
		foreach (var instance in highlightInstances) {
			instance.SetActive(active);
		}
	}
}

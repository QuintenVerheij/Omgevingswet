using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour{
	/*private float _sensitivity = 1f;
	private Vector3 _mouseReference;
	private Vector3 _mouseOffset;
	private Vector3 _rotation = Vector3.zero;
	private bool _isSelected;*/
	
	public GameObject highlightPrefab;
	public List<GameObject> highlightInstances = new List<GameObject>();

	private void CreateHighlightObject(MeshFilter parentMeshFilter) {
		GameObject copy = Instantiate(highlightPrefab, parentMeshFilter.transform);
		int submeshCount = parentMeshFilter.sharedMesh.subMeshCount;
		copy.GetComponent<MeshFilter>().sharedMesh = parentMeshFilter.sharedMesh;

		MeshRenderer copy_renderer = copy.GetComponent<MeshRenderer>();
		MeshRenderer prefab_renderer = highlightPrefab.GetComponent<MeshRenderer>();

		Material highlightMaterial = prefab_renderer.sharedMaterial;
		Material[] materials = new Material[submeshCount];
		for(int i = 0; i < submeshCount; i++) {
			materials[i] = highlightMaterial;
		}
		copy_renderer.sharedMaterials = materials;
		copy.SetActive(false);
		highlightInstances.Add(copy);
	}
	private void Start() {
		MeshFilter filter = GetComponent<MeshFilter>();
		if (filter) {
			CreateHighlightObject(filter);
		}
		MeshFilter[] childFilters = GetComponentsInChildren<MeshFilter>();
		foreach(var childFilter in childFilters) {
			CreateHighlightObject(childFilter);
		}
	}

	void Update() {
		/*if (_isSelected) {
			// offset
			_mouseOffset = (Input.mousePosition - _mouseReference);

			// apply rotation
			_rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

			// rotate
			gameObject.transform.Rotate(_rotation);

			// store new mouse position
			_mouseReference = Input.mousePosition;
		}*/
	}

	void OnMouseDown() {
		// rotating flag
		//_isSelected = true;

		// store mouse position
		//_mouseReference = Input.mousePosition;
		//CSharpscaling.ScaleTransform = this.transform;
	}

	void OnMouseUp() {
		// rotating flag
		//_isSelected = false;
	}

	public void SetHighlight(bool active) {
		foreach (var instance in highlightInstances) {
			instance.SetActive(active);
		}
	}
}

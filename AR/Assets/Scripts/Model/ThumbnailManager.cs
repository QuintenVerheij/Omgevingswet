using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbnailManager : MonoBehaviour{
    public struct PrefabData {
        public float distance;
        public Quaternion orientation;
        public GameObject prefab;
        public RenderTexture texture;
        public Camera thumbnailCamera;
    }
    private Queue<PrefabData> thumbnailQueue = new Queue<PrefabData>();
    public static ThumbnailManager Instance { get; private set; }
    private void Awake() {
        Instance = this;
        StartCoroutine(ThumbnailCoroutine());
    }

    public RenderTexture CreateThumbnail(Camera thumbnailCamera, int width, int height, float distance, Quaternion orientation, GameObject prefab) {
        RenderTexture renderTexture = new RenderTexture(width, height, 1);
        PrefabData data = new PrefabData { distance = distance, orientation = orientation, prefab = prefab, texture = renderTexture, thumbnailCamera = thumbnailCamera };
        thumbnailQueue.Enqueue(data);
        return renderTexture;
    }

    private void SetLayerRecursive(GameObject obj, int layer) {
        obj.layer = layer;
        foreach (Transform child in obj.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

    public IEnumerator ThumbnailCoroutine() {
        while (true) {
            if (thumbnailQueue.Count > 0) {
                PrefabData data = thumbnailQueue.Dequeue();

                Vector3 position = data.thumbnailCamera.transform.position + data.thumbnailCamera.transform.forward * data.distance;
                data.prefab.SetActive(true);
                GameObject thumbnailObject = Instantiate(data.prefab, data.thumbnailCamera.transform);
                thumbnailObject.transform.position = position;
                thumbnailObject.transform.localRotation = data.orientation;

                SetLayerRecursive(thumbnailObject, data.thumbnailCamera.gameObject.layer);
                data.thumbnailCamera.targetTexture = data.texture;
                data.thumbnailCamera.Render();

                data.thumbnailCamera.targetTexture = null;
                Destroy(thumbnailObject);
            }
            yield return 0; //wait for another frame
        }
    }
}

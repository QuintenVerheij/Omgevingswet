using System.Collections;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Examples;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class SpawnOnMap : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    string[] _locationStrings;
    Vector2d[] _locations;

    [SerializeField]
    float _spawnScale = 100f;

    [SerializeField]
    GameObject _markerPrefab;

    List<GameObject> _spawnedObjects;

    void Start()
    {

        StartCoroutine(LoadMarkers());

    }

    IEnumerator LoadMarkers()
    {
        bool isLoggedin = false;
        string url;
        currentUser cu = new currentUser();
        UnityWebRequest www;
        if (cu.readUserId() != -1)
        {
            byte[] jsonToSend = new AuthorizedAction<int>(new AuthorizationToken(cu.readToken()), cu.readUserId()).toJsonRaw();
            url = AppStartup.APIURL + ":8080/model/read";
            www = new UnityWebRequest(url, "POST");
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            isLoggedin = true;

        }
        else
        {
            url = AppStartup.APIURL + ":8080/model/public/read";
            www = new UnityWebRequest(url, "GET");
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        }

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Response text if WebRequest gives an error
            //responseText.text = www.error;
            Debug.Log(www.error);
        }
        else
        {
            //Response to Json
            string response = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
            Debug.Log(response);
            ModelOutputPreview[] markers;
            if (cu.readUserId() != -1)
            {
                MessageWithItem<ModelOutputPreview[]> messageWithItems = JsonConvert.DeserializeObject<MessageWithItem<ModelOutputPreview[]>>(response);
                markers = messageWithItems.item;
            }
            else
            {
                markers = JsonConvert.DeserializeObject<ModelOutputPreview[]>(response);
            }
            _locations = new Vector2d[markers.Length];
            _spawnedObjects = new List<GameObject>();
            //Populate marker objects with data
            for (int i = 0; i < markers.Length; i++)
            {
                //Get location from markerdata
                ModelOutputPreview marker = markers[i];
                _locations[i] = new Vector2d(marker.longitude, marker.latitude);
                var instance = Instantiate(_markerPrefab);

                instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                //Set markerdata in OnClickScript for marker
                ClickMarker CM = instance.GetComponent("ClickMarker") as ClickMarker;
                CM.SetMarkerData(marker);
                

                _spawnedObjects.Add(instance);
            }

        }
    }

    private void Update()
    {
        if (_spawnedObjects != null)
        {
            int count = _spawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _spawnedObjects[i];
                var location = _locations[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }
    }
}
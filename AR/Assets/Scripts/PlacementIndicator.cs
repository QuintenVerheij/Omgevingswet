using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{

    private ARRaycastManager rayManager;
    private GameObject visual;

    private TrackableType trackableType = TrackableType.All;
    private int trackableTypeIndex = 0;
    public UnityEngine.UI.Button nextTypeButton;

     void Start()
    {
        // get the component
        rayManager = FindObjectOfType<ARRaycastManager>();
        visual = transform.GetChild(0).gameObject;

        // hide the placement visual

        visual.SetActive(false);

    }

    void Update()
    {
        // shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        rayManager.Raycast(ray, hits, trackableType);
        
        // if we hit AR Plane, Update the postion and rotation

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            //transform.rotation = hits[0].pose.rotation;

            if (!visual.activeInHierarchy)
                visual.SetActive(true);
        }
        else {
            visual.SetActive(false);
        }
    }

    public void NextTrackableType() {
        trackableTypeIndex += 1;
        if(trackableTypeIndex >= 10) {
            trackableTypeIndex = 0;
        }
        string text = "";
        switch (trackableTypeIndex) {
            case 0: trackableType = TrackableType.All; text = "All"; break;
            case 1: trackableType = TrackableType.Face; text = "Face"; break;
            case 2: trackableType = TrackableType.FeaturePoint; text = "FeaturePoint"; break;
            case 3: trackableType = TrackableType.Image; text = "Image"; break;
            case 4: trackableType = TrackableType.None; text = "None"; break;
            case 5: trackableType = TrackableType.PlaneEstimated; text = "PlaneEstimated"; break;
            case 6: trackableType = TrackableType.Planes; text = "Planes"; break;
            case 7: trackableType = TrackableType.PlaneWithinBounds; text = "PlaneWithinBounds"; break;
            case 8: trackableType = TrackableType.PlaneWithinInfinity; text = "PlaneWithinInfinity"; break;
            case 9: trackableType = TrackableType.PlaneWithinPolygon; text = "PlaneWithinPolygon"; break;
        }

        nextTypeButton.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = text;
    }
}

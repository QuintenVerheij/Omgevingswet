using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Experimental.XR;
using System;

public class AR_tap_to_place_object : MonoBehaviour
{
    public GameObject placementIndicator;
    private ARSessionOrigin arOrigin;
    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    // Start is called before the first frame update
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void CalculatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            placementPose.rotation.SetLookRotation(new Vector3(1,0,1).normalized);
            Vector3 planeCenter = Vector3.zero;
            foreach (ARPlane plane in arPlaneManager.trackables)
            {
                if (planeCenter == Vector3.zero)
                {
                    planeCenter = plane.center;
                    placementPose.position = plane.center;
                }
                else
                {
                    if (plane.center.x + plane.center.y + plane.center.z < planeCenter.x + planeCenter.y + planeCenter.z)
                    {
                        planeCenter = plane.center;
                        placementPose.position = plane.center;
                    }
                }
            }
        }
    }

}

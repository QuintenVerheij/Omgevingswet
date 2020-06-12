using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.Location;

public class BackToCurrentLocation : MonoBehaviour
{
    public AbstractMap map;
    public AbstractLocationProvider location;
        
    public virtual void GetCurrentLocation()
    {
        map.UpdateMap(location.CurrentLocation.LatitudeLongitude,15);
    }
}

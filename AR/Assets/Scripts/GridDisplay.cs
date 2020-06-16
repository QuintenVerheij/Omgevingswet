using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour
{
    public Renderer grid_xz;
    public bool DisplayGrids { get; private set; }
    
    public void SetGridDisplay(bool active) {
        grid_xz.enabled = active;
        DisplayGrids = active;
    }

    public void Toggle() {
        SetGridDisplay(!DisplayGrids);
    }
}

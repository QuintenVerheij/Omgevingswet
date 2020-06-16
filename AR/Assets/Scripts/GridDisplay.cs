using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour
{
    public Renderer grid_xz;
    public Renderer grid_xy;
    public Renderer grid_yz;
    public bool DisplayGrids { get; private set; }
    
    public void SetGridDisplay(bool active) {
        grid_xz.enabled = active;
        grid_xy.enabled = active;
        grid_yz.enabled = active;
        DisplayGrids = active;
    }

    public void Toggle() {
        SetGridDisplay(!DisplayGrids);
    }
}

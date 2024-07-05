using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public void low()
    {
        QualitySettings.SetQualityLevel(0);
        Debug.Log("11");
    }
    public void med()
    {
        QualitySettings.SetQualityLevel(1);
    }
    public void high()
    {
        QualitySettings.SetQualityLevel(2);
    }
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneMan : MonoBehaviour
{
    public GameObject OptionsMen;
    public GameObject MainMen;

    public void OpenMain()
    {
        MainMen.SetActive(false);
    }

    public void OpenOptions()
    {
        OptionsMen.SetActive(true);
    }
}

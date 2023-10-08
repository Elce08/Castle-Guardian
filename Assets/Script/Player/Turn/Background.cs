using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    GameObject Background1;
    private void Awake()
    {
        int proto = Random.Range(0, 4);
        switch (proto)
        {
            case 0:
                proto = 0;
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 1:
                proto = 1;
                transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                proto = 2;
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 3:
                proto = 3;
                transform.GetChild(3).gameObject.SetActive(true);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingPoint : MonoBehaviour
{
    DefenceManager defenceManager;

    private void Awake()
    {
        defenceManager = FindObjectOfType<DefenceManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            defenceManager.LosingPoint.Invoke();
            other.gameObject.SetActive(false);
        }
    }
}

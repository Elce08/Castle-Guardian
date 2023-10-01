using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TIleState
    {
        Empty = 0,
        PlayerOn
    }

    TIleState state = TIleState.Empty;

    public TIleState State
    {
        get => state;
        set
        {
            if(state != value)
            {
                state = value;
                switch (state)
                {
                    case TIleState.PlayerOn:
                        this.gameObject.tag = "Untagged";
                        break;
                    case TIleState.Empty:
                        this.gameObject.tag = "Ground";
                        break;
                }
            }
        }
    }
    private void Awake()
    {
    }

    private void Start()
    {
        // select.selected = LightUp;
    }

    private void LightUp(bool light)
    {
        if(light)
        {

        }
        else
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            State = TIleState.Empty;
        }
    }
}

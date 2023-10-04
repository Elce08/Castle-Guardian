using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public enum TIleState
    {
        Empty = 0,
        PlayerOn
    }

    public TIleState state = TIleState.Empty;

    public TIleState State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;
                switch (state)
                {
                    case TIleState.PlayerOn:
                        gameObject.tag = "Untagged";
                        break;
                    case TIleState.Empty:
                        gameObject.tag = "Ground";
                        break;
                }
            }
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

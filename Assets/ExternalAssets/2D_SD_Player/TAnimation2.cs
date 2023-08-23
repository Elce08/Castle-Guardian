using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TAnimation2 : MonoBehaviour {

    Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            if (anim.GetBool("isAttack") == false)
            {
                anim.SetBool("isAttack", true);
                anim.SetBool("isIdle", false);
            }

        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (anim.GetBool("isHited") == false)
            {
                anim.SetBool("isHited", true);
                anim.SetBool("isIdle", false);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (anim.GetBool("isDied") == false)
            {
                anim.SetBool("isDied", true);
                anim.SetBool("isIdle", false);
            }
        }
        else if (Input.GetKey(KeyCode.F))
        {
            if (anim.GetBool("isRun") == false)
            {
                anim.SetBool("isRun", true);
                anim.SetBool("isIdle", false);
            }
        }
        else if (Input.GetKey(KeyCode.G))
        {
            if (anim.GetBool("isVictory") == false)
            {
                anim.SetBool("isVictory", true);
                anim.SetBool("isIdle", false);
            }
        }
        else
        {
            if (anim.GetBool("isIdle") == false)
            {
                anim.SetBool("isHited", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isAttack", false);
                anim.SetBool("isVictory", false);
                anim.SetBool("isDied", false);
                anim.SetBool("isRun", false);
            }
        }
    }
}

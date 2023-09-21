using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public GameManager gameManager;

    Image item1Image;
    Image item2Image;

    TextMeshProUGUI resultText;

    PlayerInputActions inputActions;

    GameObject Actor;

    Animator actorAnim;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        item1Image = child.GetComponent<Image>();
        child = transform.GetChild(1);
        item2Image = child.GetComponent<Image>();
        resultText = GetComponentInChildren<TextMeshProUGUI>();
        inputActions = new();
    }

    private void Start()
    {
        SpawnActor();
    }

    private void OnEnable()
    {
        inputActions.ResultUI.Enable();
        inputActions.ResultUI.Space.performed += Space_performed;
        inputActions.ResultUI.LeftClick.performed += LeftClick_performed;
    }

    private void OnDisable()
    {
        inputActions.ResultUI.Space.performed -= Space_performed;
        inputActions.ResultUI.LeftClick.performed -= LeftClick_performed;
        inputActions.ResultUI.Disable();
    }

    void SpawnActor()
    {
        int random = Random.Range(0, 3);
        PlayerType actor = PlayerType.None;
        switch (random)
        {
            case 0:
                actor = gameManager.player1Type;
                break;
            case 1:
                actor = gameManager.player2Type;
                break;
            case 2:
                actor = gameManager.player3Type;
                break;
        }
        switch (actor)
        {
            case PlayerType.None:
                Actor = GameObject.Instantiate(gameManager.playerTypePrefabs[0], Vector3.zero, Quaternion.identity);
                break;
            case PlayerType.Archor:
                Actor = GameObject.Instantiate(gameManager.playerTypePrefabs[1], Vector3.zero, Quaternion.identity);
                break;
            case PlayerType.Archor_LongBow:
                Actor = GameObject.Instantiate(gameManager.playerTypePrefabs[2], Vector3.zero, Quaternion.identity);
                break;
            case PlayerType.Gunner:
                Actor = GameObject.Instantiate(gameManager.playerTypePrefabs[3], Vector3.zero, Quaternion.identity);
                break;
            case PlayerType.Soldier_LongSword:
                Actor = GameObject.Instantiate(gameManager.playerTypePrefabs[4], Vector3.zero, Quaternion.identity);
                break;
            case PlayerType.Soldier_ShortSword:
                Actor = GameObject.Instantiate(gameManager.playerTypePrefabs[5], Vector3.zero, Quaternion.identity);
                break;
            case PlayerType.Warrior_Hammer:
                Actor = GameObject.Instantiate(gameManager.playerTypePrefabs[6], Vector3.zero, Quaternion.identity);
                break;
        }
        actorAnim = Actor.GetComponent<Animator>();
        Actor.layer = 3;
    }

    private void LeftClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if(hit.collider == null)
        {
            // ¸¶À»·Î º¹±Í
        }
    }

    private void Space_performed(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        // ¸¶À»·Î º¹±Í
    }

    public void Win()
    {
        resultText.text = "Victory";
        actorAnim.SetBool("isIdle", false);
        actorAnim.SetBool("isVictory", true);
    }

    public void Lose()
    {
        resultText.text = "Lose";
        actorAnim.SetBool("isIdle", false);
        actorAnim.SetBool("isDied", true);
        item1Image.gameObject.SetActive(false);
        item2Image.gameObject.SetActive(false);
    }
}

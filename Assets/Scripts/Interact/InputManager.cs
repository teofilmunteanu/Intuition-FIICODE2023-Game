using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.FirstPersonActions firstPerson;

    // Start is called before the first frame update
    void Awake()
    {  
        playerInput = new PlayerInput();
        firstPerson = playerInput.FirstPerson;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        firstPerson.Enable();
    }
    private void OnDisable()
    {
        firstPerson.Disable();
    }
}

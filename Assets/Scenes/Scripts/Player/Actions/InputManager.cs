using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum actions
{
    MOVE,
    ATTACK,
    JUMP,

}
public enum AnimatorParams
{
    Moving,
    Attacking,
    Jumping,
    AttackCounter,
    ForceTransition,
    IsHeavyAttacking,
    IsInCombo
}
public class InputManager : MonoBehaviour
{
    public bool Move = false;
    public bool jump = false;
    public int attackCounter = 0;
    public int getKey = 0;
    public int getKeyDown = 0;
    public int getKeyUp = 0;



    public static InputManager instance = null;

// hold to Interact 
    private void Update()
    {   // returns true WHILE BUTTON is being PRESSED
        // returns true ON the FIRST FRAME the button is PRESSED
        // returns true ON the FIRST FRAME the button is RELEASED
        if (Input.GetKey(KeyCode.Return))
            getKey++;
        if (Input.GetKeyDown(KeyCode.Return))
            getKeyDown++;
        if (Input.GetKeyUp(KeyCode.Return))
            getKeyUp++;
        Debug.Log($"Pressed: GetKey: {getKey} {Input.GetKey(KeyCode.Return)}|| GetKeyDown: {getKeyDown} {Input.GetKeyDown(KeyCode.Return)} || GetKeyUp: {Input.GetKeyUp(KeyCode.Return)}{getKeyUp}");
    }
    private void Awake()
    {
        instance = this;
    }



}

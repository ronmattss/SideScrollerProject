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
    IsInCombo
}
public class InputManager : MonoBehaviour
{
    public bool Move = false;
    public bool jump = false;
    public  int attackCounter = 0;
    


    public static InputManager instance = null;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }


}

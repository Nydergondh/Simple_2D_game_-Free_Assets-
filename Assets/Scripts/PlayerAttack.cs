using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private AttackTypes currentAttack;
    private Animator playerAnim; //same anim as the PlayerMOvement Script, so be carefull;

    // Start is called before the first frame update
    void Start()
    {
        currentAttack = AttackTypes.None;

    }

    // Update is called once per frame
    void Update() {
        CheckAttack();

    }

    private void CheckAttack() {
        if (Input.GetButton("Attack_A") || Input.GetButton("Attack_B")) {
            SetAttack();
        }
    }

    private void SetAttack() {
        if (Input.GetButton("Attack_A")) {
            currentAttack = AttackTypes.Attack_A;
        }
        else if (Input.GetButton("Attack_B")) {
            currentAttack = AttackTypes.Attack_B;
        }
    }

    public AttackTypes GetAttackType() {
        return currentAttack;
    }

    public void EndAttack() {
        currentAttack = AttackTypes.None;
    }
}

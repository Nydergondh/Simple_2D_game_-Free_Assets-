using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] AttackTypes currentAttack;
    [SerializeField] int atkTypeAnim; // important variable to set the animator attack types that are ints to not add more bools to the animator
    private Animator playerAnim; //same anim as the PlayerMOvement Script, so be carefull;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        atkTypeAnim = 0;
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
    //add input in the Input Manager to this commands to work
    private void SetAttack() {
        if (Input.GetButton("Attack_A")) {

            atkTypeAnim = 1;
            playerAnim.SetInteger("AttackType", atkTypeAnim);
            playerAnim.SetBool("Attacking", true);

            currentAttack = AttackTypes.Attack_A;

        }
        else if (Input.GetButton("Attack_B")) {

            atkTypeAnim = 2;
            playerAnim.SetInteger("AttackType", atkTypeAnim);
            playerAnim.SetBool("Attacking", true);
            
            currentAttack = AttackTypes.Attack_B;
        }

    }

    public AttackTypes GetAttackType() {
        return currentAttack;
    }

    public void EndAttack() {

        playerAnim.SetBool("Attacking", false);
        playerAnim.SetBool("CrouchJump", false);
        atkTypeAnim = 0;
        currentAttack = AttackTypes.None;

    }
}

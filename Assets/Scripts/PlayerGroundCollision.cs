﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollision : MonoBehaviour
{

    private CapsuleCollider2D feetCollider;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        feetCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //if is jumping and touch the ground
        if (feetCollider.IsTouchingLayers(groundLayer) && PlayerMovement.player.jumping) {
            PlayerMovement.player.CrouchAfterJump();
            PlayerMovement.player.isTouchingGround = true;
        }
        //if is not jumping, but is falling and touch the ground
        else if (feetCollider.IsTouchingLayers(groundLayer) && !PlayerMovement.player.jumping) {
            if (PlayerMovement.player.GetYSpeed() < PlayerMovement.player.deltaYMinimum) {
                PlayerMovement.player.CrouchAfterJump();
            }
            PlayerMovement.player.isTouchingGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            if (!feetCollider.IsTouchingLayers(groundLayer)) {
                PlayerMovement.player.Fall();
            }
        }
    }

}

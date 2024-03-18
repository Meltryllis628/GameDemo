﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Game {
    public enum AttackStage {
        BeforeAttack,
        Attack,
        AfterAttack,
    }
    public class AttackState : BaseActionState {
        private AttackStage stage;
        private Vector2 dir;
        private int frame;
        private bool comboFlag;
        private float maxSpeed;
        public AttackState(PlayerController controller) : base(EActionState.Attack, controller) {
        }

        public override IEnumerator Coroutine() {
            yield return player.AttackFrames1 * (1 / 60f);
            stage = AttackStage.Attack;
            maxSpeed = player.AttackFrames2MaxSpeed;
            yield return player.AttackFrames2 * (1 / 60f);
            stage = AttackStage.AfterAttack;
            maxSpeed = player.AttackFrames3MaxSpeed;
            comboFlag = true;
            yield return player.AttackFrames3 * (1 / 60f);
            player.AttackEnd();
            player.SetState((int)EActionState.Normal);
        }

        public override bool IsCoroutine() {
            return true;
        }

        public override void OnBegin() {
            stage = AttackStage.BeforeAttack;
            maxSpeed = player.AttackFrames1MaxSpeed;
            dir = player.LastAim;
            comboFlag = false;
            frame = 0;
        }

        public override void OnEnd() {
        }

        public override EActionState Update(float deltaTime) {
            state = EActionState.Attack;
            player.PlayerVelocity.x = maxSpeed * dir.x;
            player.PlayerVelocity.y = maxSpeed * dir.y;
            if (comboFlag && player.CanDash) {
                return player.Dash();
            }
            if (comboFlag && player.CanAttack) {
                //Debug.Log("Combo " + frame);
                return EActionState.Combo;
            }
            return state;
        }
    }
}
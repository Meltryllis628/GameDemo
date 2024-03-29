﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Game {
    public class DashState : BaseActionState {
        private Vector2 DashDir;
        private Vector2 beforeDashSpeed;

        public DashState(PlayerController controller) : base(EActionState.Dash, controller) {
        }

        public override IEnumerator Coroutine() {
            yield return null;
            //
            var dir = player.LastAim;
            var newSpeed = dir * Constants.DashSpeed;
            //惯性
            if (Math.Sign(beforeDashSpeed.x) == Math.Sign(newSpeed.x) && Math.Abs(beforeDashSpeed.x) > Math.Abs(newSpeed.x)) {
                newSpeed.x = beforeDashSpeed.x;
            }
            player.PlayerVelocity = newSpeed;

            DashDir = dir;
            if (DashDir.y == 0 && player.DashStartedOnGround) {
                player.PlayAnimation("Roll");
            }
            else
            {
                 player.PlayAnimation("Dash");
            }
            if (DashDir.x != 0)
                player.Facing = (Facings)Math.Sign(DashDir.x);
            player.PlayDashEffect(player.Position, DashDir);
           

            yield return Constants.DashTime;
            if (DashDir.y >= 0) {
                player.PlayerVelocity = DashDir * Constants.EndDashSpeed;
            }
            if (player.PlayerVelocity.y > 0) {
                player.PlayerVelocity.y *= Constants.EndDashUpMult;
            }

            player.SetState((int)EActionState.Normal);
        }

        public override bool IsCoroutine() {
            return true;
        }

        public override void OnBegin() {
            player.launched = false;
            // 冻结帧
            player.WallSlideTimer = Constants.WallSlideTime;
            player.DashCooldownTimer = Constants.DashCooldown;
            player.DashRefillCooldownTimer = Constants.DashRefillCooldown;
            beforeDashSpeed = player.PlayerVelocity;
            player.PlayerVelocity = Vector2.zero;
            DashDir = Vector2.zero;
            player.DashStartedOnGround = player.OnGround;
            
        }

        public override void OnEnd() {
        }

        public override EActionState Update(float deltaTime) {
            state = EActionState.Dash;
            player.invinsibleTimer = 0.5f;
            #region 尾迹
            #endregion
            #region super
            #endregion

            return state;
        }
    }
}
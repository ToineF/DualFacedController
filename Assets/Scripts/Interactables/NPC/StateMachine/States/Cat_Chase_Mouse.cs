using System.Collections;
using Cattac.Character.Visuals;
using DG.Tweening;
using UnityEngine;

namespace Cattac.Interactables.NPC.States
{
    /// <summary>
    /// The cat chases the mouse, attacking it
    /// </summary>
    public class Cat_Chase_Mouse : Cat_State
    {
        public override void StartState(Cat_StateManager stateManager)
        {
            stateManager.StartCoroutine(dd(stateManager));
        }

        private IEnumerator dd(Cat_StateManager stateManager)
        {
            var target = stateManager.Seeker.Target.gameObject;
            var animators = GameObject.FindObjectsByType < HeadAnimator>(FindObjectsSortMode.InstanceID);
            GameObject animator = null;
            GameObject otherHead = null;
            foreach (var VARIABLE in animators)
            {
                if (VARIABLE.CharacterHead.gameObject == target)
                {
                    animator =  VARIABLE.gameObject;
                }
                else
                {
                    otherHead = VARIABLE.gameObject;
                }
            }
            var targetPos = target.transform.position;
            targetPos.y = stateManager.transform.position.y;
            Debug.Log(target.gameObject.name + "at position" + targetPos, target);
            var mover = stateManager.Mover.gameObject.transform.GetChild(0);
            var moverOriginalPos = mover.transform.localPosition;

            animator?.SetActive(false);
            target?.SetActive(false);
            mover.transform.DOMove(targetPos, stateManager.Data.ChaseMouseDuration).SetEase(stateManager.Data.ChaseMouseEase);

            yield return new WaitForSeconds(1f);
            mover.transform.DOLocalMove(moverOriginalPos, stateManager.Data.ChaseMouseDuration).SetEase(stateManager.Data.ChaseMouseEase);
            yield return new WaitForSeconds(1f);
            target.transform.parent.position = otherHead.transform.position;
            animator?.SetActive(true);
            target?.SetActive(true);

        }

        /*public override void UpdateState(Cat_StateManager stateManager)
        {
            stateManager.Mover.UpdateInternal(stateManager.Seeker.Target);
        }*/
        public override void OnNewTarget(Cat_StateManager stateManager, IDetectable newTarget, IDetectable oldTarget)
        {
            var newState = newTarget.OnDetect(stateManager);
            if (newState == this) return;
            stateManager.SwitchState(newState);
        }

        public override void OnTargetLost(Cat_StateManager stateManager, IDetectable oldTarget)
        {
            stateManager.SwitchState(stateManager.Cat_State_Idle);
        }
    }
}
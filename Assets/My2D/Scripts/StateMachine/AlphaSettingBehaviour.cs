using UnityEngine;

namespace My2D {
    public class AlphaSettingBehaviour : StateMachineBehaviour {
        #region Variables
        //참조
        SpriteRenderer sr;
        GameObject deadTarget;
        //지연 시간
        public float delayTime = 1f;
        float delayTimer = 0f;
        //점멸 효과
        public float fadeTime = 1f;
        float fadeTimer = 0f;
        //new Color 반복 선언을 피하기 위함 + 최초 색상 저장
        Color startColor;
        Color newColor;
        #endregion
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            sr = animator.GetComponent<SpriteRenderer>();
            startColor = sr.color;
            deadTarget = animator.gameObject;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            //지연 시간이 지난 후에 점차 투명하게 하고 게임오브젝트 파괴
            if (delayTime > delayTimer) delayTimer += Time.deltaTime;
            else if (fadeTime > fadeTimer) {
                    fadeTimer += Time.deltaTime;
                    float alpha = Mathf.Lerp(startColor.a, 0, fadeTimer / fadeTime);
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            }
            if(startColor.a <= 0) {
                Destroy(deadTarget);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
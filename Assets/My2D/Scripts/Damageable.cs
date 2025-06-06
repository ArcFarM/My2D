using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    // Health를 관리하는 클래스, takedamage, die 구현
    public class Damageable : MonoBehaviour
    {
        #region Variables
        //참조
        public Animator animator;
        
        //체력
        private float currentHealth;
        //초기 체력(최대 체력)
        [SerializeField] private float maxHealth = 100;
        //죽음 체크
        private bool isDeath = false;

        //무적 타이머
        private bool isInvincible = false;  //true이면 데미지를 입지 않는다
        [SerializeField]
        private float invincibleTime = 3f;  //무적 타임
        private float countdown = 0f;

        //델리게이트 이벤트 함수
        //매개변수로 float, Vector2가 있는 함수 등록 가능
        public UnityAction<float, Vector2> hitAction;
        #endregion

        #region Property
        //체력
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                currentHealth = value;

                if(currentHealth <= 0)
                {
                    Die();
                }
            }
        }

        //최대 체력
        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }
            private set
            {
                maxHealth = value;
            }
        }

        //죽음 체크
        public bool IsDeath
        {
            get
            {
                return isDeath;
            }
            private set
            {
                isDeath = value;
            }
        }

        //이동 속도 잠그기
        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.lockVelocity);
            }
            set
            {
                animator.SetBool(AnimationString.lockVelocity, value);
            }
        }

        public bool isFullHealth {
            get {
                return currentHealth >= maxHealth;
            }
        }
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //초기화
            CurrentHealth = MaxHealth;
        }

        private void Update()
        {
            //무적 타이머
            if(isInvincible)
            {
                countdown += Time.deltaTime;
                if (countdown >= invincibleTime)
                {
                    //타이머 기능
                    isInvincible = false;
                }
            }
            


        }
        #endregion

        #region Custom Method
        //매개변수로 데미지량과 뒤로 밀리는 값을 받아온다
        public bool TakeDamage(float damage, Vector2 knockback)
        {
            if(IsDeath || isInvincible)
            {
                return false;
            }

            CurrentHealth -= damage;
            //Debug.Log($"CurrentHealth : {CurrentHealth}");

            //무적 모드 셋팅 - 타이머 초기화
            isInvincible = true;
            countdown = 0;

            //애니메이션
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;

            //델리게이트 함수에 등록된 함수들 호출            
            hitAction?.Invoke(damage, knockback);
            CharacterEvents.CharTakeDmg?.Invoke(this.gameObject, damage);
            return true;
        }

        //체력 회복
        public void Heal(float healAmount) {
            if (IsDeath || isFullHealth) {
                return;
            }
            //Debug.Log($"Before Heal CurrentHealth : {CurrentHealth}");
            healAmount = Mathf.Min(healAmount, MaxHealth - CurrentHealth);
            CharacterEvents.CharHeal?.Invoke(gameObject, healAmount);
        }

        private void Die()
        {
            IsDeath = true;
            animator.SetBool(AnimationString.isDeath, true);
        }
        #endregion
    }
}

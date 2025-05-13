using UnityEngine;

namespace My2D {
    public class HealItem : MonoBehaviour {
        #region Variables
        //회전 속도
        [SerializeField] float spinSpeedY;
        Vector3 spinSpeed;
        //체력 회복량
        [SerializeField] float healAmount;
        //체력 회복 시 소리 재생
        AudioSource healSound;
        #endregion

        #region Unity Event Methods
        private void Start() {
            spinSpeed = new Vector3(0, spinSpeedY, 0);
            healSound = GetComponent<AudioSource>();
        }
        private void Update() {
            //아이템 회전
            transform.Rotate(spinSpeed * Time.deltaTime);
        }
        private void OnTriggerEnter2D(Collider2D other) {
            //아이템을 캐릭터가 먹었을 때
            Damageable d = other.gameObject.TryGetComponent<Damageable>(out d) ? d : null;
            if (d != null && !d.isFullHealth) { 
                d.Heal(healAmount);
                healSound.Play();
                //아이템을 먹은 후 사라짐
                Destroy(this.gameObject);
            }

        }
        #endregion
    }

}

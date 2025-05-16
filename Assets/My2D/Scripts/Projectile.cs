using System.Collections;
using UnityEngine;

namespace My2D {
    public class Projectile : MonoBehaviour {
        #region Variables
        Rigidbody2D rb2d;
        [SerializeField] private Vector2 dirVector;
        [SerializeField] private Vector2 knockbackVector;
        [SerializeField] float damage = 20f;
        float delayedDestroyTime = 5f; //파괴될 때까지 대기하는 시간
        //충돌 시 생성할 폭발효과
        [SerializeField] GameObject explosionPrefab;
        Vector2 effectOffset = new Vector2(0.5f, 0); //폭발 이펙트의 위치 보정값
        //충돌 시 효과음
        [SerializeField] AudioSource explosionSound;
        #endregion
        #region Property
        #endregion
        #region Unity Event Methods
        private void Start() {
            rb2d = GetComponent<Rigidbody2D>();
            //이동 방향에 따라 벡터 수정
            if (transform.localScale.x < 0) {
                dirVector = new Vector2(-dirVector.x, dirVector.y);
                knockbackVector = new Vector2(-knockbackVector.x, knockbackVector.y);
                effectOffset = new Vector2(-effectOffset.x, effectOffset.y);
            }
            rb2d.linearVelocity = dirVector;
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            Damageable damageable = collision.gameObject.GetComponent<Damageable>();
            if (damageable != null) {
                damageable.TakeDamage(damage, knockbackVector);
            }
            StartCoroutine(DelayedDestroy(delayedDestroyTime));
        }
        //화살 충돌 시 폭발 이펙트 생성
        #endregion
        #region Custom Methods
        IEnumerator DelayedDestroy(float time){
            //폭발 이펙트 생성
            GameObject explosion = Instantiate(explosionPrefab, (Vector2)transform.position + effectOffset, Quaternion.identity);
            //폭발 이펙트 파괴
            Destroy(explosion, 1f);
            //비활성화 후 지연 시간 뒤에 삭제
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }


        #endregion
    }
}

using System.Collections;
using UnityEngine;

namespace My2D {
    public class ProjectileLauncher : MonoBehaviour {
        #region Variables
        public GameObject projectilePrefab; //발사할 화살 프리팹
        public Transform firePoint; //화살 발사 위치
        public PlayerController pc;
        #endregion

        #region Unity Event Methods
        private void Start() {
            //부모(플레이어) 방향에 맞추기
                Vector3 originalScale = projectilePrefab.transform.localScale;
            projectilePrefab.transform.localScale = new Vector3(
                originalScale.x * (transform.parent.localScale.x > 0 ? 1 : -1),
                originalScale.y,
                originalScale.z);
        }

        #endregion
        #region Custom Methods
        public void FireProjectile() {
            //화살 발사
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }

        #endregion
    }

}

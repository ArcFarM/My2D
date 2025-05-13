using UnityEngine;
using TMPro;

namespace My2D {
    public class HealDamageDisplay : MonoBehaviour {
        #region Variables
        //기능 1 : 피해를 입거나 체력을 회복할 때 대상의 머리 위에서 나타나서 위로 일정 이동 후 텍스트 삭제
        [SerializeField] float moveSpeed = 35f;
        RectTransform textRect;
        float destroyTime = 1f; // 텍스트가 사라지는 시간
        float destroyTimer = 0f; // 타이머

        Color textColor;
        #endregion

        #region Unity Event Methods
        private void Awake() {
            textRect = GetComponent<RectTransform>();
            textColor = GetComponent<TextMeshProUGUI>().color;
        }

        private void Update() {
            textRect.position += Vector3.up * moveSpeed * Time.deltaTime;
            if(destroyTimer < destroyTime) {
                destroyTimer += Time.deltaTime;
                Color newColor = textColor;
                newColor.a = Mathf.Lerp(1f, 0f, destroyTimer / destroyTime);
                GetComponent<TextMeshProUGUI>().color = newColor;
            }
            else {
                Destroy(gameObject);
            }
        }
        #endregion

    }

}

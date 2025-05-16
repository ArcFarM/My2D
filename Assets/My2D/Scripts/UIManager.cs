using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace My2D {
    public class UIManager : MonoBehaviour {

        #region Variables
        //피격 시/체력 회복 시 텍스트 프리팹
        [SerializeField] GameObject dmgPrefab;

        //프리팹 출력할 캔버스
        [SerializeField] Canvas canvas;
        //카메라 위치
        Camera camera;
        //데미지 텍스트가 좀 더 위에 나타나면 좋겠음
        [SerializeField] Vector3 offset;
        #endregion

        #region Unity Event Methods

        private void Awake() {
            camera = Camera.main;
        }
        private void OnEnable() {
            CharacterEvents.CharTakeDmg += CharDmg;
            CharacterEvents.CharHeal += CharHeal;
        }

        private void OnDisable() {
            CharacterEvents.CharTakeDmg -= CharDmg;
            CharacterEvents.CharHeal -= CharHeal;
        }
        #endregion

        #region Custom Methods
        public void CharDmg(GameObject character, float dmg) {
            //입은 피해량을 표시하는 텍스트 프리팹 생성
            Vector3 spawnPos = camera.WorldToScreenPoint(character.transform.position);
            GameObject createdDmg = Instantiate(dmgPrefab, spawnPos + offset, Quaternion.identity);
            //텍스트 프리팹을 캔버스의 자식으로 설정
            createdDmg.transform.SetParent(canvas.transform);
            //텍스트 수정
            createdDmg.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = dmg.ToString();
        }
        public void CharHeal(GameObject character, float heal) {
            //받은 회복량 표시하는 텍스트 프리팹 생성
            Vector3 spawnPos = camera.WorldToScreenPoint(character.transform.position);
            GameObject createdHeal = Instantiate(dmgPrefab, spawnPos + offset, Quaternion.identity);
            //피해량 텍스트 프리팹에서 색상만 변경하기
            createdHeal.GetComponent<HealDamageDisplay>().GetSetColor = Color.green;
            //텍스트 프리팹을 캔버스의 자식으로 설정
            createdHeal.transform.SetParent(canvas.transform);
            //텍스트 수정
            createdHeal.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = heal.ToString();
        }
        #endregion
    }
}


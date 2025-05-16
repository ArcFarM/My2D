using UnityEngine;
using UnityEngine.Events;

namespace My2D {
    public class CharacterEvents {
        public static UnityAction<GameObject, float> CharTakeDmg;
        public static UnityAction<GameObject, float> CharHeal;
    }

}


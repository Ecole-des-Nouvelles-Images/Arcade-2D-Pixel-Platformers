using UnityEngine;

namespace Michael.Scripts
{
    [CreateAssetMenu(menuName = "New Character")]
    public class CharacterData : ScriptableObject
    {
        public string CharacterName;
        public float MoveSpeed;
        public Animator Animator;
        

    }
}

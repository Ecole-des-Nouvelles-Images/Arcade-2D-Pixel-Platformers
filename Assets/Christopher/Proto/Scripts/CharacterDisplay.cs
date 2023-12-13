using UnityEngine;

namespace Christopher.Proto.Scripts
{
    public class CharacterDisplay : MonoBehaviour
    {
        [SerializeField] private AnimatorOverrideController[] Dwarf_1;
        [SerializeField] private AnimatorOverrideController[] Dwarf_2;
        [SerializeField] private AnimatorOverrideController[] Dwarf_3;
        [SerializeField] private AnimatorOverrideController[] Dwarf_4;

        private AnimatorOverrideController[][] Dwarfs = new AnimatorOverrideController[4][];

        private void Awake()
        {
            for (int i = 0; i < 4; i++)
            {
                Dwarfs[0] = Dwarf_1;
                Dwarfs[1] = Dwarf_2;
                Dwarfs[2] = Dwarf_3;
                Dwarfs[3] = Dwarf_4;
            }
        }

        public AnimatorOverrideController CharacterAnimatorSelection ( int index, string color)
        {
            if (color == "rouge") return Dwarfs[index][0];
            else if (color == "bleu") return Dwarfs[index][1];
            else
            {
                return null;
            }
        }
    }
}

using Michael.Scripts;
using UnityEngine;

namespace Christopher.Proto.Scripts
{
    public class CharacterDisplay : MonoBehaviour
    {
        public GameObject[] ballAbsorbedFX; // 0 = rouge ; 1 = bleu
        public GameObject[] armorChangeFX; // 0 = rouge ; 1 = bleu
        public GameObject[] deathFX; // 0 = rouge ; 1 = bleu
        public GameObject moveFX;
        public GameObject dashFX;

       // [SerializeField] private AudioSource DashSound;
        [SerializeField] private GameObject characterFoot;
        [SerializeField] private float timerWalkFX;
        [SerializeField] private AnimatorOverrideController[] Dwarf_1;
        [SerializeField] private AnimatorOverrideController[] Dwarf_2;
        [SerializeField] private AnimatorOverrideController[] Dwarf_3;
        [SerializeField] private AnimatorOverrideController[] Dwarf_4;

        private AnimatorOverrideController[][] Dwarfs = new AnimatorOverrideController[4][];
        private float _currentWalkTime;

        private void Awake()
        {
            for (int i = 0; i < 4; i++)
            {
                Dwarfs[0] = Dwarf_1;
                Dwarfs[1] = Dwarf_2;
                Dwarfs[2] = Dwarf_3;
                Dwarfs[3] = Dwarf_4;
            }

            

            //_currentWalkTime = timerWalkFX;
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

        public void BallAbsorbed(string color)
        {
            if (color == "rouge")
            {
                var o = Instantiate(ballAbsorbedFX[0],transform);
                o.transform.position = transform.position;
                Destroy(o,2);
            }
            if (color == "bleu")
            {
                var o = Instantiate(ballAbsorbedFX[1],transform);
                o.transform.position = transform.position;
                Destroy(o,2);
            }
        }
        public void PlayDeathFX(string color)
        {
            if (color == "rouge")
            {
                var o = Instantiate(deathFX[0]);
                o.transform.position = transform.position;
                Destroy(o,2);
            }
            if (color == "bleu")
            {
                var o = Instantiate(deathFX[1]);
                o.transform.position = transform.position;
                Destroy(o,2);
            }
        }

        public void PlayWalkingFX()
        {
            if (_currentWalkTime <= 0)
            {
                var o = Instantiate(moveFX);
                o.transform.position = characterFoot.transform.position;
                _currentWalkTime = timerWalkFX;
                Destroy(o,2);
            }
            if (_currentWalkTime > 0)
            {
                _currentWalkTime -= Time.deltaTime;
            }
        }

        public void PlayDashFX()
        {
            var o = Instantiate(dashFX,transform);
            o.transform.position = transform.position;
            //DashSound.Play();
            Destroy(o,2);
        }
    }
}

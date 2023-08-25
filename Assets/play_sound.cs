using UnityEngine;

namespace TopDownShooter
{
    public class play_sound : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioSource townSound;
        public float volume;
        
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !audioSource.isPlaying)
            {
                audioSource.Play();
                townSound.Pause();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player" && audioSource.isPlaying)
            {
                audioSource.Pause();
                townSound.Play();
            }
        }

    }
}

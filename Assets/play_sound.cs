using UnityEngine;

namespace TopDownShooter
{
    public class play_sound : MonoBehaviour
    {
        public AudioSource audioSource;
        public float volume;
        
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player" && audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }

    }
}

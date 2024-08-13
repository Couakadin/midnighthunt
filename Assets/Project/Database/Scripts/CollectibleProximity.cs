using UnityEngine;

public class CollectibleProximity : MonoBehaviour
{
    public Transform player;  // R�f�rence au Transform du joueur
    private AudioSource audioSource;

    public float maxVolume = 1.0f;  // Volume maximum du son
    public float minDistance = 5.0f;  // Distance � laquelle le volume est maximum
    public float maxDistance = 20.0f; // Distance � partir de laquelle le son commence � diminuer

    void Start()
    {
        // R�cup�re l'AudioSource attach� au GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Calcule la distance entre le joueur et cet objet
        float distance = Vector3.Distance(player.position, transform.position);

        // Si la distance est inf�rieure � la distance minimum, le volume est au maximum
        if (distance < minDistance)
        {
            audioSource.volume = maxVolume;
        }
        // Si la distance est sup�rieure � la distance maximum, le volume est � 0
        else if (distance > maxDistance)
        {
            audioSource.volume = 0f;
        }
        // Sinon, le volume diminue progressivement en fonction de la distance
        else
        {
            // Calcule le volume en fonction de la distance entre minDistance et maxDistance
            float volume = Mathf.Lerp(maxVolume, 0f, (distance - minDistance) / (maxDistance - minDistance));
            audioSource.volume = volume;
        }
    }
}

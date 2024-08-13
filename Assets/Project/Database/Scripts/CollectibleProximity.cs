using UnityEngine;

public class CollectibleProximity : MonoBehaviour
{
    public Transform player;  // Référence au Transform du joueur
    private AudioSource audioSource;

    public float maxVolume = 1.0f;  // Volume maximum du son
    public float minDistance = 5.0f;  // Distance à laquelle le volume est maximum
    public float maxDistance = 20.0f; // Distance à partir de laquelle le son commence à diminuer

    void Start()
    {
        // Récupère l'AudioSource attaché au GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Calcule la distance entre le joueur et cet objet
        float distance = Vector3.Distance(player.position, transform.position);

        // Si la distance est inférieure à la distance minimum, le volume est au maximum
        if (distance < minDistance)
        {
            audioSource.volume = maxVolume;
        }
        // Si la distance est supérieure à la distance maximum, le volume est à 0
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

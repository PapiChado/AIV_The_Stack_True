using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Collision_Detector : MonoBehaviour
{
    int collisions;

    int points;

    private AudioSource _audioSource;
    private AudioClip _audioClip;
    
    public Text ScoreText;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioClip = Resources.Load<AudioClip>("Sounds/metal_pipe");
        ScoreText.text = "Score: 0";

    }

    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
        _audioSource.PlayOneShot(_audioClip, 0.5f);
        points += collision.gameObject.GetComponent<Block>().Points;
        ScoreText.text = "Score: " + points;
        Debug.Log($"Collisions:{collisions} points:{points}");
    }

    private void OnCollisionExit(Collision collision)
    {
        collisions--;
        points -= collision.gameObject.GetComponent<Block>().Points;
        ScoreText.text = "Score: " + points;
        Debug.Log($"Collisions:{collisions} points:{points}");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarm;
    private float _startVolume = 0;
    private float _maxVolume = 1;
    private float _duration = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Goblin>(out Goblin goblin)) 
        {
            _alarm.PlayOneShot(_alarm.clip);
            StartIncreaseSound();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartTurnDownSound();
    }

    private void StartTurnDownSound()
    {
        StartCoroutine(TurnDownSound(_duration));
    }

    private IEnumerator TurnDownSound(float Duration)
    {
        var numberOfChanges = Duration / Time.deltaTime;

        for(int i = 0; i < numberOfChanges; i++) 
        {
            _alarm.volume = _maxVolume - (_maxVolume / numberOfChanges * i);

            yield return null;
        }

        _alarm.Stop();
    }

    private void StartIncreaseSound()
    {
        StartCoroutine(IncreaseSound(_duration));
    }

    private IEnumerator IncreaseSound(float Duration)
    {
        var numberOfChanges = Duration / Time.deltaTime;

        for(int i = 0; i < numberOfChanges; i++) 
        {
            _alarm.volume = _startVolume + (_maxVolume / numberOfChanges * i);

            yield return null;
        }
    }
}

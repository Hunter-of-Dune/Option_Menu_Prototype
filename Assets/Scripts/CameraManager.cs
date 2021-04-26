using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] Animator animator;

    [SerializeField] ParticleSystem eyeExplosion;

    private void Start()
    {
        GameManager.Instance.StartGameEvent.AddListener(Move);
    }

    private void Move()
    {
        animator.SetBool("startGame", true);
    }


    public void DoneMoving()
    {
        GameManager.Instance.FadeOut.Invoke();
    }

    public void CallEyeExplosion()
    {
        eyeExplosion.startLifetime = 4f;
        eyeExplosion.startSize = 3f;
        eyeExplosion.playbackSpeed = 1f;
        eyeExplosion.Play();

        AudioManager.Instance.StartCoroutine(AudioManager.Instance.FadeOutAudio(AudioManager.Instance.BGMusic, 4f));
        AudioManager.Instance.StartCoroutine(AudioManager.Instance.FadeOutAudio(AudioManager.Instance.SFX, 4f));

    }
}

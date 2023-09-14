using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    ParticleSystem ps;

    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    //public Transform playerTransform;

    private void Start()
    {

        ps = GetComponent<ParticleSystem>();
        //ps.Play();
        //var triggerModule = ps.trigger;
        //Collider2D playerCollider = playerTransform.GetComponent<Collider2D>();

        //if (playerCollider != null)
        //{
        //    // Trigger 모듈에 플레이어의 콜라이더를 할당합니다.
        //    triggerModule.SetCollider(0, playerCollider);
        //    Debug.Log("플레이어 Transform에 Collider 컴포넌트가 있습니다.");
        //}

        //else
        //{
        //    Debug.LogError("플레이어 Transform에 Collider 컴포넌트가 없습니다.");
        //}
    }


    private void OnParticleTrigger()
    {
        Debug.Log("트리거 충돌");
        int triggerParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter  , particles);

        for (int i = 0; i < triggerParticles; i++)
        {
            ParticleSystem.Particle p = particles[i];

            //ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            Debug.Log("We colleted 1 particle");
            particles[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter , particles);
        ps.Stop();
    }
}

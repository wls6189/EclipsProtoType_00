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
        //    // Trigger ��⿡ �÷��̾��� �ݶ��̴��� �Ҵ��մϴ�.
        //    triggerModule.SetCollider(0, playerCollider);
        //    Debug.Log("�÷��̾� Transform�� Collider ������Ʈ�� �ֽ��ϴ�.");
        //}

        //else
        //{
        //    Debug.LogError("�÷��̾� Transform�� Collider ������Ʈ�� �����ϴ�.");
        //}
    }


    private void OnParticleTrigger()
    {
        Debug.Log("Ʈ���� �浹");
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

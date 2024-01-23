using UnityEngine;

public class GatherPoint : MonoBehaviour
{
    [Header("Stats")]
    public float ResourceAmount;

    [Header("Unity setup")]
    public Transform GatherLocation;
    public WorkerLogic CurrentWorker;
    public ParticleSystem MiningParticles;

    private void Start()
    {
        StopMining();
    }

    public void StartMining()
    {
        if (MiningParticles.isStopped)
        {
            MiningParticles.Play();
        }
    }

    public void StopMining()
    {
        MiningParticles.Stop();
    }
}
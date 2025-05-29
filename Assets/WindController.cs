using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class VortexBladeAffector : MonoBehaviour
{
    public Transform[] bladeTips; // Assign each blade tip here in the Inspector
    public float vortexStrength = 5f;
    public float updraftStrength = 1f;
    public float vortexRadius = 2f;

    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    void LateUpdate()
    {
        int numParticlesAlive = ps.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            foreach (var bladeTip in bladeTips)
            {
                Vector3 toBlade = particles[i].position - bladeTip.position;
                float distance = toBlade.magnitude;
                if (distance < vortexRadius)
                {
                    // Tangential direction around blade
                    Vector3 tangent = Vector3.Cross(Vector3.up, toBlade.normalized);
                    float force = vortexStrength * (1 - distance / vortexRadius);
                    particles[i].velocity += tangent * force * Time.deltaTime;
                    particles[i].velocity += Vector3.up * updraftStrength * Time.deltaTime;
                }
            }
        }

        ps.SetParticles(particles, numParticlesAlive);
    }
}
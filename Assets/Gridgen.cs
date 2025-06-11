using UnityEngine;

public class Gridgen : MonoBehaviour
{
    public ParticleSystem ps;
    public int numLines = 10;
    public float lineSpacing = 1.0f;
    public float emitInterval = 0.05f;
    private float timer;
    private int currentLine = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= emitInterval)
        {
            timer = 0f;
            EmitInLine(currentLine);
            currentLine = (currentLine + 1) % numLines;
        }
    }

    void EmitInLine(int lineIndex)
    {
        var emitParams = new ParticleSystem.EmitParams();
        // Example: Lines along Y axis, spread along X
        emitParams.position = new Vector3(lineIndex * lineSpacing, 0, 0);
        emitParams.velocity = Vector3.forward * 2f; // Move along Z axis

        ps.Emit(emitParams, 1);
    }
}

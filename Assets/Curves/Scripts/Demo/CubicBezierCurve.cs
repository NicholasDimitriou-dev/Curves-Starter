using UnityEngine;

/*
 * CubicBezierCurve owns the four control-point Transforms in the Demo scene.
 * This is where their world positions connect to CubicBezierMath, Scene-view
 * gizmos, and the Play Mode line.
 */

public class CubicBezierCurve : MonoBehaviour
{
    [Header("Bezier Points")]
    public Transform p0;
    public Transform p1;
    public Transform p2;
    public Transform p3;

    public int numSamples = 10;

    void Start()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numSamples;
        for (int i = 0; i < numSamples; i++)
        {
            float t = i / (float)(numSamples -1);
            Vector3 sample = SamplePoint(t);
            lineRenderer.SetPosition(i,sample);
        }
        // Space them evenly in t and include both endpoints.
        // Check: restart Play Mode. The line matches the gizmos and reaches p0 and p3.
        // Next: Slice 3.5 in Bezier/CubicBezierMath.cs.
    }

    void OnDrawGizmos()
    {
        if (p0 == null || p1 == null || p2 == null || p3 == null) return;
       
        // Check: four markers, an open three-segment polygon, and a curve that reaches
        // both ends. Move a point outside Play Mode; everything follows.
        // Next: Slice 3.4 in Start.
        CurveGizmos.Draw(10,SamplePoint,p0,p1,p2,p3);
    }

    public Vector3 SamplePoint(float t)
    {
       
        // current world positions.
        // Check: SceneCurve_SamplesCurrentWorldPointsWithoutCallbacks(True,False) passes.
        // Next: Slice 3.3 in OnDrawGizmos.
        return CubicBezierMath.SamplePoint(p0.position, p1.position, p2.position, p3.position,t);   
    }

    public Vector3 SampleTangent(float t)
    {
       
        // current world positions. Do not normalize it.
        // Check: SceneCurve_SamplesCurrentWorldPointsWithoutCallbacks(True,True) passes.
        // All four cubic tests pass.
        // Next: open Main Game, Slice 4.1 in Game/ThrownAxe.cs. </> end of Slice 3
        return CubicBezierMath.SampleTangent(p0.position, p1.position, p2.position, p3.position,t);
    }
}

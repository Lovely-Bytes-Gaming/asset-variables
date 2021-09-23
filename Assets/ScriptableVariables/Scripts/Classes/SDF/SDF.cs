using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SDF
{
    public abstract bool Contains(Vector3 point);
    public UnionSDF Join(SDF other) => new UnionSDF(this, other);
    public IntersectionSDF Intersect(SDF other) => new IntersectionSDF(this, other);
    public DistinctSDF Distinct(SDF other) => new DistinctSDF(this, other);
    public DifferenceSDF Difference(SDF other) => new DifferenceSDF(this, other);
}

public class UnionSDF : SDF
{
    protected SDF a, b;

    public UnionSDF(SDF a, SDF b)
    {
        this.a = a; 
        this.b = b;
    }

    public override bool Contains(Vector3 point)
        => a.Contains(point) || b.Contains(point);
}

public class IntersectionSDF : SDF
{
    protected SDF a, b;

    public IntersectionSDF(SDF a, SDF b)
    {
        this.a = a;
        this.b = b;
    }

    public override bool Contains(Vector3 point)
        => a.Contains(point) && b.Contains(point);
}

public class DistinctSDF : SDF
{
    protected SDF a, b;

    public DistinctSDF(SDF a, SDF b)
    {
        this.a = a;
        this.b = b;
    }

    public override bool Contains(Vector3 point)
        => a.Contains(point) ^ b.Contains(point);
}

public class DifferenceSDF : SDF
{
    protected SDF a, b;

    public DifferenceSDF(SDF a, SDF b)
    {
        this.a = a;
        this.b = b;
    }

    public override bool Contains(Vector3 point)
        => a.Contains(point) && !b.Contains(point);
}

public class SphereSDF : SDF
{
    private Vector3 center;
    private float radiusSquared;

    public SphereSDF(Vector3 center, float radius)
    {
        this.center = center;
        radiusSquared = radius * radius;
    }

    public override bool Contains(Vector3 point)
    {
        Vector3 toCenter = center - point;
        return Vector3.Dot(toCenter, toCenter) < radiusSquared;
    }
}
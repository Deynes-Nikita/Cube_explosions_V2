using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        SetColor();
    }

    public void SetScale(float reductionRatio)
    {
        transform.localScale /= reductionRatio;
    }

    private void SetColor()
    {
        _material.color = new Color(Random.value, Random.value, Random.value);
    }
}

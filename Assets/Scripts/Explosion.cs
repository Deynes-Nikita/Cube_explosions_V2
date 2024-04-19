using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minQuantityPartsInExplosion = 2;
    [SerializeField] private int _maxQuantityPartsInExplosion = 7;
    [SerializeField] private float _reductionRatio = 2f;
    [SerializeField] private float _probability = 1f;
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private float explosionRadius = 2f;

    private void OnMouseDown()
    {
        TryCreateCubes();
            
        Explode();

        Destroy(gameObject);
    }

    private bool TryCreateCubes()
    {
        if (_probability >= Random.value)
        {
            SetParameters();

            int quantityParts = Random.Range(_minQuantityPartsInExplosion, _maxQuantityPartsInExplosion);

            for (int i = 0; i < quantityParts; i++)
            {
                Instantiate(_cubePrefab, transform.position, Random.rotation).SetScale(_reductionRatio);
            }

            return true;
        }

        return false;
    }

    private void SetParameters()
    {
        _probability /= _reductionRatio;
        _explosionForce *= _reductionRatio;
        explosionRadius *= _reductionRatio;
    }

    private void Explode()
    {
        foreach (Rigidbody expodableCube in GetExpodableCube())
        {
            expodableCube.AddExplosionForce(_explosionForce, transform.position, explosionRadius);
        }
    }

    private List<Rigidbody> GetExpodableCube()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}

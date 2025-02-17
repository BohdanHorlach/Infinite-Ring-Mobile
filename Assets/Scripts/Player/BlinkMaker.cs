using System;
using System.Collections;
using UnityEngine;


public class BlinkMaker : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField, Range(0, 1)] private float _minAlpha = 0.2f;
    [SerializeField, Range(0, 1)] private float _maxAlpha = 1f;
    [SerializeField] private float _countOfBlink = 3f;
    [SerializeField, Min(0.1f)] private float _transitionTime = 0.5f;


    private void OnEnable()
    {
        ChangeSurfaceMode(SetOpaqueMode);

        //SetAlphaToMaterials(1f);
    }

    private void SetAlphaToMaterials(float alpha)
    {
        foreach (Material material in _materials)
        {
            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }


    private void SetTransparentMode(Material material)
    {
        material.SetFloat("_Surface", 1);
        material.SetFloat("_Blend", 0);
        material.SetFloat("_ZWrite", 0);
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }


    private void SetOpaqueMode(Material material)
    {
        material.SetFloat("_Surface", 0);
        material.SetFloat("_Blend", 1);
        material.SetFloat("_ZWrite", 1);
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
    }


    private void ChangeSurfaceMode(Action<Material> surfaceMode)
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            surfaceMode.Invoke(_materials[i]);
        }
    }


    private IEnumerator MakeBlink(float start, float end)
    {
        float elapsed = 0.0f;

        Color startColor = _materials[0].color;
        Color endColor = startColor;
        endColor.a = end;

        while (elapsed < _transitionTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _transitionTime);
            Color currentColor = Color.Lerp(startColor, endColor, t);

            SetAlphaToMaterials(currentColor.a);
            yield return null;
        }
    }


    private IEnumerator StartBlinking()
    {
        ChangeSurfaceMode(SetTransparentMode);

        int blinkState = 2;

        for (int i = 0; i < _countOfBlink * blinkState; i++)
        {
            bool isEven = i % 2 == 0;

            float start = isEven ? _minAlpha : _maxAlpha;
            float end = !isEven ? _maxAlpha : _minAlpha;

            yield return MakeBlink(start, end);
        }

        ChangeSurfaceMode(SetOpaqueMode);
    }


    public void PlayBlink()
    {
        StartCoroutine("StartBlinking");
    }
}

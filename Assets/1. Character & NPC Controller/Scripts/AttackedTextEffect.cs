using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackedTextEffect : MonoBehaviour
{
    public float duration = 1.0f;
    public float speed = 1.0f;

    private TextMeshPro textMeshPro;
    private float timer = 0f;

    private Transform lookTarget;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        lookTarget = Camera.main.transform;
    }

    public void Set(string text, Color color)
    {
        textMeshPro.text = text;
        textMeshPro.color = color;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        textMeshPro.alpha = 1f - (timer / duration);

        transform.LookAt(lookTarget);
        transform.position += Vector3.up * speed * Time.deltaTime;

        if ( timer > duration)
        {
            Destroy(gameObject);
        }


    }

}

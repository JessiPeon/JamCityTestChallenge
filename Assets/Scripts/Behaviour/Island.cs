using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Logic;

namespace Behaviour
{
    public class Island : MonoBehaviour, IPointerDownHandler
    {
        public Role role;
        [SerializeField] private Vector3 originalPosition;
        [SerializeField] public Vector3 targetPosition;
        public bool changePosition;
        [SerializeField] private float speed = 120;

        public static event Action<Island> IslandIsSelected;

        private void Start()
        {
            SetColor();
            originalPosition = transform.position;
            targetPosition = originalPosition;
        }

        private void Update()
        {
            float scaleFactor = Mathf.Clamp(1f - transform.position.y / 5f, 0.5f, 1f);
            transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

            Vector3 displacement = targetPosition - transform.position;
            if (changePosition && displacement.magnitude > 0f)
            {
                transform.Translate(displacement.normalized * speed * Time.deltaTime);
            }
            if (displacement.magnitude < 0.5f)
            {
                changePosition = false;
            }
        }

        public void InicializeIsland(Role role_, int delay)
        {
            SetRole(role_);
            SetAnimation(delay);
        }

        public void SetColor()
        {

            SpriteRenderer land = GetComponentsInChildren<Transform>().Where(child => child.CompareTag("Color")).Select(child => child.gameObject).First().GetComponent<SpriteRenderer>();
            land.color = GetRandomColor();
        }

        private void SetRole(Role role_)
        {
            role = role_;
            TextMesh textMesh = GetComponentsInChildren<Transform>().Where(child => child.CompareTag("Text")).Select(child => child.gameObject).First().GetComponent<TextMesh>();
            textMesh.text = role.Name;
        }
        private void SetAnimation(int delay)
        {
            Animator animator = GetComponentsInChildren<Transform>().Where(child => child.CompareTag("Land")).Select(child => child.gameObject).First().GetComponent<Animator>();
            animator.Play("idle", -1, delay/10f);
        }

        private Color GetRandomColor()
        {
            float red = UnityEngine.Random.Range(0f, 1f);
            float green = UnityEngine.Random.Range(0f, 1f);
            float blue = UnityEngine.Random.Range(0f, 1f);

            return new Color(red, green, blue);
        }

        public void ResetIsland()
        {
            changePosition = true;
            targetPosition = originalPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            changePosition = true;
            IslandIsSelected?.Invoke(this);
        }
    }
}


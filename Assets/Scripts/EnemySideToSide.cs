using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideToSide : MonoBehaviour
{
    [SerializeField] public float MoveSpeed = 2f;
    [SerializeField] public float MoveDuration = 2f; // How long to move in one direction
    [SerializeField] public Transform Weakpt_Parent;

    private Transform[] childObjs;
    private Transform[] weakPts;
    private int count;
    private bool movingRight = true;

    void Start()
    {
        Setup();
        StartCoroutine(Pattern());
    }

    IEnumerator Pattern()
    {
        while (true)
        {
            int checkCount = transform.childCount;
            if (checkCount != count)
            {
                Setup();
            }

            float elapsed = 0f;
            float direction = movingRight ? 1f : -1f;

            // Move in current direction for MoveDuration seconds
            while (elapsed < MoveDuration)
            {
                Move(direction);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Flip direction
            movingRight = !movingRight;

            // Wait before switching direction again (optional)
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Move(float direction)
    {
        for (int i = 0; i < childObjs.Length; i++)
        {
            Rigidbody2D rb = childObjs[i].GetComponent<Rigidbody2D>();
            Rigidbody2D rbWeakpt = weakPts[i].GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = new Vector2(direction * MoveSpeed, rb.velocity.y);
            }

            if (rbWeakpt != null)
            {
                rbWeakpt.velocity = new Vector2(direction * MoveSpeed, rbWeakpt.velocity.y);
            }
        }
    }

    void Setup()
    {
        count = transform.childCount;
        childObjs = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            childObjs[i] = transform.GetChild(i);
        }

        int countTwo = Weakpt_Parent.childCount;
        weakPts = new Transform[countTwo];
        for (int j = 0; j < countTwo; j++)
        {
            weakPts[j] = Weakpt_Parent.GetChild(j);
        }
    }
}

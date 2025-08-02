using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    [SerializeField] public float JumpPower;
    [SerializeField] public float Delay;
    [SerializeField] public Transform Weakpt_Parent;

    private Transform[] childObjs;
    private Transform[] weakPts;
    private int count;

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
            if(checkCount != count)
            {
                Setup();
            }
            for (int i = 0; i < childObjs.Length; i++)
            {
                Jump(childObjs[i], weakPts[i]);
                yield return new WaitForSeconds(Delay);
            }

            yield return new WaitForSeconds(2f);
        }
    }

    void Jump(Transform obj, Transform objTwo)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        Rigidbody2D rbWeakpt = objTwo.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower * rb.gravityScale);
        }

        if(rbWeakpt != null)
        {
            rbWeakpt.velocity = new Vector2(rbWeakpt.velocity.x, JumpPower*(-1)*rb.gravityScale);
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

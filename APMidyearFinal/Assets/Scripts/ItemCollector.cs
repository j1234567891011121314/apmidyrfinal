 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int pearls;
    [SerializeField] private Text score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("pearl")){
            Destroy(collision.gameObject);
            pearls = pearls + 10;
            score.text = "Score: " + pearls;
        }
    }
}

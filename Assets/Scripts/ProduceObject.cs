using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceObject : MonoBehaviour
{
    public bool isFruit;

    void Update()
    {
        transform.position -= new Vector3(0, 0.015f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
        switch (collision.gameObject.tag)
        {
            case "FruitBasket":
                if (isFruit)
                {
                    UI.score++;
                    Destroy(gameObject);
                }
                else
                {
                    UI.strikes++;
                    Destroy(gameObject);
                }
                break;
            case "VeggieBasket":
                if (isFruit)
                {
                    UI.strikes++;
                    Destroy(gameObject);
                }
                else
                {
                    UI.score++;
                    Destroy(gameObject);
                }
                break;
            case "Floor":
                UI.strikes++;
                Destroy(gameObject);
                break;
            default:
                Debug.Log("Error: Trigger is not a basket or the floor");
                break;
        }
    }
}

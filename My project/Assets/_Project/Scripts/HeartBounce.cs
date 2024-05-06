using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeartBounce : MonoBehaviour
{
    bool velocityWasStored = false;
    Vector3 storedVelocity;
    Rigidbody rb;

    public TextMeshProUGUI hitCount;
    int numHits = 0;
    int bestScore = 0;
    int lastBest = 0;
    bool hasLost = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "tray")
        {
            Debug.Log("yes! hit tray!");
            if (!velocityWasStored)
            {
                storedVelocity = rb.velocity;
                velocityWasStored = true;
            }
            if (rb.velocity.y > 1)
            {
                numHits++;
            }
            rb.velocity = new Vector3(rb.velocity.x, storedVelocity.y, rb.velocity.z);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        string str = "";
        if (!hasLost)
        {
            str = "Score: " + numHits.ToString();
        } else
        {
            str = "Hits: " + numHits.ToString() + "\nYour best: " + bestScore;
            if (bestScore > lastBest) str += "\nNEW RECORD!";
        }
        hitCount.text = str;
        if (transform.position.y < -3)
        {
            if (!hasLost)
            {
                hasLost = true;
                lastBest = bestScore;
                if (numHits > bestScore)
                {
                    bestScore = numHits;
                }
            }
        }
    }

    private void OnGUI()
    {
        if (hasLost)
        {
            int buttonW = 100;
            int buttonH = 50;
            float halfScreenW = Screen.width / 2;
            float halfButtonW = buttonW / 2;
            if (GUI.Button(new Rect(halfScreenW - halfButtonW, Screen.height * 0.8F, buttonW, buttonH), "Play Again"))
            {
                numHits = 0;
                hasLost = false;
                transform.position = new Vector3(0.5F, 2, -0.05F);
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }
}

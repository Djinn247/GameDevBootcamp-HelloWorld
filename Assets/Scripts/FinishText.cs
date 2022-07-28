using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishText : MonoBehaviour
{
    private TextMesh textMesh;
    private Player1stPCollectible playerCollectibleScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        textMesh = this.gameObject.GetComponent<TextMesh>();
        playerCollectibleScript = GameObject.Find("Player (1stP)").GetComponent<Player1stPCollectible>();

        textMesh.text = "Score: " + playerCollectibleScript.score + ((playerCollectibleScript.score == playerCollectibleScript.maxScore) ? "!" : "") + " Time: " + decimal.Round((decimal)Time.timeSinceLevelLoadAsDouble, 2);
    }
}

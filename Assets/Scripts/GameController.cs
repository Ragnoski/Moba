using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Player player;
    public Text DescriptionText;


    private IEnumerator Counter()
    {
        for (int i = 1; i <= 16; i++)
        {
            yield return new WaitForSeconds(2);
            player.LevelUp();
        }
    }

    private void Start()
    {
        Language.CurrentLanguage = Language.Portuguese;
        StartCoroutine(Counter());
    }


    public void ShowNextDescription(string firebutton)
    {
        DescriptionText.text = player.GetNextSkillDescription(firebutton);
    }

    public void HideNextDescription()
    {
        DescriptionText.text = "";
    }
}

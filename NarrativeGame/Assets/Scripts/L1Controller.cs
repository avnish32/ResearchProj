using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1Controller : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private UIController uiController;

    [SerializeField]
    private AudioController audioController;

    [SerializeField]
    private AudioClip levelBGM;

    // Start is called before the first frame update
    void Start()
    {
        audioController.PlayMusic(levelBGM);

        string[] prologueDialogues =
        {
            "A year ago, a small article appeared in the local newspaper.",
            "It was an expose, hinting at some wrongdoing at a lettings agency called ‘AB&C’.",
            "The article was tucked away in a remote corner of the inside pages, so it was unlikely to draw much attention.",
            "Unfortunately for the reporter who wrote it, it still managed to catch the eyes of some people at AB&C Lettings.",
            "The reporter was never seen again after that. His family reported him as missing; a search was undertaken, " +
            "but after a few months, everyone just assumed he was dead.",
            "Bit by bit, everyone moved on, even his family.",
            "Except one guy.",
            "My name is Jared, and that reporter was my father.",
            "I have not rested easy since his disappearance. I know that the evil AB&C Lettings is behind it, " +
            "but I have no way of proving it.",
            "After weeks of research, I have found that a girl at my school works part-time with AB&C.",
            "Her name is Anya, and I have decided to infiltrate the lettings agency by getting a job through her.",
            "I just need to find her and persuade her to refer me."
        };

        uiController.StartDialogues(prologueDialogues, ECharacters.NARRATOR, () => {
            uiController.FadeFromBlack(null);
            gameController.EnablePlayerMovement();
        });
    }
}

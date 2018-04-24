using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogicManager : MonoBehaviour {

    public static Character currentCharacter = null;
    public static string playerName = GameConstant.PLAYER_NAME;
    [HideInInspector] public static Dialogue currentDialogue;
    [HideInInspector] public static int endResult;
    public Character charaDebug;
    private PlayMusic playMusic;
    private int savedScene = 1;
    private bool set = false;


    public static void SetCurrentCharacter(Character character) {
        currentCharacter = character;
        currentDialogue = character.dialog;
    }


    // Use this for initialization
    void Start() {
        DontDestroyOnLoad(this);
        this.playMusic = this.GetComponent<PlayMusic>();
    }

    private void Update() {
        var currentscene = SceneManager.GetActiveScene().buildIndex;
        if (currentscene != 0) {
            if (this.savedScene != currentscene) {
                this.savedScene = SceneManager.GetActiveScene().buildIndex;
                this.playMusic.SwitchMusic(GameConstant.MUSIC_FADE_TIME);
            }
        }
        if (currentCharacter != null && !set) {
            charaDebug = currentCharacter;
            set = true;
        }
    }


    public static Character GetCurrentCharacter() {
        return currentCharacter;
    }
}

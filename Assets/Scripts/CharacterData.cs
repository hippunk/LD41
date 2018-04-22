using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CreateCharacter")]
public class CharacterData : ScriptableObject {

    [Header("Character Name")]
    public string characterName;

    [Header("Titre")]
    public string titre;

    [Header("Relation")]
    public int relation;

    [Header("Emotion")]
    public Emotion emotion;

    [Header("Date Card Sprite")]
    public Sprite dateCard;

    [Header("Chara Sprite")]
    public Sprite charaSprite;
}




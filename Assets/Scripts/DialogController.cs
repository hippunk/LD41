using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogController : MonoBehaviour, IPointerClickHandler {

    public GameObject nameGO;
    public GameObject contentGO;

    public bool defilement;
    private bool choicePending = false;
    public TextAnimator textAnimator;
    public Dialogue dialogue;
    public GameObject fonduChoix;
    public GameObject choiceContainer;
    public GameObject choicePrefab;

    public GameObject clickFeedback;
    public Sprite meSprite;
    public Image dateImage;
    public bool fin = false;

    private Queue<string> vNodes = new Queue<string>();

    void Start() {
        dialogue = GameLogicManager.currentDialogue;
        SetDialogue();
    }

    void RawDebug() {
        Dialogue.Choice choice = dialogue.GetChoices()[0];
        Debug.Log(choice.dialogue + " count choices" + dialogue.GetChoices().Length);
        dialogue.PickChoice(dialogue.GetChoices()[0]);

        choice = dialogue.GetChoices()[0];
        Debug.Log(choice.dialogue + " count choices" + dialogue.GetChoices().Length);
        dialogue.PickChoice(dialogue.GetChoices()[0]);

        choice = dialogue.GetChoices()[0];
        Debug.Log(choice.dialogue + " count choices" + dialogue.GetChoices().Length);
        dialogue.PickChoice(dialogue.GetChoices()[0]);

        choice = dialogue.GetChoices()[0];
        Debug.Log(choice.dialogue + " count choices" + dialogue.GetChoices().Length);
        dialogue.PickChoice(dialogue.GetChoices()[0]);
    }

    public void SetDialogue() {

        List<Dialogue.Choice> aviableChoices = GetAviableChoices();

        if(vNodes.Count > 0)
        {
            string node = vNodes.Dequeue();
            SetContent(node);
            textAnimator.ChangeText(contentGO.GetComponent<Text>().text);
            clickFeedback.SetActive(false);
            
        }
        else if (aviableChoices.Count == 1) {
            Dialogue.Choice choice = aviableChoices.First();

            clickFeedback.GetComponentInChildren<Text>().text = "Next...";
            SplitIntoVirtualNodes(choice.dialogue);
            UpdateCharacterData(choice);
            SetImage(choice);
            dialogue.PickChoice(choice);
            SetDialogue();
        }
        else {
            GenerateChoiceList(aviableChoices);
        }

        if (dialogue.GetChoices() == null || dialogue.GetChoices().Length == 0 || aviableChoices.Count == 0) {
            clickFeedback.GetComponentInChildren<Text>().text = "Fin";
            fin = true;
        }

    }

    public void SetImage(Dialogue.Choice choice) {
        //Debug.Log(choice.speaker);
        if (choice.speaker == "Moi") {
            dateImage.sprite = meSprite;
            SetName(GameLogicManager.playerName);
        }

        else
        {
            SetName(GameLogicManager.currentCharacter.characterName+"-"+ GameLogicManager.currentCharacter.titre);
            dateImage.sprite = GameLogicManager.currentCharacter.dateCard;
        }
    }

    public List<Dialogue.Choice> GetAviableChoices() {
        List<Dialogue.Choice> selectableChoices = new List<Dialogue.Choice>();
        //Debug.Log(dialogue);
        foreach (Dialogue.Choice choice in dialogue.GetChoices()) {
            //Debug.Log(choice.dialogue);
            if (this.isChoiceSelectable(choice)) {
                //Debug.Log("Is Selecable");
                selectableChoices.Add(choice);
            }
            else
            {
                //Debug.Log("Is Not Selecable");
            }
        }

        return selectableChoices;
    }

    void GenerateChoiceList(List<Dialogue.Choice> aviableChoices) {

        fonduChoix.SetActive(true);
        clickFeedback.GetComponentInChildren<Text>().text = "Answer";
        choicePending = true;


        foreach (Dialogue.Choice choice in aviableChoices) {
            GameObject newRow = Instantiate(choicePrefab, choiceContainer.transform);
            newRow.GetComponentInChildren<Text>().text = choice.dialogue;
            newRow.GetComponent<Button>().onClick.AddListener(() => ButtonHandler(choice));
        }
    }


    private bool isChoiceSelectable(Dialogue.Choice choice) {
        var constraintData = this.GetChoiceConstraints(choice);
        if (constraintData != null) {
            var constraints = constraintData.Split(' ');
            foreach (var constraint in constraints) {
                if (constraint.StartsWith("E:")) {
                    var trimData = constraint.TrimStart('E', ':');
                    if (GameLogicManager.currentCharacter.emotion.ToString() != trimData) {
                        return false;
                    }
                }
                else if (constraint.StartsWith("R:")) {
                    int relationConstraint;
                    var trimData = constraint.TrimStart('R', ':');
                    if (trimData.StartsWith(">")) {
                        var processedTrimData = trimData.Trim('>');
                        //Debug.Log(">" + processedTrimData + " "+ GameLogicManager.currentCharacter.relation);
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: Error in relation constraint value (" + GameLogicManager.currentCharacter.characterName + ")");
                        }
                        if (GameLogicManager.currentCharacter.relation <= relationConstraint) {
                            return false;
                        }
                    }
                    else {
                        
                        var processedTrimData = trimData.Trim('<');
                        //Debug.Log("< "+ processedTrimData+" "+GameLogicManager.currentCharacter.relation);
                        if (!Int32.TryParse(processedTrimData, out relationConstraint)) {
                            Debug.Log("DIALOG CONTROLLER :: Error in relation constraint value (" + GameLogicManager.currentCharacter.characterName + ")");
                        }
                        if (GameLogicManager.currentCharacter.relation >= relationConstraint) {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    private string GetChoiceConstraints(Dialogue.Choice choice) {
        if (choice.userData != null) {
            var splitData = choice.userData.Split('|');
            if (splitData.Length > 1) {
                return splitData[1];
            }
        }
        return null;
    }

    private string GetChoiceParameters(Dialogue.Choice choice) {
        if (choice.userData != null) {
            var splitData = choice.userData.Split('|');
            if (splitData.Length > 0) {
                return splitData[0];
            }
        }
        return null;
    }

    private Emotion GetChoiceEmotionParameter(Dialogue.Choice choice) {
        var emotion = GameLogicManager.currentCharacter.emotion;
        if (choice.userData != null) {
            var parametersData = this.GetChoiceParameters(choice);
            var parameters = parametersData.Split(' ');
            foreach (var parameter in parameters) {
                if (parameter.StartsWith("E:")) {
                    var trimParameter = parameter.TrimStart('E', ':');
                    if (GameConstant.StringToEmotion(trimParameter) != Emotion.none) {
                        emotion = GameConstant.StringToEmotion(trimParameter);
                    }
                }
            }
        }
        return emotion;
    }

    private int GetChoiceRelationParameter(Dialogue.Choice choice) {
        var relation = 0;
        if (choice.userData != null) {
            var parametersData = this.GetChoiceParameters(choice);
            var parameters = parametersData.Split(' ');
            foreach (var parameter in parameters) {
                if (parameter.StartsWith("R:")) {
                    int relationParameter;
                    var trimParameter = parameter.TrimStart('R', ':');
                    if (!Int32.TryParse(trimParameter, out relationParameter)) {
                        Debug.Log("DIALOG CONTROLLER :: Error in relation parameter value (" + GameLogicManager.currentCharacter.characterName + ")");
                    }
                    relation += relationParameter;
                }
            }
        }
        return relation;
    }

    private bool TriggerTransitionFade(Dialogue.Choice choice) {
        //Debug.Log("trasision fade");
        if (choice.userData != null) {
            var parametersData = this.GetChoiceParameters(choice);

            if (parametersData.Contains('F')) {
                Debug.Log("fade");
                FadeManager startOptions = GameObject.Find("Canvas").GetComponent<FadeManager>();
                startOptions.fadeOutImage.raycastTarget = true;
                StartCoroutine(startOptions.FadeCanvasGroupAlpha(0f, 1f));
                return true;
            }
        }
        return false;
    }

    private bool IsItAnEnd(Dialogue.Choice choice) {
        if (choice.userData != null) {
            var parametersData = this.GetChoiceParameters(choice);
            if (parametersData.Contains("End:")) {
                Debug.Log("End");
                return this.SetEndNumber(choice);
            }
        }
        return false;
    }

    private void TriggerEnd() {
        GameObject.Find("Menu UI").GetComponent<StartOptions>().Fade(GameConstant.END_SCENE_NUMBER);
    }

    private bool SetEndNumber(Dialogue.Choice choice) {
        var parametersData = this.GetChoiceParameters(choice);
        int endNumber = 0;
        var trimParameter = parametersData.TrimStart('E', 'n', 'd', ':');
        if (!Int32.TryParse(trimParameter, out endNumber)) {
            Debug.Log("DIALOG CONTROLLER :: Error in relation parameter value (" + GameLogicManager.currentCharacter.characterName + ")");
        }
        GameLogicManager.endResult = endNumber;
        return true;
    }

    void UpdateCharacterData(Dialogue.Choice choice) {
        if (!this.IsItAnEnd(choice)) {
            TriggerTransitionFade(choice);
            GameLogicManager.currentCharacter.emotion = this.GetChoiceEmotionParameter(choice);
            GameLogicManager.currentCharacter.relation += this.GetChoiceRelationParameter(choice);
        }
        else {
            this.TriggerEnd();
        }
    }

    void ButtonHandler(Dialogue.Choice choice) {
        fonduChoix.SetActive(false);
        foreach (Transform transform in choiceContainer.transform) {
            Destroy(transform.gameObject);
        }
        UpdateCharacterData(choice);
        dialogue.PickChoice(choice);
        choicePending = false;
        SetDialogue();
    }

    public void SetContent(string text) {

        var processedText = text.Replace(GameConstant.PLAYER_NAME_TEMPLATE, GameLogicManager.playerName);
        Text contentText = contentGO.GetComponent<Text>();
        contentText.text = processedText;
    }

    public class Word
    {
        public string text;
        public int width;
        public int height;

        public Word(string word, int width,int height)
        {
            this.text = word;
            this.width = width;
            this.height = height;
        }
    }

    public class Line
    {
        public string text;
        public int width;
        public int height;

        public Line(string line, int width, int height)
        {
            this.text = line;
            this.width = width;
            this.height = height;
        }
    }

    public class TextEngine
    {
        public Vector2 bounds;
        public Text textObject;

        private Font font;
        private FontStyle fontStyle;
        private int fontSize;

        public TextEngine(Text textObject, Vector2 bounds)
        {
            this.bounds = bounds;
            this.textObject = textObject;
        }

        private List<Word> GetWords(string text)
        {
            font = textObject.font;
            fontStyle = textObject.fontStyle;
            fontSize = textObject.fontSize;
            textObject.font.RequestCharactersInTexture(text, fontSize, fontStyle);

            List<Word> words = new List<Word>();
            string formatedText = text.Replace("\n", " ");
            string[] splittedText = formatedText.Split(' ');

            CharacterInfo characterInfo = new CharacterInfo();

            foreach (string word in splittedText)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    int sum = 0;
                    int height = 0;
                    foreach (char c in word)
                    {
                        font.GetCharacterInfo(c, out characterInfo, fontSize, fontStyle);
                        sum += characterInfo.advance;
                        if (characterInfo.glyphHeight > height)
                            height = characterInfo.glyphHeight;
                    }
                    words.Add(new Word(word, sum, height));
                }
            }

            return words;
        }

        private List<Line> GetLines(string text)
        {
            List<Line> lines = new List<Line>();

            List<Word> words = GetWords(text);

            CharacterInfo characterInfo = new CharacterInfo();

            font.GetCharacterInfo(' ', out characterInfo, fontSize, fontStyle);
            int espace = characterInfo.advance;

            int sum = 0;
            int height = 0;
            string line = "";

            foreach (Word word in words)
            {
                //Cas premier mot
                if (string.IsNullOrEmpty(line))
                {
                    line += word.text;
                    sum = word.width;
                    if (word.height > height)
                        height = word.height;
                }
                //Cas place pour autre mot
                else if ((sum + word.width + espace) < bounds.x)
                {
                    line += " " + word.text;
                    sum += word.width + espace;
                    if (word.height > height)
                        height = word.height;
                }
                //Cas débordement.
                else if ((sum + word.width + espace) >= bounds.x)
                {
                    lines.Add(new Line(line, sum, height));
                    sum = word.width;
                    line = word.text;
                }
            }

            //Traitement fin
            lines.Add(new Line(line, sum, height));

            foreach (Line debugline in lines)
            {
                //Debug.Log("Line : " + debugline.text + " width : " + debugline.width + " height " + debugline.height);
            }

            return lines;
        }

        public List<string> GetVirtualNodes(string text)
        {
            List<string> virtualNodes = new List<string>();
            List<Line> lines = GetLines(text);
            int maxHeight = (int)(bounds.y * 1.1); //Offcet de 10% dont on peut se permettre de dépasser pour l'ajout de la dernière ligne (dépendant de la taille ? demande ajustements)
            //Debug.Log("maxHeight : " + maxHeight);

            int sum = 0;
            string node = "";
            foreach(Line line in lines)
            {
                //Cas premier mot
                if (string.IsNullOrEmpty(node))
                {
                    node += line.text;
                    sum = line.height;
                }
                //Cas place pour autre mot
                else if ((sum + line.height) < maxHeight)
                {
                    node += "\n" + line.text;
                    sum += line.height;
                }
                //Cas débordement.
                else if ((sum + line.height) >= maxHeight)
                {
                    virtualNodes.Add(node);
                    sum = line.height;
                    node = line.text;
                }
            }

            //Traitement fin
            virtualNodes.Add(node);

            foreach(string debugNode in virtualNodes)
            {
                Debug.Log(debugNode);
            }

            return virtualNodes;
        }
    }

    public void SplitIntoVirtualNodes(string text)
    {
        RectTransform textTransform = contentGO.GetComponent<RectTransform>();
        float width = textTransform.sizeDelta.x;
        float height = textTransform.sizeDelta.y;

        Text textComponent = contentGO.GetComponent<Text>();
        TextEngine textEngine = new TextEngine(textComponent, new Vector2(width,height));
        List<string> virtualNodes = textEngine.GetVirtualNodes(text);
        foreach(string node in virtualNodes)
        {
            vNodes.Enqueue(node);
        }
    }

    public void ClearContent() {
        Text contentText = contentGO.GetComponent<Text>();
        contentText.text = "";
    }

    void SetName(string name) {

        Text nameText = nameGO.GetComponent<Text>();
        nameText.text = name;
    }

    void OpenDialogBox() {
        //set animation here
    }

    // Update is called once per frame
    void Update() {
        if (textAnimator.finished && !choicePending) {
            clickFeedback.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (dialogue.GetChoices() == null || dialogue.GetChoices().Length == 0) {
            clickFeedback.GetComponentInChildren<Text>().text = "Fin";
        }
        if (!textAnimator.finished) {
            textAnimator.finished = true;
            clickFeedback.SetActive(true);
        }
        else if (!choicePending) {
            //setName(dialogue.GetChoices()[0].speaker);
            SetDialogue();
        }
    }
}

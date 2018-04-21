using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class XMLLoader : MonoBehaviour {

    public TextAsset xmlFile;
    
	// Use this for initialization
	void Start () {
        ParseXML();
	}
	
	void ParseXML()
    {
        XDocument xDoc = XDocument.Parse(xmlFile.text);
        Debug.Log(xDoc);
        
    }
}

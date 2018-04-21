using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	public List<Node> children;

	public Node(){
        children = new List<Node>();
	}

	public void AddChild(Node node){
        this.children.Add(node);
	}

    public Node GetChild(int id) {
        return this.children[id];
    }

}

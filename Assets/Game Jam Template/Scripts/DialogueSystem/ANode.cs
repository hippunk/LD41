using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	public List<Node> children;
    public int constraint;

	/*public Node(List<Node> value){
		children = value;
	}*/

	public void AddChild(Node node){
        this.children.Add(node);
	}

    public void GetNext(ref List<Node> next) {

    }
}

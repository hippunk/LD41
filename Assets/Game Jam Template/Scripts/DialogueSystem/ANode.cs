using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANode {
	public List<ANode> next;

	public ANode(List<ANode> value){
		next = value;
	}

	public abstract bool isOK ();

	public ANode getNext(int id){
		if (next.Count >= id) {
			return null;
		}
		return next [id];
	}
}

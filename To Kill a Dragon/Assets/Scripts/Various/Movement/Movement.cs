using UnityEngine;
using System.Collections;

/**
 * Line class
 * Basic Node for NPC/cutscene movement; has two parts and a link to the next node
 * int direction: Range 0 - 3, determines what direction the line is pointing (0 -> down, 1 -> right, 2 -> up, 3 -> left)
 * float length: determines length of the line
 * Line nextLine: link to the next node
 * **/
public class Line {
	int direction;
	float length;

	Line nextLine;
	
	public Line (float deg, float len) {
				direction = (int)(deg / 90) % 4;
				length = len;
		nextLine = null;
		}

	public int getDirection () {
				return direction;
		}

	public float getLength () {
				return length;
		}

	#region Linked List Region

	public void setNext (Line nL) {
				nextLine = nL;
		}

	public Line getNext () {
				return nextLine;
		}

	#endregion

	#region Overrides Region

	public override string ToString () {
				return "Line in direction " + direction + " with length " + length;
		}

	public override bool Equals (object obj)
	{
		if (obj is Line) {
						Line temp = (Line)obj;
						return this.getLength () == temp.getLength () && 
								this.getDirection () == temp.getDirection ();
				} else {
						return base.Equals (obj);
				}
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	#endregion
}

/**
 * Path class
 * Linked List for use with the Line class; has a start Line, end Line, and current Line. Has count for number of lines
 * Line firstLine: the first Line in the path
 * Line lastLine: the last Line in the path
 * Line curLine: the current Line in the path
 * int count: number of Lines in the path
 * **/
public class Path {
	Line firstLine;
	Line lastLine;

	Line curLine;

	int count;

	/**
	 * Initializes the Path
	 * firstLine, lastLine, curLine set to null; count set to 0
	 * **/
	public Path () {
				firstLine = lastLine = null;
				curLine = null;
				count = 0;
		}

	/**
	 * Get for firstLine
	 * Returns: firstLine
	 * **/
	public Line GetFirst () {
				return firstLine;
		}

	/**
	 * Get for lastLine
	 * Returns: lastLine
	 * **/
	public Line GetLast () {
				return lastLine;
		}

	/**
	 * Get for curLine
	 * Returns: curLine
	 * **/
	public Line GetCurrent () {
				return curLine;
		}

	/**
	 * Set for curLine
	 * Parameter: Line to set curLine to
	 * Returns: nothing
	 * **/
	public void SetCurrent (Line l) {
				curLine = l;
		}

	/**
	 * Inserts a Line at the end of the path
	 * Parameter: Line l
	 * Result: count incremented by one, l inserted at end of path
	 * **/
	public void InsertLineEnd (Line l) {
				if (firstLine == null) {
						firstLine = l;
						lastLine = l;

						lastLine.setNext (null);
				} else {
						lastLine.setNext (l);
						lastLine = l;
				}
				count++;
		}

	/**
	 * Inserts a Line at the front of the path
	 * Parameter: Line l
	 * Result: If l is not null, count incremented by one, l inserted at front of path. Return true.
	 * Result: If l is null, return false.
	 * **/
	public bool InsertLineFront (Line l) {
				if (l == null) {
						return false;
				}
				if (firstLine == null) {
						firstLine = l;
						lastLine = l;

						lastLine.setNext (null);
				} else {
						Line tempLine = firstLine;
						firstLine = l;
						firstLine.setNext (tempLine);
				}
				count++;
				return true;
		}

	/**
	 * Removes a specified Line
	 * Parameter: Line l
	 * Result: If l was found, it was removed and count decremented by one; return true. Otherwise, no change; return false
	 * **/
	public bool RemoveLine (Line l) {
				if (l == null) {
						return false;
				}
				if (firstLine != null && firstLine.Equals (l)) {
						return RemoveFirst ();
				}

				Line tempLine = firstLine;

				while (tempLine != null) {
						if (tempLine.getNext () != null && tempLine.getNext ().Equals (l)) {
								tempLine.setNext (tempLine.getNext ().getNext ());
								return true;
						}
						tempLine = tempLine.getNext ();
				}
				return false;
		}

	/**
	 * Removes the first Line
	 * Result: If firstLine was null, no change; return false.
	 * Otherwise, firstLine shifted to next in path and count decremented by one; return true.
	 * **/
	public bool RemoveFirst () {
				if (firstLine == null) {
						return false;
				} else {
						firstLine = firstLine.getNext ();
						count--;
						return true;
				}
		}

	/**
	 * Removes the last Line
	 * Result: If lastLine was null, no change; return false.
	 * Otherwise, lastLine set to previous in path and count decremented by one; return true.
	 * **/
	public bool RemoveLast () {
				if (lastLine == null) {
						return false;
				} else {
						Line tempLine = firstLine;
						if (tempLine == lastLine) {
								firstLine = lastLine = null;
								count--;
								return true;
						}
						while (tempLine != null) {
								if (tempLine.getNext ().Equals (lastLine)) {
										lastLine = tempLine;
										lastLine.setNext (null);
										count--;
										return true;
								}
								tempLine = tempLine.getNext ();
						}
						return false;
				}
		}

	/**
	 * Gets the length of the current line segment
	 * Returns: -1 if curLine is null, length of curLine otherwise
	 * **/
	public float GetLength () {
				if (curLine == null) {
						return -1;
				} else {
						return curLine.getLength ();
				}
		}

	/**
	 * Sets curLine to the next line in the path.
	 * Returns: true if curLine is now the next line. false if curLine was null.
	 * **/
	public virtual bool NextLine () {
				if (curLine == null) {
						return false;
				} else {
						curLine = curLine.getNext ();
						return true;
				}
		}

	/**
	 * Debug purposes: prints out the lines currently in the path
	 * Result: path printed out in Debug.Log
	 * **/
	public void ShowPath () {
				Line tempLine = firstLine;
				while (tempLine != null) {
						Debug.Log (tempLine.ToString());
						tempLine = tempLine.getNext ();
				}
		}

	/**
	 * Clears the path, setting everything to null.
	 * Result: firstLine = lastLine = curLine = null; count = 0.
	 * **/
	public void ClearPath () {
				firstLine = lastLine = curLine = null;
				count = 0;
		}

	/**
	 * Returns whether the path is empty
	 * Result: true if empty, false if at least one line
	 * Throws: throws ArithmeticException if count is negative (theoretically impossible)
	 * **/
	public bool IsEmpty () {
				if (count < 0) {
						throw new System.ArithmeticException ("Somehow, count is negative");
				} else if (count == 0) {
						return true;
				} else {
						return false;
				}
		}

	/**
	 * Starts the path at the beginning.
	 * Result: curLine is equal to firstLine
	 * **/
	public void Start() {
				curLine = firstLine;
		}
}

/**
 * LoopedPath class
 * Linked List for use with the Line class
 * Inherits from Path class.
 * **/
public class LoopedPath : Path {
	public LoopedPath () {
	}

	/**
	 * Overrides Path's NextLine(). When the current line is the last line, current gets set to first line.
	 * Returns: true if curLine is now the next line. false if curLine was null.
	 * **/
	public override bool NextLine ()
	{
				if (GetCurrent () == null) {
						return false;
				} else {
						if (GetCurrent () == GetLast ()) {
								SetCurrent (GetFirst ());
						} else {
								SetCurrent (GetCurrent ().getNext ());
						}
						return true;
				}
		}

	/**
	 * Checks to see if the path is a loop
	 * Result: 0 if the path is empty, 1 if the path loops. -1 if it doesn't.
	 * **/
	public float CheckPath () {
		if (IsEmpty ()) {
			return 0;
		}

		Line temp = GetCurrent ();
		float xDist = 0, yDist = 0;

		while (temp != null) {
						switch (temp.getDirection ()) {
						case 0:
								yDist -= temp.getLength ();
								break;
						case 1:
								xDist += temp.getLength ();
								break;
						case 2:
								yDist += temp.getLength ();
								break;
						case 3:
								xDist -= temp.getLength ();
								break;
						default:
								Debug.Log ("Somehow an impossible thing happened.");
								break;
						}
						temp.getNext ();
				}

		if (xDist + yDist == 0) {
						return 1;
				} else {
						return -1;
				}
	}
}

/**
 * MARKED FOR DELETION: TESTING ONLY
 * **/
public class TestClass {
	public TestClass () {
				test ();
		}

	void test(){
				LoopedPath testPath = new LoopedPath ();

				testPath.InsertLineEnd (new Line (90, 5));

				Debug.Log ("Test one.");
				testPath.RemoveLast ();
				testPath.ShowPath ();

				testPath.InsertLineEnd (new Line (270, 5));
				testPath.InsertLineFront (new Line (0, 3));
				testPath.InsertLineEnd (new Line (180, 3));

				Debug.Log ("Test two.");
				testPath.RemoveFirst ();
				testPath.RemoveLast ();
				testPath.InsertLineEnd (new Line (90, 6));
				testPath.ShowPath ();

				Debug.Log ("Test three.");
				testPath.InsertLineFront (null);
				testPath.ShowPath ();

				Debug.Log ("Test four.");
				testPath.RemoveLine (null);
				testPath.ShowPath ();

				Debug.Log ("Test five.");
				testPath.RemoveLine (new Line (270, 5));
				testPath.ShowPath ();

				Debug.Log ("Test six.");
				testPath.ClearPath ();
				testPath.ShowPath ();

				Debug.Log ("Test seven.");
				testPath.InsertLineEnd (new Line (90, 5));
				testPath.InsertLineEnd (new Line (0, 2));
				testPath.InsertLineEnd (new Line (270, 3));
				testPath.InsertLineEnd (new Line (180, 1));
				testPath.InsertLineEnd (new Line (270, 2));
				testPath.InsertLineEnd (new Line (180, 1));
				testPath.ShowPath ();
				Debug.Log ("The path " + (testPath.CheckPath () > 0 ? "loops." : "doesn't loop."));
		}
}
﻿using UnityEngine;
using System.Collections;

public class CutscenePathManager : MonoBehaviour {

	public Transform[] pathPoints;
	private CutsceneMovementController MovementController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/**
	 * <para>Starts the given named participant on the path.</para>
	 * <para>Note: Participant must implement CutsceneParticipant interface</para>
	 * <param name="ParticipantName">Name of participant</param>
	 * **/
	public void BeginPath (CutsceneMovementController MovementController, string ParticipantName) {
		this.MovementController = MovementController;

		CutsceneParticipant participant = GameObject.Find (ParticipantName).GetComponent<CutsceneParticipant> ();
		if (participant == null) {
			NotifyComplete();
			return;
		}

		participant.SetPath (pathPoints, this);
	}

	/**
	 * <para>Notifies the Movement controller that the path is complete</para>
	 * **/
	public void NotifyComplete() {
		MovementController.NotifyPathComplete ();
	}
}
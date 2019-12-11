using System;
using System.Collections.Generic;
using System.Linq;
using entities;
using UnityEngine;
namespace Logic
{
    public class Recorder : MonoBehaviour
    {
        public Recorder()
        {
        }

        private List<PlayerAction> PlayerActions { get; set; }
        private bool isRecording;
        
        private bool isPlaying;

        private void Start()
        {
            
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.R) && !isPlaying)
            {
                isRecording = true;
                PlayerActions = new List<PlayerAction>();
            }
            if (Input.GetKey(KeyCode.P) && !isRecording)
            {
                isPlaying = true;
            }

            Record();
            
        }

        public void Record()
        {   
            if (isRecording)
            {
                
                Invoke("StopRecord", 5f);
            
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        print("thrusting record");
                        StartAction(ActionType.Thrust);
                    }
                    else
                    {
                        StartAction(ActionType.RotateRight);
                    }
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        print("thrusting record");
                        StartAction(ActionType.Thrust);
                    }
                    else
                    {
                        StartAction(ActionType.RotateLeft);
                    }
                }
                if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
                {
                    print("thrusting record");
                    StartAction(ActionType.Thrust);
                }
            }
        }

        public List<PlayerAction> GetPlayerActions()
        {
            return PlayerActions;
        }

        private void StopRecord()
        {
            isRecording = false;
        }

        private void StopPlay()
        {
            isPlaying = false;
        }

        private void StartAction(ActionType actionType)
        {
            var playerAction = new PlayerAction()
            {
                ActionType = actionType,
                StartTime = DateTime.Now
            };
            PlayerActions.Add(playerAction);
            
        }

        private void EndAction(ActionType actionType)
        {
            PlayerActions
                .OrderByDescending(pa => pa.StartTime)
                .FirstOrDefault(pa => pa.ActionType == actionType)
                .EndTime = DateTime.Now;
            
        }
    }
}


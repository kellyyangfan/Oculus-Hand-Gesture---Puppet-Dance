using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture {
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
}

public class GestureDetection : MonoBehaviour
{
    public float threshold = 0.1f;
    public int gestureNumMax = 3;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    public bool playMode = false;

    public GameObject Avatar;
    public GameObject DebugText;
    public GameObject GestureText;
    public GameObject SaveBtn;
    public GameObject SwitchModeBtn;

    private GameObject Board;
    private GameObject BoardDes;
    private List<OVRBone> fingerBones;
    private Gesture previousGesture;
    private int gestureNum;
    
    // Start is called before the first frame update
    void Start()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
        //DebugLog.DebugLine = "Starting!!\n";
        DebugText.GetComponent<TextMesh>().text = "Application Started!";
        gestureNum = 0;

        Board = GameObject.Find("Board");
        BoardDes = GameObject.Find("BoardDes");

    }

    // Update is called once per frame
    void Update()
    {

        if (gestureNum == gestureNumMax) {
            SwitchModeBtn.SetActive(true);
        }

        if (playMode) {
            SaveBtn.SetActive(false);
            SwitchModeBtn.SetActive(false);
            Board.transform.position = BoardDes.transform.position;
            Board.transform.rotation = BoardDes.transform.rotation;

            Gesture currentGesture = Recognize();
            bool hasRecognized = !currentGesture.Equals(new Gesture());
            //check if there's new gesture
            if (hasRecognized && !currentGesture.Equals(previousGesture)) {
                //Debug.Log("New Gesture Found:" + currentGesture.name);
                //DebugLog.DebugLine += "New Gesture Found:" + currentGesture.name + "\n";
                DebugText.GetComponent<TextMesh>().text = "New Gesture Found: " + currentGesture.name;

                //Action when detect gesture
                if (currentGesture.name == "Gesture1") {
                    Avatar.GetComponent<DwarfAnim>().Gesture1();
                }
                if (currentGesture.name == "Gesture2")
                {
                    Avatar.GetComponent<DwarfAnim>().Gesture2();
                }
                if (currentGesture.name == "Gesture3")
                {
                    Avatar.GetComponent<DwarfAnim>().Gesture3();
                }


                previousGesture = currentGesture;
                currentGesture.onRecognized.Invoke();
            }
        }
    }

    public void Save()
    {
        if (gestureNum < gestureNumMax) {
            gestureNum++;
            Debug.Log("save");
            DebugText.GetComponent<TextMesh>().text = "Save new Gesture!";
            Gesture g = new Gesture();
            //g.name = "Gesture" + (fingerBones.Count + 1);
            g.name = "Gesture" + (gestures.Count + 1);
            List<Vector3> data = new List<Vector3>();

            foreach (var bone in fingerBones)
            {
                data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
            }

            g.fingerDatas = data;
            gestures.Add(g);

            GestureText.GetComponent<TextMesh>().text = "";
            foreach (var gName in gestures)
            {
                GestureText.GetComponent<TextMesh>().text += gName.name + "\n";
            }

        }
        else {
            DebugText.GetComponent<TextMesh>().text = "Save at most " + gestureNumMax + " gestures.";

        }
    }

    Gesture Recognize() {
        Gesture currentgesture = new Gesture();
        float currentMin = Mathf.Infinity;

        foreach (var gesture in gestures) {

            float sumDistance = 0;
            bool isDiscarded = false;

            for (int i = 0; i < fingerBones.Count; i++) {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);

                if (distance > threshold) {
                    isDiscarded = true;
                    break;
                }

                sumDistance += distance;
            }

            if (!isDiscarded && sumDistance < currentMin) {
                currentMin = sumDistance;
                currentgesture = gesture;
            
            }
        }

        return currentgesture;
    }
}

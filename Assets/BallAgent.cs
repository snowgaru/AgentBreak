using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class BallAgent :Agent
{
    void Start()
    {
        Time.timeScale = 5;
    }

    public Game game;
    public float power;
    public Rigidbody rb;
    public int timer;
    Vector3 dir = Vector3.zero;
    private int maxPoint = 2000;

    public List<GameObject> breakList = new List<GameObject>();

    public Transform pos;
    public override void Initialize()
    {
        game.StartSetting();
    }

    public override void OnEpisodeBegin()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.gameObject.transform.position = pos.position;
        breakList.ForEach((x) => x.SetActive(true));
        game.StartSetting();
        StartCoroutine(Coroutines());
    }

    private IEnumerator Coroutines()
    {
        yield return new WaitForSeconds(100f);
        game.TextSetting();
        EndEpisode();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float h = Mathf.Clamp(actions.ContinuousActions[0], -1.0f, 1.0f);
        float v = Mathf.Clamp(actions.ContinuousActions[1], -1.0f, 1.0f);
        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
        Debug.Log(h + v);
        rb.AddForce(dir* power, ForceMode.Impulse);

        //SetReward(-0.001f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        actionsOut.ContinuousActions.Array[0] = Input.GetAxis("Horizontal");
        actionsOut.ContinuousActions.Array[1] = Input.GetAxis("Vertical");
    }

    //public void Update()
    //{ 
    //    if (Input.GetKey(KeyCode.LeftArrow) == true)
    //    {
    //        rb.AddForce(Vector3.left * power, ForceMode.Impulse);
    //    }

    //    if (Input.GetKey(KeyCode.RightArrow) == true)
    //    {
    //        rb.AddForce(Vector3.right * power, ForceMode.Impulse);
    //    }

    //    if (Input.GetKey(KeyCode.UpArrow) == true)
    //    {
    //        rb.AddForce(Vector3.forward * power, ForceMode.Impulse);
    //    }

    //    if (Input.GetKey(KeyCode.DownArrow) == true)
    //    {
    //        rb.AddForce(Vector3.back * power, ForceMode.Impulse);
    //    }
    //}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Break"))
        {
            collision.gameObject.SetActive(false);
            SetReward(75);
            game.point += 100;
            if(game.point == maxPoint)
            {
                SetReward(80);
                EndEpisode();
            }
            game.TextSetting();
        }
        if (collision.transform.CompareTag("Destroy"))
        {
            SetReward(-100);
            Debug.Log("¤±¤¤¤·¤©");
            EndEpisode();
        }
    }
}

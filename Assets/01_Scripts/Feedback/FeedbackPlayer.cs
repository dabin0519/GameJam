using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feedbacks;

    private void Awake()
    {
        _feedbacks = new List<Feedback>();
        GetComponents<Feedback>(_feedbacks);  
    }

    public void PlayFeedback()
    {
        foreach(var f in _feedbacks)
        {
            f.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach(var f in _feedbacks)
        {
            f.FinishFeedback();
        }
    }
}

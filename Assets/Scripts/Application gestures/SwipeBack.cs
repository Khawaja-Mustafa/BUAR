using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwipeBack : MonoBehaviour
{
    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    [SerializeField]
    private float minSwipeDistance = 20.0f;

    [SerializeField]
    private float maxSwipeTime = 0.5f;

    [SerializeField]
    private float swipeStartEdgeDistance = 20.0f;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    fingerStartTime = Time.time;
                    fingerStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    float fingerSwipeTime = Time.time - fingerStartTime;
                    Vector2 fingerSwipeDist = touch.position - fingerStartPos;

                    if (fingerSwipeTime < maxSwipeTime &&
                        fingerSwipeDist.magnitude > minSwipeDistance)
                    {
                        float edgeDistance = Mathf.Min(fingerStartPos.x, Screen.width - fingerStartPos.x);
                        if (edgeDistance <= swipeStartEdgeDistance && Mathf.Abs(fingerSwipeDist.x) > Mathf.Abs(fingerSwipeDist.y))
                        {
                            //Further Backsipe conditions are needed to be added.
                            if(SceneManager.GetActiveScene().name == "StudentLogin 1")
                            {
                                SceneManager.LoadScene("StartupPage 1");
                            }
                            else if(SceneManager.GetActiveScene().name == "VisitorLogin")
                            {
                                SceneManager.LoadScene("StartupPage 1");
                            }
                            else if (SceneManager.GetActiveScene().name == "FacultyLogin")
                            {
                                SceneManager.LoadScene("StartupPage 1");
                            }
                            else
                            {
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                            }
                            
                        }
                    }
                    break;
            }
        }
    }
}

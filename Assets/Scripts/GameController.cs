using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static float portalFaceState = 0; // 0,1→oldSpace進入不可 2→oldSpace進入可能
    [SerializeField] Camera portalCam;
    [SerializeField] Camera PortalExitCam;
    [SerializeField] GameObject window;
    [SerializeField] Text metabagu;
    [SerializeField] Texture bugTexture;
    [SerializeField] Texture oldSpace;
    Camera mainCam;
    Ray ray;
    RaycastHit hit;
    Vector3 pos;
    float touchTime;
    float metabagusuu;
    Color transparentColor;

    void Start()
    {
        mainCam = Camera.main;
    }
    void Update()
    {
        // portalカメラとportalExitカメラの動きをを常にMainカメラと同期
        portalCam.transform.position = mainCam.transform.position;
        portalCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up);
        PortalExitCam.transform.position = mainCam.transform.position;
        PortalExitCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up);

        //画面操作
        if (Application.isEditor)
        {
            if (Input.GetMouseButton(0))
            {
                LongTouch();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                pos = Input.mousePosition;
                Press(pos);
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                pos = touch.position;

                if (touch.phase == TouchPhase.Stationary)
                {
                    LongTouch();
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    Press(pos);
                }
            }
        }
    }

    //画面タップ
    void Press(Vector3 pos)
    {
        ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit))
        {
            if (touchTime < 2)
            {
                // キラバグを押下
                if (hit.collider.tag == "Metabagu")
                {
                    hit.collider.gameObject.SetActive(false);
                    metabagusuu += 2.0f;
                    metabagu.text = "メタバグ数：" + metabagusuu + "メタ";
                }
                else if (hit.collider.tag == "PortalFace")
                {
                    Renderer renderer = hit.collider.gameObject.GetComponent<Renderer>();
                    switch (portalFaceState)
                    {
                        case 0:
                            renderer.material.SetTexture("_MainTex", bugTexture);
                            transparentColor = Color.white;
                            transparentColor.a = 0.0f;
                            renderer.material.color = transparentColor;
                            portalFaceState = 1;
                            break;
                        case 1:
                            renderer.material.SetTexture("_MainTex", oldSpace);
                            portalFaceState = 2;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        touchTime = 0;
    }

    // 画面長押し
    void LongTouch()
    {
        touchTime += Time.fixedDeltaTime;
        if (touchTime > 1)
        {
            // ウィンドウを表示切替
            window.SetActive(!window.activeSelf);
            touchTime = 0;
        }
    }
}

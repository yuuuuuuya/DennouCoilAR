using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Portal : MonoBehaviour
{
    static bool enteredOldSpace = false; // oldSpaceに入っている→true
    [SerializeField] Camera portalCam;
    [SerializeField] Camera PortalExitCam;
    [SerializeField] GameObject PortalFace;
    [SerializeField] GameObject MaskFace;
    Camera mainCam;
    PostProcessLayer mainCamPPLayer;
    PostProcessLayer portalCamPPLayer;

    void Start()
    {
        mainCam = Camera.main;
        mainCamPPLayer = mainCam.GetComponent<PostProcessLayer>();
        portalCamPPLayer = portalCam.GetComponent<PostProcessLayer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") && GameController.portalFaceState == 2)
        {
            //portalからカメラへの方向ベクトル
            Vector3 dictance = transform.InverseTransformDirection(other.transform.position - transform.position);

            // portalの正面方向から衝突時のみ
            if (dictance.z < 0)
            {
                // メインカメラで、OldSpaceレイヤーの表示・非表示切替
                mainCam.cullingMask ^= 1 << LayerMask.NameToLayer("OldSpace");
                mainCamPPLayer.volumeLayer ^= 1 << LayerMask.NameToLayer("OldSpace");

                // サブカメラで、OldSpaceレイヤーの表示・非表示切替
                portalCam.cullingMask ^= 1 << LayerMask.NameToLayer("OldSpace");
                portalCamPPLayer.volumeLayer ^= 1 << LayerMask.NameToLayer("OldSpace");

                // portalFaceとMaskFaceの切替
                if (enteredOldSpace)
                {
                    PortalExitCam.enabled = false;
                    PortalFace.SetActive(true);
                    MaskFace.SetActive(false);
                    enteredOldSpace = false;
                }
                else
                {
                    PortalExitCam.enabled = true;
                    PortalFace.SetActive(false);
                    MaskFace.SetActive(true);
                    enteredOldSpace = true;
                }
            }
        }
    }
}

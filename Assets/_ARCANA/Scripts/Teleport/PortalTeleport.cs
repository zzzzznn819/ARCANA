using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class PortalTeleport : MonoBehaviour
{
    [Header("傳送目的地")]
    public Transform destination;

    // 全部傳送門共用冷卻時間，避免連續傳送
    private static float nextTeleportTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < nextTeleportTime)
            return;

        if (other.gameObject.name != "HeadTrigger")
            return;

        XROrigin xrOrigin = other.GetComponentInParent<XROrigin>();

        if (xrOrigin == null || destination == null)
            return;

        if (xrOrigin.Camera == null)
            return;

        nextTeleportTime = Time.time + 1f;

        // 保留目前頭部高度，改變水平位置
        Vector3 targetPosition = new Vector3(
            destination.position.x,
            xrOrigin.Camera.transform.position.y,
            destination.position.z
        );

        xrOrigin.MoveCameraToWorldLocation(targetPosition);

        Debug.Log("傳送成功：" + destination.name);
    }
}

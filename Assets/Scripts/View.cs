using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    #region Follow

    void FixedUpdate()
    {
        Vector3 playerPosition = Manager.Instance.m_player.transform.position;
        Vector3 cameraPosition = transform.position;
        transform.position += new Vector3(playerPosition.x - cameraPosition.x, playerPosition.y - cameraPosition.y, 0) * 0.3f;
    }

    #endregion
}

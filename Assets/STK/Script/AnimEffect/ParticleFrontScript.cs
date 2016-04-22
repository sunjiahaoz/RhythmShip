﻿using UnityEngine;
using System.Collections;
namespace sunjiahaoz.AnimEffect
{
    public class ParticleFrontScript : MonoBehaviour
    {
        public int renderQueue = 4000;//在Transparent前面

        void Start()
        {
            SetRenderQueue(transform);
        }
        void SetRenderQueue(Transform currentTransform)
        {
            if (currentTransform.GetComponent<Renderer>() != null && currentTransform.GetComponent<Renderer>().sharedMaterial != null)
            {
                currentTransform.GetComponent<Renderer>().material.renderQueue = renderQueue;
            }
            if (currentTransform.childCount != 0)
            {
                foreach (Transform child in currentTransform)
                {
                    SetRenderQueue(child);
                }
            }
        }

        [ContextMenu("立即执行")]
        void ApplyNow()
        {
            SetRenderQueue(transform);
        }
    }
}

/**********************************************************************************
* Blueprint Reality Inc. CONFIDENTIAL
* 2019 Blueprint Reality Inc.
* All Rights Reserved.
*
* NOTICE:  All information contained herein is, and remains, the property of
* Blueprint Reality Inc. and its suppliers, if any.  The intellectual and
* technical concepts contained herein are proprietary to Blueprint Reality Inc.
* and its suppliers and may be covered by Patents, pending patents, and are
* protected by trade secret or copyright law.
*
* Dissemination of this information or reproduction of this material is strictly
* forbidden unless prior written permission is obtained from Blueprint Reality Inc.
***********************************************************************************/

#if UNITY_STANDALONE_WIN
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueprintReality.MixCast {
    [System.Serializable]
    public class FrameDelayQueue<T> : IDisposable {
        [Serializable]
        public class Frame<D>
        {
            public float Timestamp { get; protected set; }
            public D Data { get; protected set; }

            public Frame(float timestamp, D data)
            {
                Timestamp = timestamp;
                Data = data;
            }

            public void UpdateTimestamp(float newTimestamp)
            {
                Timestamp = newTimestamp;
            }
        }

        //IN MILLISECONDS
        public float delayDuration = 0;

        public List<Frame<T>> unusedFrames = new List<Frame<T>>();
        public List<Frame<T>> usedFrames = new List<Frame<T>>();

        private Func<T> allocFrameData;
        private Action<T> releaseFrameData;

        public FrameDelayQueue(Func<T> allocFunc, Action<T> releaseFunc = null)
        {
            allocFrameData = allocFunc;
            releaseFrameData = releaseFunc;
        }
        public void Dispose()
        {
            if (releaseFrameData != null)
            {
                for (int i = 0; i < unusedFrames.Count; i++)
                {
                    releaseFrameData(unusedFrames[i].Data);
                }
                for (int i = 0; i < usedFrames.Count; i++)
                {
                    releaseFrameData(usedFrames[i].Data);
                }
            }
            unusedFrames.Clear();
            usedFrames.Clear();
        }


        public T OldestFrameData
        {
            get
            {
                return usedFrames[0].Data;
            }
        }

        public void Update()
        {
            while (usedFrames.Count > 1 && usedFrames[1].Timestamp < Time.realtimeSinceStartup - (delayDuration * 0.001f)) // realtime is seconds, delayDuration is ms
            {
                unusedFrames.Add(usedFrames[0]);
                usedFrames.RemoveAt(0);
            }
        }

        public Frame<T> GetNewFrame()
        {
            float timestamp = Time.realtimeSinceStartup;

            Frame<T> frame;
            if (unusedFrames.Count == 0)
            {
                frame = new Frame<T>(timestamp, allocFrameData());
            }
            else
            {
                frame = unusedFrames[unusedFrames.Count - 1];
                unusedFrames.RemoveAt(unusedFrames.Count - 1);
                frame.UpdateTimestamp(Time.realtimeSinceStartup);
            }

            
            usedFrames.Add(frame);
            return frame;
        }
	}
}
#endif

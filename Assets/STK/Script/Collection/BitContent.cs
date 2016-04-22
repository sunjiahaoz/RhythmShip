﻿using System;
using System.Collections.Generic;
using System.Text;

namespace sunjiahaoz.Collection
{
    public class BitContent
    {
        int[] m_pBitContent;		// 保存所有信息
        int m_nBitDataSize;	// 设置的需要保存的数据数量
        int m_nIntSize;			// 根据Init的数量计算出需要使用多少个int值
        int m_nMemBitSize = sizeof(int) * 8;		// int的字节数

        public void Init(int nObstacleSize)
        {
            // 存储障碍的数据的字节数
            //m_nMemBitSize = sizeof(int) * 8;

            // 障碍数量
            m_nBitDataSize = nObstacleSize;

            // 要使用多少个int值保存这些障碍
            m_nIntSize = nObstacleSize / m_nMemBitSize;
            if (nObstacleSize % m_nMemBitSize != 0)
            {
                m_nIntSize++;
            }
            if (m_nIntSize == 0 && nObstacleSize != 0)
            {
                m_nIntSize++;
            }

            // 保存障碍容器
            m_pBitContent = new int[m_nIntSize];
            // 初始化为全部为非障碍
            for (int i = 0; i < m_nIntSize; ++i)
            {
                m_pBitContent[i] = 0;
            }
        }

        public void SetData(int[] pObstacleData, int nDataSize)
        {
            if (m_pBitContent == null
                || nDataSize > m_nIntSize)
            {
                m_pBitContent = null;
                m_pBitContent = new int[nDataSize];
            }
            m_nIntSize = nDataSize;
            m_nBitDataSize = nDataSize * 32;

            for (int i = 0; i < nDataSize; ++i)
            {
                m_pBitContent[i] = pObstacleData[i];
            }
        }

        public int GetDataSize() { return m_pBitContent.Length; }
        public int GetDataByIndex(int nIndex)
        {
            if (nIndex < 0
                || nIndex >= m_pBitContent.Length)
            {
                return 0;
            }
            return m_pBitContent[nIndex];
        }

        public int SetValue(int nPos, bool bYes)
        {
            if (nPos >= m_nBitDataSize)		// 超出范围
            {
                return 0;
            }

            // 先计算出该障碍所处的 位 会在哪个数据里面
            int nIntPos = nPos / m_nMemBitSize;

            // 再计算在该数据的哪一位
            int nObPos = (nPos % m_nMemBitSize);
            int unTerrain = 0;
            if (bYes)
            {
                unTerrain = 1;
                unTerrain = unTerrain << nObPos;
                m_pBitContent[nIntPos] |= unTerrain;
            }
            else
            {
                unTerrain = 1;
                unTerrain = unTerrain << nObPos;
                unTerrain = ~unTerrain;
                m_pBitContent[nIntPos] &= unTerrain;
            }
            return 1;
        }

        public bool GetValue(int nPos)
        {
            if (nPos >= m_nBitDataSize)	// 超出范围
            {
                return false;
            }

            // 计算出该障碍所处的 位 会在哪个数据里面
            int nIntPos = nPos / m_nMemBitSize;
            // 计算该数据的哪一位
            int nObPos = (nPos % m_nMemBitSize);

            int index = 1;
            index <<= nObPos;

            index &= m_pBitContent[nIntPos];

            return (index != 0);
        }

        void Reset()
        {
            for (int i = 0; i < m_nIntSize; ++i)
            {
                m_pBitContent[i] = 0;
            }
        }
    }
}

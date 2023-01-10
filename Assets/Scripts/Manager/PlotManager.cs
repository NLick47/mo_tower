using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : Singleton<PlotManager>
{
    private Dictionary<int, int> _plotDictionary = new Dictionary<int, int>();

    public Dictionary<int, int> PlotDictionary { get => _plotDictionary; set => _plotDictionary = value; }

    /// <summary>
    /// ��ʼ������
    /// </summary>
    public void Init()
    {
        _plotDictionary = new Dictionary<int, int>
        {
            // 2 ��С͵����
            {1,1 },
            // 6 �����˾���
            {2,1 },
            // 7 �����˾���
            {3,1 },
            // 10 �����öӳ�����
            {4,1 },
            // 12 �����˾���
            {5,1 },
            // 15 �����˾���
            {6,1 },
            // 23 ��ǽ�ھ���
            {7,0 },
            // 31 ��ǽ�ھ���
            {8,1 },
            // 35 �������·����
            {9,1 },
            // 38 �����˾���
            {10,1 },
            // 39 �����˾���
            {11,1 },
            // 45 �����˾���
            {12,1 },
            // 47 �����˾���
            {13,1 },
            // 49 ��ħ������
            {14,1 },
            // 20 ����Ѫ�����
            {15,1 },
            // 40 ����ʿ�ӳ�����
            {16,1 },
            // 25 ���ʦ����
            {17,1 },
            // 26 �㹫������
            {18,1 },
        };
    }
}

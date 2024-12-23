using System;
using UnityEngine;

public class Example : MonoBehaviour
{
    //public Action<int> _action;

    private void Awake()
    {
        DoExample((int number, bool isGround) => Debug.Log($"������� ��������� ����� ������ ����� {number}, {isGround}"));
        DoExample((number, isGround) => Debug.Log($"������� ��������� ����� ������ ����� {number}, {isGround}"));
        //DoExample(number => Debug.Log($"������� ��������� ����� ������ ����� {number}"));

        //DoExampleFunc((int n1, int n2) => n1 > n2);
        //DoExampleFunc((n1, n2) => n1 > n2);
        DoExampleFunc((int number) => number > 0);
        DoExampleFunc((number) => number > 0);
        DoExampleFunc(number => number > 0);
    }

    public void DoExample(Action<int, bool> action)
    {
        action?.Invoke(3, default);
        //DoSomething(3);
    }

    public void DoExampleFunc(Func<int, bool> funk)
    {
        if (funk != null)
        {
            funk.Invoke(2);            
        }
    }

    private void DoSomething(int number) => Debug.Log($"������� ��������� {number}");

    private bool IsTrue(int n1, int n2) => n1 > n2;

}

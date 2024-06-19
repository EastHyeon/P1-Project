using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̱����� �ƴ� �����ڸ� �������� �ʴ´ٴ� ���� �����ϼ���.
///   `T myT = new T();`�� ���� �����ڸ� ������ ���մϴ�.
/// �̸� �����Ϸ��� �̱��� Ŭ������ `protected T () {}`�� �߰��ϼ���.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "'��(��) ���ø����̼��� ������ �� �̹� �����Ǿ����ϴ�." +
                    " �ٽ� �������� �ʽ��ϴ�. - null�� ��ȯ�մϴ�.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] ���� ũ�� �߸��Ǿ����ϴ�. " +
                            " - �̱����� �� �� �̻� �����մϴ�!" +
                            " ����� �ٽ� ���� ������ �ذ�� �� �ֽ��ϴ�.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        Debug.Log("[Singleton] " + typeof(T) +
                            " �� �ν��Ͻ��� ���� �ʿ��ϹǷ� '" + singleton +
                            "'��(��) �����Ǿ����ϴ�.");
                        GameObject obj = GameObject.Find("Managers");
                        if (obj != null)
                        {
                            singleton.transform.parent = obj.transform;
                        }
                    }

                    else
                    {
                        //Debug.Log("[Singleton] ����Ϸ��� �ν��Ͻ��� �̹� �����Ǿ��ֽ��ϴ�.: " + _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    private static bool IsDontDestroyOnLoad()
    {
        if (_instance == null)
        {
            return false;
        }
        // Object exists independent of Scene lifecycle, assume that means it has DontDestroyOnLoad set
        if ((_instance.gameObject.hideFlags & HideFlags.DontSave) == HideFlags.DontSave)
        {
            return true;
        }
        return false;
    }

    private static bool applicationIsQuitting = false;

    /// <summary>
    /// Unity�� ����Ǹ� ������Ʈ�� ������ ������ �Ҹ��ŵ�ϴ�.
    /// ��Ģ������ �̱����� ���ø����̼��� ����� ���� �Ҹ�˴ϴ�.
    /// �̱����� �Ҹ�� �Ŀ� ��ũ��Ʈ���� �ν��Ͻ��� ȣ���ϸ� 
    /// ���װ� �ִ� ��Ʈ ������Ʈ�� �����Ͽ� ������ ���� �����ְ� �˴ϴ�.
    /// ���ø����̼� ����� ������ �Ŀ��� ������ ���� �����ְ� �˴ϴ�. ���� �� ����!
    /// �׷��� ���װ� �ִ� ��Ʈ ������Ʈ�� �������� �ʵ��� �ϱ� ���� ���� ���Դϴ�.
    /// </summary>
    public void OnDestroy()
    {
        if (IsDontDestroyOnLoad())
        {
            applicationIsQuitting = true;
        }
    }
}

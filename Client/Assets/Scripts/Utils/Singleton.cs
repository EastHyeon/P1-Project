using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 싱글톤이 아닌 생성자를 방지하지 않는다는 점에 유의하세요.
///   `T myT = new T();`와 같은 생성자를 막지는 못합니다.
/// 이를 방지하려면 싱글톤 클래스에 `protected T () {}`를 추가하세요.
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
                    "'은(는) 애플리케이션을 종료할 때 이미 삭제되었습니다." +
                    " 다시 생성되지 않습니다. - null을 반환합니다.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] 무언가 크게 잘못되었습니다. " +
                            " - 싱글톤이 한 개 이상 존재합니다!" +
                            " 장면을 다시 열면 문제가 해결될 수 있습니다.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        Debug.Log("[Singleton] " + typeof(T) +
                            " 의 인스턴스가 씬에 필요하므로 '" + singleton +
                            "'이(가) 생성되었습니다.");
                        GameObject obj = GameObject.Find("Managers");
                        if (obj != null)
                        {
                            singleton.transform.parent = obj.transform;
                        }
                    }

                    else
                    {
                        //Debug.Log("[Singleton] 사용하려는 인스턴스가 이미 생성되어있습니다.: " + _instance.gameObject.name);
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
    /// Unity가 종료되면 오브젝트를 임의의 순서로 소멸시킵니다.
    /// 원칙적으로 싱글톤은 애플리케이션이 종료될 때만 소멸됩니다.
    /// 싱글톤이 소멸된 후에 스크립트에서 인스턴스를 호출하면 
    /// 버그가 있는 고스트 오브젝트를 생성하여 에디터 씬에 남아있게 됩니다.
    /// 애플리케이션 재생이 중지된 후에도 에디터 씬에 남아있게 됩니다. 정말 안 좋죠!
    /// 그래서 버그가 있는 고스트 오브젝트를 생성하지 않도록 하기 위해 만든 것입니다.
    /// </summary>
    public void OnDestroy()
    {
        if (IsDontDestroyOnLoad())
        {
            applicationIsQuitting = true;
        }
    }
}

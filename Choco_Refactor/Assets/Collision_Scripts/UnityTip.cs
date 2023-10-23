using UnityEngine;


public interface IUnityTip { }

[System.Serializable]
public class TestA : IUnityTip
{
    public float a;
    public float b;
}

[System.Serializable]
public class TestB : IUnityTip
{
    public string c;
    public GameObject d;
    public Rigidbody e;
}
public class UnityTip : MonoBehaviour
{
    [SerializeReference] public IUnityTip reference;

    [ContextMenu("Toggle Instance")]
    private void ToggleInstance()
    {
        if (reference is TestA)
        {
            reference = new TestB();
        }
        else
        {
            reference = new TestA();
        }
    }
}

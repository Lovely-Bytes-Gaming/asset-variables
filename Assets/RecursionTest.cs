using System.Threading.Tasks;
using UnityEngine;
using LovelyBytes.AssetVariables;

public class RecursionTest : MonoBehaviour
{
    [SerializeField] 
    private IntVariable variable;
    
    // Start is called before the first frame update
    void Awake()
    {
        variable.OnValueChanged += (_, newVal) =>
        {
            variable.Value++;
            Debug.Log(newVal);
        };
    }

    private async void Start()
    {
        await Task.Run(() =>
        {
            variable.Value = 42;
        });
    }
}

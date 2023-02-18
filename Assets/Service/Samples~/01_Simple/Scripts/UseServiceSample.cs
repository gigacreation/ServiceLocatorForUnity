using UnityEngine;

namespace GigaCreation.Tools.Service.Sample01
{
    public class UseServiceSample : MonoBehaviour
    {
        [SerializeField] private bool _tryGetServiceOnStart;
        [SerializeField] private bool _logIfServiceIsUnregistered;

        private void Start()
        {
            if (_tryGetServiceOnStart)
            {
                if (ServiceLocator.TryGet(out SampleService sampleService))
                {
                    sampleService.Bark();
                }
                else if (_logIfServiceIsUnregistered)
                {
                    Debug.Log("Fail to get SampleService.", gameObject);
                }
            }
        }
    }
}

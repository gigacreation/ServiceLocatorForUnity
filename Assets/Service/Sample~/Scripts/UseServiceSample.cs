using UnityEngine;

namespace GigaceeTools.Service.Sample
{
    public class UseServiceSample : MonoBehaviour
    {
        [SerializeField] private bool _tryGetServiceOnStart;
        [SerializeField] private bool _logIfServiceIsUnregistered;

        private void Start()
        {
            if (_tryGetServiceOnStart)
            {
                if (ServiceLocator.TryGet(out ISampleService sampleService))
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

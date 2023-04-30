using UnityEngine;

namespace GigaCreation.Tools.Service.Sample03
{
    public class UseServiceSample : MonoBehaviour
    {
        [SerializeField] private bool _tryGetServiceOnStart;
        [SerializeField] private bool _logIfServiceIsUnregistered;

        private void Start()
        {
            if (_tryGetServiceOnStart)
            {
                // Get a service via the base type.
                if (ServiceLocator.TryGet(out IBaseSampleService sampleService, true))
                {
                    sampleService.Bark();

                    // You cannot call this method of the derived interface.
                    // sampleService.Scratch();
                }
                else if (_logIfServiceIsUnregistered)
                {
                    Debug.Log("Fail to get SampleService.", gameObject);
                }
            }
        }
    }
}

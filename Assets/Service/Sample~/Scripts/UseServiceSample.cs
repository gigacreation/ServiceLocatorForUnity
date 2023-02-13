using UnityEngine;

namespace GigaceeTools.Service.Sample
{
    public class UseServiceSample : MonoBehaviour
    {
        private void Start()
        {
            if (ServiceLocator.TryGet(out ISampleService sampleService))
            {
                sampleService.Bark();
            }
        }
    }
}

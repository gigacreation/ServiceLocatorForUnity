using UnityEngine;

namespace GigaceeTools.Service.Sample
{
    public class SampleService : ISampleService
    {
        public void Bark()
        {
            Debug.Log("Meow!");
        }

        public void Dispose()
        {
            Debug.Log("SampleService disposed.");
        }
    }
}

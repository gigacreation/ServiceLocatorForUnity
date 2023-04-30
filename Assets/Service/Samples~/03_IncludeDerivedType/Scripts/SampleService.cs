using UnityEngine;

namespace GigaCreation.Tools.Service.Sample03
{
    public class SampleService : IDerivedSampleService
    {
        public void Bark()
        {
            Debug.Log("Meow!");
        }

        public void Scratch()
        {
            Debug.Log("Ouch!");
        }
    }
}

using UnityEngine;

namespace GigaCreation.Tools.Service.Sample01
{
    public class SampleService : IService
    {
        public void Bark()
        {
            Debug.Log("Bowwow!");
        }
    }
}

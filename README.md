# Service Locator for Unity

This package provides a service locator for your Unity project.

## 日本語による説明 / Explanation in Japanese

TODO

## Usage

1. Implement `IService` in your class that you want to use via the service locator.

```cs
using UnityEngine;

public class SampleService : IService
{
    public void Bark()
    {
        Debug.Log("Bowwow!");
    }
}
```

2. Register your class in the service locator.

```cs
using System.Collections;
using UnityEngine;

public class RegisterServiceSample : MonoBehaviour
{
    private SampleService _sampleService;

    private void Awake()
    {
        _sampleService = new SampleService();

        ServiceLocator.Register(_sampleService);
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister(_sampleService);
    }
}
```

3. You can use your class anywhere!

```cs
using UnityEngine;

public class UseServiceSample : MonoBehaviour
{
    private void Start()
    {
        var sampleService = ServiceLocator.Get<SampleService>()

        sampleService.Bark();
    }
}
```

### IDisposable

If your class implements `IDisposable` , `Dispose()` will be called on unregister.

```cs
using UnityEngine;

public class SampleService : IService, IDisposable
{
    public void Bark()
    {
        Debug.Log("Bowwow!");
    }

    public void Dispose()
    {
        // ISampleService inherits IDisposable, so this method is called on unregister.
        Debug.Log("SampleService disposed.");
    }
}
```

### Use with an interface

Registration to the service locator via the interface can limit the methods that can be called.

```cs
using System;
using UnityEngine;

public class SampleService : ISampleService
{
    public void Bark()
    {
        Debug.Log("Meow!");
    }

    public void Scratch()
    {
        // This method is public, but you cannot call it via ISampleService.
        Debug.Log("Ouch!");
    }
}

public interface ISampleService : IService
{
    void Bark();
}
```

## Installation

### Package Manager

- `https://github.com/gigacreation/ServiceLocatorForUnity.git?path=Assets/Service`

### Manual

- Copy `Assets/Service/` to your project.

## API References

<https://gigacreation.github.io/ServiceLocatorForUnity/api/GigaCreation.Tools.Service.ServiceLocator.html>

# Service Locator for Unity

This package provides a service locator for your Unity project.

## 日本語による説明 / Explanation in Japanese

[Unity 向けのサービスロケーターを公開しました](https://blog.gigacreation.jp/entry/2023/02/20/205236)

## Usage

1. Implement `IService` in your class that you want to use via the service locator.

```cs
using GigaCreation.Tools.Service;
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
using GigaCreation.Tools.Service;
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
using GigaCreation.Tools.Service;
using UnityEngine;

public class UseServiceSample : MonoBehaviour
{
    private void Start()
    {
        var sampleService = ServiceLocator.Get<SampleService>();

        sampleService.Bark();
    }
}
```

### IDisposable

If your class implements `IDisposable` , `Dispose()` will be called on unregister.

```cs
using System;
using GigaCreation.Tools.Service;
using UnityEngine;

public class SampleService : IService, IDisposable
{
    public void Bark()
    {
        Debug.Log("Bowwow!");
    }

    public void Dispose()
    {
        // SampleService implements IDisposable, so this method is called on unregister.
        Debug.Log("SampleService disposed.");
    }
}
```

### Use with an interface

Registration to the service locator via the interface can limit the methods that can be called.

```cs
using GigaCreation.Tools.Service;
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

### Dispose asynchronously

You can use `DisposeAsync()` by implementing `IAsyncDisposable` .

```cs
using GigaCreation.Tools.Service;
using UnityEngine;

public class SampleService : ISampleService
{
    public void Bark()
    {
        Debug.Log("Meow!");
    }

    public async ValueTask DisposeAsync()
    {
        // First, print a message immediately when this method is called.
        Debug.Log("Wait for 1 second...");

        // We wait for 1 second.
        await Task.Delay(TimeSpan.FromSeconds(1d));

        // ISampleService inherits IAsyncDisposable, so this method is called on unregister asynchronously.
        Debug.Log("SampleService disposed asynchronously.");
    }
}

public interface ISampleService : IService, IAsyncDisposable
{
    void Bark();
}
```

UniTask is also supported.

```cs
using GigaCreation.Tools.Service;
using UnityEngine;

public class SampleService : ISampleService
{
    public void Bark()
    {
        Debug.Log("Meow!");
    }

    public async UniTask DisposeAsync()
    {
        // First, print a message immediately when this method is called.
        Debug.Log("Wait for 1 second...");

        // We wait for 1 second.
        await UniTask.Delay(TimeSpan.FromSeconds(1d));

        // ISampleService inherits IUniTaskAsyncDisposable, so this method is called on unregister asynchronously.
        Debug.Log("SampleService disposed asynchronously with UniTask.");
    }
}

public interface ISampleService : IService, IUniTaskAsyncDisposable
{
    void Bark();
}
```

## API References

<https://gigacreation.github.io/ServiceLocatorForUnity/api/GigaCreation.Tools.Service.ServiceLocator.html>

## Installation

### Package Manager

- `https://github.com/gigacreation/ServiceLocatorForUnity.git?path=Assets/Service`

### Manual

- Copy `Assets/Service/` to your project.

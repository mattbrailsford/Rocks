# Cannot Specify Type With Open Generic Parameters
If the given type has open generic parameters, a mock cannot be created.
```csharp
public interface IMock<T> { ... }

public class Factory<T>
{
  public IMock<T> Mock()
  {
    // This will generate ROCK5
    var expectations = Rock.Create<IMock<T>>;
  }
}
```
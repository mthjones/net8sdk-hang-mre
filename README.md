This is a regression introduced in the .NET 8 SDK when compiling projects targeting net48. What previously used to work fine now leads to an infinite loop at runtime.

## Scenarios

Several cases have been tested, varying on target framework, configuration, and SDK version. These tests cases can be run by passing the case number as an argument to the application.

### Case 1

```cs
IFoo<IBaseParams> foo = new FooBase();
```

This case works in all configurations.

### Case 2

```cs
IFoo<IBaseParams> foo = new Foo<IParams>();
```

This case works in all configurations.

### Case 3

```cs
IFoo<IParams> foo = new Foo<IParams>();
```

|                 | .NET 7.0.404 SDK | .NET 8.0.101 SDK |
|-----------------|------------------|------------------|
| net48(Debug)    | Works            | Works            |
| net48(Release)  | Works            | **Hangs**        |
| net7.0(Debug)   | Works            | Works            |
| net7.0(Release) | Works            | Works            |

### Case 4

```cs
IFoo<IBaseParams> foo = new Foo<IBaseParams>();
```

|                 | .NET 7.0.404 SDK   | .NET 8.0.101 SDK   |
|-----------------|--------------------|--------------------|
| net48(Debug)    | **Stack overflow** | **Stack overflow** |
| net48(Release)  | **Hangs**          | **Hangs**          |
| net7.0(Debug)   | **Stack overflow** | **Stack overflow** |
| net7.0(Release) | **Stack overflow** | **Stack overflow** |

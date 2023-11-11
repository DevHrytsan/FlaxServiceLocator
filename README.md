<img align="left" src="https://github.com/DevHrytsan/FlaxServiceLocator/Icon/fsl_icon.png" width="110px"/>
<h1>Flax Service Locator</h1>

## Description
- Utilizes a Service Locator pattern to allow easy access to various services across the game.
- Better than using Singleton`s
- Easy to use
- It's also possible to use with other game engines with minor modifications.
  
## How to install
### For Flax 1.7+ 
1. Open your project.
2. Navigate to Tools -> Plugins -> Clone Plugin Project.
3. Enter the following URL: https://github.com/DevHrytsan/FlaxServiceLocator.git.
4. It will ask you to reload the editor. Do it.
5. Now u can work with it.
   
### For Flax 1.6 and below
1. DON`T open your project in Editor.
2. Add FlaxServiceLocator folder to the Plugin folder in your existing project
   > [!NOTE]
   > If you do not already have a Plugin folder in your project, create one.
   > Ensure that the FlaxServiceLocator folder is named correctly as "FlaxServiceLocator".
3. Next, add a reference from your game project to the added plugin project. Open <project_name>.flaxproj with a text editor and add a reference to the plugin project.
Like this:
``` csharp
 "References": [
      ....,
        {
            "Name": "$(ProjectPath)/Plugins/FlaxServiceLocator/FlaxServiceLocator.flaxproj"
        }
    ],
```
4. Open your project.
5. Enjoy!
   
## Usage
At the beginning of your C# script, you should include the necessary namespace:
```csharp
using FlaxServiceLocator;
```

### Accessing the Service Locator
To access the Service Locator from anywhere in your code, use the following:
```csharp
ServiceLocatorPlugin.Services
```
But if you want separate service locator instance. You can create any amount service locators. By creating new instance of `ServiceLocator`:
``` csharp
ServiceLocator NewService = new ServiceLocator():
```

### Registration
Services need to be registered with the Service Locator before they can be accessed. 

The `IRegistrableService` interface serves as a marker interface that indicates a service is eligible for registration.
All services class must inherit it.
``` csharp
 public class MyServiceClass : Script
 {
   
 }
```
There is already implemented this interface for `Script` class as `ScriptRegistrable` 
``` csharp
 public class MyServiceScript : ScriptRegistrable
 {
   
 }
```
There are two main ways to register services:

**Manual Registration** <br />
You can manually register a service using the `Register` method. I recommend you doing it in `OnAwake` function.

``` csharp
ServiceLocatorPlugin.Services.Register(myService);
```

**Automatic Registration** <br />
To make a service auto-registerable,use it with the `[AutoRegisteredService]` attribute:

``` csharp
 [AutoRegisteredService]
 public class MyServiceScript : ScriptRegistrable
 {
   
 }
```
And then use `CheckAutoRegisteredServices()` method to check which services are automatically registered 
```
ServiceLocatorPlugin.Services.CheckAutoRegisteredServices();
```

### Registering Services with Safe Check
By default, services are registered with a safe check, which means an exception is thrown if you attempt to register a service type that has already been registered. To disable this behavior, set the `safe` parameter to `false` when registering the service.
``` csharp
ServiceLocatorPlugin.Services.Register(myService, safe: false);
```

### Retrieving Services
Once services are registered, you can retrieve them using the `Get` method. <br />
If a service has not been registered and you attempt to retrieve it without forcing, an exception will be thrown:
``` csharp
var myService = ServiceLocatorPlugin.Services.Get<MyService>();
```
Don`t forget, You can force retrieval even if the service hasn't been registered:
``` csharp
var myService = ServiceLocatorPlugin.Services.Get<MyService>(Retrieve.Find);
```

You can force the retrieval and create the service if it doesn't exist. Additionally, ensure to specify the second parent `Actor` to include in which the instance of the retrieved service will be created if the search sequence fails.

``` csharp
var myService = ServiceLocatorPlugin.Services.Get<MyService>(Retrieve.FindOrCreate, myServiceActorParent);
```
### Checking Service Registration
You can check if a service of a specific type is registered using the `IsRegistered` method.

``` csharp
bool isRegistered = ServiceLocatorPlugin.Services.IsRegistered<MyService>();
```

## Example 
You'll find it in the plugin's root folder under "Content -> Demo" and open the "FlaxServiceLocatorDEMO" scene. It showcases the usage of Flax Service Locator and have example scripts with nicely commented code. To remove the demo content, delete all the "Demo" folders.

## Contribution
Feel free to contribute to this project and suggest some ideas for it. Don`t forget about [Flax Engine discord server](https://discord.com/invite/yFBCmY9)
## License
Under the MIT license. Feel free to use. Be safe :)



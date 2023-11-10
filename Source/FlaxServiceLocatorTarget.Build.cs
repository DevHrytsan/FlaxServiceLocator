using Flax.Build;

public class FlaxServiceLocatorTarget : GameProjectTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for game
        Modules.Add("FlaxServiceLocator");
    }
}

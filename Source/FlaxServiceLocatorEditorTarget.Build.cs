using Flax.Build;

public class FlaxServiceLocatorEditorTarget : GameProjectEditorTarget
{
    /// <inheritdoc />
    public override void Init()
    {
        base.Init();

        // Reference the modules for editor
        Modules.Add("FlaxServiceLocator");
        Modules.Add("FlaxServiceLocatorEditor");
    }
}

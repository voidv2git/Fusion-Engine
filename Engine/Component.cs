namespace FusionEngine.Engine
{
    public abstract class Component
    {
        public Entity parent;

        public abstract void OnLoad();
        public abstract void OnUpdate();
    }
}

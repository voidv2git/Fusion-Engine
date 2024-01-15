using FusionEngine.Engine.Rendering;
using System.Collections.Generic;

namespace FusionEngine.Engine
{
    public class Entity
    {
        public Vector position;
        public Vector scale;
        public float angle;
        public object material;

        public List<Component> components = new List<Component>();

        public Entity(Vector position, Vector scale, float angle, object material)
        {
            this.position = position;
            this.scale = scale;
            this.angle = angle;
            this.material = material;

            Game.RegisterEntity(this);
        }

        public void AddComponent(Component component)
        {
            component.parent = this;
            component.OnLoad();
            
            components.Add(component);
        }

        public void Destroy()
        {
            Game.entityStack.Remove(this);
        }
    }
}

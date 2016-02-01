using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DemoWorld
{
    abstract class Entity
    {
        protected Vector3 position;
        protected Model model;
        protected BoundingBox hitBox;

        public abstract void Draw();

        public abstract void Update(GameTime gt);

    }
}

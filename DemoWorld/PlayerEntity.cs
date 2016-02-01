using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShuzenEngine;

namespace DemoWorld
{
    class PlayerEntity : Entity
    {
        private Matrix world;
        private Matrix view;
        private Matrix projection;
        private Vector3 camera;

        public PlayerEntity(Model model, Vector3 spawnPos)
        {
            this.model = model;
            position = spawnPos;
            camera = new Vector3(position.X, position.Y, position.Z + 185);
            world = Matrix.CreateTranslation(spawnPos);
            
        }

        public override void Draw()
        {
            view = Matrix.CreateLookAt(camera, new Vector3(0, 0, 0), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 200f);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.TextureEnabled = true;
                }
                mesh.Draw();
            }
        }

        public override void Update(GameTime gt)
        {

        }
    }
}

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
        private Vector3 cameraReferencePosition;
        // Set rates in world units per 1/60th second 
        // (the default fixed-step interval).
        float rotationSpeed = 1f / 60f;
        private float cameraRotationSpeedX;
        private float cameraRotationSpeedY;

        public PlayerEntity(Model model, Vector3 spawnPos)
        {
            this.model = model;
            position = spawnPos;
            cameraReferencePosition = new Vector3(position.X + 5, position.Y + 5, position.Z - 5);
            world = Matrix.CreateTranslation(spawnPos);
        }

        public override void Draw()
        {

            // Erstellung einer Matrix mit den Rotations parametern
            Matrix rotationMatrix = Matrix.CreateRotationY(cameraRotationSpeedY);
            //Formel zur Drehung um die x-Achse 
            
            // Create a vector pointing the direction the camera is facing.
            Vector3 transformedReference = Vector3.Transform(cameraReferencePosition, rotationMatrix);

            // Calculate the position the camera is looking at.
            Vector3 cameraPosition = transformedReference + position;

            // Set up the view matrix and projection matrix.
            // Matrix.CreateLookAt(Kamera Position, Kamera Zielobject(Blickrichtung), CameraUpVerctor);
            view = Matrix.CreateLookAt(cameraPosition, position, Vector3.UnitY);

            // Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRation, nearPlaneDistance, Anzeige enfernung des Models)
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 800f / 480f, 0.1f, 20000f);
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
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                // Rotate left.
                cameraRotationSpeedY += rotationSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                // Rotate right
                cameraRotationSpeedY -= rotationSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                // Rotate up
                cameraRotationSpeedX += rotationSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                // Rotate down
                cameraRotationSpeedX -= rotationSpeed;
            }
        }
    }
}

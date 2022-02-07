﻿using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace CSharpIntermediate.Code
{
    public class RaycastDemo : SyncScript
    {
        public CollisionFilterGroupFlags CollideWithGroup;
        public bool CollideWithTriggers = false;
        public Entity HitPoint;

        private float maxDistance = 4.0f;
        private Entity laser;
        private Simulation simulation;
      
        public override void Start()
        {
            simulation = this.GetSimulation();
            laser = Entity.FindChild("Laser");
        }

        public override void Update()
        {
            int drawX = 40;
            int drawY = 80;
            DebugText.Print("Raycast demo", new Int2(drawX, drawY));

            var raycastStart = Entity.Transform.Position;
            var raycastEnd = Entity.Transform.Position + new Vector3(0, 0, maxDistance);
          
            drawY += 40;
            if (simulation.Raycast(raycastStart, raycastEnd, out HitResult hitResult, CollisionFilterGroups.DefaultFilter, CollideWithGroup, CollideWithTriggers))
            {
                HitPoint.Transform.Position = hitResult.Point;
                var distance = Vector3.Distance(hitResult.Point, raycastStart);
                laser.Transform.Scale.Z = distance;

                DebugText.Print("Hit a collider", new Int2(drawX, drawY));
                DebugText.Print($"Raycast hit distance: {distance}", new Int2(drawX, drawY + 20));
                DebugText.Print($"Raycast hit point: {hitResult.Point}", new Int2(drawX, drawY + 40));
                DebugText.Print($"Raycast hit entity: {hitResult.Collider.Entity.Name}", new Int2(drawX, drawY + 60));
            }
            else
            {
                HitPoint.Transform.Position = raycastEnd;
                laser.Transform.Scale.Z = Vector3.Distance(raycastStart, raycastEnd);
                DebugText.Print("No collider hit", new Int2(drawX, drawY));
            }
        }
    }
}

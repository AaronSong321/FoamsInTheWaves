using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Jobs;
using Unity.Collections;
using UnityEngine;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace Jmas
{
    public class WaveGenerator: Actor
    {
        public float waveScale;
        public float waveOffsetSpeed;
        public float waveHeight;
        public MeshFilter waterMeshFilter;
        private Mesh waterMesh;
        private NativeArray<Vector3> waterVertices;
        private NativeArray<Vector3> waterNormals;

        //[NativeContainerSupportsDeallocateOnJobCompletion]
        private struct UpdateMeshJob : IJobParallelFor
        {
            // [DeallocateOnJobCompletion]
            public NativeArray<Vector3> waterVertices;
            // [DeallocateOnJobCompletion]
            public NativeArray<Vector3> waterNormals;

            [ReadOnly] public float offsetSpeed;
            [ReadOnly] public float time;
            [ReadOnly] public float scale;
            [ReadOnly] public float height;
            
            public void Execute(int i)
            {
                if (waterNormals[i].z > 0f)
                {
                    var vertex = waterVertices[i];
                    float n = noise.snoise(math.float2(vertex.x * scale + offsetSpeed * time, vertex.y * scale + offsetSpeed * time));
                    waterVertices[i] = new Vector3(vertex.x , vertex.y, n * height + 0.3f);
                }
            }
        }
        private UpdateMeshJob meshModifyJob;
        private JobHandle meshModifyHandle;

        protected override void Start()
        {
            base.Start();
            waterMesh = waterMeshFilter.mesh;
            waterMesh.MarkDynamic();
            waterVertices = new NativeArray<Vector3>(waterMesh.vertices, Allocator.Persistent);
            waterNormals = new NativeArray<Vector3>(waterMesh.normals, Allocator.Persistent);
        }

        protected override void Update()
        {
            base.Update();
            meshModifyJob = new UpdateMeshJob {
                waterVertices = waterVertices,
                waterNormals = waterNormals,
                offsetSpeed = waveOffsetSpeed,
                time = GameMode.Instance.CurrentTime,
                scale = waveScale,
                height = waveHeight
            };
            meshModifyHandle = meshModifyJob.Schedule(waterVertices.Length, 64);
        }

        private void LateUpdate()
        {
            meshModifyHandle.Complete();
            waterMesh.SetVertices(meshModifyJob.waterVertices);
            waterMesh.RecalculateNormals();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            waterVertices.Dispose();
            waterNormals.Dispose();
        }
    }
}

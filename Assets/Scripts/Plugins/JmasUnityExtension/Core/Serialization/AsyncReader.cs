using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;


namespace Jmas
{
    public class AsyncReader: MonoBehaviour
    {
        private ReadHandle readHandle;
        private NativeArray<ReadCommand> cmd;
        public string relativePathToStreamingData;

        public unsafe void Start()
        {
            string path = Path.Combine(Application.streamingAssetsPath, relativePathToStreamingData);
            cmd = new NativeArray<ReadCommand>(1, Allocator.Persistent);
            var command = new ReadCommand{
                Offset = 0,
                Size = 1024,
                Buffer = (byte*) UnsafeUtility.Malloc(1024, 16, Allocator.Persistent)
            };
            cmd[0] = command;
            readHandle = AsyncReadManager.Read(path, (ReadCommand*) cmd.GetUnsafePtr(), 1);
        }

        public unsafe void Update()
        {
            if (!readHandle.IsValid() || readHandle.Status == ReadStatus.InProgress) 
                return;
            readHandle.Dispose();
            UnsafeUtility.Free(cmd[0].Buffer, Allocator.Persistent);
            cmd.Dispose();
        }
    }
    
    
}

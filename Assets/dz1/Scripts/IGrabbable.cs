using UnityEngine;

namespace Refactoring
{
    public interface IGrabbable
    {
        void OnGrab();

        void OnRelease();
    }
}
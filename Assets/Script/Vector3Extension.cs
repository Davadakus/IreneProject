using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VConvert {
    public static class Vector3Extension {
        public static Vector2 AsVector2(this Vector3 _v) {
            return new Vector2(_v.x, _v.y);
        }
    }

}
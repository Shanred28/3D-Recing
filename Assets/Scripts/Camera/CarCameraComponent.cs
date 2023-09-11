using UnityEngine;

namespace Race
{
    [RequireComponent(typeof(CarCameraController))]
    public abstract class CarCameraComponent : MonoBehaviour
    {
        protected Car _car;
        protected new Camera _camera;

        public virtual void SetProperties(Car car, Camera camera)
        { 
            this._car = car;
            this._camera = camera;
        }
    }
}


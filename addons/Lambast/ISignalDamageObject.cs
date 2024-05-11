using System;
namespace LambastNamespace
{
    public interface ISignalDamageObject
    {
        public event Action DealDamageInstance;
        public event Action<DamageResource[]> InitDamageObject;
        public event Action<bool> DamageTimerStatus;
    }
}

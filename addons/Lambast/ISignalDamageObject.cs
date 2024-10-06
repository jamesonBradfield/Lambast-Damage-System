using System;
namespace LambastNamespace
{
    public interface ISignalDamageObject
    {
        public event Action<DamageResource> DealDamageInstance;
    }
}

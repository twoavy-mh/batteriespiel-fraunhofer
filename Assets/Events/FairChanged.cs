using UnityEngine.Events;

namespace Events
{
    public class FairChanged : UnityEvent<int>
    {
        public interface IUseFair
        {
            public void UseFair(int fairCode);
        }
    }
}
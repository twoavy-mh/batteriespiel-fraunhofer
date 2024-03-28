using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Events
{
    public class ApiErrorEvent : UnityEvent<Exception>
    {
        public interface IUseApiError
        {
            public void UseApiError(Exception e);
        }
    }
}
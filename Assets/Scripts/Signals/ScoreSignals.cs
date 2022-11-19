using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction onUpdateScore = delegate {  };
        public UnityAction<Vector3> onGetHookPos = delegate{  };
    }
}
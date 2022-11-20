using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay = delegate {  };
        public UnityAction onReset = delegate {  };
        public UnityAction onGameFailed = delegate {  };
        public UnityAction<bool> onInteractionWithBorder = delegate{  };
        public UnityAction<bool> onInteractionWithHookEntry = delegate {  };
        public UnityAction<bool> onInteractionWithHookExit = delegate {  };
        public UnityAction onHasImpact = delegate { };
    }
}
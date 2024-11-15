using ReactiveUI;

namespace Darts.Avalonia.ViewRouting
{
    public class RoutingStateWrapper : IScreen
    {
        public RoutingState Router { get; }

        public RoutingStateWrapper(RoutingState router)
        {
            Router = router;
        }
    }
}

using BikeShopManager.AppCore.Interfaces.Dispatcher;

namespace BikeShopManager.AppCore.Interfaces.Behaviors;

public interface IPipelineBehaviorAsync<TCommand, TResult>
{
    Task<TResult> Handle(TCommand request, RequestHandlerDelegate<TResult> next);
}
